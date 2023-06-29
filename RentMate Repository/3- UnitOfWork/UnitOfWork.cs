using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RentMate_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppDbContext Context { get; }
        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(AppDbContext context)
        {
            Context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _currentTransaction = await Context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await Context.SaveChangesAsync();
            await _currentTransaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _currentTransaction.RollbackAsync();
        }

        public async Task Commit()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
