using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MeuProjeto.Pages.jogo
{
    public class addGameModel : PageModel
    {
        private readonly LegendsStoreContext _context;

        public addGameModel(LegendsStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string GameName { get; set; }
        
        [BindProperty]
        public string GameDescription { get; set; }

        [BindProperty]
        public float GameValue { get; set; }

        [BindProperty]
        public string GameImg { get; set; }

        [BindProperty]
        public string GameBanner { get; set; }

        [BindProperty]
        public int GameCategory { get; set; }

        public bool EmptyForm()
        {
            return string.IsNullOrEmpty(GameName) ||
                   string.IsNullOrEmpty(GameDescription) ||
                   string.IsNullOrEmpty(Convert.ToString(GameValue)) ||
                   string.IsNullOrEmpty(GameImg) ||
                   string.IsNullOrEmpty(GameBanner);
        }

        public List<Categoria> Categorias { get; set; }

        public void OnGet()
        {
            Categorias = _context.Categorias.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // 1. Validação de campos vazios
            if (EmptyForm())
            {
                ViewData["ErrorMessage"] = "Preencha todos os campos.";
                return Page();
            }

            // 2. Validação de senhas iguais
            if (float.IsNaN(GameValue))
            {
                ViewData["ErrorMessage"] = "O valor deve ser um número";
                return Page();
            }

            ViewData["ErrorMessage"] = null;

            try
            {

                var jogo = new Jogo
                {
                    nome = GameName,
                    descricao = GameDescription,
                    valor = GameValue,
                    capa = GameImg,
                    banner = GameImg,
                    criado_em = DateTime.Today,
                    categoriaId = GameCategory
                };

                _context.Jogos.Add(jogo);
                await _context.SaveChangesAsync();

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {

                ViewData["ErrorMessage"] = "Ocorreu um erro ao registrar o usuário";
                return Page();
            }
        }
    }
}
