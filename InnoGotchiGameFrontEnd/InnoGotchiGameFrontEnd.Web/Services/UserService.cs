﻿using InnoGotchiGame.Web.Models.Users;
using InnoGotchiGameFrontEnd.Web.Models.Authorize;
using InnoGotchiGameFrontEnd.Web.Models.Users;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.Web.Services
{
    public class UserService : BaseService
    {
        private string _clientName;

        public UserService(IHttpClientFactory httpClientFactory, AuthorizeModel authorize) : base(httpClientFactory, authorize)
        {
            _clientName = "Users";
        }

        public async Task<IEnumerable<User>> OnGet()
        {
            var httpClient = GetHttpClient(_clientName);
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
            var httpClient = GetHttpClient(_clientName);
            var httpResponseMessage = await httpClient.GetAsync(httpClient.BaseAddress + $"/{id}");

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
        public async Task<User?> OnGetAuthodizedUser()
        {
            var httpClient = GetHttpClient(_clientName);
            var httpResponseMessage = await httpClient.GetAsync(httpClient.BaseAddress + $"/Authorized");

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

        public async Task<bool> OnPost(AddUserModel addModel)
        {
            var httpClient = GetHttpClient(_clientName);
            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(addModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await httpClient.PostAsync("", jsonContent);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            else return false;
        }
        public async Task<bool> OnPut(UpdateUserModel updateModel)
        {
            var httpClient = GetHttpClient(_clientName);

            using StringContent jsonContent = new(
                                     JsonSerializer.Serialize(updateModel),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponseMessage = await httpClient.PutAsync("", jsonContent);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            else return false;
        }
        public async Task<bool> OnDelete(int userId)
        {
            var httpClient = GetHttpClient(_clientName);

            var httpResponseMessage = await httpClient.DeleteAsync(httpClient.BaseAddress + $"/{userId}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            else return false;
        }

        public async Task<string?> Authorize(string email, string password)
        {
            var httpClient = GetHttpClient(_clientName);
            var parameters = new Dictionary<string, string>();
            parameters["email"] = email;
            parameters["password"] = password;
            var httpResponseMessage = await httpClient.PostAsync(httpClient.BaseAddress + "/token", new FormUrlEncodedContent(parameters));

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var token = (await httpResponseMessage.Content.ReadAsStringAsync()).Replace("\"", "");
                AuthorizeModel.AccessToken = token;
                return token;
            }
            return null;
        }
    }
}
