using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.ViewModels;

namespace web.Services.User
{
    public class SignupService : IDisposable
    {
        private readonly IDataContext _ctx;

        public SignupService(IDataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<(Data.Entities.User user, string error)> Signup(SignupViewModel model) 
        {
            var user = new Data.Entities.User 
            {
                Email = model.Email,
                Password = HashPassword(model.Password),
            };
            await _ctx.AddAsync(user);
            await _ctx.SaveChangesAsync();
            return (user, null);
        }

        public string HashPassword(string clearTextPassword) => BCrypt.Net.BCrypt.HashPassword(clearTextPassword ?? string.Empty);

        public void Dispose() => _ctx.Dispose();
    }
}