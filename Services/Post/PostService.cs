using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.Data;
using web.ViewModels.Post;

namespace web.Services.Post
{
    public class PostService : IDisposable
    {
        #region Fields

        private readonly IDataContext _ctx;

        #endregion

        #region Constructors

        public PostService(IDataContext ctx)
        {
            _ctx = ctx;
        }

        #endregion

        #region Methods

        public async Task CreateAsync(PostCreateModel post, int enteredById)
        {
            var newPost = await _ctx.AddAsync(new Data.Entities.Post
            {
                Caption = post.Caption,
                EnteredById = enteredById,
            });

            await _ctx.SaveChangesAsync();

            var imageService = new ImageService(_ctx);
            using (var stream = post.File.OpenReadStream())
            {
                await imageService.UpdateImageAsync(newPost.Id, stream);
            }
        }

        public void Dispose() => _ctx.Dispose();

        public async Task<ICollection<PostModel>> GetFeedAsync(int count)
        {
            var posts = await _ctx.Posts
                .Include(x => x.EnteredBy)
                .OrderByDescending(x => x.Id)
                .Take(count)
                .ToListAsync();

            return posts.Select(x => new PostModel(x)).ToList();
        }

        #endregion
    }
}