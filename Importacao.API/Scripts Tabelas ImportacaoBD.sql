USE [ImportacaoBD]
GO
/****** Object:  Table [dbo].[Importacao]    Script Date: 21/12/2020 23:56:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Importacao](
	[id_importacao] [int] IDENTITY(1,1) NOT NULL,
	[data_cadastro] [datetime] NOT NULL,
 CONSTRAINT [PK_Importacao_1] PRIMARY KEY CLUSTERED 
(
	[id_importacao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImportacaoItem]    Script Date: 21/12/2020 23:56:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImportacaoItem](
	[id_importacao_item] [int] IDENTITY(1,1) NOT NULL,
	[id_importacao] [int] NOT NULL,
	[descricao] [nvarchar](50) NOT NULL,
	[quantidade] [int] NOT NULL,
	[valor_unitario] [decimal](18, 2) NOT NULL,
	[data_entrega] [date] NOT NULL,
 CONSTRAINT [PK_Importacao] PRIMARY KEY CLUSTERED 
(
	[id_importacao_item] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ImportacaoItem]  WITH CHECK ADD  CONSTRAINT [FK_ImportacaoItem_Importacao] FOREIGN KEY([id_importacao])
REFERENCES [dbo].[Importacao] ([id_importacao])
GO
ALTER TABLE [dbo].[ImportacaoItem] CHECK CONSTRAINT [FK_ImportacaoItem_Importacao]
GO
