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
    public class DersController : Controller
    {
        // GET: Ders
        public ActionResult List()
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                var model = db.Derslers.Include(x => x.Bolumler).Include(x => x.Ogretmenler).ToList();
                return View(model);

            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                DersVM ders = new DersVM()
                {
                    Bolumler = db.Bolumlers.ToList(),
                    Ogretmenler = db.Ogretmenlers.ToList()
                };
                return View(ders);
            }
        }
        [HttpPost]
        public ActionResult Create(DersVM model)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                if(!ModelState.IsValid)
                {
                    DersVM ders = new DersVM()
                    {
                        Bolumler = db.Bolumlers.ToList(),
                        Dersler = model.Dersler,
                        Ogretmenler = db.Ogretmenlers.ToList()
                    };
                    return View(ders);
                }
                if (model.Dersler.ID == 0)
                    db.Derslers.Add(model.Dersler);
                else
                {
                    db.Entry(model.Dersler).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("List");
            }
        }
        public ActionResult Edit(int ID)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                var model = new DersVM()
                {
                    Bolumler = db.Bolumlers.ToList(),
                    Dersler = db.Derslers.Find(ID),
                    Ogretmenler = db.Ogretmenlers.ToList()
                };
                if (model.Dersler == null)
                    return HttpNotFound();
                return View("Create",model);
            }
        }
        public ActionResult Delete(int ID)
        {
            using (OIDbEntities db = new OIDbEntities())
            {
                var silinecek = db.Derslers.Find(ID);
                if (silinecek == null)
                    return HttpNotFound();
                db.Derslers.Remove(silinecek);
                db.SaveChanges();
                return RedirectToAction("List");
            }
        }
    }
}