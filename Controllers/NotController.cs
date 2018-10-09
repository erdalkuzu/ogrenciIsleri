using OgrenciIsleri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OgrenciIsleri.ViewModel;

namespace OgrenciIsleri.Controllers
{
    [Authorize]
    public class NotController : Controller
    {
        // GET: Not
        public ActionResult Index()
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                var bıd = Convert.ToInt32(Session["BID"]);
                var model = db.Ogrencilers.Include(x => x.Bolumler).Include(x=>x.Sınıflar).Where(x => x.BolumID == bıd).ToList();
                if (model.Count == 0)
                    ViewBag.Ogryok = "Bu öğretmene dair hiç bir öğrenci yoktur.";
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult NotVer(int id)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                var ogrtID = Convert.ToInt32(Session["OgrID"]);
                NotVM note = new NotVM()
                {
                    Ogrenciler = db.Ogrencilers.Include(x => x.Bolumler).Where(x => x.ID == id).ToList(),
                    Ders = db.Derslers.Where(x => x.OgretmenID == ogrtID).ToList()
                };
                if (note.Ogrenciler == null)
                    return HttpNotFound();
                return View(note);
            }
        }
        [HttpPost]
        public ActionResult NotVer(int id, NotVM note)
        {

            using (OIDbEntities db = new OIDbEntities())
            {
                if (!ModelState.IsValid)
                {
                    var ogrtID = Convert.ToInt32(Session["OgrID"]);
                    NotVM not = new NotVM()
                    {
                        Ogrenciler = db.Ogrencilers.Include(x => x.Bolumler).Where(x => x.ID == id).ToList(),
                        Ders = db.Derslers.Where(x => x.OgretmenID == ogrtID).ToList()
                    };
                    return View(note);
                }
                var find = db.Ogrencilers.Find(id);
                var uid = Convert.ToInt32(Session["UserID"]);
                var user = db.Ogretmenlers.Find(uid);
                var ogrıd = Convert.ToInt32(Session["OgrID"]);
                db.Notlars.Add(note.Not);
                note.Not.OgretmenID = user.ID;
                note.Not.BolumID = user.BolumID;
                note.Not.OgrencıID = find.ID;
                db.SaveChanges();

                return RedirectToAction("Index", id);

            }
        }
    }
}