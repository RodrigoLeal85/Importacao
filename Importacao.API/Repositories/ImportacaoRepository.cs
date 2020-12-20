using Importacao.API.Models;
using Importacao.API.Repositories.Base;

namespace Importacao.API.Repositories
{
    public class ImportacaoRepository : RepositoryBase<ImportacaoModel>
    {
        public ImportacaoRepository(ImportacaoBDContext dbContext) : base(dbContext)
        {
        }
    }
}