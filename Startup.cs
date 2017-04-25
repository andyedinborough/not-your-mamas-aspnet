using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using web.Authentication;
using web.Data;
using web.Services.Post;
using web.Services.User;

namespace web
{
    public class Startup
    {
        #region Constructors

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        #endregion

        #region Properties

        public IConfigurationRoot Configuration { get; }

        #endregion

        #region Methods

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DataContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = UserPrincipal.SCHEME,
                LoginPath = "/user/login",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            app.UseMvc();

            try
            {
                Task.Run(() => DbInitializer.InitializeAsync(context)).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<DataContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                });

            services
                .AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Validators.User.LoginViewModelValidator>());

            services
                .AddTransient<IDataContext, DataContext>()
                .AddTransient<SignupService>()
                .AddTransient<UserService>()
                .AddTransient<PostService>()
                .AddTransient<ImageService>();
        }

        #endregion
    }
}