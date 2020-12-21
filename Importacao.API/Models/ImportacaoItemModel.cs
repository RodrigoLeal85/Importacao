using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Importacao.API.Models
{
    public partial class ImportacaoItemModel
    {
        public int IdImportacaoItem { get; set; }

        public int IdImportacao { get; set; }

        [Required(ErrorMessage = "A data de entrega não pode ser nula.")]
        [CustomDateAttribute(ErrorMessage = "A data não pode ser menor ou igual a data atual.")]
        public DateTime? DataEntrega { get; set; }

        [Required(ErrorMessage = "A descrição não pode ser nula.")]
        [MaxLength(50, ErrorMessage = "A descrição deve conter no máximo 50 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A quantidade não pode ser nula.")]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "A quantidade deve ser maior que 0.")]
        public int? Quantidade { get; set; }

        [Required(ErrorMessage = "O valor unitário não pode ser nulo.")]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "O valor unitário deve ser maior que 0.")]
        public decimal? ValorUnitario { get; set; }

        public class CustomDateAttribute : RangeAttribute
        {
            public CustomDateAttribute()
              : base(typeof(DateTime),
                      DateTime.Today.AddDays(1).ToShortDateString(),
                      DateTime.MaxValue.ToShortDateString())
            { }
        }

        public virtual ImportacaoModel IdImportacaoNavigation { get; set; }
    }
}