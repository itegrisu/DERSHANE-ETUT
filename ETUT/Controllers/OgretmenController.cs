using ETUT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETUT.Controllers
{
    public class OgretmenController : Controller
    {
        public class EtutViewModel
        {
            // include the models you want to use
            public DateTime Zaman { get; set; }
            public List<TBLOGRENCI> OgrenciBilgileri { get; set; }
            public List<TBLSAAT> SaatBilgileri { get; set; }
        }
        // GET: Ogretmen
        EtutEntities1 db = new EtutEntities1();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OgretmenAnasayfasi()
        {
            var secilenOgretmen = Convert.ToInt32(TempData["girisYapanOgretmen"]);
            TempData.Keep("girisYapanOgretmen");
          
            var etutleriListele = db.TBLETUT.Include(t => t.TBLOGRENCI).Include(t => t.TBLSAAT).Where(t => t.OGRETMENID == secilenOgretmen && t.AKTIFMI == true);


            return View(etutleriListele.ToList());
        }




    }
}