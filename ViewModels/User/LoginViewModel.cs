namespace web.ViewModels.User
{
    public class LoginViewModel
    {
        #region Properties

        public string Email { get; set; }

        public string Password { get; set; }

        public Data.Entities.User User { get; set; }

        #endregion
    }
}