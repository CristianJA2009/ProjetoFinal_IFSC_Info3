using Microsoft.EntityFrameworkCore;

namespace ProjetoFinal_IFSC_info3.model
{
    public class LojaOnlineContext : DbContext
    {
        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<Usuario_Jogo> Usuarios_Jogos { get; set; }
        public DbSet<Compra_Jogo> Compra_Jogos { get; set; }
        public DbSet<Carrinho_Jogo> Carrinhos_Jogos{ get; set; }
        public LojaOnlineContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}