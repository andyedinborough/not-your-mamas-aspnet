using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using web.Data;

namespace web.Services.User
{
    public class UserService : IDisposable
    {
        #region Fields

        private readonly IDataContext _ctx;

        #endregion

        #region Constructors

        public UserService(IDataContext ctx)
        {
            _ctx = ctx;
        }

        #endregion

        #region Methods

        public void Dispose() => _ctx.Dispose();

        public Task<Data.Entities.User> FindAsync(string email) => _ctx.Users.FirstOrDefaultAsync(x => x.Email == email);

        #endregion
    }
}