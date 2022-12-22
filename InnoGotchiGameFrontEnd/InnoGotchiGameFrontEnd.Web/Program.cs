using AutoMapper;
using InnoGotchiGameFrontEnd.BLL;
using InnoGotchiGameFrontEnd.BLL.Mappings;
using InnoGotchiGameFrontEnd.BLL.Model.Identity;
using InnoGotchiGameFrontEnd.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var config = new MapperConfiguration(cnf => cnf.AddProfiles(new List<Profile>() { new BllMappingProfile() }));

builder.Services.AddTransient<IMapper>(x => new Mapper(config));
builder.Services.AddTransient<UserManager>();
builder.Services.AddTransient<ColaborationRequestManager>();
builder.Services.AddTransient<FarmManager>();
builder.Services.AddTransient<PetManager>();
builder.Services.AddTransient<PictureManager>();
builder.Services.AddScoped<SecurityToken>();
builder.Services.AddLogging();
HttpClientsConfiguration(builder.Services, "https://localhost:7209/api/");

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseMiddleware<JwtTokenMiddleware>();
app.UseMiddleware<AuthorizeUserMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<BasePetViewInitializer>();
app.UseHttpLogging();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "users/{action=Index}");

app.Run();


void HttpClientsConfiguration(IServiceCollection services, string baseUri)
{
    services.AddHttpClient("Users", httpClient =>
    {
        httpClient.BaseAddress = new Uri(baseUri + "users");
    });    
    services.AddHttpClient("Farms", httpClient =>
    {
        httpClient.BaseAddress = new Uri(baseUri + "farms");
    });
    services.AddHttpClient("Pets", httpClient =>
    {
        httpClient.BaseAddress = new Uri(baseUri + "pets");
    });
    services.AddHttpClient("Pictures", httpClient =>
    {
        httpClient.BaseAddress = new Uri(baseUri + "pictures");
    });
    services.AddHttpClient("Colaborators", httpClient =>
    {
        httpClient.BaseAddress = new Uri(baseUri + "colaborators");
    });
}
