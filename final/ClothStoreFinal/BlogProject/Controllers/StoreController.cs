using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClothStore.Models;

namespace ClothStore.Controllers
{
    public class StoreController : Controller
    {
        private StoreDbContext db = new StoreDbContext();

        // GET: Store
        public ActionResult Index()
        {
            ViewBag.providers = db.Providers.ToList();
            return View(db.Clothes.OrderByDescending(c => c.AddedDate).ToList());
        }

        public ActionResult Management()
        {
            return View(db.Providers.ToList());
        }   

        // GET: Store/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provider provider = db.Providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Provider provider)
        {
            if (ModelState.IsValid)
            {
                db.Providers.Add(provider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index", "Management");
        }

        // GET: Store/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provider provider = db.Providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        // POST: Store/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Provider provider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(provider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index", "Management");
        }

        // GET: Store/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provider provider = db.Providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        // POST: Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Provider provider = db.Providers.Find(id);
            db.Providers.Remove(provider);
            db.SaveChanges();

            return View("Index", "Management");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Cloth

        public ActionResult Cloth(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Provider provider = db.Providers.Find(id);
            var clothes = provider.Clothes.ToList();
            if (clothes == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProviderId = id;
            ViewBag.ProviderName = provider.Name;

            return View("Cloth", clothes);
        }

        // POST: Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClothCreate([Bind(Include = "ID,ProviderID,Image,Description,Price,Quantity,Color,AddedDate")] Cloth cloth)
        {
            if (ModelState.IsValid)
            {
                Provider provider = db.Providers.Find(cloth.ProviderID);
                cloth.Provider = provider;
                cloth.AddedDate = DateTime.Now;

                db.Clothes.Add(cloth); 
                db.SaveChanges();
                return RedirectToAction("Cloth",new { id = cloth.ProviderID });
            }

            return View("Index", "Management");
        }

        public ActionResult DeleteCloth(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cloth cloth = db.Clothes.Find(id);
            if (cloth == null)
            {
                return HttpNotFound();
            }
            var providerId = cloth.ProviderID;
            db.Clothes.Remove(cloth);
            db.SaveChanges();

            return RedirectToAction("Cloth", new { id = providerId });
        }

        public ActionResult EditCloth(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cloth cloth = db.Clothes.Find(id);
            if (cloth == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProviderId = cloth.ProviderID;

            return View(cloth);
        }


        // POST: Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCloth([Bind(Include = "ID,ProviderID,Image,Description,Price,Quantity,Color,AddedDate")] Cloth cloth)
        {

            if (ModelState.IsValid)
            {
                db.Entry(cloth).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Cloth", new { id = cloth.ProviderID });
            }

            return View("Index", "Management");
        }


        public ActionResult FilterCloth(string Provider, string Color, int Price)
        {
            var cloth = from p in db.Clothes.Where(p => p.Price <= Price) select p;

            if (!string.IsNullOrEmpty(Provider) && Provider != "All")
            {
                cloth = cloth.Where(p => p.Provider.Name == Provider);
            }

            if (!string.IsNullOrEmpty(Color))
            {
                cloth = cloth.Where(p => p.Color == Color);
            }

            ViewBag.providers = db.Providers.ToList();
            return View("Index", cloth.OrderByDescending(c => c.AddedDate).ToList());
        }
    }
}
