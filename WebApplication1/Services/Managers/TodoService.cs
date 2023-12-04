using WebApplication1.Models;
using WebApplication1.Models.Dtos;
using WebApplication1.Services.Daos;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services.Managers;

public class TodoService(TodoDaos dao) : ITodoService
{
    private readonly TodoDaos _dao = dao;

    public async Task<Guid> AddTodoAsync(TodoChildDto todo, ApplicationUser currentUser)
    {
        var newtodo = _dao.CreateNewTodo(todo, currentUser);
        if (newtodo is not null)
            return newtodo.Result.Id;
        return Guid.Empty;
    }

    public async Task<bool> DeleteTodoAsync(Guid id, ApplicationUser currentUser)
    {
        if (Exists(id) != true)
            return default;
        return await _dao.Remove(id, currentUser);
    }

    public bool Exists(Guid id) => _dao.Find(id);

    public async Task<TodoChildResponseDto>? GetTodoAsync(Guid id) => !Exists(id) ? default : _dao.Mapping(_dao.FindSingle(id));

    public async Task<IList<TodoChildResponseDto>> GetTodoListAsync(QueryParameter? filter)
    {
        List<TodoChildResponseDto> list = [];
        filter ??= new QueryParameter();
        try
        {
            _dao.FindAllByCondition(filter).ForEach(e =>
            {
                list.Add(_dao.Mapping(e));
            });

            return _dao.Sorting(list, filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return list;
    }

    public async Task<bool> UpdateTodoAsync(Guid id, TodoChildDto todo, ApplicationUser currentUser)
    {
        try
        {
            if (!Exists(id))
                return false;

            var record = _dao.FindSingle(id);

            _dao.Assign(ref record!, todo);

            _dao.Save(record);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
}
