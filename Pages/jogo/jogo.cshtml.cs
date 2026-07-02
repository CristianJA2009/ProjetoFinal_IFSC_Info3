using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Pages;

public class JogoModel : PageModel
{
    private readonly LegendsStoreContext _context;

    //criação dos objetos das classes
    public Jogo Jogo { get; set; }
    public Categoria Categoria { get; set; }

    //construtor do context
    public JogoModel(LegendsStoreContext context)
    {
        _context = context;
    }

    //quando a página carrega os objetos pegam as informações conforme o id
    public async Task OnGet(int Id)
    {
        Jogo = _context.Jogos.FirstOrDefault(j => j.Id == Id);
        Categoria = _context.Categorias.FirstOrDefault(c => c.Id == Jogo.categoriaId);
    }
}
