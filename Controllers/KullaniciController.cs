using OgrenciIsleri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OgrenciIsleri.ViewModel;
using System.Web.Security;

namespace OgrenciIsleri.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Kullanıcı
        [Authorize]
        public ActionResult List()
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                var model = db.Kullanıcılar.Include(x => x.Ogretmenler).ToList();
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                KullanıcıVM user = new KullanıcıVM()
                {
                    Ogretmenlers = db.Ogretmenlers.ToList()
                };
                return View(user);

            }
        }
        [HttpPost]
        public ActionResult Create(KullanıcıVM model)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                if(!ModelState.IsValid)
                {
                    KullanıcıVM user = new KullanıcıVM()
                    {
                        Kullanıcılar = model.Kullanıcılar,
                        Ogretmenlers = db.Ogretmenlers.ToList()
                    };
                    return View(user);
                }

                db.Kullanıcılar.Add(model.Kullanıcılar);
                db.SaveChanges();
                return RedirectToAction("List");

            }
        }
        public ActionResult SignOut()
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login");

            }
        }
     
        [HttpGet]
        public ActionResult Login()
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(Kullanıcılar model)
         {
            using (OIDbEntities db = new OIDbEntities())
            {
                var sonuc = db.Kullanıcılar.Where(x => x.KullanıcıAdi == model.KullanıcıAdi && x.Sifre == model.Sifre).SingleOrDefault();
                if(sonuc!=null)
                {
                    FormsAuthentication.SetAuthCookie(model.KullanıcıAdi, true);
                    Session["OgrID"] = sonuc.OgretmenID;
                    Session["UserID"] = sonuc.ID;
                    Session["BID"] = sonuc.Ogretmenler.BolumID;
                    return RedirectToAction("List", "Ogrenci");
                }
                else
                {
                    return View(model);
                }
            }
        }

    }
}



