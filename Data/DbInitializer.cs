using System.Linq;
using System.Threading.Tasks;
using web.Services.User;

namespace web.Data
{
    public static class DbInitializer
    {
        #region Methods

        public static async Task InitializeAsync(DataContext context)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            if (context.Users.Any()) return;

            var signupService = new SignupService(context);
            await signupService.SignupAsync(new ViewModels.SignupViewModel
            {
                Name = "Andy Edinborough",
                Username = "andyedinborough",
                Email = "andy@edinborough.org",
                Password = "totessecret"
            });
        }

        #endregion
    }
}