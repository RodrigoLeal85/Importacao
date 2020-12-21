using Importacao.API.Models;
using Importacao.API.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Importacao.API.Repositories
{
    public class ImportacaoModelRepository : RepositoryBase<ImportacaoModel>
    {
        private readonly ImportacaoBDContext _dbContext;
        public ImportacaoModelRepository(ImportacaoBDContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ImportacaoModel>> FindAllAsyncIncludeImportacaoItems()
        {
            return await _dbContext.Set<ImportacaoModel>().Include(x => x.ImportacaoItems).ToListAsync();
        }

        public async Task<ImportacaoModel> FindAsyncIncludeImportacaoItems(int id)
        {
            return await _dbContext.Set<ImportacaoModel>().Include(x => x.ImportacaoItems).SingleOrDefaultAsync(x => x.IdImportacao == id);
        }

    }
}