using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MeuProjeto.Pages.jogo

{
    public class editarModel : PageModel
    {
        private readonly LegendsStoreContext _context;

        public editarModel(LegendsStoreContext context)
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
        public Jogo Jogo { get; set; }

        public void OnGet(int Id)
        {
            Categorias = _context.Categorias.ToList();
            Jogo = _context.Jogos.FirstOrDefault(j => j.Id == Id);
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

            string GameImgPath;
            string GameBannerPath;

            string pasta = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot",
                           "uploads");

            if (GameImg == null) 
            {
                GameImgPath = Jogo.capa;
            } else
            {
                string ImgName = Path.GetFileName(GameImg.FileName);
                string extensionImg = Path.GetExtension(ImgName); //Pega a extensão do arquivo
                GameImgPath = $"{Guid.NewGuid():N}{extensionImg}"; //Gera um nome único para o arquivo

                string caminhoImg = Path.Combine(pasta, GameImgPath);

                using (var stream = new FileStream(caminhoImg, FileMode.Create))
                {
                    await GameImg.CopyToAsync(stream);
                }
            }

            if (GameBanner == null) 
            { 
                GameBannerPath = Jogo.banner;
            } else
            {
                string BannerName = Path.GetFileName(GameBanner.FileName);
                string extensionBanner = Path.GetExtension(BannerName);
                GameBannerPath = $"{Guid.NewGuid():N}{extensionBanner}";

                string caminhoBanner = Path.Combine(pasta, GameBannerPath);

                using (var stream = new FileStream(caminhoBanner, FileMode.Create))
                {
                    await GameBanner.CopyToAsync(stream);
                }
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
