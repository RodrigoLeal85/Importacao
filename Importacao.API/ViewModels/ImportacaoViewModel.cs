using System;
using System.Collections.Generic;

namespace Importacao.API.ViewModels
{
    public class ImportacaoViewModel
    {
        public ImportacaoViewModel()
        {
            ImportacaoItems = new HashSet<ImportacaoItemViewModel>();
        }

        public int IdImportacao { get; set; }

        public DateTime DataCadastro { get; set; }

        public int NumeroItems { get; set; }

        public DateTime MenorDataEntrega { get; set; }

        public decimal ValorTotalImportacao { get; set; }

        public virtual ICollection<ImportacaoItemViewModel> ImportacaoItems { get; set; }
    }
}