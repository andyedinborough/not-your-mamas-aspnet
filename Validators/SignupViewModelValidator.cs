using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using web.Data;
using web.Services.User;
using web.ViewModels;

namespace web.Validators
{
    public class SignupViewModelValidator : AbstractValidator<SignupViewModel>, IDisposable
    {
        private readonly UserService _userService;

        public SignupViewModelValidator(UserService userService)
        {
            _userService = userService;

            RuleFor(model => model.Email).EmailAddress().MustAsync(BeUniqueAsync);
            RuleFor(model => model.Password).Length(6, 100);
        }

        public async Task<bool> BeUniqueAsync(SignupViewModel model, string email, CancellationToken cancel)
        {
            var existing = await _userService.FindAsync(email);
            return existing == null;
        }

        public void Dispose() => _userService.Dispose();
    }
}