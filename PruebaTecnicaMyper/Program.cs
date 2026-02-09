using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using PruebaTecnicaMyper.Data;
using PruebaTecnicaMyper.Repositories;
using PruebaTecnicaMyper.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
	.AddDataAnnotationsLocalization();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnection")
	)
);

builder.Services.AddScoped<ITrabajadorRepository, TrabajadorRepository>();

builder.Services.AddScoped<ITrabajadorService, TrabajadorService>();

var app = builder.Build();

var supportedCultures = new[] { new CultureInfo("es-PE") };
var localizationOptions = new RequestLocalizationOptions
{
	DefaultRequestCulture = new RequestCulture("es-PE"),
	SupportedCultures = supportedCultures,
	SupportedUICultures = supportedCultures
};
app.UseRequestLocalization(localizationOptions);

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Trabajador}/{action=Index}/{id?}");

app.Run();
