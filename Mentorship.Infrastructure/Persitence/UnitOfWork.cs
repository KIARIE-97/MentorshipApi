using System;
using Mentorship.Api.Data;
using Mentorship.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Mentorship.Infrastructure.Persitence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;
    private IDbContextTransaction? _currentTransaction;

    public async Task<int> SaveChangesAsync (CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task BeginTransactionAsync()
    {
        _currentTransaction ??= await _context.Database.BeginTransactionAsync();
    }
    public async Task CommitTransactionAsync()
    {
        try
        {
            await SaveChangesAsync();
            if(_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync();
            }
        }
        catch 
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }
     public async Task RollbackTransactionAsync()
    {
        try
            {
            if (_currentTransaction != null)
                await _currentTransaction.RollbackAsync();
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }

}
