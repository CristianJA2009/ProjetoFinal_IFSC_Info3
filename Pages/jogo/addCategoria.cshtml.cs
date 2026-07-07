using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeuProjeto.Pages.jogo
{
    public class addCategoriaModel : PageModel
    {
        private readonly LegendsStoreContext _context;

        public addCategoriaModel(LegendsStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string CatName { get; set; }

        public Categoria Categoria { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var categoria = new Categoria
                {
                    nome = CatName
                };

                _context.Categorias.Add(categoria);
                _context.SaveChanges();

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Ocorreu um erro ao criar a categoria: " + ex.Message;
                return Page();
            }
        }
    }
}
