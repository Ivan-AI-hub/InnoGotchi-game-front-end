using InnoGotchiGameFrontEnd.Presentation;
using InnoGotchiGameFrontEnd.Presentation.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

internal class TokenStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
	private ILocalStorageService _localStorageService;
    public TokenStateProvider(HttpClient httpClient,ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorageService.GetAsync<SecurityToken>(nameof(SecurityToken));
        if(token == null)
        {
            return GetAnonymous();
        }

        if(String.IsNullOrEmpty(token.AccessToken) || token.ExpireAt < DateTime.UtcNow) 
        {
            return GetAnonymous();
        }

		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.AccessToken);
		var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, token.UserName),
            new Claim(ClaimTypes.Expired, token.ExpireAt.ToLongDateString()),

        };

		var identity = new ClaimsIdentity(claims, "Bearer");
        var principal = new ClaimsPrincipal(identity);
        return new AuthenticationState(principal);
    }

    public void MakeUserAnonymous()
    {
        _localStorageService.RemoveAsync(nameof(SecurityToken));

        var authState = Task.FromResult(GetAnonymous());
        NotifyAuthenticationStateChanged(authState);
    }

	private AuthenticationState GetAnonymous()
	{
		var anonymousIdentity = new ClaimsIdentity();
		var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
		return new AuthenticationState(anonymousPrincipal);
	}
}