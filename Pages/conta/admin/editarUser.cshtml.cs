using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MeuProjeto.Services;

namespace MeuProjeto.Pages.conta.admin
{

    public class editarUserModel : PageModel
    {
        //construtor do context
        private readonly LegendsStoreContext _context;
        private readonly UsuarioSessao _usuario;

        public editarUserModel(LegendsStoreContext context, UsuarioSessao usuario)
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
        public int Pontos { get; set; }

        [BindProperty]
        public string Tipo { get; set; }

        //função que verifica input vazio
        public bool EmptyFormUser()
        {
            return string.IsNullOrEmpty(RegisterName) ||
                   string.IsNullOrEmpty(RegisterEmail) ||
                   string.IsNullOrEmpty(Convert.ToString(Pontos)) ||
                   string.IsNullOrEmpty(Tipo);
        }

        public bool EmptyFormAdmin()
        {
            return string.IsNullOrEmpty(RegisterName) ||
                   string.IsNullOrEmpty(RegisterEmail) ||
                   string.IsNullOrEmpty(Convert.ToString(Pontos));
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
            if (Usuario.Id.ToString() != _usuario.Id)
            {
                if (EmptyFormUser())
                {
                    ViewData["ErrorMessage"] = "Preencha todos os campos.";
                    return Page();
                }
            }
            else
            {
                if (EmptyFormAdmin())
                {
                    ViewData["ErrorMessage"] = "Preencha todos os campos.";
                    return Page();
                }
            }


            // 2. Validação de float
            if (!int.TryParse(Pontos.ToString(), out _))
            {
                ViewData["ErrorMessage"] = "O valor deve ser um número";
                return Page();
            }

            ViewData["ErrorMessage"] = null;

            //tenta atualizar o objeto de jogo
            try
            {
                if (Usuario.Id.ToString() != _usuario.Id)
                {
                    //objeto de jogo recebe as informações dos inputs
                    Usuario = new Usuario
                    {
                        Id = Id,
                        nome = RegisterName,
                        email = RegisterEmail,
                        senha = Usuario.senha,
                        criado_em = Usuario.criado_em,
                        pontos = Pontos,
                        tipo = Tipo
                    };
                } else
                {
                    //objeto de jogo recebe as informações dos inputs
                    Usuario = new Usuario
                    {
                        Id = Id,
                        nome = RegisterName,
                        email = RegisterEmail,
                        senha = Usuario.senha,
                        criado_em = Usuario.criado_em,
                        pontos = Pontos,
                        tipo = Usuario.tipo
                    };
                }

                //atualiza
                _context.Usuarios.Update(Usuario);
                await _context.SaveChangesAsync();

                return RedirectToPage("/conta/admin/admin");
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
