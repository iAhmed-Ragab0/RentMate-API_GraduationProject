using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Repository.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        public Task<T> GetByIdAsync<O>(O id, params Expression<Func<T, object>>[] includes);
        public Task<T> AddAsync(T entity);
        public Task<T> UpdateAsync<O>(O id, T entity, Expression<Func<T, O>> keySelector);
        public Task<T> DeleteAsync<O>(O id);


    }
}
