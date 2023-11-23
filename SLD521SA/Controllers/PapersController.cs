using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SLD521SA.Models;

namespace SLD521SA.Controllers
{
    [Authorize(Roles = "Author")]
    public class PapersController : Controller
    {
        private readonly sld521_saEntities db = new sld521_saEntities();
        private static string reset = "";
        private static string message = "";

        // GET: Papers
        public ActionResult MyPapers()
        {
            var papers = db.Papers.Where(p => p.PaperAuthor == User.Identity.Name).OrderBy(p => p.PaperTitle);
            ViewBag.Message = message;
            message = reset;
            return View(papers);
        }

        // GET: Papers/Create
        public ActionResult Submit()
        {
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName");
            return View();
        }

        // POST: Papers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit([Bind(Include = "PaperId,PaperTitle,PaperAbstract,PaperAuthor,PaperDateSubmitted,TopicId")] Paper paper)
        {
            if (ModelState.IsValid)
            {
                db.Papers.Add(paper);
                try
                {
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    Console.Out.WriteLine(e);
                }
                message = "Paper " + paper.PaperId + " was submitted";
                return RedirectToAction("MyPapers");
            }

            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", paper.TopicId);
            return View(paper);
        }

        // GET: Papers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paper paper = db.Papers.Find(id);
            if (paper == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", paper.TopicId);
            return View(paper);
        }

        // POST: Papers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaperId,PaperTitle,PaperAbstract,PaperAuthor,PaperDateSubmitted,TopicId")] Paper paper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paper).State = EntityState.Modified;
               try
                {
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    Console.Out.WriteLine(e);
                }
                message = "Paper " + paper.PaperId + " updated successfully";
                return RedirectToAction("MyPapers");
            }
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", paper.TopicId);
            return View(paper);
        }

        // GET: Papers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paper paper = db.Papers.Find(id);
            if (paper == null)
            {
                return HttpNotFound();
            }
            
            return DeleteConfirmed(paper.PaperId);
        }

        // POST: Papers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paper paper = db.Papers.Find(id);
            db.Papers.Remove(paper);
            db.SaveChanges();
            message = "Paper " + paper.PaperId + " deleted successfully";
            return RedirectToAction("MyPapers");
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
