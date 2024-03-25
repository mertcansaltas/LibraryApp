using LibraryApp.Context;
using LibraryApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KitapTuruController : Controller
    {
        private readonly IKitapTuruRepository _kitapTuruRepo;

        public KitapTuruController(IKitapTuruRepository kitapTuruRepo)
        {
            _kitapTuruRepo = kitapTuruRepo;
        }

        public IActionResult Index()
        {
            List<KitapTuru> kitapTuruList=_kitapTuruRepo.GetAll().ToList();  
            return View(kitapTuruList);
        }

        public IActionResult Ekle()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult Ekle(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)
            {
                _kitapTuruRepo.Ekle(kitapTuru);
                _kitapTuruRepo.Kaydet(); //Savechanges yapmazsak bilgiler veri tabanına eklenmez.
                TempData["bilgi"] = "Yeni Kitap Türü başarıyla oluşturuldu!";
                return RedirectToAction("Index","KitapTuru"); 
            }
            return View();
           
        }
        public IActionResult Guncelle(int? id) //id null olursa program patlamasın diye soru işareti koyduk. id'yi identity olarak ayarladık şu an için programın patlaması imkansız ancak biz yine de bunu alışkanlık haline getirelim.
        {
            if (id == null || id==0)
            {
                return NotFound();  
            }
            KitapTuru? kitapTuru = _kitapTuruRepo.Get(x=>x.ID==id);
            if (kitapTuru == null)
            {
                return NotFound();
            }
            return View(kitapTuru);
        }
        [HttpPost]
        public IActionResult Guncelle(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)
            {
                _kitapTuruRepo.Guncelle(kitapTuru);
                _kitapTuruRepo.Kaydet();
                TempData["bilgi"] = "Yeni Kitap Türü başarıyla güncellendi!";
                return RedirectToAction(nameof(Index));
            }
            return View();
     
        }
        public IActionResult Sil(int? id)
        {
            KitapTuru? kitapTuru = _kitapTuruRepo.Get(x=>x.ID==id);
            _kitapTuruRepo.Sil(kitapTuru);
            _kitapTuruRepo.Kaydet();
            TempData["bilgi"] = "Kayıt Silme işlemi başarılı!";
            return RedirectToAction("Index");
        }
    }
}
