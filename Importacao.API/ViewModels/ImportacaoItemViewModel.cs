using System;

namespace Importacao.API.ViewModels
{
    public class ImportacaoItemViewModel
    {
        public int IdImportacaoItem { get; set; }

        public int IdImportacao { get; set; }

        public DateTime DataEntrega { get; set; }

        public string Descricao { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorUnitario { get; set; }

        public decimal ValorTotal { get; set; }
    }
}