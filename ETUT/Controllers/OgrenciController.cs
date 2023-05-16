using ETUT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;


public class SaatTarihViewModel // ViewModel sınıfı
{
    public Nullable<int> SAATID { get; set; } // Saat özelliği
    public DateTime Tarih { get; set; } // Tarih özelliği
    public bool EtutAlinmisMi { get; set; }


}
namespace ETUT.Controllers
{
        public class OgrenciController : Controller
    {
        EtutEntities1 db = new EtutEntities1();
        // GET: Etut

        public ActionResult OgrenciAnasayfasi()
        {
            return View();
        }

        public ActionResult OgrencininEtutleriniGoster()
        {
            var girisYapanOgrenci = Convert.ToInt32(TempData["girisYapanOgrenci"]);
            TempData.Keep("girisYapanOgrenci");

            var etutleriListele = db.TBLETUT.Where(t => t.OGRENCIID == girisYapanOgrenci);


            return View(etutleriListele.ToList());
        }

        public ActionResult DersSecimEkrani()
        {
            var alanlar = db.TBLORTALAN.ToList();
            var ogretmenler = new SelectList(new List<TBLOGRETMEN>(), "ID", "AD");
            var saatler = new List<SelectListItem>();
            var model = new TBLETUT();

            ViewBag.Alanlar = new SelectList(alanlar, "ID", "DERSADI");
            ViewBag.Ogretmenler = ogretmenler; // burada boş bir SelectList atanıyor
            ViewBag.Saatler = new SelectList(saatler, "Value", "Text");

            return View(model);
        }

        public ActionResult OgretmenlerıAlandanGetir(int alanId)
        {
            var ogretmenler = db.TBLOGRETMEN.Where(x => x.ALANID == alanId).ToList();
            TempData["BransID"] = alanId;
            var ogretmenDatalari = ogretmenler.Select(o => new SelectListItem
            {
                Value = o.ID.ToString(),
                Text = $"{o.AD} {o.SOYAD}",
            });


            ViewBag.Ogretmenler = ogretmenDatalari;
            return Json(ogretmenDatalari, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult OgretmenIdGetir(int ogretmenId)
        {
            TempData["OgretmenID"] = ogretmenId;
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
          
        [HttpGet]
        public ActionResult EtutAl()
        {
            return View();
        }

        public ActionResult _EtutAl()
        {
            var liste = new List<SaatTarihViewModel>(); // ViewModel sınıfından bir liste oluşturuyoruz
            var secilenOgretmen = Convert.ToInt32(TempData["OgretmenID"]);
            TempData.Keep("OgretmenId"); // TempDatayı bir kez daha kulllanabilmek için Keep methodunu kullanıyoruz
            // Veritabanından etütleri alın
            var saatler = (from t in db.TBLETUT where t.SAATID > 0 select t.TBLSAAT.SAAT).ToList();
            var zamanlar = (from t in db.TBLETUT select t.ZAMAN). ToList();
            var secilenHocadanAlinmisEtutler = (from s in db.TBLETUT
                                                where s.OGRETMENID == secilenOgretmen
                                                select new
                                                {
                                                    SAATID = s.SAATID,
                                                    ZAMAN = s.ZAMAN,
                                                }).ToList();

            var saatTarihViewModelList = secilenHocadanAlinmisEtutler.Select(x => new SaatTarihViewModel
            {
                SAATID = x.SAATID,
                Tarih = x.ZAMAN,
                EtutAlinmisMi = true
            }).ToList();

            var model = new TBLETUT();
            model.Liste = saatTarihViewModelList;
            return PartialView(model);
        }


        [HttpPost]
        public ActionResult EtutAl(TBLETUT yeniEtut, string hiddenDate, string hiddenTime)
        {

            var saat = hiddenTime; // örnek saat verisi
            var id = db.TBLSAAT.FirstOrDefault(s => s.SAAT == saat)?.ID;

            yeniEtut.OGRENCIID = Convert.ToInt32(TempData["OgrenciID"]);
            yeniEtut.ALANID = Convert.ToInt32(TempData["BransID"]);
            yeniEtut.OGRETMENID = Convert.ToInt32(TempData["OgretmenID"]);
            TempData.Keep("OgretmenId");
            yeniEtut.ZAMAN = Convert.ToDateTime(hiddenDate);
            yeniEtut.SAATID = id;
            yeniEtut.AKTIFMI = false;

            if (ModelState.IsValid)
            {
                db.TBLETUT.Add(yeniEtut);
                db.SaveChanges();
                return RedirectToAction("OgrenciAnasayfasi","Ogrenci");
            }
            else
            {
                return View();
            }
        }
     


    }
}