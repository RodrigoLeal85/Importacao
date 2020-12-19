using System;
using System.Collections.Generic;

#nullable disable

namespace Importacao.API.Models
{
    public partial class Importacao
    {
        public int IdImportacao { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public DateTime DataEntrega { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
