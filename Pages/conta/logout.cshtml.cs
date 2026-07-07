using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MeuProjeto.Services;

public class LogoutModel : PageModel
{
    private readonly UsuarioSessao _usuario;

    public LogoutModel(UsuarioSessao usuario)
    {
        _usuario = usuario;
    }

    public IActionResult OnGet()
    {
        _usuario.EncerrarSessao();
        return RedirectToPage("/conta/login");
    }
}