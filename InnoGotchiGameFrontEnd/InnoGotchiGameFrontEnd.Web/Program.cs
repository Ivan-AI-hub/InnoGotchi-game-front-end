using InnoGotchiGameFrontEnd.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<UserService>();
builder.Services.AddLogging();
HttpClientsConfiguration(builder.Services, "https://localhost:7209/api/");

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");

	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseHttpLogging();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


void HttpClientsConfiguration(IServiceCollection services, string baseUri)
{
	services.AddHttpClient("Users", httpClient =>
	{
		httpClient.BaseAddress = new Uri(baseUri + "users");
	});
}
