using AutoMapper;
using Importacao.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Importacao.API.ViewModels.Mapper
{
    public class ImportacaoProfile : Profile
    {
        public ImportacaoProfile()
        {
            CreateMap<ImportacaoModel, ImportacaoViewModel>()
                .ForMember(d => d.NumeroItems, o => o.MapFrom(s => s.ImportacaoItems.Count))
                .ForMember(d => d.ValorTotalImportacao, o => o.MapFrom(s => s.ImportacaoItems.Sum(i => i.ValorUnitario * i.Quantidade)))
                .ForMember(d => d.MenorDataEntrega, o => o.MapFrom(s => s.ImportacaoItems.OrderBy(i => i.DataEntrega).FirstOrDefault().DataEntrega));
            CreateMap<ImportacaoItemModel, ImportacaoItemViewModel>()
                .ForMember(d => d.ValorTotal, o => o.MapFrom(s => s.ValorUnitario * s.Quantidade));
        }
    }
}
