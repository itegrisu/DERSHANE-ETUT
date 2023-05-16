using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ETUT.Models;

namespace ETUT.Controllers
{
    public class KayitController : Controller
    {
        EtutEntities1 db = new EtutEntities1();
        // GET: Kayit
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult OgrenciKayit()
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

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult OgrenciKayit(TBLOGRENCI p1)
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

            bool varolanEposta = db.TBLOGRENCI.Any(x => x.EPOSTA == p1.EPOSTA);
            bool varolanTc = db.TBLOGRENCI.Any(x => x.TC == p1.TC);

            if (varolanEposta)
            {
                TempData["hata"] = "Bu eposta kullanımda.";

                ModelState.AddModelError("Username", "Bu kullanıcı adı kullanımda.");
            }
            if (varolanTc)
            {
                TempData["hata2"] = "Bu TC kullanımda.";
                ModelState.AddModelError("Email", "Bu e-posta adresi kullanımda.");

            }

            if (ModelState.IsValid)
            {
                var secilenBrans = db.TBLOGRBRANS.Where(br => br.ID == p1.TBLOGRBRANS.ID).FirstOrDefault();
                p1.TBLOGRBRANS = secilenBrans;

                var secilenSeviye = db.TBLOGRSINIF.Where(sv => sv.ID == p1.TBLOGRSINIF.ID).FirstOrDefault();
                p1.TBLOGRSINIF = secilenSeviye;
                                
                db.TBLOGRENCI.Add(p1);

                db.SaveChanges();


                TempData["KayitBasarili"] = true;
                return View(p1);
            }
            else
            {
                return View(p1);
            }


        }

        [AllowAnonymous]
        public ActionResult OgretmenKayit()
        {
            List<SelectListItem> degerler = (from i in db.TBLORTALAN.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.DERSADI,
                                                 Value = i.ID.ToString()
                                             }).ToList();


            ViewBag.dgr3 = degerler;


            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult OgretmenKayit(TBLOGRETMEN p1)
        {
            List<SelectListItem> degerler = (from i in db.TBLORTALAN.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.DERSADI,
                                                 Value = i.ID.ToString()
                                             }).ToList();


            ViewBag.dgr3 = degerler;

            bool varolanEposta = db.TBLOGRETMEN.Any(x => x.EPOSTA == p1.EPOSTA);
            bool varolanTc = db.TBLOGRETMEN.Any(x => x.TC == p1.TC);

            if (varolanEposta)
            {
                TempData["hata3"] = "Bu eposta kullanımda.";
                
                ModelState.AddModelError("Username", "Bu kullanıcı adı kullanımda.");
            }
            if (varolanTc)
            {
                TempData["hata4"] = "Bu TC kullanımda.";
                ModelState.AddModelError("Email", "Bu e-posta adresi kullanımda.");

            }

            if (ModelState.IsValid)
            {

                var secilenAlan = db.TBLORTALAN.Where(br => br.ID == p1.TBLORTALAN.ID).FirstOrDefault();
                p1.TBLORTALAN = secilenAlan;



                db.TBLOGRETMEN.Add(p1);

                db.SaveChanges();

                TempData["KayitBasarili"] = true;
                return View(p1);
            }
            else
            {
                return View(p1);
            }


        }

       




    }
}