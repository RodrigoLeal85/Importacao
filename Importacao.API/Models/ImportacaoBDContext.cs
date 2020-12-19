using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Importacao.API.Models
{
    public partial class ImportacaoBDContext : DbContext
    {
        public ImportacaoBDContext()
        {
        }

        public ImportacaoBDContext(DbContextOptions<ImportacaoBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Importacao> Importacaos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Program.Configuracao["ConnectionStrings:ImportacaoBD"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Importacao>(entity =>
            {
                entity.HasKey(e => e.IdImportacao);

                entity.ToTable("Importacao");

                entity.Property(e => e.IdImportacao).HasColumnName("id_importacao");

                entity.Property(e => e.DataCadastro)
                    .HasColumnType("datetime")
                    .HasColumnName("data_cadastro");

                entity.Property(e => e.DataEntrega)
                    .HasColumnType("date")
                    .HasColumnName("data_entrega");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("descricao");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.Property(e => e.ValorUnitario)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("valor_unitario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
