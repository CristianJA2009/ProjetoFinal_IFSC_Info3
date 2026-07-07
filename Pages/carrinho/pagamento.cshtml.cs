using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Pages.carrinho
{
    public class pagamentoModel : PageModel
    {
        private readonly LegendsStoreContext _context;
        public pagamentoModel(LegendsStoreContext context)
        {
            _context = context;
        }
        public List<Jogo> Jogos { get; set; }
        public Usuario Usuario { get; set; }
        public Compra Compra { get; set; }
        public List<Carrinho> Carrinhos { get; set; }
        public List<CompraJogo> CompraJogos { get; set; }
        public List<UsuarioJogo> UsuarioJogos { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var carrinhos = await _context.Carrinhos.Where(c => c.UsuarioId == id).ToListAsync();

            // Garantir inicialização das listas
            Jogos = Jogos ?? new List<Jogo>();
            CompraJogos = CompraJogos ?? new List<CompraJogo>();
            UsuarioJogos = UsuarioJogos ?? new List<UsuarioJogo>();

            float valorTotal = 0;

            foreach (var carrinho in carrinhos)
            {
                var jogo = await _context.Jogos.FirstOrDefaultAsync(j => j.Id == carrinho.JogoId);
                if (jogo != null)
                {
                    Jogos.Add(jogo);
                    valorTotal += jogo.valor; // <- corrigido
                }
            }

            Compra = new Compra
            {
                valor_total = valorTotal,
                criado_em = DateTime.Now,
                UsuarioId = id
            };

            _context.Compras.Add(Compra);
            await _context.SaveChangesAsync(); // garante que Compra.Id será preenchido

            foreach (var j in Jogos)
            {
                CompraJogos.Add(new CompraJogo
                {
                    jogoId = j.Id,
                    compraId = Compra.Id
                });

                UsuarioJogos.Add(new UsuarioJogo
                {
                    jogoId = j.Id,
                    usuarioId = id
                });
            }

            _context.CompraJogos.AddRange(CompraJogos);
            _context.Carrinhos.RemoveRange(carrinhos);
            _context.UsuarioJogos.AddRange(UsuarioJogos);
            

            await _context.SaveChangesAsync();

            return RedirectToPage($"/Index");
        }
    }
}
