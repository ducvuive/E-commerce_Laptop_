using BackendAPI.Application.Abstractions;
using BackendAPI.Persistence.Data;

namespace BackendAPI.Persistence.Repositories;

public sealed class EfUnitOfWork : IUnitOfWork
{
    private readonly UserDbContext context;

    public EfUnitOfWork(UserDbContext context)
    {
        this.context = context;
    }

    public async Task ExecuteInTransactionAsync(
        Func<CancellationToken, Task> operation,
        CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await operation(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
