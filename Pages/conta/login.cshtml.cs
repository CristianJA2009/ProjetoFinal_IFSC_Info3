using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class LoginModel : PageModel
{
    private readonly LegendsStoreContext _context;

    public LoginModel(LegendsStoreContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string LoginEmail { get; set; }
    
    [BindProperty]
    public string LoginPassword { get; set; }

    public bool EmptyForm()
    {
        return string.IsNullOrWhiteSpace(LoginEmail) ||
               string.IsNullOrEmpty(LoginPassword);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (EmptyForm()) {
            ViewData["ErrorMessage"] = "Nenhum campo pode estar vazio";
            return Page();
        }

        var usuarioExiste = await _context.Usuarios.FirstOrDefaultAsync(u => u.email == LoginEmail);

        if (usuarioExiste != null) {
            bool passVerify = BCrypt.Net.BCrypt.Verify(LoginPassword, usuarioExiste.senha);

            if (passVerify)
            {
                HttpContext.Session.SetString("UsuarioId", usuarioExiste.Id.ToString());
                HttpContext.Session.SetString("UsuarioName", usuarioExiste.nome.ToString());
                HttpContext.Session.SetString("UsuarioTipo", usuarioExiste.tipo.ToString());

                return RedirectToPage("/Index");
            }
            else
            {
                ViewData["ErrorMessage"] = "Email ou senha incorreto";
                return Page();
            }
        }        

        else
        {
            ViewData["ErrorMessage"] = "Email ou senha incorreto";
            return Page();
        }
    }

}