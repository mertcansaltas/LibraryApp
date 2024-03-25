using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class Kiralama
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int OgrenciID { get; set; }


        [ValidateNever]
        public int KitapID { get; set; }


        [ValidateNever]
        [ForeignKey("KitapID")]
        public Kitap Kitap { get; set; }

    }
}
