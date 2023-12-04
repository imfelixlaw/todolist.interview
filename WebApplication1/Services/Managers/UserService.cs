using System.Text;
using WebApplication1.Services.Daos;
using WebApplication1.Services.Interfaces;
namespace WebApplication1.Services.Managers;

public class UserService(UserDaos dao) : IUserService
{
    private readonly UserDaos _dao = dao;

    public async Task<string> Authenticate(string username, string password)
    {
        var user = await _dao.Authenticate(username, password);

        if(user != Guid.Empty) 
        {
            var authenticationString = $"{username}:{password}";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));
        }

        return string.Empty;
    }

    public async Task<Guid> FindUser(string username) => await _dao.FindUser(username);
}
