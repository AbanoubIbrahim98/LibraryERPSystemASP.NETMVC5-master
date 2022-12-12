using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryMVCAPP.Models;

namespace LibraryMVCAPP.Controllers
{
    public class PurchaseTablesController : Controller
    {
        private LibraryMVCEntities db = new LibraryMVCEntities();

        // GET: PurchaseTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var purchaseTables = db.PurchaseTables.Include(p => p.Supplier).Include(p => p.User).Include(p => p.Book);
            return View(purchaseTables.ToList());
        }

        // GET: PurchaseTables/Details/5
        public ActionResult Details(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseTable purchaseTable = db.PurchaseTables.Find(id);
            if (purchaseTable == null)
            {
                return HttpNotFound();
            }
            return View(purchaseTable);
        }

        // GET: PurchaseTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookTitle");
            return View();
        }

        // POST: PurchaseTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PurchaseTable purchaseTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            purchaseTable.UserID = userid;
            if (ModelState.IsValid)
            {
                db.PurchaseTables.Add(purchaseTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", purchaseTable.SupplierID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", purchaseTable.UserID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookTitle", purchaseTable.BookID);
            return View(purchaseTable);
        }

        // GET: PurchaseTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseTable purchaseTable = db.PurchaseTables.Find(id);
            if (purchaseTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", purchaseTable.SupplierID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", purchaseTable.UserID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookTitle", purchaseTable.BookID);
            return View(purchaseTable);
        }

        // POST: PurchaseTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PurchaseTable purchaseTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            purchaseTable.UserID = userid;

            if (ModelState.IsValid)
            {
                db.Entry(purchaseTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", purchaseTable.SupplierID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", purchaseTable.UserID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookTitle", purchaseTable.BookID);
            return View(purchaseTable);
        }

        // GET: PurchaseTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseTable purchaseTable = db.PurchaseTables.Find(id);
            if (purchaseTable == null)
            {
                return HttpNotFound();
            }
            return View(purchaseTable);
        }

        // POST: PurchaseTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            PurchaseTable purchaseTable = db.PurchaseTables.Find(id);
            db.PurchaseTables.Remove(purchaseTable);
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
