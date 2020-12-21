using System.Collections.Generic;
using System.Threading.Tasks;

namespace Importacao.API.Repositories.Base
{
    public interface IRepositoryBase<T>
    {
        Task<T> FindAsync(params object[] key);

        Task<IEnumerable<T>> FindAllAsync();

        Task<T> CreateAsync(T entity);

        Task<List<T>> CreateAsync(List<T> list_entity);

        Task SaveAsync();
    }
}