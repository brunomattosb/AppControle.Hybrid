using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AppControle.API.Filters;
public class GetCurrentUserAttribute : TypeFilterAttribute
{
    public GetCurrentUserAttribute() : base(typeof(GetCurrentUserAttribute))
    {
    }

    private class GetCurrentUser : IAsyncActionFilter
    {

        //private readonly UserManager<SeuUsuario> _userManager;

        //public GetCurrentUser(UserManager<SeuUsuario> userManager)
        //{
        //    _userManager = userManager;
        //}

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
        //    if (context.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //        if (!string.IsNullOrEmpty(userId))
        //        {
        //            var usuarioLogado = await _userManager.FindByIdAsync(userId);

        //            if (usuarioLogado != null)
        //            {
        //                context.HttpContext.Items["UsuarioLogado"] = usuarioLogado;
        //            }
        //        }
        //    }

            await next();
        }
    }
}