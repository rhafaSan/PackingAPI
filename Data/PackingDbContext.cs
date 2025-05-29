using Microsoft.EntityFrameworkCore;
using PackingServiceApi.Models;

public class EmpacotamentoDbContext : DbContext
{
    public EmpacotamentoDbContext(DbContextOptions<EmpacotamentoDbContext> options)
        : base(options) { }

    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ResultadoEmpacotamento> ResultadosEmpacotamento { get; set; }
    public DbSet<CaixaEmbalagem> CaixasEmbalagem { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pedido>()
    .HasKey(p => p.Id);

        modelBuilder.Entity<Pedido>()
            .HasIndex(p => p.Pedido_Id)
            .IsUnique();

        modelBuilder.Entity<Pedido>()
            .HasMany(p => p.Produtos)
            .WithOne(p => p.Pedido)
            .HasForeignKey(p => p.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Produto>()
            .OwnsOne(p => p.Dimensao);

        modelBuilder.Entity<ResultadoEmpacotamento>()
            .HasOne(r => r.Pedido)
            .WithMany(p => p.ResultadosEmpacotamento)
            .HasForeignKey(r => r.PedidoId)
            .HasPrincipalKey(p => p.Pedido_Id);

        modelBuilder.Entity<ResultadoEmpacotamento>()
            .HasMany(r => r.Caixas)
            .WithOne(c => c.ResultadoEmpacotamento)
            .HasForeignKey(c => c.ResultadoEmpacotamentoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CaixaEmbalagem>()
            .HasMany(c => c.Produtos)
            .WithOne(p => p.CaixaEmbalagem)
            .HasForeignKey(p => p.CaixaEmbalagemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
