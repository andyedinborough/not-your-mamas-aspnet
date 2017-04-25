using System;
using System.Threading.Tasks;
using web.Data;
using web.ViewModels;

namespace web.Services.User
{
    public class SignupService : IDisposable
    {
        #region Fields

        private readonly IDataContext _ctx;

        #endregion

        #region Constructors

        public SignupService(IDataContext ctx)
        {
            _ctx = ctx;
        }

        #endregion

        #region Methods

        public void Dispose() => _ctx.Dispose();

        public string HashPassword(string clearTextPassword) => BCrypt.Net.BCrypt.HashPassword(clearTextPassword ?? string.Empty);

        public async Task<Data.Entities.User> SignupAsync(SignupViewModel model)
        {
            var user = new Data.Entities.User
            {
                Name = model.Name,
                Email = model.Email,
                Password = HashPassword(model.Password),
                Username = model.Username
            };
            await _ctx.AddAsync(user);
            await _ctx.SaveChangesAsync();
            return user;
        }

        #endregion
    }
}