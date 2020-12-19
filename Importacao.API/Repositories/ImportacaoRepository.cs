using Importacao.API.Models;
using Importacao.API.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Importacao.API.Repositories
{
    public class ImportacaoRepository : RepositoryBase<ImportacaoRepository>
    {
        public ImportacaoRepository(ImportacaoBDContext dbContext) : base(dbContext)
        {
        }
    }
}
