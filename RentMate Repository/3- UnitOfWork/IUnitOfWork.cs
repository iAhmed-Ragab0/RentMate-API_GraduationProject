using Microsoft.EntityFrameworkCore.Storage;
using RentMate_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        AppDbContext Context { get; }
        Task BeginTransactionAsync();
        Task CommitTransactionAsync( );
        Task RollbackTransactionAsync( );
        Task Commit();
    }
}
