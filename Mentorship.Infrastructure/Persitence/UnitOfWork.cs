using System;
using Mentorship.Api.Data;
using Mentorship.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Mentorship.Infrastructure.Persitence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;
    private IDbContextTransaction? _currentTransaction;

   public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _currentTransaction ??= await _context.Database.BeginTransactionAsync(cancellationToken);
    }
    
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);  // Now this works without passing token
            if (_currentTransaction != null)
                await _currentTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }
    
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_currentTransaction != null)
                await _currentTransaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }

}
