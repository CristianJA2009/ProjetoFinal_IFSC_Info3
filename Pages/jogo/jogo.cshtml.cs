using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Pages;

public class JogoModel : PageModel
{
    private readonly LegendsStoreContext _context;

    public JogoModel(LegendsStoreContext context)
    {
        _context = context;
    }
    public async Task OnGetAsync(int Id)
    {
        var jogo = await _context.Jogos.FirstOrDefaultAsync(j => j.Id == Id);
    }
}
