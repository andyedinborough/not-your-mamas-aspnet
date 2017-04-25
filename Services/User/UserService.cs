using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web.Data;

namespace web.Services.User
{
    public class UserService : IDisposable
    {
        private readonly IDataContext _ctx;

        public UserService(IDataContext ctx)
        {
            _ctx = ctx;
        }

        public Task<Data.Entities.User> FindAsync(string email) => _ctx.Users.FirstOrDefaultAsync(x => x.Email == email);

        public void Dispose() => _ctx.Dispose();
    }

}