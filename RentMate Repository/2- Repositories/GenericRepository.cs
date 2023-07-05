using Microsoft.EntityFrameworkCore;
using RentMate_Domain;
using RentMate_Repository.IRepositories;
using RentMate_Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Repository.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _Context;


        public GenericRepository(AppDbContext context)
        {
            _Context = context;
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = _Context.Set<T>().AsQueryable();
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                    query = query.Include(include);

            }
            return await query.ToListAsync();
        }
        public async Task<T> GetByIdAsync<O>(O id, params Expression<Func<T, object>>[] includes)
        {
            var query = _Context.Set<T>().AsQueryable();

            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var entity = await  query
                .FirstOrDefaultAsync(d => EF.Property<T>(d, "Id").Equals(id));

            return  entity;

        }
        public async Task<T> AddAsync(T entity)
        {
            var result = await _Context.Set<T>().AddAsync(entity);
            ///*var result =*/ await _Context.SaveChangesAsync();

            if (result != null)
                return result.Entity;
            else
                return null;
        }

        public async Task<T> UpdateAsync<O>(O id, T entity, Expression<Func<T, O>> keySelector)
        {
            var foundEntity = await _Context.Set<T>().FindAsync(id);
            if (foundEntity == null) return null;

            _Context.Entry(entity).Property(keySelector).CurrentValue = id;
            _Context.Entry(foundEntity).CurrentValues.SetValues(entity);

            return foundEntity;
        }

        public async Task<T> DeleteAsync<O>(O id)
        {
            var query = await _Context.Set<T>().FirstOrDefaultAsync(d => EF.Property<O>(d, "Id").Equals(id));

            if (query != null)
            {
                _Context.Remove(query);
                await _Context.SaveChangesAsync();
            }
            else 
            {
                return null;
            }

            return query;
        }


    }
}
