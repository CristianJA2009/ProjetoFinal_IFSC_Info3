using MeuProjeto.Models;
using MeuProjeto.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Pages.jogo
{
    public class deletarModel : PageModel
    {

        private readonly LegendsStoreContext _context;

        public deletarModel(LegendsStoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var jogo = await _context.Jogos.FirstOrDefaultAsync(j => j.Id == id);

            if (jogo == null)
            {
                return NotFound();
            }

            _context.Jogos.Remove(jogo);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
