using CodingChallenge.Core.Entities;
using CodingChallenge.Core.Infrastructure;
using CodingChallenge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseClass
    {
        protected readonly CodingChallengeDbContext _dbContext;

        public BaseRepository(CodingChallengeDbContext context)
        {
            _dbContext = context;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task<T> UpdateAsync(T entity)
        {

            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }


    }
}
