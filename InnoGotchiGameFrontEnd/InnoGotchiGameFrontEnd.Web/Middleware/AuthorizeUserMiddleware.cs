using InnoGotchiGameFrontEnd.BLL;
using InnoGotchiGameFrontEnd.BLL.Model.Authorize;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.Web.Middleware
{
    public class AuthorizeUserMiddleware
    {
        private RequestDelegate _next;

        public AuthorizeUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, AuthorizeModel authorizeModel, UserManager manager)
        {
            if (authorizeModel.AccessToken != null)
            {
                authorizeModel.User = await manager.GetAuthodizedUser();
            }
            await _next.Invoke(httpContext);
        }
    }
}
