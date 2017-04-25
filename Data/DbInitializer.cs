using Microsoft.AspNetCore.Http.Internal;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using web.Authentication;
using web.Services.Post;
using web.Services.User;
using web.ViewModels.Post;
using Microsoft.Extensions.Logging;

namespace web.Data
{
    public static class DbInitializer
    {
        #region Methods

        public static async Task InitializeAsync(DataContext context, ILogger logger)
        {
            logger.LogInformation("Deleting database");
            await context.Database.EnsureDeletedAsync();

            logger.LogInformation("Creating database");
            await context.Database.EnsureCreatedAsync();

            if (context.Users.Any()) return;

            var signupService = new SignupService(context);
            logger.LogInformation("Creating user");
            var user = await signupService.SignupAsync(new ViewModels.User.SignupViewModel
            {
                Name = "Andy Edinborough",
                Username = "andyedinborough",
                Email = "andy@edinborough.org",
                Password = "totessecret"
            });

            var me = new UserPrincipal(user);

            var postService = new PostService(context);
            for (var i = 1; i <= 5; i++)
            {
                logger.LogInformation($"Downloading image #{i}");
                var image = await GetImageAsync(i);
                var post = new PostCreateModel
                {
                    Caption = "Random image #" + i,
                    File = new FormFile(image.stream, 0, image.length, "picture", $"image-{i}.jpg")
                };

                logger.LogInformation($"Creating post #{i}");
                await postService.CreateAsync(post, user.Id);
            }
        }

        public static async Task<(Stream stream, long length)> GetImageAsync(int n)
        {
            var request = System.Net.WebRequest.CreateHttp("https://unsplash.it/400/500/?random"); // $"http://lorempixel.com/500/500/cats/{n}/");
            using (var response = await request.GetResponseAsync())
            using (var stream = response.GetResponseStream())
            {
                var mem = new MemoryStream();
                await stream.CopyToAsync(mem);
                mem.Position = 0;

                return (mem, mem.Length); // (new StreamWrapper(stream, response), response.ContentLength);
            }
        }

        #endregion
    }
}