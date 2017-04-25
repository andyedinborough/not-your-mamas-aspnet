using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using web.Authentication;
using web.ViewModels;

namespace web.Controllers
{
    public class LoginController : Controller
    {
        #region Methods

        [HttpGet]
        [Route("user/login")]
        public IActionResult Login() => View();

        [HttpPost]
        [Route("user/login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                await HttpContext.Authentication.SignInAsync(UserPrincipal.SCHEME, new UserPrincipal(model));
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [Route("user/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync(UserPrincipal.SCHEME);
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}