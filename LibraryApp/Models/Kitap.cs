using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class Kitap
    {
        public int ID { get; set; }
        public string KitapAdi { get; set; }
        public string Tanim { get; set; }

        [Required]
        public string Yazar { get; set; }


        [Required]
        [Range(10,5000)]
        public double Fiyat { get; set; }

        [ValidateNever]
        public int KitapTuruID { get; set; }

        [ForeignKey("KitapTuruID")]

        [ValidateNever]
        public KitapTuru KitapTuru { get; set; }

        [ValidateNever]
        public string ResimUrl { get; set; }
    }
}
