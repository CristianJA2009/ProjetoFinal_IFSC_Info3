using MeuProjeto.Models;
using MeuProjeto.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Pages.conta
{
    public class editarModel : PageModel
    {
        //construtor do context
        private readonly LegendsStoreContext _context;
        private readonly UsuarioSessao _usuario;

        public editarModel(LegendsStoreContext context, UsuarioSessao usuario)
        {
            _context = context;
            _usuario = usuario;
        }

        //propriedades que recebem os inputs
        [BindProperty]
        public string RegisterName { get; set; }

        [BindProperty]
        public string RegisterEmail { get; set; }

        [BindProperty]
        public string Senha { get; set; }

        //função que verifica input vazio
        public bool EmptyForm()
        {
            return string.IsNullOrEmpty(RegisterName) ||
                   string.IsNullOrEmpty(RegisterEmail) ||
                   string.IsNullOrEmpty(Senha);
        }

        public Usuario Usuario { get; set; }

        //objetos recebem os valores
        public async Task OnGetAsync(int Id)
        {
            Usuario = _context.Usuarios.FirstOrDefault(u => u.Id == Id);
        }

        public async Task<IActionResult> OnPostAsync(int Id)
        {
            Usuario = _context.Usuarios.AsNoTracking().FirstOrDefault(j => j.Id == Id);

            // 1. Validação de campos vazios

            if (EmptyForm())
            {
                ViewData["ErrorMessage"] = "Preencha todos os campos.";
                return Page();
            }

            ViewData["ErrorMessage"] = null;

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Senha);

            //tenta atualizar o objeto de jogo
            try
            {

                //objeto de jogo recebe as informações dos inputs
                Usuario = new Usuario
                {
                    Id = Id,
                    nome = RegisterName,
                    email = RegisterEmail,
                    senha = hashedPassword,
                    criado_em = Usuario.criado_em,
                    pontos = Usuario.pontos,
                    tipo = Usuario.tipo
                };

                //atualiza
                _context.Usuarios.Update(Usuario);
                await _context.SaveChangesAsync();

                return RedirectToPage($"/conta/perfil");
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
