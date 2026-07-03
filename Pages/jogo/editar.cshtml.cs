using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MeuProjeto.Pages.jogo

{
    public class editarModel : PageModel
    {
        //construtor do context
        private readonly LegendsStoreContext _context;

        public editarModel(LegendsStoreContext context)
        {
            _context = context;
        }

        //propriedades que recebem os inputs
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

        //função que verifica input vazio
        public bool EmptyForm()
        {
            return string.IsNullOrEmpty(GameName) ||
                   string.IsNullOrEmpty(GameDescription) ||
                   string.IsNullOrEmpty(Convert.ToString(GameValue)) ||
                   GameImg == null ||
                   GameBanner == null;
        }

        //objetos de lista de categorias e do jogo
        public List<Categoria> Categorias { get; set; }
        public Jogo Jogo { get; set; }

        //objetos recebem os valores
        public void OnGet(int Id)
        {
            Categorias = _context.Categorias.ToList();
            Jogo = _context.Jogos.FirstOrDefault(j => j.Id == Id);
        }

        public async Task<IActionResult> OnPostAsync(int Id)
        {
            // 1. Validação de campos vazios
            if (EmptyForm())
            {
                ViewData["ErrorMessage"] = "Preencha todos os campos.";
                return Page();
            }

            // 2. Validação de float
            if (float.IsNaN(GameValue))
            {
                ViewData["ErrorMessage"] = "O valor deve ser um número";
                return Page();
            }

            ViewData["ErrorMessage"] = null;

            //uploads de imagens
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
                string ImgName = Path.GetFileName(GameImg.FileName); //Pega o nome do arquivo
                string extensionImg = Path.GetExtension(ImgName); //Pega a extensão do arquivo
                GameImgPath = $"{Guid.NewGuid():N}{extensionImg}"; //Gera um nome único para o arquivo com sua extensão

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

            //tenta atualizar o objeto de jogo
            try
            {
                //objeto de jogo recebe as informações dos inputs
                Jogo = new Jogo
                {
                    Id = Id,
                    nome = GameName,
                    descricao = GameDescription,
                    valor = GameValue,
                    capa = GameImgPath,
                    banner = GameBannerPath,
                    criado_em = DateTime.Today,
                    categoriaId = GameCategory
                };

                //atualiza
                _context.Jogos.Update(Jogo);
                await _context.SaveChangesAsync();

                return RedirectToPage("/Index");
            }
            //senão retorna erro
            catch (Exception ex) 
            {

                ViewData["ErrorMessage"] = $"Ocorreu um erro ao atualizar o usuário: {ex}";
                return Page();
            }
        }
    }
}
