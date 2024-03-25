using LibraryApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Context
{
    public class AppDbContext: IdentityDbContext //AppDbContext, EntityFramework kütüphanesindeki DbContext sınıfından türer
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {

        }  
        public DbSet<KitapTuru> KitapTurleri { get; set; }
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Kiralama> Kiralamalar { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } //Veritabanında tablo oluşturmak için
    }
}
