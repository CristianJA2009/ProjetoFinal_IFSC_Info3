using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class RegistroModel : PageModel
{
    private readonly LegendsStoreContext _context;

    public RegistroModel(LegendsStoreContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string RegisterName { get; set; }

    [BindProperty]
    public string RegisterEmail { get; set; }

    [BindProperty]
    public string RegisterPassword { get; set; }

    [BindProperty]
    public string RegisterVerifyPassword { get; set; }

    public bool EmptyForm()
    {
        return string.IsNullOrWhiteSpace(RegisterName) ||
               string.IsNullOrEmpty(RegisterEmail) ||
               string.IsNullOrEmpty(RegisterPassword) ||
               string.IsNullOrEmpty(RegisterVerifyPassword);
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
        if (RegisterPassword != RegisterVerifyPassword)
        {
            ViewData["ErrorMessage"] = "As senhas não coincidem.";
            return Page();
        }

        ViewData["ErrorMessage"] = null;

        try
        {
            // 3. Validação de Email Duplicado (Melhoria)
            var emailExiste = await _context.Usuarios.AnyAsync(u => u.email == RegisterEmail);
            if (emailExiste)
            {
                ViewData["ErrorMessage"] = "Este e-mail já está cadastrado.";
                return Page();
            }

            // Criptografia da senha
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(RegisterPassword);

            var usuario = new Usuario
            {
                nome = RegisterName,
                email = RegisterEmail,
                senha = hashedPassword,
                criado_em = DateTime.Now,
                pontos = 0,
                tipo = "user"
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return RedirectToPage("/conta/login");
        }
        catch (Exception ex)
        {

            ViewData["ErrorMessage"] = "Ocorreu um erro ao registrar o usuário";
            return Page();
        }
    }
}