using LibraryApp.Context;
using LibraryApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryApp.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KiralamaController : Controller
    {
        private readonly IKiralamaRepository _kiralamaRepo;
        private readonly IKitapRepository _kitapRepo;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public KiralamaController(IKiralamaRepository kiralamaRepo, IKitapRepository kitapRepo, IWebHostEnvironment webHostEnvironment)
        {
            _kiralamaRepo = kiralamaRepo;
            _kitapRepo = kitapRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {

             List<Kiralama> kiralamaList=_kiralamaRepo.GetAll(includeProps:"Kitap").ToList();

            return View(kiralamaList);
        }

        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> objKitapList = _kitapRepo.GetAll()
            .Select(x => new SelectListItem
            {
                Text = x.KitapAdi,
                Value = x.ID.ToString()
            });
            ViewBag.KitapList= objKitapList;
            if (id==null|| id==0)
            {
                //ekle
                return View();
            }
            else
            {
                //guncelle
                Kiralama? kiralama = _kiralamaRepo.Get(x => x.ID == id);
                if (kiralama == null)
                {
                    return NotFound();
                }
                return View(kiralama);

            }
          
        }

        [HttpPost]
        public IActionResult EkleGuncelle(Kiralama kiralama)
        {

            if (ModelState.IsValid)
            {

                if (kiralama.ID==0)
                {
                    _kiralamaRepo.Ekle(kiralama);
                    TempData["bilgi"] = "Yeni Kiralama Kaydı başarıyla oluşturuldu!";
                }
                else
                {
                    _kiralamaRepo.Guncelle(kiralama);
                    TempData["bilgi"] = "Kiralama Kaydı başarıyla güncellendi!";
                }
               
                _kiralamaRepo.Kaydet(); //Savechanges yapmazsak bilgiler veri tabanına eklenmez.
                return RedirectToAction("Index","Kiralama"); 
            }
            return View();
           
        }
   
        public IActionResult Sil(int? id)
        {
            Kiralama? kiralama = _kiralamaRepo.Get(x=>x.ID==id);
            _kiralamaRepo.Sil(kiralama);
            _kiralamaRepo.Kaydet();
            TempData["bilgi"] = "Kayıt Silme işlemi başarılı!";
            return RedirectToAction("Index");
        }
    }
}
