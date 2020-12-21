using Microsoft.EntityFrameworkCore;

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

        public virtual DbSet<ImportacaoModel> Importacaos { get; set; }
        public virtual DbSet<ImportacaoItemModel> ImportacaoItems { get; set; }

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

            modelBuilder.Entity<ImportacaoModel>(entity =>
            {
                entity.HasKey(e => e.IdImportacao)
                    .HasName("PK_Importacao_1");

                entity.ToTable("Importacao");

                entity.Property(e => e.IdImportacao).HasColumnName("id_importacao");

                entity.Property(e => e.DataCadastro)
                    .HasColumnType("datetime")
                    .HasColumnName("data_cadastro");
            });

            modelBuilder.Entity<ImportacaoItemModel>(entity =>
            {
                entity.HasKey(e => e.IdImportacaoItem)
                    .HasName("PK_Importacao");

                entity.ToTable("ImportacaoItem");

                entity.Property(e => e.IdImportacaoItem).HasColumnName("id_importacao_item");

                entity.Property(e => e.DataEntrega)
                    .HasColumnType("date")
                    .HasColumnName("data_entrega");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("descricao");

                entity.Property(e => e.IdImportacao).HasColumnName("id_importacao");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.Property(e => e.ValorUnitario)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("valor_unitario");

                entity.HasOne(d => d.IdImportacaoNavigation)
                    .WithMany(p => p.ImportacaoItems)
                    .HasForeignKey(d => d.IdImportacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImportacaoItem_Importacao");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}