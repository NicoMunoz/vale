using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;

namespace BlogProject.Controllers
{
    public class BlogFansController : Controller
    {
        private BlogFanDbContext db = new BlogFanDbContext();

        // GET: BlogFans
        public ActionResult Index()
        {
            return View(db.FansClub.ToList());
        }

        // GET: BlogFans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogFan blogFan = db.FansClub.Find(id);
            if (blogFan == null)
            {
                return HttpNotFound();
            }
            return View(blogFan);
        }

        // GET: BlogFans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogFans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Gender,BirthDate,Seniority")] BlogFan blogFan)
        {
            if (ModelState.IsValid)
            {
                db.FansClub.Add(blogFan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogFan);
        }

        // GET: BlogFans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogFan blogFan = db.FansClub.Find(id);
            if (blogFan == null)
            {
                return HttpNotFound();
            }
            return View(blogFan);
        }

        // POST: BlogFans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Gender,BirthDate,Seniority")] BlogFan blogFan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogFan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogFan);
        }

        // GET: BlogFans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogFan blogFan = db.FansClub.Find(id);
            if (blogFan == null)
            {
                return HttpNotFound();
            }
            return View(blogFan);
        }

        // POST: BlogFans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogFan blogFan = db.FansClub.Find(id);
            db.FansClub.Remove(blogFan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
