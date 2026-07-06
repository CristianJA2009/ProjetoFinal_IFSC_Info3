using MeuProjeto.Models;
using MeuProjeto.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Pages.carrinho
{
    public class addCarrinhoModel : PageModel
    {
        private readonly LegendsStoreContext _context;
        private readonly UsuarioSessao _usuarioSessao;

        // Utilizando a sintaxe de construtor padrão para garantir a injeção correta
        public addCarrinhoModel(LegendsStoreContext context, UsuarioSessao usuarioSessao)
        {
            _context = context;
            _usuarioSessao = usuarioSessao;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // 1. Validação de Segurança Primeiro: O usuário está logado?
            if (_usuarioSessao.Id == null)
            {
                return RedirectToPage("/conta/login");
            }

            int usuarioId = Convert.ToInt32(_usuarioSessao.Id);

            // 2. Busca o jogo de forma assíncrona
            var jogo = await _context.Jogos.FirstOrDefaultAsync(j => j.Id == id);
            if (jogo == null)
            {
                TempData["AlertMessage"] = "Você já possui este item";
                return RedirectToPage("/Index");
            }

            // 3. Regra de Negócio: Verifica se este item/jogo já está no carrinho do usuário
            var itemExistenteCar = await _context.Carrinhos
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId && c.JogoId == id);
            var itemExistenteCont = await _context.UsuarioJogos.AnyAsync(c => c.usuarioId == usuarioId && c.jogoId == id);

            if (itemExistenteCar == null && !itemExistenteCont)
            {
                // Se não existir, adiciona o item ao carrinho
                var novoItemCarrinho = new Carrinho
                {
                    UsuarioId = usuarioId,
                    JogoId = id
                    // Se sua entidade tiver "Quantidade", você definiria aqui: Quantidade = 1
                };


                _context.Carrinhos.Add(novoItemCarrinho);
                await _context.SaveChangesAsync();
                return RedirectToPage("/carrinho/carrinho");
            } else
            {
                ViewData["Error Message"] = "Você ja possui este item";
                return RedirectToPage();
            }            
        }
    }
}