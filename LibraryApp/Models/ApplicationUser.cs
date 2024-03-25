using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public int OgrenciNo { get; set; }
        public string? Adres { get; set; } //Soru işareti koymamızın sebebi boş olabilir demek.
        public string? Fakülte { get; set; }
        public string? Bolum { get; set; }

    }
}
