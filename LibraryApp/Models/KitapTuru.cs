using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class KitapTuru
    {
        [Key] //primary key
        public int ID { get; set; } // Böylece "Key" yazarak hem primary key hem de identity olarak tanımlamış olduk. SQL Design kısmından bakarak da kontrol edebiliriz.


        [Required(ErrorMessage ="Kitap Tür Adı boş bırakılamaz!")] //not null
        [MaxLength(25)] //Karakter sayısı maksimum 25 olmalıdır.
        [DisplayName("Kitap Türü Adı")] //label kısmında Ad yerine Kitap Türü Adı yazısı görünecek
        public string Ad { get; set; }
     

    }
}
