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
        public ActionResult Index()
        {
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null) { ViewBag.autorized = true; }
            else
            {
                ViewBag.Autorized = false;
            }
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
           
            if (user != null)
            {
                User currentUser = db.Users.FirstOrDefault(x => x.email == user.email && x.password == user.password);
                if (currentUser != null)
                {
                    FormsAuthentication.SetAuthCookie(currentUser.email, user.rememberme);
                    //HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                    //FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    
                }  
                        return RedirectToAction("Index", "Home");
                 
        }
            



    
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