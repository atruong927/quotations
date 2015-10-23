using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuotationsApp.Models;

namespace QuotationsApp.Controllers
{
    public class QuotationsController : Controller
    {
        private QuotationsAppContext db = new QuotationsAppContext();

        // GET: Quotations
        public ActionResult Index(string input)
        {
            var quotations = db.Quotations.Include(q => q.Category);
            //Search for Quote, Category, or Author from input
            if (!String.IsNullOrEmpty(input))
            {
                quotations = quotations.Where(q => q.Quote.Contains(input) || q.Category.Name.Contains(input) || q.Author.Contains(input));
            }
            //Display All Button
            
            return View(quotations.ToList());
        }

        // GET: Quotations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            return View(quotation);
        }

        // GET: Quotations/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View();
        }

        // POST: Quotations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuotationID,Author,Quote,DateAdded,CategoryID")] Quotation quotation, string CategoryName)
        {
            quotation.DateAdded = DateTime.Now;
            var categoryNames = from c in db.Categories
                              select c.Name;
            //Add New Category with CategoryName if it's not null and it's not in Categories already
            if (!String.IsNullOrEmpty(CategoryName) && !categoryNames.Contains(CategoryName))
            {
                quotation.CategoryID = 1;
                Category newCategory = new Category();
                newCategory.Name = CategoryName;
                quotation.CategoryID = db.Categories.Add(newCategory).CategoryID;
            }
            if (ModelState.IsValid)
            {
                db.Quotations.Add(quotation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", quotation.CategoryID);
            return View(quotation);
        }

        // GET: Quotations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", quotation.CategoryID);
            return View(quotation);
        }

        // POST: Quotations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuotationID,Author,Quote,DateAdded,CategoryID")] Quotation quotation, string CategoryName)
        {
            var categoryNames = from c in db.Categories
                                select c.Name;
            //Add New Category with CategoryName if it's not null and it's not in Categories already
            if (!String.IsNullOrEmpty(CategoryName) && !categoryNames.Contains(CategoryName))
            {
                Category newCategory = new Category();
                newCategory.Name = CategoryName;
                quotation.CategoryID = db.Categories.Add(newCategory).CategoryID;
            }
            if (ModelState.IsValid)
            {
                db.Entry(quotation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", quotation.CategoryID);
            return View(quotation);
        }

        // GET: Quotations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            return View(quotation);
        }

        // POST: Quotations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quotation quotation = db.Quotations.Find(id);
            db.Quotations.Remove(quotation);
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
