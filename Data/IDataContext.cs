using System;
using System.Linq;
using System.Threading.Tasks;
using web.Data.Entities;

namespace web.Data 
{
    public interface IDataContext : IDisposable
    {
        IQueryable<User> Users { get; }
        
        IQueryable<Post> Posts { get; }

        Task AddAsync<T>(T item) where T : class;

        Task<int> SaveChangesAsync();
    }
}