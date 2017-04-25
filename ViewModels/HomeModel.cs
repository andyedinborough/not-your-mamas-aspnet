using System.Collections.Generic;
using web.ViewModels.Post;

namespace web.ViewModels
{
    public class HomeModel
    {
        #region Properties

        public ICollection<PostModel> Posts { get; set; }

        #endregion
    }
}