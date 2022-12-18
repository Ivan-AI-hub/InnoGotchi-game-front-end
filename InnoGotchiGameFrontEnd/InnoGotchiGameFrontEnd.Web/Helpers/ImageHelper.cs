using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace InnoGotchiGameFrontEnd.Web.Helpers
{
    public static class ImageHelper
    {
        public static HtmlString ImageFromByteArray(this IHtmlHelper html, byte[] image, int heigh = 200, int width = 200, string imgClass = "img-thumbnail rounded float-right")
        {
            var builder = new StringBuilder();
            builder.Append($"<img src=\"data:image/png;base64," +
                           $"{@Convert.ToBase64String(image)}\" " +
                           $"height=\"{heigh}\" " +
                           $"width=\"{width}\" " +
                           $"class=\"{imgClass}\" />");
            return new HtmlString(builder.ToString());
        }
    }
}
