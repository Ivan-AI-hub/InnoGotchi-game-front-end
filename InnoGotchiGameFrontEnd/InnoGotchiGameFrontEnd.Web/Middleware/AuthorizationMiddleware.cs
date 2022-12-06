using InnoGotchiGameFrontEnd.Web.Models.Authorize;
using InnoGotchiGameFrontEnd.Web.Services;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.Web.Middleware
{
    public class AuthorizationMiddleware
    {
        private RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext,UserService service, AuthorizeModel authorizeModel)
        {
            var sessionValue = httpContext.Session.GetString("token");
            if (sessionValue != null)
            {
                var sessionModel = JsonSerializer.Deserialize<AuthorizeModel>(sessionValue);
                authorizeModel.AccessToken = sessionModel.AccessToken;
                authorizeModel.User = await service.OnGetAuthodizedUser();
            }
            await _next.Invoke(httpContext);
        }
    }
}
