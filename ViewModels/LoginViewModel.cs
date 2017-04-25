using web.Data.Entities;

namespace web.ViewModels
{
    public class LoginViewModel
    {
        #region Properties

        public string Email { get; set; }

        public string Password { get; set; }

        public User User { get; set; }

        #endregion
    }
}