using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Importacao.API.Repositories.Base
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> FindAsync(params object[] key);
        Task<IEnumerable<T>> FindAllAsync();
        Task CreateAsync(T entity);
        Task SaveAsync();
    }
}
