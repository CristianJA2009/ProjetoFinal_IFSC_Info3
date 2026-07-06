using MeuProjeto.Models;
using MeuProjeto.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Pages.carrinho
{
    public class carrinhoModel : PageModel
    {
        private readonly LegendsStoreContext _context;
        private readonly UsuarioSessao _usuarioSessao;
        public List<Carrinho> CarrinhoItens { get; set; }
        public List<Jogo> Jogos { get; set; }
        public bool TemJogos { get; set; }
        public carrinhoModel(LegendsStoreContext context, UsuarioSessao usuarioSessao)
        {
            _context = context;
            _usuarioSessao = usuarioSessao;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            // 1. Validação de Segurança Primeiro: O usuário está logado?
            if (_usuarioSessao.Id == null)
            {
                return RedirectToPage("/conta/login");
            }

            TemJogos = _context.Jogos.Any();

            if (TemJogos)
            {
                int usuarioId = Convert.ToInt32(_usuarioSessao.Id);
                // 2. Busca os itens do carrinho do usuário de forma assíncrona
                CarrinhoItens = await _context.Carrinhos
                    .Where(c => c.UsuarioId == usuarioId)
                    .ToListAsync();
                // 3. Busca os jogos correspondentes aos itens do carrinho
                var jogoIds = CarrinhoItens.Select(c => c.JogoId).ToList();
                Jogos = await _context.Jogos
                    .Where(j => jogoIds.Contains(j.Id))
                    .ToListAsync();
            }

            return Page();
        }
    }
}