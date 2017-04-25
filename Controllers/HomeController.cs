using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using web.Services.Post;
using web.ViewModels;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly PostService _postService;

        #endregion

        #region Constructors

        public HomeController(PostService postService)
        {
            _postService = postService;
        }

        #endregion

        #region Methods

        [Route("error")]
        public IActionResult Error() => View();

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var model = new HomeModel
            {
                Posts = await _postService.GetFeedAsync(100)
            };

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _postService.Dispose();
        }

        #endregion
    }
}