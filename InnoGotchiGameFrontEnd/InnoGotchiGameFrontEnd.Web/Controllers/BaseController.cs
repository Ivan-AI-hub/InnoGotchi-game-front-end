using InnoGotchiGameFrontEnd.Web.Models.Authorize;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchiGameFrontEnd.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string GetValueFromCookie(HttpRequest request, string cookieName, string queryName, string defaultValue = "")
        {
            if (request.Query[queryName].Count() > 0)
            {
                return request.Query[queryName][0];
            }
            else if (request.Cookies[cookieName] != null)
            {
                return request.Cookies[cookieName];
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
