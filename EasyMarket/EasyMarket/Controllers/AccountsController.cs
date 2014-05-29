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

namespace EasyMarket.Controllers
{
    public class AccountsController : Controller
    {
        Easy_MarketEntities db = new Easy_MarketEntities();
        //
        // GET: /Accounts/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration(string errorMessage = "")
        {
            ViewBag.error = errorMessage;
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {


                if (db.Users.FirstOrDefault(u => u.email == model.email) != null)
                {

                    return RedirectToAction("Registration", "Account", new { @errorMessage = "Введенный Email занят другим пользователем!" });
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
                    rememberme = false
                };

                db.Users.Add(user);
                db.SaveChanges();


                return RedirectToAction("Index", "Home", new { @modal = "true" });
            }
            return View(model);
        }
	}
}