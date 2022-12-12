using LibraryMVCAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryMVCAPP.Controllers
{
    public class HomeController : Controller
    {
        private LibraryMVCEntities db = new LibraryMVCEntities();


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginUser(string Username, string password)
        {
            if (Username != null && password != null)
            {
                var finduser = db.Users.Where(u => u.Username == Username && u.Password == password && u.IsActive == true).ToList();

                if (finduser.Count() == 1)
                {


                    Session["UserID"] = finduser[0].UserID;
                    Session["UserTypeID"] = finduser[0].UserTypeID;
                    Session["Username"] = finduser[0].Username;
                    Session["Password"] = finduser[0].Password;
                    Session["EmployeeID"] = finduser[0].EmployeeID;




                    string url = string.Empty;
                    if (finduser[0].UserTypeID == 1)
                    {
                        return RedirectToAction("About");
                    }

                    else if (finduser[0].UserTypeID == 2)
                    {
                        return RedirectToAction("About");
                    }
                    else if (finduser[0].UserTypeID == 3)
                    {
                        return RedirectToAction("About");
                    }

                    else if (finduser[0].UserTypeID == 4)
                    {
                        return RedirectToAction("About");
                    }
                    else
                    {
                        url = "About";
                    }
                    return RedirectToAction(url);


                }

                else
                {
                    Session["UserID"] = string.Empty;
                    Session["UserTypeID"] = string.Empty;
                    Session["Username"] = string.Empty;
                    Session["Password"] = string.Empty;
                    Session["EmployeeID"] = string.Empty;

                    ViewBag.message = "Username and Password is incorrect";
                }


            }
            else
            {
                Session["UserID"] = string.Empty;
                Session["UserTypeID"] = string.Empty;
                Session["Username"] = string.Empty;
                Session["Password"] = string.Empty;
                Session["EmployeeID"] = string.Empty;
                ViewBag.message = "Some unexpected Issues occured please try again";
            }

            return View("Login");
        }
        
    public ActionResult Logout()
        {

            Session["UserID"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            Session["Username"] = string.Empty;
            Session["Password"] = string.Empty;
            Session["EmployeeID"] = string.Empty;

            return RedirectToAction("Login");


        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}