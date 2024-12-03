using BlazorApp1.Data;
using BlazorApp1.Auth;
using BlazorApp1.Data;
using BlazorApp1.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

namespace BlazorApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("Server=sql.bsite.net\\MSSQL2016;Database=leemon_;User Id=leemon_;Password=11111;TrustServerCertificate=true;");
            Console.WriteLine($"Connection String: {connectionString}");

            builder.Services.AddDbContext<LeemonContext>(options =>
                options.UseSqlServer("Server=sql.bsite.net\\MSSQL2016;Database=leemon_;User Id=leemon_;Password=11111;TrustServerCertificate=true;"));

            builder.Services.AddAuthenticationCore();
            //builder.Services.AddRazorComponents()
            //.AddInteractiveServerComponents();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddMudServices();


            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<ProtectedSessionStorage>();
            builder.Services.AddScoped<ProtectedLocalStorage>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseAntiforgery();

            //app.MapRazorComponents<App>()
            //.AddInteractiveServerRenderMode();

            app.UseRouting();
            app.MapRazorPages();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Program>();
            });

    }
}