using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Pages.conta.admin
{
    public class deletarUserModel : PageModel
    {
        private readonly LegendsStoreContext _context;
        public deletarUserModel(LegendsStoreContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync(int id)
        {
            //objeto que recebe o jogo
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(c => c.Id == id);

            //remove o jogo que possi aquelas informações
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            RedirectToPage("/conta/admin");
        }
    }
}
