using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Data
{
    public class ApplicaionContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Network Library=DBMSSOCN;Initial Catalog=myDataBase;User ID=myUsername;Password=myPassword;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(p =>
            {
                p.ToTable("Clientes");
                p.HasKey(p => p.Id);
                p.Property(p => p.Nome).HasColumType('VARCHAR(80)').IsRequired();
                p.Property(p => p.Telefone).HasColumType('CHAR(11)');
                p.Property(p => p.CEP).HasColumType('CHAR(8)').IsRequired();
                p.Property(p => p.Estado).HasColumType('CHAR(2)').IsRequired();
                p.Property(p => p.Cidade).HasMaxLength(60).IsRequired();

                p.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
            });
            
            modelBuilder.Entity<Produto>(p =>
            {
                p.ToTable("Produtos");
                p.HasKey(p => p.Id);
                p.Property(p => p.CodigoBarras).HasColumType('VARCHAR(14)').IsRequired();
                p.Property(p => p.Descricao).HasColumType('VARCHAR(60)');
                p.Property(p => p.Valor).IsRequired();
                p.Property(p => p.TipoProduto).HasConversion<string>();
            });

            modelBuilder.Entity<Pedido>(p =>
            {
                p.ToTable("Pedidos");
                p.HasKey(p => p.Id);
                p.Property(p => p.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGenerateOnAdd();
                p.Property(p => p.StatusPedido).HasConversion<string>();
                p.Property(p => p.TipoFrete).HasConversion<string>();
                p.Property(p => p.Observacao).HasColumType('VARCHAR(512)');

                p.HasMany(p => p.Itens).WithOne(p => p.Pedido).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PedidoItem>(p =>
            {
                p.ToTable("PedidoItens");
                p.HasKey(p => p.Id);
                p.Property(p => p.Quantidade).HasDefaultValueSql(1).IsRequired();
                p.Property(p => p.Valor).IsRequired();
                p.Property(p => p.Desconto).IsRequired();
            });
        }
    }
}