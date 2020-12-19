using Importacao.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task CreateAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await SaveAsync();
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
