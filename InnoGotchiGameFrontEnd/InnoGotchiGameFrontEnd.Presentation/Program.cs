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
builder.Services.AddScoped<AuthenticationStateProvider, TokenStateProvider>();
builder.Services.AddScoped(sp => 
new HttpClient 
{ 
	BaseAddress = new Uri("https://localhost:10000/api/") 
});

var config = new MapperConfiguration(cnf => cnf.AddProfiles(new List<Profile>() { new BllMappingProfile() }));

builder.Services.AddTransient<IMapper>(x => new Mapper(config));
builder.Services.AddTransient<UserManager>();
builder.Services.AddTransient<ColaborationRequestManager>();
builder.Services.AddTransient<FarmManager>();
builder.Services.AddTransient<PetManager>();
builder.Services.AddTransient<PictureManager>();

await builder.Build().RunAsync();
