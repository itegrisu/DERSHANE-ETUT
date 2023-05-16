using ETUT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ETUT.Controllers
{
    // create a ViewModel class
    public class BilgilerViewModel
    {
        // include the models you want to use
        public int Id { get; set; }
        public List<TBLOGRENCI> OgrenciBilgileri { get; set; }
        public List<TBLOGRETMEN> OgretmenBilgileri { get; set; }
    }
    public class AdminController : Controller
    {
        EtutEntities1 db = new EtutEntities1();

       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminAnasayfasi()
        {

            ViewBag.ogrenciSayisi = db.TBLOGRENCI.Where(x => x.KAYITDURUMU == false || x.KAYITDURUMU == true).ToList().Count();
            ViewBag.ogretmenSayisi = db.TBLOGRETMEN.Where(x => x.KAYITDURUMU == false || x.KAYITDURUMU == true).ToList().Count();
            var tBLETUT = db.TBLETUT.Include(t => t.TBLOGRENCI).Include(t => t.TBLOGRETMEN).Include(t => t.TBLORTALAN).Include(t => t.TBLSAAT).Where(t => t.AKTIFMI == false);

            return View(tBLETUT.ToList());
        }

        public ActionResult OgrenciDuzenle(int? id)
        {
            List<SelectListItem> degerler = (from i in db.TBLOGRBRANS.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.BRANSADI,
                                                 Value = i.ID.ToString()
                                             }).ToList();

            List<SelectListItem> degerler1 = (from i in db.TBLOGRSINIF.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.SINIFSEVIYESI,
                                                  Value = i.ID.ToString()
                                              }).ToList();
            ViewBag.dgr = degerler;
            ViewBag.dgr1 = degerler1;

            TBLOGRENCI secilenOgrenci = db.TBLOGRENCI.Find(id);

            return View(secilenOgrenci);
        }     

        [HttpPost]

        public ActionResult OgrenciDuzenle(TBLOGRENCI p1)
        {
           
            List<SelectListItem> degerler = (from i in db.TBLOGRBRANS.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.BRANSADI,
                                                 Value = i.ID.ToString()
                                             }).ToList();

            List<SelectListItem> degerler1 = (from i in db.TBLOGRSINIF.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.SINIFSEVIYESI,
                                                  Value = i.ID.ToString()
                                              }).ToList();
            ViewBag.dgr = degerler;

            ViewBag.dgr1 = degerler1;         

            if (ModelState.IsValid)
            {
              
                db.Set<TBLOGRENCI>().AddOrUpdate(p1);
                db.SaveChanges();
                return RedirectToAction("AdminAnasayfasi", "Admin");              
               
            }
            else
            {
                return View(p1);
            }

        }


        public ActionResult OgretmenDuzenle(int? id)
        {
            List<SelectListItem> degerler = (from i in db.TBLORTALAN.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.DERSADI,
                                                 Value = i.ID.ToString()
                                             }).ToList();


            ViewBag.dgr3 = degerler;

            TBLOGRETMEN secilenOgretmen = db.TBLOGRETMEN.Find(id);
        

            return View(secilenOgretmen);
        }

        [HttpPost]
        public ActionResult OgretmenDuzenle(TBLOGRETMEN p1)
        {

            List<SelectListItem> degerler = (from i in db.TBLORTALAN.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.DERSADI,
                                                 Value = i.ID.ToString()
                                             }).ToList();


            ViewBag.dgr3 = degerler;



            if (ModelState.IsValid)
            {

                db.Set<TBLOGRETMEN>().AddOrUpdate(p1);
                db.SaveChanges();
                return RedirectToAction("AdminAnasayfasi", "Admin");
            }
            else
            {
                return View(p1);
            }

        }

        public ActionResult Sil(int id)
        {
            var ogretmen = db.TBLOGRETMEN.FirstOrDefault(o => o.ID == id);
            if (ogretmen != null)
            {
                // Öğretmeni veritabanından sil
                db.TBLOGRETMEN.Remove(ogretmen);

                var etutler = db.TBLETUT.Where(e => e.OGRETMENID == id).ToList();
                // Sonra bu kayıtları veritabanından sil
                db.TBLETUT.RemoveRange(etutler);
                db.SaveChanges();
               
            }

            // TBLOGRENCI tablosunda id'ye göre ara
            var ogrenci = db.TBLOGRENCI.FirstOrDefault(o => o.ID == id);

            // Eğer öğrenci bulunduysa
            if (ogrenci != null)
            {
                // Öğrenciyi veritabanından sil
                db.TBLOGRENCI.Remove(ogrenci);
                // Değişiklikleri kaydet
                var etutler = db.TBLETUT.Where(e => e.OGRENCIID == id).ToList();
                // Sonra bu kayıtları veritabanından sil
                db.TBLETUT.RemoveRange(etutler);
                // Değişiklikleri kaydet
                db.SaveChanges();
            }


            return RedirectToAction("AdminAnasayfasi");
        }

        public ActionResult Duzenle(string aramaKelimesi)
        {
          
            // create a ViewModel instance
            var viewModel = new BilgilerViewModel();

            viewModel.OgrenciBilgileri = db.TBLOGRENCI.Where(x => x.KAYITDURUMU == false || x.KAYITDURUMU == true).ToList();
            viewModel.OgretmenBilgileri = db.TBLOGRETMEN.Where(x => x.KAYITDURUMU == false || x.KAYITDURUMU == true).ToList();

            if (!string.IsNullOrEmpty(aramaKelimesi))
            {
                viewModel.OgrenciBilgileri = viewModel.OgrenciBilgileri.Where(x => x.AD.Contains(aramaKelimesi) || x.SOYAD.Contains(aramaKelimesi)).ToList();
                viewModel.OgretmenBilgileri = viewModel.OgretmenBilgileri.Where(x => x.AD.Contains(aramaKelimesi) || x.SOYAD.Contains(aramaKelimesi)).ToList();
            }

            return View(viewModel);
        }

        public ActionResult Etutler()
        {
            var tBLETUT = db.TBLETUT.Include(t => t.TBLOGRENCI).Include(t => t.TBLOGRETMEN).Include(t => t.TBLORTALAN).Include(t => t.TBLSAAT).Where(t => t.AKTIFMI == true);
            return View(tBLETUT.ToList());
        }
        public ActionResult EtutOnayla(int id)
        {
            var onaylanacakDers = db.TBLETUT.Find(id);
            onaylanacakDers.AKTIFMI = true;
            db.SaveChanges();
            return RedirectToAction("AdminAnasayfasi");
        }

        public ActionResult EtutSil(int id)
        {
            TBLETUT silenecekEtut = db.TBLETUT.Find(id);
            db.TBLETUT.Remove(silenecekEtut);
            db.SaveChanges();

            return RedirectToAction("AdminAnasayfasi");
        }



    }
}