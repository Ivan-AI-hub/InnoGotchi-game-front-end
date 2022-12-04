using InnoGotchiGameFrontEnd.Web.Models.Authorize;
using System.Net.Http.Headers;

namespace InnoGotchiGameFrontEnd.Web.Services
{
    public class BaseService
    {
        private IHttpClientFactory _httpClientFactory;
        protected AuthorizeModel AuthorizeModel;


        public BaseService(IHttpClientFactory httpClientFactory, AuthorizeModel authorize)
        {
            _httpClientFactory = httpClientFactory;
            AuthorizeModel = authorize;
        }

        protected HttpClient GetHttpClient(string clientName)
        {
            var httpClient = _httpClientFactory.CreateClient(clientName);
            if (AuthorizeModel.AccessToken != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthorizeModel.AccessToken);
            return httpClient;
        }
    }
}
