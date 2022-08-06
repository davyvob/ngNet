using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Infrastructure
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> ListAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> UpdateAsync(T entity);
        Task<T> AddAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}
