using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using web.Services.User;
using web.ViewModels;

namespace web.Controllers
{
    public class LoginController : Controller
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) 
        {
            var user = await _loginService.TryLoginAsync(model);
            return View(model);
        } 

        protected override void Dispose(bool disposing)
        {
            if(disposing) 
            {
                _loginService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
