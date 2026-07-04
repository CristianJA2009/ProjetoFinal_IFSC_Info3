using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
public class _LayoutModel : PageModel
{
    public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        var paginaAtual = context.ActionDescriptor.ViewEnginePath.ToLower();
        var usuarioTipo = context.HttpContext.Session.GetString("UsuarioTipo");


        if (!string.IsNullOrEmpty(usuarioTipo))
        {
            if (paginaAtual == "/conta/login" || paginaAtual == "/conta/registro")
            {
                context.Result = new RedirectToPageResult("/conta/perfil");
                return;
            }
        }

        base.OnPageHandlerExecuting(context);
    }
}