using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using web.Data.Entities;
using System;
using System.Data.Common;

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

        public DbSet<Post> Posts { get; set; }

        IQueryable<Post> IDataContext.Posts => Posts;

        public DbSet<User> Users { get; set; }

        IQueryable<User> IDataContext.Users => Users;

        #endregion

        #region Methods

        Task IDataContext.AddAsync<T>(T item) => AddAsync(item);

        Task<int> IDataContext.SaveChangesAsync() => SaveChangesAsync();

        public DbConnection GetConnection() => Database.GetDbConnection();

        #endregion
    }
}