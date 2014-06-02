using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using EasyMarket.Models;
using EasyMarket.Classes;
using System.Web.Security;
using System.Web.UI;

namespace EasyMarket.Controllers
{
    public class AccountsController : Controller
    {
        Easy_MarketEntities db = new Easy_MarketEntities();
        //
        // GET: /Accounts/
        
        public ActionResult Registration(string errorMessage = "")
        {
            ViewBag.price = CurrentUser.GetCurrentUserBusketItemsPrice(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);
            if (CurrentUser.GetCurrentUserEmail(Request.Cookies[FormsAuthentication.FormsCookieName]) != null) { ViewBag.Autorized = true; }
            else
            {
                ViewBag.Autorized = false;
            }
            ViewBag.error = errorMessage;
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationModel model)
        {
            ViewBag.price = CurrentUser.GetCurrentUserBusketItemsPrice(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);
            if (CurrentUser.GetCurrentUserEmail(Request.Cookies[FormsAuthentication.FormsCookieName]) != null) { ViewBag.Autorized = true; }
            else
            {
                ViewBag.Autorized = false;
            }
            if (ModelState.IsValid)
            {              
                if (db.Users.FirstOrDefault(u => u.email == model.email) != null)
                {
                    return RedirectToAction("Registration", "Accounts", new { @errorMessage = "Введенный Email занят другим пользователем!" });
                }
                Guid userBusket;
                if(CurrentUser.GetCurrentUserBusket(Request.Cookies[FormsAuthentication.FormsCookieName]) != null)
                { 
                   userBusket =  new Guid(CurrentUser.GetCurrentUserBusket(Request.Cookies[FormsAuthentication.FormsCookieName]));
                }
                else
                {
                    userBusket = Guid.NewGuid();
                    Busket newBusket = new Busket{Id_Busket = userBusket};
                    db.Buskets.Add(newBusket);
                }
                User user = new User
                {
                    id_user = Guid.NewGuid(),
                    email = model.email,
                    name = model.name,
                    surname = model.surname,
                    password = model.password,
                    address = model.address,
                    phone = model.phone,
                    num_purchases = 0,
                    busket = userBusket,
                    rememberme = false
                };

                db.Users.Add(user);
                db.SaveChanges();


                return RedirectToAction("Index", "Home", new { @modal = "true" });
            }
            return View(model);
        }

        public ActionResult UserPage()
        {
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);
            return View();
        }

        public ActionResult LogOut()
        {
            Guid currentBusket = new Guid(CurrentUser.GetCurrentUserBusketItems(Request.Cookies[FormsAuthentication.FormsCookieName]));
            List<BusketItemsAsgmt> asgmt = new List<BusketItemsAsgmt>(db.BusketItemsAsgmts.Where(i => i.id_Busket == currentBusket));
            foreach (var item in asgmt)
            {
                db.BusketItemsAsgmts.Remove(item);
            }
            db.SaveChanges();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Autorization(User user)
        {
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);
            if (user != null)
            {                         
                User currentUser = db.Users.FirstOrDefault(x => x.email == user.email && x.password == user.password);
                if (CurrentUser.GetCurrentUserBusket(Request.Cookies[FormsAuthentication.FormsCookieName]) != null)
                {
                    if (currentUser != null)
                    {
                    Guid oldBusket = new Guid(CurrentUser.GetCurrentUserBusket(Request.Cookies[FormsAuthentication.FormsCookieName]));
                    User checkUser = db.Users.FirstOrDefault(i => i.busket == oldBusket);
                    if (checkUser == null)
                    {
                        Guid currentBusket = (Guid)currentUser.busket;
                        List<BusketItemsAsgmt> asgmt = new List<BusketItemsAsgmt>(db.BusketItemsAsgmts.Where(b => b.id_Busket == oldBusket));
                        foreach(var item in asgmt)
                        {
                            item.id_Busket = currentBusket;
                        }
                        Busket delBusket = db.Buskets.FirstOrDefault(i => i.Id_Busket == oldBusket);
                        db.Buskets.Remove(delBusket);
                     }
                    db.SaveChanges();
                    FormsAuthentication.SignOut();
                }
                }
                    FormsAuthentication.SetAuthCookie(currentUser.email, user.rememberme);
                    //HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                    //FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    string currentUserName = currentUser.name;
                    return Json(currentUserName);
                }
                else
                {
                    
                    return null;
                }
            
        }
	}
}