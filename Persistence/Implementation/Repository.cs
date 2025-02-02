using Kernal.Contracts;
using Kernal.Enums;
using Kernal.Models;
using Kernel.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Persistence.Implementation
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet;
        private readonly IHttpContext _httpContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbContext Context { get; private set; }
        public Repository(DbContext context, IHttpContext httpContext, IHttpContextAccessor httpContextAccessor)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
            _httpContext = httpContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task AddAsync(T entity)
        {
            // var userName = _httpContext.IntranetUser?.UserName ?? "System";
            var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            entity.CreatedBy = email;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.StatusId = (short)EntityStatus.Active;

            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            entity.UpdatedBy = email;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            _dbSet.Update(entity);
        }
        public async Task DeleteAsync(T entity) => _dbSet.Remove(entity);

        public async Task<List<T>> ToListAsync(IQueryable<T> query, CancellationToken cancellationToken = default) =>
            await query.ToListAsync(cancellationToken);

        public async Task<T?> FirstOrDefaultAsync(IQueryable<T> query, CancellationToken cancellationToken = default) =>
            await query.FirstOrDefaultAsync(cancellationToken);

        public IQueryable<T> Query() => _dbSet.AsQueryable();

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, string[]? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<T?> GetByNameAsync(string name) =>
            await _dbSet.FirstOrDefaultAsync(e => EF.Property<string>(e, "Name") == name);

        public async Task<(IEnumerable<T>, int)> GetPaginatedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            if (pageIndex <= 0) pageIndex = 1;
            if (pageSize <= 0) pageSize = 10;

            IQueryable<T> query = _dbSet;

            if (filter != null) query = query.Where(filter);

            var totalRecords = await query.CountAsync();

            if (orderBy != null) query = orderBy(query);

            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalRecords);
        }

        public async Task<bool> ExistsAsync(int id) =>
            await _dbSet.AnyAsync(e => EF.Property<int>(e, "Id") == id);

        public async Task<T?> GetByPropertyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
        public async Task<T> SoftDelete(T entity, CancellationToken cancellationToken = default)
        {

            entity.StatusId = (short)EntityStatus.Deleted;
            _dbSet.Entry(entity).State = EntityState.Deleted;
            _dbSet.Update(entity);
            await Task.CompletedTask;
            return entity;
        }
    }
}
