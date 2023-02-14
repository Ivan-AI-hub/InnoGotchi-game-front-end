using AuthorizationInfrastructure;
using AuthorizationInfrastructure.HttpClients;
using InnoGotchiGameFrontEnd.BLL;
using InnoGotchiGameFrontEnd.BLL.Mappings;
using InnoGotchiGameFrontEnd.Presentation;
using InnoGotchiGameFrontEnd.Presentation.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<IStorageService, LocalStorageService>();
builder.Services.AddScoped<IElementReferenceService, ElementReferenceService>();
builder.Services.AddScoped<AuthenticationStateProvider, TokenStateProvider>();

builder.Services.AddAutoMapper(typeof(BllMappingProfile));
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<ColaborationRequestManager>();
builder.Services.AddScoped<FarmManager>();
builder.Services.AddScoped<PetManager>();
builder.Services.AddScoped<PictureManager>();

builder.Services.AddHttpClient<IAuthorizedClient, AuthorizedClient>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetSection("BackEndAddress").Value + "api/");
    });

builder.Services
    .AddSingleton<MouseService>()
    .AddSingleton<IMouseService>(ff => ff.GetRequiredService<MouseService>());

await builder.Build().RunAsync();
