using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;
using web.Services.User;
using web.ViewModels.User;

namespace web.Validators.User
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>, IDisposable
    {
        #region Fields

        private readonly UserService _userService;

        #endregion

        #region Constructors

        public LoginViewModelValidator(UserService userService)
        {
            _userService = userService;

            RuleFor(model => model.Email).EmailAddress();
            RuleFor(model => model.Password).Length(6, 100).MustAsync(MatchAsync);
        }

        #endregion

        #region Methods

        public void Dispose() => _userService.Dispose();

        public bool IsPasswordValid(string userSubmittedPassword, string hashedPassword) => BCrypt.Net.BCrypt.Verify(userSubmittedPassword ?? string.Empty, hashedPassword ?? string.Empty);

        public async Task<bool> MatchAsync(LoginViewModel model, string userSubmittedPassword, CancellationToken cancel)
        {
            var user = await _userService.FindAsync(model.Email);
            var valid = IsPasswordValid(userSubmittedPassword, user?.Password);
            if(valid)
            {
                model.User = user;
            }
            return valid;
        }

        #endregion
    }
}