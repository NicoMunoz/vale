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
    public class BlogController : Controller
    {
        private BlogDbContext db = new BlogDbContext();

        // GET: Management
        public ActionResult Management()
        {
            return View(db.Posts.ToList());
        }

        // GET: Blog
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,AuthorName,AuthorWebSite,PublishDate,Content,Image,Video")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.PublishDate = DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Management");
            }

            return View(post);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,AuthorName,AuthorWebSite,PublishDate,Content,Image,Video")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Management");
            }
            return View(post);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Management");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Comments

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "ID,PostID,Title,AuthorName,AuthorWebSite,content")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PostID = new SelectList(db.Posts, "ID", "Title", comment.PostID);
            return View(comment);
        }

        public ActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            var postID = comment.PostID;
            if (comment == null)
            {
                return HttpNotFound();
            }
            db.Comments.Remove(comment);
            db.SaveChanges();

            var comments = db.Comments.Where(c => c.PostID == postID).ToList();
            return View("Comments", comments);
        }

        public ActionResult Comments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comments = db.Comments.Where(c => c.PostID == id).ToList();
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View("Comments", comments);
        }

        public ActionResult SearchPost(String Date, String PostedBy, String WebSite, String CommentContent, int MinComments)
        {
            var posts = from p in db.Posts where p.Comments.Count >= MinComments select p;

            DateTime postDate;
            if (DateTime.TryParse(Date, out postDate))
            {
                posts = posts.Where(p => p.PublishDate.Day == postDate.Day && p.PublishDate.Month == postDate.Month && p.PublishDate.Year == postDate.Year);
            }

            if (!String.IsNullOrEmpty(PostedBy))
            {
                posts = posts.Where(p => p.AuthorName == PostedBy);
            }

            if (!String.IsNullOrEmpty(WebSite))
            {
                posts = posts.Where(p => p.AuthorWebSite == WebSite);
            }

            if (!String.IsNullOrEmpty(CommentContent))
            {
                posts = posts.Where(p => p.Comments.Where(c => c.content.Contains(CommentContent)).Count() > 0);
            }
           
            return View("Index", posts.ToList());
        }
    }
}
