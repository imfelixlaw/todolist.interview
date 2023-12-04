using WebApplication1.Models;
using WebApplication1.Models.Dtos;

namespace WebApplication1.Services.Interfaces;

public interface ITodoService
{
    Task<Guid> AddTodoAsync(TodoChildDto todo, ApplicationUser currentUser);

    Task<bool> UpdateTodoAsync(Guid id, TodoChildDto todo, ApplicationUser currentUser);

    bool Exists(Guid id);

    Task<IList<TodoChildResponseDto>> GetTodoListAsync(QueryParameter? filter);

    Task<TodoChildResponseDto> GetTodoAsync(Guid id);

    Task<bool> DeleteTodoAsync(Guid id, ApplicationUser currentUser);
}
