using System.Collections.Generic;
using System.Linq;
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

        public UserPrincipal(Data.Entities.User model)
            : base(new ClaimsIdentity(GetClaims(model), SCHEME))
        {
        }

        #endregion

        #region Methods

        private static IEnumerable<Claim> GetClaims(Data.Entities.User user)
        {
            yield return new Claim(ClaimTypes.Name, user.Name);
            yield return new Claim(ClaimTypes.Email, user.Email);
            yield return new Claim(ClaimTypes.Sid, user.Id.ToString());
        }

        public static string GetClaim(ClaimsPrincipal principal, string type) => principal.Claims.FirstOrDefault(x => x.Type == type)?.Value;

        public static int GetId(ClaimsPrincipal principal) => int.TryParse(GetClaim(principal, ClaimTypes.Sid), out int result) ? result : 0;

        #endregion
    }
}