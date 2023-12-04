using WebApplication1.Ef.Models;

namespace WebApplication1.Services.Interfaces;

public interface IUserService
{
    Task<string> Authenticate(string username, string password);

    Task<Guid> FindUser(string username);
}
