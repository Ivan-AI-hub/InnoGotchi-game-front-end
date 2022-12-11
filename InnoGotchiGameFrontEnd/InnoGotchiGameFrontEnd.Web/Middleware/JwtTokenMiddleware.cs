using InnoGotchiGameFrontEnd.BLL;
using InnoGotchiGameFrontEnd.BLL.Model.Authorize;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.Web.Middleware
{
    public class JwtTokenMiddleware
    {
        private RequestDelegate _next;

        public JwtTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, AuthorizeModel authorizeModel)
        {
            var cookieValue = httpContext.Request.Cookies["token"];
            if (cookieValue != null)
            {
                var sessionModel = JsonSerializer.Deserialize<AuthorizeModel>(cookieValue);
                authorizeModel.AccessToken = sessionModel.AccessToken;
            }
            await _next.Invoke(httpContext);
        }
    }
}
