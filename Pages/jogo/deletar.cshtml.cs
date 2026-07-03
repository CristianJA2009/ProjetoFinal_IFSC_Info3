using MeuProjeto.Models;
using MeuProjeto.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Pages.jogo
{
    public class deletarModel : PageModel
    {
        //construtor do context
        private readonly LegendsStoreContext _context;

        public deletarModel(LegendsStoreContext context)
        {
            _context = context;
        }

        //pega o id do url da págin
        public async Task<IActionResult> OnGetAsync(int id)
        {
            //objeto que recebe o jogo
            var jogo = await _context.Jogos.FirstOrDefaultAsync(j => j.Id == id);

            if (jogo == null)
            {
                return NotFound();
            }

            //remove o jogo que possi aquelas informações
            _context.Jogos.Remove(jogo);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
