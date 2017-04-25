using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;
using web.Services.User;
using web.ViewModels;

namespace web.Validators
{
    public class SignupViewModelValidator : AbstractValidator<SignupViewModel>, IDisposable
    {
        #region Fields

        private readonly UserService _userService;

        #endregion

        #region Constructors

        public SignupViewModelValidator(UserService userService)
        {
            _userService = userService;

            RuleFor(model => model.Email).EmailAddress().MustAsync(BeUniqueAsync);
            RuleFor(model => model.Password).Length(6, 100);
            RuleFor(model => model.Name).Length(3, 100);
            RuleFor(model => model.Username).Length(3, 50);
        }

        #endregion

        #region Methods

        public async Task<bool> BeUniqueAsync(SignupViewModel model, string email, CancellationToken cancel)
        {
            var existing = await _userService.FindAsync(email);
            return existing == null;
        }

        public void Dispose() => _userService.Dispose();

        #endregion
    }
}