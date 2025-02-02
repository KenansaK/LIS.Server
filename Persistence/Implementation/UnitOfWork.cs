using Kernal.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence.Implementation;

public class UnitOfWork(DbContext dbContext) : IUnitOfWork, IDisposable
{
    private IDbContextTransaction? _transaction;
    public void BeginTransaction() => _transaction = dbContext.Database.BeginTransaction();
    public async Task SaveChanges(CancellationToken cancellationToken) => await dbContext.SaveChangesAsync(cancellationToken);
    public async Task<int> Commit(CancellationToken cancellationToken)
    {
        try
        {
            var result = await dbContext.SaveChangesAsync(cancellationToken);
            await _transaction!.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();

            return result;
        }
        catch (DbUpdateException)
        {
            await _transaction!.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task Rollback(CancellationToken cancellationToken) => await _transaction!.RollbackAsync(cancellationToken);

    public void Dispose() => dbContext.Dispose();
}