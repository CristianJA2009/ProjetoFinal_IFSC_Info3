using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MeuProjeto.Models
{
    public class LegendsStoreContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioJogo>()
                .HasKey(uj => new { uj.usuarioId, uj.jogoId });

            modelBuilder.Entity<UsuarioJogo>()
                .HasOne(uj => uj.Usuario)
                .WithMany(u => u.UsuarioJogos)
                .HasForeignKey(uj => uj.usuarioId);

            modelBuilder.Entity<UsuarioJogo>()
                .HasOne(uj => uj.Jogo)
                .WithMany(j => j.UsuarioJogos)
                .HasForeignKey(uj => uj.jogoId);

            modelBuilder.Entity<CompraJogo>()
                .HasKey(cj => new { cj.compraId, cj.jogoId });

            modelBuilder.Entity<CompraJogo>()
                .HasOne(cj => cj.Compra)
                .WithMany(c => c.CompraJogos)
                .HasForeignKey(cj => cj.compraId);

            modelBuilder.Entity<CompraJogo>()
                .HasOne(cj => cj.Jogo)
                .WithMany(j => j.CompraJogos)
                .HasForeignKey(cj => cj.jogoId);

            modelBuilder.Entity<CarrinhoJogo>()
                .HasKey(cj => new { cj.carrinhoId, cj.jogoId });

            modelBuilder.Entity<CarrinhoJogo>()
                .HasOne(cj => cj.Carrinho)
                .WithMany(c => c.CarrinhoJogos)
                .HasForeignKey(cj => cj.carrinhoId);

            modelBuilder.Entity<CarrinhoJogo>()
                .HasOne(cj => cj.Jogo)
                .WithMany(j => j.CarrinhoJogos)
                .HasForeignKey(cj => cj.jogoId);


        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioJogo> UsuarioJogos { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<CompraJogo> CompraJogos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CarrinhoJogo> CarrinhoJogos { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }

        public LegendsStoreContext(DbContextOptions options) : base(options)
        {

        }

      
    }
}
