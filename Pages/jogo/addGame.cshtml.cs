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
        public IFormFile GameImg { get; set; }

        [BindProperty]
        public IFormFile GameBanner { get; set; }

        [BindProperty]
        public int GameCategory { get; set; }

        public bool EmptyForm()
        {
            return string.IsNullOrEmpty(GameName) ||
                   string.IsNullOrEmpty(GameDescription) ||
                   string.IsNullOrEmpty(Convert.ToString(GameValue)) ||
                   GameImg == null ||
                   GameBanner == null;
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

            string ImgName = Path.GetFileName(GameImg.FileName);
            string extensionImg = Path.GetExtension(ImgName); //Pega a extensão do arquivo
            string GameImgPath = $"{Guid.NewGuid():N}{extensionImg}"; //Gera um nome único para o arquivo
            string pasta = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot",
                           "uploads");

            string caminho = Path.Combine(pasta, GameImgPath);

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                await GameImg.CopyToAsync(stream);
            }

            string BannerName = Path.GetFileName(GameBanner.FileName);
            string extensionBanner = Path.GetExtension(BannerName);
            string GameBannerPath = $"{Guid.NewGuid():N}{extensionBanner}";

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                await GameBanner.CopyToAsync(stream);
            }

            try
            {

                var jogo = new Jogo
                {
                    nome = GameName,
                    descricao = GameDescription,
                    valor = GameValue,
                    capa = GameImgPath,
                    banner = GameBannerPath,
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
