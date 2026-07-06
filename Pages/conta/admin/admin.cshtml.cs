using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Pages.conta.admin
{
    public class adminModel : PageModel
    {
        private readonly LegendsStoreContext _context;
        public adminModel(LegendsStoreContext context)
        {
            _context = context;
        }

        public List<Usuario> Usuarios { get; set; }
        public List<Jogo> Jogos { get; set; }
        public List<Categoria> Categorias { get; set; }
        public bool TemUsuario { get; set; }
        public bool TemJogo { get; set; }
        public bool TemCategoria { get; set; }

        public async Task OnGetAsync(string IdString)
        {
            int Id = Convert.ToInt32(IdString);

            TemUsuario = await _context.Usuarios.AnyAsync();
            TemJogo = await _context.Jogos.AnyAsync();
            TemCategoria = await _context.Categorias.AnyAsync();

            Usuarios = _context.Usuarios.ToList();
            Jogos = _context.Jogos.ToList();
            Categorias = _context.Categorias.ToList();


        }
    }
}
