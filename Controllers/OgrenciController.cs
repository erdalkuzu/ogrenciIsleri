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
    public class OgrenciController : Controller
    {
        // GET: Ogrenci
        public ActionResult List()
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                var model = db.Ogrencilers.Include(x => x.Bolumler).Include(x => x.Uyruklar).Include(x => x.Cinsiyetler).Include(x => x.Sınıflar).Include(x => x.Sehirler).ToList();
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                OgrenciVM ogrenci = new OgrenciVM()
                {
                    Bolumler = db.Bolumlers.ToList(),
                    Cinsiyetlers = db.Cinsiyetlers.ToList(),
                    Sehirlers = db.Sehirlers.ToList(),
                    Sınıflars = db.Sınıflar.ToList(),
                    Uyruklars = db.Uyruklars.ToList()
                };
                return View(ogrenci);
            }
        }
        [HttpPost]
        public ActionResult Create(OgrenciVM model, HttpPostedFileBase txtFoto)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                if(!ModelState.IsValid)
                {
                    OgrenciVM ogr = new OgrenciVM()
                    {
                        Bolumler = db.Bolumlers.ToList(),
                        Cinsiyetlers = db.Cinsiyetlers.ToList(),
                        Ogrenciler = model.Ogrenciler,
                        Sehirlers = db.Sehirlers.ToList(),
                        Sınıflars = db.Sınıflar.ToList(),
                        Uyruklars = db.Uyruklars.ToList(),
                    };
                    return View(ogr);
                }
                if (model.Ogrenciler.ID == 0)
                {
                    if (txtFoto != null)
                    {
                        WebImage img = new WebImage(txtFoto.InputStream);
                        FileInfo info = new FileInfo(txtFoto.FileName);
                        string newFoto = Guid.NewGuid().ToString() + info.Extension;
                        img.Resize(150, 350);
                        img.Save("~/OgrenciFoto/" + newFoto);
                        model.Ogrenciler.Fotograf = "/OgrenciFoto/" + newFoto;
                    }
                    db.Ogrencilers.Add(model.Ogrenciler);
                }
                else
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("List");
            }
        }
        public ActionResult Detail(int ID)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                var model = db.Ogrencilers.Include(x => x.Uyruklar).Include(x => x.Sehirler).Include(x => x.Sınıflar).Include(x => x.Bolumler).Include(x => x.Cinsiyetler).Where(x => x.ID == ID).SingleOrDefault();
                if (model == null)
                    return HttpNotFound();
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult Update(int ID)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                OgrenciVM ogrenci = new OgrenciVM()
                {
                    Bolumler = db.Bolumlers.ToList(),
                    Cinsiyetlers = db.Cinsiyetlers.ToList(),
                    Ogrenciler = db.Ogrencilers.Find(ID),
                    Sehirlers = db.Sehirlers.ToList(),
                    Sınıflars = db.Sınıflar.ToList(),
                    Uyruklars = db.Uyruklars.ToList()
                };
                if (ogrenci.Ogrenciler == null)
                    return HttpNotFound();
                return View(ogrenci);
            }
        }
        [HttpPost]
        public ActionResult Update(int ID, HttpPostedFileBase Foto, OgrenciVM model)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                if(!ModelState.IsValid)
                {
                    OgrenciVM ogr = new OgrenciVM()
                    {
                        Uyruklars = db.Uyruklars.ToList(),
                        Bolumler = db.Bolumlers.ToList(),
                        Cinsiyetlers = db.Cinsiyetlers.ToList(),
                        Ogrenciler = model.Ogrenciler,
                        Sehirlers = db.Sehirlers.ToList(),
                        Sınıflars = db.Sınıflar.ToList()
                    };
                    return View(ogr);
                }
                var ogrenci = db.Ogrencilers.Where(x => x.ID == ID).SingleOrDefault();
                if (Foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(ogrenci.Fotograf)))
                    {
                        System.IO.File.Delete(Server.MapPath(ogrenci.Fotograf));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo info = new FileInfo(Foto.FileName);
                    string newFoto = Guid.NewGuid().ToString() + info.Extension;
                    img.Resize(300, 150);
                    img.Save("~/OgrenciFoto/" + newFoto);
                    ogrenci.Fotograf = "/OgrenciFoto/" + newFoto;
                }
                ogrenci.Adsoyad = model.Ogrenciler.Adsoyad;
                ogrenci.Adres = model.Ogrenciler.Adres;
                ogrenci.CinsiyetID = model.Ogrenciler.CinsiyetID;
                ogrenci.BolumID = model.Ogrenciler.BolumID;
                ogrenci.DogumTarihi = model.Ogrenciler.DogumTarihi;
                ogrenci.Mail = model.Ogrenciler.Mail;
                ogrenci.Sınıf = model.Ogrenciler.Sınıf;
                ogrenci.TCNo = model.Ogrenciler.TCNo;
                ogrenci.TelNo = model.Ogrenciler.TelNo;
                ogrenci.UyrukID = model.Ogrenciler.UyrukID;
                ogrenci.DogumYeri = model.Ogrenciler.DogumYeri;
                db.SaveChanges();
                return RedirectToAction("List");
            }
        }
        public ActionResult Delete(int ID)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                var silinecek = db.Ogrencilers.Find(ID);
                if (silinecek == null)
                    return HttpNotFound();
                db.Ogrencilers.Remove(silinecek);
                db.SaveChanges();
                return RedirectToAction("List");
            }
        }
        // Daha ayarlanmadı böyle kaldı notları falan girince halledersin Erdem.
        public ActionResult Search(string Ara=null)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                var model = db.Ogrencilers.Include(x=>x.Cinsiyetler).Include(x=>x.Sehirler).Include(x=>x.Bolumler).Where(x => x.TCNo.Contains(Ara)).SingleOrDefault();
                if (model == null)
                    return HttpNotFound();
                return View(model);
            }
        }
    }
}