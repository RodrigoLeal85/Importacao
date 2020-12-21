using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Importacao.API.Models
{
    public partial class ImportacaoModel
    {
        public ImportacaoModel()
        {
            ImportacaoItems = new HashSet<ImportacaoItemModel>();
        }

        public int IdImportacao { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; }

        public virtual ICollection<ImportacaoItemModel> ImportacaoItems { get; set; }
    }
}