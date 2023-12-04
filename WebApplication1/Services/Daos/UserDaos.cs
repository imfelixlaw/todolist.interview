using WebApplication1.Ef.Models;

namespace WebApplication1.Services.Daos
{
    public class UserDaos(todoContext context)
    {
        private readonly todoContext _db = context;

        public async Task<Guid> FindUser(string username)
        {
            if (string.IsNullOrEmpty(username))
                return Guid.Empty;
            var user = _db.Users.Where(x => x.Email == username).FirstOrDefault();
            return user is null ? Guid.Empty : user.Id;
        }

        public async Task<Guid> Authenticate(string username, string password)
        {
            var user = _db.Users.Where(x=> x.Email == username && x.Password == password).FirstOrDefault();

            // on auth fail: empty guid is returned because user is not found
            if (user is null) 
                return Guid.Empty;
            // on auth success: user guid is returned
            return user.Id;
        }
    }
}
