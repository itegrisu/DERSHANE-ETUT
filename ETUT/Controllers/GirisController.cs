using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETUT.Models;
using System.Net.Mail;
using System.Web.Security;

namespace ETUT.Controllers
{
    public class GirisController : Controller
    {
        EtutEntities1 db = new EtutEntities1();
        // GET: Giris
        [AllowAnonymous]
        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Giris(string eposta, string SIFRE)
        {
            var user = db.TBLOGRENCI.FirstOrDefault(u => u.EPOSTA == eposta && u.SIFRE == SIFRE);
            string userType = "OGRENCI";
            if (user != null)
            {
                TempData["girisYapanOgrenci"] = user.ID;
            }

            if (user == null)
            {
                var user1 = db.TBLOGRETMEN.FirstOrDefault(u => u.EPOSTA == eposta && u.SIFRE == SIFRE);
                userType = "OGRETMEN";
                if (user1 != null )
                {
                    TempData["girisYapanOgretmen"] = user1.ID;
                }
                if (user1 == null)
                {
                    var user2 = db.TBLADMIN.FirstOrDefault(u => u.EPOSTA == eposta && u.SIFRE == SIFRE);
                    userType = "ADMIN";
                    if (user2 == null)
                    {
                        // Kullanıcı bulunamadı
                        ViewBag.Message = "Kullanıcı adı veya şifre hatalı";
                        return View();
                    }
                }
            }
            else
            {
                TempData["OgrenciID"] = user.ID;
            }

            // Kullanıcıyı oturum açtır
            FormsAuthentication.SetAuthCookie(eposta, false);
            switch (userType)
            {
                case "OGRENCI":
                    return RedirectToAction("OgrenciAnasayfasi", "Ogrenci");
                case "OGRETMEN":
                    return RedirectToAction("OgretmenAnasayfasi", "Ogretmen");
                case "ADMIN":
                    return RedirectToAction("AdminAnasayfasi", "Admin");
                default:
                    return View();
            }
        }

        [AllowAnonymous]
        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Giris", "Giris");
        }


        [AllowAnonymous]
        public ActionResult SifreSifirla(string typeEmail)
        {
            if (string.IsNullOrEmpty(typeEmail))
            {
                return View("SifreSifirla");
            }

            string userEmail = typeEmail;

            var ogr = db.TBLOGRENCI.FirstOrDefault(u => u.EPOSTA == userEmail);
            var ort = db.TBLOGRETMEN.FirstOrDefault(u => u.EPOSTA == userEmail);

            if (ogr == null)
            {
                if (ort == null)
                {
                    // Kullanıcı bulunamadıysa hata mesajı gösterin
                    ViewBag.ErrorMessage = "Bu e-posta adresiyle ilişkili bir hesap bulunamadı.";
                    return View("SifreSifirla");
                }
                else
                {
                    // Kullanıcının şifresini içeren bir e-posta gönderin
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(ort.EPOSTA);
                    message.To.Add(userEmail);
                    message.Subject = "Şifreniz Yenilendi";
                    message.Body = "Merhaba " + ort.AD + ",\n\n" +
                    "Şifreniz : " + ort.SIFRE + "\n\n" +
                    "Lütfen bir dahaki sefere bu şifreyi kullanarak giriş yapın.\n\n" +
                    "Saygılarımızla,\n" +
                    "Şimşek Ekibi";
                    // iwyxahdgeypmesjt ilk kod

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    smtpClient.Credentials = new System.Net.NetworkCredential("itegrisu@gmail.com", "dlosqalcvwddokyq");
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(message);

                    // Başarılı bir şekilde tamamlandı mesajı gösterin
                    ViewBag.SuccessMessage = "Şifreniz başarıyla yenilendi. Lütfen e-posta adresinizi kontrol ediniz.";
                    return View("SifreSifirla");
                }
            }
            else
            {
                // Kullanıcının şifresini içeren bir e-posta gönderin
                MailMessage message = new MailMessage();
                message.From = new MailAddress(ogr.EPOSTA);
                message.To.Add(userEmail);
                message.Subject = "Mevcut Şifreniz ";
                message.Body = "Merhaba " + ogr.AD + ",\n\n" +
                    "Şifreniz : " + ogr.SIFRE + "\n\n" +
                    "Lütfen bir dahaki sefere bu şifreyi kullanarak giriş yapın.\n\n" +
                    "Saygılarımızla,\n" +
                    "Şimşek Ekibi";
                // iwyxahdgeypmesjt ilk kod

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new System.Net.NetworkCredential("itegrisu@gmail.com", "dlosqalcvwddokyq");
                smtpClient.EnableSsl = true;
                smtpClient.Send(message);

                // Başarılı bir şekilde tamamlandı mesajı gösterin
                ViewBag.SuccessMessage = "Şifreniz başarıyla yenilendi. Lütfen e-posta adresinizi kontrol edin.";
                return View("SifreSifirla");
            }






        }



    }
}
