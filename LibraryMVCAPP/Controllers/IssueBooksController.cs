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
    public class IssueBooksController : Controller
    {
        private LibraryMVCEntities db = new LibraryMVCEntities();

        // GET: IssueBooks
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                return RedirectToAction("Login", "Home");
            }
            

            var issueBooks = db.IssueBooks.Include(i => i.EmployeeTable).Include(i => i.User).Include(i => i.Book);
            return View(issueBooks.ToList());
        }

        // GET: IssueBooks/Details/5
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
            IssueBook issueBook = db.IssueBooks.Find(id);
            if (issueBook == null)
            {
                return HttpNotFound();
            }
            return View(issueBook);
        }

        // GET: IssueBooks/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "FullName","0");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", "0");
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookTitle","0");
            return View();
        }

        // POST: IssueBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IssueBook issueBook)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            issueBook.UserID = userid;
            if (ModelState.IsValid)
            {
                var find = db.IssueBooks.Where(b => b.ReturnDate >= DateTime.Now && b.BookID == issueBook.BookID && (b.Status == true || b.ReservedNoOfcopies == true)).ToList();

                int issuebook = 0;

                foreach (var item in find)
                {
                    issuebook = issuebook + item.IssueCopies;

                }

                var stockbooks = db.Books.Where(b => b.BookID == issueBook.BookID).FirstOrDefault();

                if(issuebook ==stockbooks.TotalCopies)
                {
                    ViewBag.Message = "Stock is Empty";
                    return View(issueBook);
                }
                db.IssueBooks.Add(issueBook);
                db.SaveChanges();
                ViewBag.Message = "Book Issues Successfully";
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.EmployeeTables, "EmployeeID", "FullName", issueBook.EmployeeID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", issueBook.UserID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookTitle", issueBook.BookID);
            return View(issueBook);
        }

        // GET: IssueBooks/Edit/5
       

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
