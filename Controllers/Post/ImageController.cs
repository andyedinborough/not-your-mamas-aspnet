using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using web.Services.Post;

namespace web.Controllers.Post
{
    public class ImageController : Controller
    {
        #region Fields

        private readonly ImageService _imageService;

        #endregion

        #region Constructors

        public ImageController(ImageService imageService)
        {
            _imageService = imageService;
        }

        #endregion

        #region Methods

        [HttpGet]
        [Route("post/{postId}/image.jpg")]
        public async Task Image(int postId)
        {
            var stream = await _imageService.GetImageStreamAsync(postId);
            Response.ContentType = "image/jpeg";
            await stream.CopyToAsync(Request.Body);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _imageService.Dispose();
        }

        #endregion
    }
}