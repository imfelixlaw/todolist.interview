using WebApplication1.Ef.Models;
using WebApplication1.Models;
using WebApplication1.Models.Dtos;
using WebApplication1.Models.Enums;
using WebApplication1.Services.Managers;

namespace WebApplication1.Services.Daos;

public class TodoDaos(todoContext context, UserService usvc)
{
    private readonly todoContext _db = context;
    private readonly TodoDaos _dao;
    private readonly UserService _usvc = usvc;

    public bool Find(Guid id) => _db.Todos.Where(x => x.Id == id).FirstOrDefault() is not null;

    public Todo? FindSingle(Guid id) => _db.Todos.Where(x => x.Id == id).FirstOrDefault();

    public List<Todo> FindAll() => _db.Todos.ToList();

    public List<Todo> FindAllByCondition(QueryParameter filter)
    {
        var record = FindAll();

        if (record is null || record.Count is 0)
            return record;

        if (filter.Statuses != null && filter.Statuses.Length > 0)
        {
            record = record.Where
            (
                x =>
                (
                   filter.Statuses.ToList().Contains((EStatus)x.Status)
                )
            ).ToList();
        }

        if (filter.DateTimes is not null && filter.DateTimes.Length > 0)
        {
            record = record.Where
            (
                x =>
                (
                    x.Duedate == null || filter.DateTimes.ToList().Contains(x.Duedate.Value)
                )
            ).ToList();
        }

        return record;
    }

    public List<TodoChildResponseDto> Sorting(List<TodoChildResponseDto> list, QueryParameter filter)
    {
        if (filter.IsSortAsc == null || filter.IsSortAsc == true)
        {
            list = filter.SortBy switch
            {
                ESortBy.Status => [.. list.OrderBy(x => x.Status)],
                ESortBy.DueDate => [.. list.OrderBy(x => x.DuetoDateTime)],
                _ => list.OrderBy(x => x.Name)
                         .ToList(),
            };
        }
        else
        {
            list = filter.SortBy switch
            {
                ESortBy.Status => [.. list.OrderByDescending(x => x.Status)],
                ESortBy.DueDate => [.. list.OrderByDescending(x => x.DuetoDateTime)],
                _ => list.OrderByDescending(x => x.Name)
                         .ToList(),
            };
        }
        return list;
    }

    public TodoChildResponseDto Mapping(Todo record)
    {
        try
        {
            TodoChildResponseDto result = new()
            {
                Id = record.Id,
                Name = record.Name,
                Status = (EStatus)record.Status,
                Description = record.Description,
                DuetoDateTime = record.Duedate,
                UserId = record.Owner.ToString(),
                Priority = (EPriority) (record.Priority ?? (byte)EPriority.Normal)
            };
            return result;
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<bool> Remove(Guid id, ApplicationUser currentUser)
    {
        try
        {
            if(await _usvc.FindUser(currentUser.Email) == Guid.Empty)
            {
                throw new InvalidOperationException("No Access");
            }

            var todo = FindSingle(id);
            
            if (todo is null) 
            { 
                return false; 
            }

            _db.Remove(todo);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<Todo> CreateNewTodo(TodoChildDto todo, ApplicationUser currentUser) 
    {
        try
        {
            Guid user = await _usvc.FindUser(currentUser.Email);
            if (user == Guid.Empty)
            {
                throw new InvalidOperationException("No Access");
            }

            Todo newTodo = new()
            {
                Id = Guid.NewGuid(),
                Name = todo.Name,
                Description = todo.Description,
                Duedate = todo.DuetoDateTime,
                Status = (byte)EStatus.NotStarted,
                Owner = await _usvc.FindUser(currentUser.Email)
            };
            _db.Add(newTodo);
            _db.SaveChanges();
            return newTodo;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public void Assign(ref Todo record, TodoChildDto todo)
    {
        record.Name = todo.Name;
        record.Description = todo.Description;
        record.Duedate = todo.DuetoDateTime;
        record.Status = (byte)todo.Status;
    }

    public async Task<bool> Save<T>(T entity) 
        where T : Todo
    {
        try
        {
            _db.Update(entity);
            _db.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
