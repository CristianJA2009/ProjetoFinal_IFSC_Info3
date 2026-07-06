using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MeuProjeto.Services;

namespace MeuProjeto.Pages.conta
{
    public class PerfilModel : PageModel
    {
        private readonly LegendsStoreContext _context;
        private readonly UsuarioSessao _usuario;

        public PerfilModel(LegendsStoreContext context , UsuarioSessao usuario)
        {
            _context = context;
            _usuario = usuario;
        }

        [BindProperty]
        public IFormFile UserImg { get; set; }

        // Adicionando BindProperty aqui para capturar os dados do usuário enviados pelo form (como o ID)
        [BindProperty]
        public Usuario Usuario { get; set; }

        public List<Jogo> Jogos { get; set; }
        public UsuarioJogo UsuarioJogo { get; set; }
        public bool temJogos { get; set; }

        

        public async Task<IActionResult> OnGetAsync(int id) // Dica: mudei para int para evitar conversões repetidas
        {
            if (_usuario.Id != id.ToString())
            {
                return RedirectToPage("/Index");
            }

            Usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            if (Usuario == null)
            {
                return NotFound();
            }

            Jogos = await _context.UsuarioJogos
                .Where(uj => uj.usuarioId == id)
                .Select(uj => uj.Jogo)
                .ToListAsync();

            temJogos = Jogos.Any(); // É mais seguro ver se a lista do usuário tem jogos, ou se o sistema inteiro tem jogos? Deixei dinâmico.

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id) // Mantido o id minúsculo por convenção C#
        {
            if (UserImg == null || UserImg.Length == 0)
            {
                return await RecarregarDadosPagina(id);
            }

            var usuarioNoBanco = await _context.Usuarios.FindAsync(id);

            if (usuarioNoBanco == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            string extensionImg = Path.GetExtension(UserImg.FileName);
            string userImgPath = $"{Guid.NewGuid():N}{extensionImg}";

            string pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            string caminhoImg = Path.Combine(pasta, userImgPath);

            using (var stream = new FileStream(caminhoImg, FileMode.Create))
            {
                await UserImg.CopyToAsync(stream);
            }

            if (!string.IsNullOrEmpty(usuarioNoBanco.foto))
            {
                string caminhoFotoAntiga = Path.Combine(pasta, usuarioNoBanco.foto);
                if (System.IO.File.Exists(caminhoFotoAntiga))
                {
                    System.IO.File.Delete(caminhoFotoAntiga);
                }
            }

            usuarioNoBanco.foto = userImgPath;

            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = usuarioNoBanco.Id });
        }

        private async Task<IActionResult> RecarregarDadosPagina(int id)
        {
            Usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (Usuario == null) return NotFound();

            Jogos = await _context.UsuarioJogos
                .Where(uj => uj.usuarioId == id)
                .Select(uj => uj.Jogo)
                .ToListAsync();
            temJogos = Jogos.Any();

            return Page();
        }
    }
}