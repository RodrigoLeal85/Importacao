#Importação Excel

Projeto composto por um projeto API asp.net core .Net 5.0, um projeto Front-end Angular 11 e base de dados SQL Server 2016.

#projeto API
- Para a importação do arquivo Excel, utilizei uma API chamada ExcelDataReader.DataSet.
- Utilizei o Entity Framework Core para conexão com a base de dados.
- Utilizei o Auto Mapper para mapeamento das viewmodel com o modelo.

#projeto Angular
- Utilizei Angular Material

#Observações
- Os arquivos de backup da base de dados e os scripts das tabelas estão dentro do projeto Importacao.API.
- A string de conexão com a base de dados está no arquivo appsettings.json do projeto Importacao.API.
- No projeto Angular, o endereço de acesso a API está no arquivo environment.ts
