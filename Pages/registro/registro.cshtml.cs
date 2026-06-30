using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{

    [BindProperty]
    public string RegisterName { get; set; }

    [BindProperty]
    public string RegisterEmail { get; set;  }

    [BindProperty]
    public string RegisterPassword { get; set; }

    [BindProperty]
    public string RegisterVerifyPassword { get; set; }

    public bool EmptyForm()
    {
        return string.IsNullOrWhiteSpace(RegisterName) || string.IsNullOrEmpty(RegisterEmail) || string.IsNullOrEmpty(RegisterPassword) || string.IsNullOrEmpty(RegisterVerifyPassword);
    }

    public IActionResult OnGet()
    {
        if (EmptyForm())
        {
            ViewData["ErrorMessage"] = "Preencha todos os campos";
            return Page();
        }

        var usuario = new Usuario { };

        return RedirectToPage("/login");
    }

    
}
