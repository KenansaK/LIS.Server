using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kernal.Contracts
{
    public interface IRepository<T>
    {
        DbContext Context { get; }
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<(IEnumerable<T>, int)> GetPaginatedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task<List<T>> ToListAsync(IQueryable<T> query, CancellationToken cancellationToken = default);
        Task<T?> FirstOrDefaultAsync(IQueryable<T> query, CancellationToken cancellationToken = default);

        IQueryable<T> Query();
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, string[]? includes = null);

        Task<T?> GetByNameAsync(string name);
        Task<bool> ExistsAsync(int id);

        Task<T?> GetByPropertyAsync(Expression<Func<T, bool>> predicate);

        Task<T> SoftDelete(T entity, CancellationToken cancellationToken = default);
    }
}
