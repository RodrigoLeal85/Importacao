using ExcelDataReader;
using Importacao.API.Models;
using Importacao.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Importacao.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImportarExcelController : ControllerBase
    {
        private ImportacaoRepository _repositorio;

        public ImportarExcelController(ImportacaoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult> GetImportacoes()
        {
            var listaImportacoes = await _repositorio.FindAllAsync();
            return Ok(listaImportacoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetImportacaoPorID(int idImportacao)
        {
            var objImportacao = await _repositorio.FindAsync(idImportacao);
            if (objImportacao != null)
                return Ok(objImportacao);
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> InsertImportacao(IFormFile arquivoImportacao)
        {
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
                    List<ImportacaoModel> listaObjImportacao = new List<ImportacaoModel>();
                    List<string> listaErros = new List<string>();
                    for (int i = 0; i < registros.Rows.Count; i++)
                    {
                        ImportacaoModel objImportacao = new ImportacaoModel();
                        string dataEntrega = registros.Rows[i][0].ToString();
                        if (!string.IsNullOrEmpty(dataEntrega))
                            objImportacao.DataEntrega = DateTime.Parse(dataEntrega).Date;
                        objImportacao.Descricao = registros.Rows[i][1].ToString();
                        string quantidade = registros.Rows[i][2].ToString();
                        if (!string.IsNullOrEmpty(quantidade))
                            objImportacao.Quantidade = int.Parse(quantidade);
                        string valorUnitario = registros.Rows[i][3].ToString();
                        if (!string.IsNullOrEmpty(valorUnitario))
                            objImportacao.ValorUnitario = decimal.Round(decimal.Parse(valorUnitario), 2);
                        objImportacao.DataCadastro = DateTime.Now;
                        if (TryValidateModel(objImportacao))
                        {
                            listaObjImportacao.Add(objImportacao);
                        }
                        else
                        {
                            var erros = ModelState.SelectMany(x => x.Value.Errors).Select(e => e.ErrorMessage).ToList();
                            for (int j = 0; j < erros.Count(); j++)
                            {
                                string erro = $"Erro na linha {i + 2}. Mensagem: {erros[j]}";//Soma 2 por causa do cabeçalho.
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
                        return Ok(await _repositorio.CreateAsync(listaObjImportacao));
                    }
                }
            }
        }
    }
}