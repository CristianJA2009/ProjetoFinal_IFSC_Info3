using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Pages.carrinho
{
    public class removerModel : PageModel
    {
        private readonly LegendsStoreContext _context;
        public removerModel(LegendsStoreContext context)
        {
            _context = context;
        }

        public Jogo Jogo { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            //objeto que recebe o jogo
            var carrinho = await _context.Carrinhos.FirstOrDefaultAsync(c => c.JogoId == id);

            if (carrinho == null)
            {
                return NotFound();
            }

            //remove o jogo que possi aquelas informações
            _context.Carrinhos.Remove(carrinho);
            await _context.SaveChangesAsync();

            return RedirectToPage("/carrinho/carrinho");
        }
    }
}
