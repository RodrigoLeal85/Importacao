using Importacao.API.Models;
using Importacao.API.Repositories.Base;

namespace Importacao.API.Repositories
{
    public class ImportacaoItemModelRepository : RepositoryBase<ImportacaoItemModel>
    {
        public ImportacaoItemModelRepository(ImportacaoBDContext dbContext) : base(dbContext)
        {
        }
    }
}