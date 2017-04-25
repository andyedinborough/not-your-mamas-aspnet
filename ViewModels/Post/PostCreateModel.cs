using Microsoft.AspNetCore.Http;

namespace web.ViewModels.Post
{
    public class PostCreateModel
    {
        #region Properties

        public string Caption { get; set; }

        public IFormFile File { get; set; }

        #endregion
    }
}