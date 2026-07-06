using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Pages.conta.admin
{
    public class deletarCatModel : PageModel
    {
        private readonly LegendsStoreContext _context;
        public deletarCatModel(LegendsStoreContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync(int id)
        {
            //objeto que recebe o jogo
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            //remove o jogo que possi aquelas informações
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            RedirectToPage("/conta/admin");
        }
    }
}
