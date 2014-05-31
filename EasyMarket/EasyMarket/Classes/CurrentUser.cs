using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyMarket.Models;
using System.Web.Security;
using System.Web.UI;

namespace EasyMarket.Classes
{
    public static class CurrentUser
    {
          private static Easy_MarketEntities db = new Easy_MarketEntities();

        //public static bool IsAdmin(string userEmail)
        //{
        //    var dataContext = new iRecordSupportsEntities();
        //    var user = dataContext.Users.FirstOrDefault(u => u.Email == userEmail);
        //    if (user == null) return false;
        //    if (user.isSupportAdmin == true) return true;
        //    return false;
        //}
          public static string GetCurrentUserName(HttpCookie newCookie)
        {
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(newCookie.Value);
            var userName = db.Users.FirstOrDefault(model => model.email == ticket.Name).name;
            if (userName == null) return null;
            return userName;
        }

          //public static bool IsUserAutorized(HttpCookie newCookie)
          //{
          //    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(newCookie.Value);
          //    if(db.Users.FirstOrDefault(model => model.email == ticket.Name)
          //    return userName;
          //}

    }
}