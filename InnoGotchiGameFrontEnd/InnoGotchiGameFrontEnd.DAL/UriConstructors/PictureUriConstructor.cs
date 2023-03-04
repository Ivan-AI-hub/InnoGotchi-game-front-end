using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PictureAggregate;
using System.Text;

namespace InnoGotchiGameFrontEnd.DAL.UriConstructors
{
    internal static class PictureUriConstructor
    {
        public static string GenerateUriQuery(PictureFiltrator filtrator)
        {
            var requestUrl = new StringBuilder($"?");

            if (!String.IsNullOrEmpty(filtrator.Name))
            {
                requestUrl.Append($"&Name={filtrator.Name}");
            }
            if (!String.IsNullOrEmpty(filtrator.Description))
            {
                requestUrl.Append($"&Description={filtrator.Description}");
            }
            if (!String.IsNullOrEmpty(filtrator.Format))
            {
                requestUrl.Append($"&Format={filtrator.Format}");
            }
            return requestUrl.ToString();
        }
    }
}
