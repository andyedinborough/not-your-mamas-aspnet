using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web.Data.Entities;

namespace web.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        Task IDataContext.AddAsync<T>(T item) =>  AddAsync(item);

        IQueryable<User> IDataContext.Users => Users;

        IQueryable<Post> IDataContext.Posts => Posts;

        Task<int> IDataContext.SaveChangesAsync() => SaveChangesAsync();
    }
}