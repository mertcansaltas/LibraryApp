using LibraryApp.Context;
using LibraryApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryApp.Controllers
{
   
    public class KitapController : Controller
    {
        private readonly IKitapRepository _kitapRepo;
        private readonly IKitapTuruRepository _kitapTuruRepo;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public KitapController(IKitapRepository kitapRepo, IKitapTuruRepository kitapTuruRepo, IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepo = kitapRepo;
            _kitapTuruRepo = kitapTuruRepo;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles="Admin,Ogrenci")]
        public IActionResult Index()
        {
             List<Kitap> kitapList=_kitapRepo.GetAll(includeProps:"KitapTuru").ToList();
            return View(kitapList);
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> objKitapTuruList = _kitapTuruRepo.GetAll()
            .Select(x => new SelectListItem
            {
                Text = x.Ad,
                Value = x.ID.ToString()
            });
            ViewBag.KitapTuruList= objKitapTuruList;
            if (id==null|| id==0)
            {
                //ekle
                return View();
            }
            else
            {
                //guncelle
                Kitap? kitap = _kitapRepo.Get(x => x.ID == id);
                if (kitap == null)
                {
                    return NotFound();
                }
                return View(kitap);

            }
          
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(Kitap kitap,IFormFile? file)
        {
            var errors=ModelState.Values.SelectMany(x => x.Errors);
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath=Path.Combine(wwwRootPath, @"img");

                if (file!=null)
                {
                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    kitap.ResimUrl = @"\img\" + file.FileName;
                }
               

                if (kitap.ID==0)
                {
                    _kitapRepo.Ekle(kitap);
                    TempData["bilgi"] = "Yeni Kitap Türü başarıyla oluşturuldu!";
                }
                else
                {
                    _kitapRepo.Guncelle(kitap);
                    TempData["bilgi"] = "Yeni Kitap Türü başarıyla güncellendi!";
                }
               
                _kitapRepo.Kaydet(); //Savechanges yapmazsak bilgiler veri tabanına eklenmez.
                return RedirectToAction("Index","Kitap"); 
            }
            return View();
           
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int? id)
        {
            Kitap? kitap = _kitapRepo.Get(x=>x.ID==id);
            _kitapRepo.Sil(kitap);
            _kitapRepo.Kaydet();
            TempData["bilgi"] = "Kayıt Silme işlemi başarılı!";
            return RedirectToAction("Index");
        }
    }
}
