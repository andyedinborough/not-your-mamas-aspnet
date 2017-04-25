using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using web.Data;
using web.Services.User;
using web.ViewModels;

namespace web.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>, IDisposable
    {
        private readonly UserService _userService;

        public LoginViewModelValidator(UserService userService)
        {
            _userService = userService;

            RuleFor(model => model.Email).EmailAddress();
            RuleFor(model => model.Password).Length(6, 100).MustAsync(MatchAsync);
        }

        public async Task<bool> MatchAsync(LoginViewModel model, string userSubmittedPassword, CancellationToken cancel)
        {
            var user = await _userService.FindAsync(model.Email);
            return IsPasswordValid(userSubmittedPassword, user.Password);
        }

        public bool IsPasswordValid(string userSubmittedPassword, string hashedPassword) => BCrypt.Net.BCrypt.Verify(userSubmittedPassword ?? string.Empty, hashedPassword ?? string.Empty);
        
        public void Dispose() => _userService.Dispose();
    }
}