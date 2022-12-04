using InnoGotchiGameFrontEnd.Web.Models.Authorize;
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

        public Task Invoke(HttpContext httpContext, AuthorizeModel authorizeModel)
        {
            var sessionValue = httpContext.Session.GetString("token");
            if (sessionValue != null)
            {
                var sessionModel = JsonSerializer.Deserialize<AuthorizeModel>(sessionValue);
                authorizeModel.AccessToken = sessionModel.AccessToken;
                authorizeModel.User = sessionModel.User;
            }
            return _next.Invoke(httpContext);
        }
    }
}
