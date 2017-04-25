using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.Data;
using web.ViewModels;

namespace web.Services.Post
{
    public class PostService : IDisposable
    {
        private readonly IDataContext _ctx;

        public PostService(IDataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ICollection<PostModel>> GetFeedAsync(int count)
        {
            var posts = await _ctx.Posts
                .Include(x => x.EnteredBy)
                .OrderByDescending(x => x.Id)
                .Take(count)
                .ToListAsync();

            return posts.Select(x => new PostModel(x)).ToList();
        }

        public void Dispose() => _ctx.Dispose();
    }
}
