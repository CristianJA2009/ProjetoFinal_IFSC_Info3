using Microsoft.EntityFrameworkCore;

namespace ProjetoFinal_IFSC_info3.model
{
    public class LojaOnlineContext : DbContext
    {
        public DbSet<Usuario> Usuarios {get; set;}

    }
}