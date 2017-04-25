using System.Collections.Generic;
using System.Security.Claims;
using web.ViewModels;

namespace web.Authentication
{
    public class UserPrincipal : ClaimsPrincipal
    {
        #region Fields

        public const string SCHEME = "web.Authentication";

        #endregion

        #region Constructors

        public UserPrincipal(LoginViewModel model)
            : base(new ClaimsIdentity(GetClaims(model), SCHEME))
        {
        }

        #endregion

        #region Methods

        private static IEnumerable<Claim> GetClaims(LoginViewModel model)
        {
            yield return new Claim(ClaimTypes.Name, model.User.Name);
            yield return new Claim(ClaimTypes.Email, model.User.Email);
        }

        #endregion
    }
}