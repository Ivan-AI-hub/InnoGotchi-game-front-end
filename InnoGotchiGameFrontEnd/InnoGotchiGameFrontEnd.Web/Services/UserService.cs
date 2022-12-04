using InnoGotchiGame.Application.Models;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net.Http.Headers;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace InnoGotchiGameFrontEnd.Web.Services
{
	public class UserService
	{
		private IHttpClientFactory _httpClientFactory;
		private string _clientName;
        private static string? _accessToken;

        public UserService(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
			_clientName = "Users";
		}

		public async Task<IEnumerable<User>> OnGet()
		{
			var httpClient = _httpClientFactory.CreateClient(_clientName);
            if (_accessToken != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_accessToken);
            var httpResponseMessage = await httpClient.GetAsync("");

			IEnumerable<User> users = new List<User>();

			if (httpResponseMessage.IsSuccessStatusCode)
			{
				using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};
				users = await JsonSerializer.DeserializeAsync<IEnumerable<User>>(contentStream, options);
			}
			return users;
		}

        public async Task<User?> OnGet(int id)
        {
            var httpClient = _httpClientFactory.CreateClient(_clientName);
            if(_accessToken != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var httpResponseMessage = await httpClient.GetAsync($"https://localhost:7209/api/users/{id}");

            User? user = null;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                user = await JsonSerializer.DeserializeAsync<User>(contentStream, options);
            }
            return user;
        }

		public async Task Authorize(string email, string password)
		{
            var httpClient = _httpClientFactory.CreateClient(_clientName);
            var parameters = new Dictionary<string, string>();
            parameters["email"] = email;
            parameters["password"] = password;
            var httpResponseMessage = await httpClient.PostAsync("https://localhost:7209/api/users/token", new FormUrlEncodedContent(parameters));

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                _accessToken = (await httpResponseMessage.Content.ReadAsStringAsync()).Replace("\"", "");
            }
        }
    }
}
