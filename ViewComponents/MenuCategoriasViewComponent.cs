using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeuProjeto.ViewComponents
{
    public class MenuCategoriasViewComponent : ViewComponent
    {
        private readonly LegendsStoreContext _context;

        public MenuCategoriasViewComponent(LegendsStoreContext context)
        {
            _context = context;
        }

        // Esse método roda sozinho quando o layout chama o componente
        public IViewComponentResult Invoke()
        {
            var categorias = _context.Categorias.ToList(); //Ja puxa automatico a lista do db
            return View(categorias); // Passa a lista para o HTML do componente
        }
    }
}
