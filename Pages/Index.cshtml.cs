using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Pages;

public class IndexModel : PageModel
{
    private readonly LegendsStoreContext _context;

    public List<Jogo> Jogos { get; set; }

    public IndexModel(LegendsStoreContext context)
    {
        _context = context;
    }
    public bool TemJogos {  get; set; }
    public void OnGet()
    {
        Jogos = _context.Jogos.ToList();
        TemJogos = _context.Jogos.Any();
    }
}
