using InnoGotchiGameFrontEnd.Presentation;
using InnoGotchiGameFrontEnd.Presentation.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddTransient<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, TokenStateProvider>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


await builder.Build().RunAsync();
