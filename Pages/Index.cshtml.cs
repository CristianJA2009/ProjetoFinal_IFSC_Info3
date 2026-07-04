using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Pages;

public class IndexModel : PageModel
{
    private readonly LegendsStoreContext _context;

    public List<Jogo> Jogos { get; set; }
    public List<Categoria> Categorias { get; set; }
    public Jogo JogoEscolhido { get; set; }

    public IndexModel(LegendsStoreContext context)
    {
        _context = context;
    }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? GameCategory { get; set; }

    public bool TemJogos {  get; set; }

    Random random = new Random();

    public async Task OnGetAsync()
    {
        ViewData["Search"] = Search;

        var consulta = _context.Jogos.AsQueryable(); //recebe todos os jogos

        if (!string.IsNullOrWhiteSpace(Search) && GameCategory == null )
        {
            // Filtra no banco de dados antes de dar o ToList()
            consulta = consulta.Where(j => j.nome.ToLower().Contains(Search.ToLower()));
        } else if (!string.IsNullOrWhiteSpace(Search) && GameCategory != null)
        {
            consulta = consulta.Where(j => j.nome.ToLower().Contains(Search.ToLower()) && j.categoriaId == GameCategory);
        } else if (string.IsNullOrWhiteSpace(Search) && GameCategory != null)
        {
            consulta = consulta.Where(j => j.categoriaId == GameCategory);
        }

        Categorias = _context.Categorias.ToList();
        Jogos = consulta.ToList(); //recebe filtrado

        int indiceAleatorio = random.Next(Jogos.Count); //recebe o primeiro jogo filtrado
        var jogoEscolhido = Jogos[indiceAleatorio];
        JogoEscolhido = jogoEscolhido;

        TemJogos = await _context.Jogos.AnyAsync();
    }
}
