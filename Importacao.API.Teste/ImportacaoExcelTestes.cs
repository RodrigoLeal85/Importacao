using Importacao.API.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Importacao.API.Teste
{
    public class ImportacaoExcelTestes
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ImportacaoExcelTestes()
        {
            //Arrange
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            _server = new TestServer(new WebHostBuilder().UseConfiguration(configuration).UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Teste_integracao_getImportacaoPorID_naoNulo()
        {
            //Act
            var response = await _client.GetAsync("/importacao/1");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ImportacaoViewModel>(responseString);

            //Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async Task Teste_integracao_getImportacoes_listaPopulada()
        {
            //Act
            var response = await _client.GetAsync("/importacao");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ImportacaoViewModel>>(responseString);

            //Assert
            Assert.Contains(data, x => x.IdImportacao == 1);
        }
    }
}