using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using web.Data.Entities;

namespace web.Data
{
    public class DataContext : DbContext, IDataContext
    {
        #region Constructors

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        #endregion

        #region Properties

        public DbSet<PostPicture> PostPictures { get; set; }

        public DbSet<Post> Posts { get; set; }

        IQueryable<Post> IDataContext.Posts => Posts;

        public DbSet<User> Users { get; set; }

        IQueryable<User> IDataContext.Users => Users;

        #endregion

        #region Methods

        async Task<T> IDataContext.AddAsync<T>(T item) => (await AddAsync(item)).Entity;

        public DbConnection GetConnection() => Database.GetDbConnection();

        Task<int> IDataContext.SaveChangesAsync() => SaveChangesAsync();

        #endregion
    }
}