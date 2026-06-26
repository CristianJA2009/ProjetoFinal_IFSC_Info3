using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Models
{
    public class LegendsStoreContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
