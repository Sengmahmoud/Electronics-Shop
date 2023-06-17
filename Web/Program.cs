
using Infrastructure.AutoMapper;
using Infrastructure.Ioc;
using Infrastructure.Migrations;
using Serilog;
using Web.Security;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.ConfigurePermission();
builder.Services.AddControllersWithViews();
builder.Services.Configureioc();
builder.Services.ConfigureAutoMapper();
var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("serilog.json")
               .Build();

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);
var app = builder.Build();


//using (var scope = app.Services.CreateScope())
//{
//    await DBIntializer.InitIdentity(scope.ServiceProvider);
//}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");

});
app.Run();
