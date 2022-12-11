using AutoMapper;
using InnoGotchiGameFrontEnd.BLL;
using InnoGotchiGameFrontEnd.BLL.Mappings;
using InnoGotchiGameFrontEnd.BLL.Model.Authorize;
using InnoGotchiGameFrontEnd.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var config = new MapperConfiguration(cnf => cnf.AddProfiles(new List<Profile>() {new BllMappingProfile() }));

builder.Services.AddTransient<IMapper>(x => new Mapper(config));
builder.Services.AddTransient<UserManager>();
builder.Services.AddTransient<ColaborationRequestManager>();
builder.Services.AddScoped<AuthorizeModel>();
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
    services.AddHttpClient("Colaborators", httpClient =>
    {
        httpClient.BaseAddress = new Uri(baseUri + "colaborators");
    });
}
