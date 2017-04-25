using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using web.Authentication;
using web.Services.Post;
using web.ViewModels.Post;

namespace web.Controllers.Post
{
    public class PostController : Controller
    {
        #region Fields

        private readonly PostService _postService;

        #endregion

        #region Constructors

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        #endregion

        #region Methods

        [Route("post/create")]
        public async Task<IActionResult> Create(PostCreateModel model)
        {
            if (ModelState.IsValid)
            {
                await _postService.CreateAsync(model, UserPrincipal.GetId(User));
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}