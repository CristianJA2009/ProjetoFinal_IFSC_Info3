using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Pages.conta
{
    public class PerfilModel : PageModel
    {
        private readonly LegendsStoreContext _context;

        public PerfilModel(LegendsStoreContext context)
        {
            _context = context;
        }

        public Usuario Usuario { get; set; }
        public List<Jogo> Jogos { get; set; }
        public UsuarioJogo UsuarioJogo { get; set; }
        public bool temJogos {  get; set; }
        public async Task OnGetAsync(string id)
        {
            Usuario = _context.Usuarios.FirstOrDefault(u => Convert.ToString(u.Id) == id);
            Jogos = await _context.UsuarioJogos.Where(uj => uj.usuarioId == Convert.ToInt32(id)).Select(uj => uj.Jogo).ToListAsync();
            temJogos = await _context.Jogos.AnyAsync();
        }
    }
}
