using System.Collections.Generic;

namespace web.ViewModels
{
    public class HomeModel
    {
        #region Properties

        public ICollection<PostModel> Posts { get; set; }

        #endregion
    }
}