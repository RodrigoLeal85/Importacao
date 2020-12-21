using Importacao.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Importacao.API.Repositories.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ImportacaoBDContext _dbContext;

        public RepositoryBase(ImportacaoBDContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> CreateAsync(List<T> list_entity)
        {
            for (int i = 0; i < list_entity.Count; i++)
            {
                _dbContext.Set<T>().Add(list_entity[i]);
            }
            await SaveAsync();
            return list_entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await SaveAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> FindAsync(params object[] key)
        {
            return await _dbContext.Set<T>().FindAsync(key);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}