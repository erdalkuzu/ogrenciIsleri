using OgrenciIsleri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OgrenciIsleri.ViewModel;
using System.Web.Helpers;
using System.IO;

namespace OgrenciIsleri.Controllers
{
    [Authorize]
    public class OgretmenlerController : Controller
    {
        // GET: Ogretmenler
        public ActionResult List()
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                var model = db.Ogretmenlers.Include(x => x.Bolumler).Include(x => x.Cinsiyetler).Include(x => x.Sehirler).ToList();
                return View(model);

            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                OgretmenVM ogretmen = new OgretmenVM()
                {
                    Bolumlers = db.Bolumlers.ToList(),
                    Cinsiyetlers = db.Cinsiyetlers.ToList(),
                    Sehirlers = db.Sehirlers.ToList()
                };
                return View(ogretmen);
            }
        }
        [HttpPost]
        public ActionResult Create(OgretmenVM model, HttpPostedFileBase Foto)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                if(!ModelState.IsValid)
                {
                    OgretmenVM ogrt = new OgretmenVM()
                    {
                        Bolumlers = db.Bolumlers.ToList(),
                        Cinsiyetlers = db.Cinsiyetlers.ToList(),
                        Ogretmenlers = model.Ogretmenlers,
                        Sehirlers = db.Sehirlers.ToList()
                    };
                    return View(ogrt);
                }
                if (Foto != null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo info = new FileInfo(Foto.FileName);
                    string newFoto = Guid.NewGuid().ToString() + info.Extension;
                    img.Resize(150, 300);
                    img.Save("~/OgretmenFoto/" + newFoto);
                    model.Ogretmenlers.Fotograf = "/OgretmenFoto/" + newFoto;
                    db.Ogretmenlers.Add(model.Ogretmenlers);
                }
                db.SaveChanges();
                return RedirectToAction("List");

            }
        }
        public ActionResult Delete(int ID)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                var model = db.Ogretmenlers.Find(ID);
                if (model == null)
                    return HttpNotFound();
                db.Ogretmenlers.Remove(model);
                db.SaveChanges();
                return RedirectToAction("List");

            }
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                OgretmenVM ogretmen = new OgretmenVM()
                {
                    Bolumlers = db.Bolumlers.ToList(),
                    Cinsiyetlers = db.Cinsiyetlers.ToList(),
                    Ogretmenlers = db.Ogretmenlers.Find(ID),
                    Sehirlers = db.Sehirlers.ToList(),
                };
                if (ogretmen.Ogretmenlers == null)
                    return HttpNotFound();
                return View(ogretmen);
            }
        }
        [HttpPost]
        public ActionResult Edit(int ID, HttpPostedFileBase Foto, OgretmenVM model)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                if(!ModelState.IsValid)
                {
                    OgretmenVM ogr = new OgretmenVM()
                    {
                        Bolumlers = db.Bolumlers.ToList(),
                        Cinsiyetlers = db.Cinsiyetlers.ToList(),
                        Ogretmenlers = model.Ogretmenlers,
                        Sehirlers = db.Sehirlers.ToList()
                    };
                    return View(ogr);
                }
                var ogretmen = db.Ogretmenlers.Where(x => x.ID == ID).SingleOrDefault();
                if (ogretmen == null)
                    return HttpNotFound();
                if(Foto!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath(ogretmen.Fotograf)))
                    {
                        System.IO.File.Delete(Server.MapPath(ogretmen.Fotograf));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo info = new FileInfo(Foto.FileName);
                    string newFoto = Guid.NewGuid().ToString() + info.Extension;
                    img.Resize(150, 300);
                    img.Save("~/OgretmenFoto/" + newFoto);
                    ogretmen.Fotograf = "/OgretmenFoto/" + newFoto;
                }
                ogretmen.Adres = model.Ogretmenlers.Adres;
                ogretmen.Adsoyad = model.Ogretmenlers.Adsoyad;
                ogretmen.BolumID = model.Ogretmenlers.BolumID;
                ogretmen.CinsiyetID = model.Ogretmenlers.CinsiyetID;
                ogretmen.DogumTarihi = model.Ogretmenlers.DogumTarihi;
                ogretmen.DogumYeri = model.Ogretmenlers.DogumYeri;
                ogretmen.Mail = model.Ogretmenlers.Mail;
                ogretmen.Tel = model.Ogretmenlers.Tel;
                db.SaveChanges();
                return RedirectToAction("List");
            }
        }
        public ActionResult Detail(int ID)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                var model =db.Ogretmenlers.Include(x=>x.Bolumler).Include(x=>x.Cinsiyetler).Include(x=>x.Sehirler).Where(x=>x.ID==ID).SingleOrDefault();
                if (model == null)
                    return HttpNotFound();
                return View(model);
            }
        }
    }
}