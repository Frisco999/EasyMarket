using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyMarket.Models;
using System.Web.Security;
using System.Web.UI;

namespace EasyMarket.Controllers
{
    public class HomeController : Controller
    {
        Easy_MarketEntities db = new Easy_MarketEntities();
        public ActionResult Index(string error = "", bool modal = false)
        {
            ViewBag.modal = modal;
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null) { ViewBag.autorized = true; }
            else
            {
                ViewBag.Autorized = false;
            }
            ViewBag.errMessage = error;
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Autorization(User user)
        {
            ViewBag.errMessage = "";
           
            if (user != null)
            {
                User currentUser = db.Users.FirstOrDefault(x => x.email == user.email && x.password == user.password);
                if (currentUser != null)
                {
                    FormsAuthentication.SetAuthCookie(currentUser.email, user.rememberme);
                    //HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                    //FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    return RedirectToAction("Index", "Home", new { @error = "Введите email и пароль" });
                }
            }  
            else
            {
                return RedirectToAction("Index", "Home", new { @error = "Введите email и пароль" });
            }        
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }
    }
}