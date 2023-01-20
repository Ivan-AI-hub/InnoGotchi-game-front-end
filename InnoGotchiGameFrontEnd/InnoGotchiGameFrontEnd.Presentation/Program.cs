using AutoMapper;
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

builder.Services.AddTransient<ILocalStorageService, LocalStorageService>();
builder.Services.AddTransient<IElementReferenceService, ElementReferenceService>();
builder.Services.AddScoped<AuthenticationStateProvider, TokenStateProvider>();
builder.Services.AddScoped(sp => 
new HttpClient 
{ 
	BaseAddress = new Uri(builder.Configuration.GetSection("BackEndAddress").Value + "api/") 
});

var config = new MapperConfiguration(cnf => cnf.AddProfiles(new List<Profile>() { new BllMappingProfile() }));

builder.Services.AddTransient<IMapper>(x => new Mapper(config));
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<ColaborationRequestManager>();
builder.Services.AddScoped<FarmManager>();
builder.Services.AddScoped<PetManager>();
builder.Services.AddScoped<PictureManager>();

builder.Services
    .AddSingleton<MouseService>()
    .AddSingleton<IMouseService>(ff => ff.GetRequiredService<MouseService>());

await builder.Build().RunAsync();
