using AutoMapper;
using ExcelDataReader;
using Importacao.API.Models;
using Importacao.API.Repositories;
using Importacao.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Importacao.API.Controllers
{
    [Route("importacao")]
    [ApiController]
    public class ImportarExcelController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ImportacaoModelRepository _repositorio;

        public ImportarExcelController(ImportacaoModelRepository repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetImportacoes()
        {
            var listaImportacoes = await _repositorio.FindAllAsyncIncludeImportacaoItems();
            var listaImportacoesMapeada = _mapper.Map<IEnumerable<ImportacaoViewModel>>(listaImportacoes);
            return Ok(listaImportacoesMapeada);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetImportacaoPorID(int id)
        {
            var objImportacao = await _repositorio.FindAsyncIncludeImportacaoItems(id);
            if (objImportacao != null)
            {
                var objImportacaoMapeado = _mapper.Map<ImportacaoViewModel>(objImportacao);
                return Ok(objImportacaoMapeado);
            }
            else
                return NotFound();
        }

        [HttpPost("importarExcel")]
        public async Task<ActionResult> InsertImportacaoExcel()
        {
            var arquivoImportacao = Request.Form.Files[0];
            using (var stream = new MemoryStream())
            {
                arquivoImportacao.CopyTo(stream);
                stream.Position = 0;
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    var registros = result.Tables[0];
                    ImportacaoModel objImportacaoModel = new ImportacaoModel();
                    objImportacaoModel.DataCadastro = DateTime.Now;
                    List<ImportacaoItemModel> listaObjImportacaoItemModel = new List<ImportacaoItemModel>();
                    List<string> listaErros = new List<string>();
                    for (int i = 0; i < registros.Rows.Count; i++)
                    {
                        ImportacaoItemModel objImportacaoItemModel = new ImportacaoItemModel();
                        string dataEntrega = registros.Rows[i][0].ToString();
                        if (!string.IsNullOrEmpty(dataEntrega))
                            objImportacaoItemModel.DataEntrega = DateTime.Parse(dataEntrega).Date;
                        objImportacaoItemModel.Descricao = registros.Rows[i][1].ToString();
                        string quantidade = registros.Rows[i][2].ToString();
                        if (!string.IsNullOrEmpty(quantidade))
                            objImportacaoItemModel.Quantidade = int.Parse(quantidade);
                        string valorUnitario = registros.Rows[i][3].ToString();
                        if (!string.IsNullOrEmpty(valorUnitario))
                            objImportacaoItemModel.ValorUnitario = decimal.Round(decimal.Parse(valorUnitario), 2);

                        if (TryValidateModel(objImportacaoItemModel))
                        {
                            listaObjImportacaoItemModel.Add(objImportacaoItemModel);
                        }
                        else
                        {
                            var erros = ModelState.SelectMany(x => x.Value.Errors).Select(e => e.ErrorMessage).ToList();
                            for (int j = 0; j < erros.Count(); j++)
                            {
                                string erro = $"\n Erro na linha {i + 2}. Mensagem: {erros[j]}";//Soma 2 por causa do cabeçalho.
                                listaErros.Add(erro);
                            }
                        }
                        ModelState.Clear();
                    }
                    if (listaErros.Count > 0)
                    {
                        return BadRequest(listaErros);
                    }
                    else
                    {
                        objImportacaoModel.ImportacaoItems = listaObjImportacaoItemModel;
                        return Ok(await _repositorio.CreateAsync(objImportacaoModel));
                    }
                }
            }
        }
    }
}