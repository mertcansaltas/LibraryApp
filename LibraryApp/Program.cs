using LibraryApp.Context;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace LibraryApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //Razor sayfalarının kullanılmasını sağlıyor.
            builder.Services.AddRazorPages();   

            //_kitapTuruRepository nesnesinin olu�turulmas�n� sa�l�yor. Dependency Injection ile
            builder.Services.AddScoped<IKitapTuruRepository, KitapTuruRepository>();

            //_kitapRepository nesnesinin olu�turulmas�n� sa�l�yor. Dependency Injection ile
            builder.Services.AddScoped<IKitapRepository, KitapRepository>();

            //_kiralamaRepository nesnesinin olu�turulmas�n� sa�l�yor. Dependency Injection ile
            builder.Services.AddScoped<IKiralamaRepository, KiralamaRepository>();

            builder.Services.AddScoped<IEmailSender,EmailSender>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication(); ;

            app.UseAuthorization();

            //Application'ımızın Razor Page'leri map edeceğini belirtiyoruz. Böylece artık razor page'leri kullanabilecek hale geliyoruz.
            app.MapRazorPages();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}