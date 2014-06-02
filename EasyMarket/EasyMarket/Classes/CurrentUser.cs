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

              if (newCookie != null)
              {
                  if (newCookie.Value != "")
                  {
                      FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(newCookie.Value);
                      if (ticket.Name.Contains("@"))
                      {
                          var userName = db.Users.FirstOrDefault(model => model.email == ticket.Name).name;
                          if (userName == null) return null;
                          return userName;
                      }
                      else return null;
                  }
                  else return null;
              }
              else return null;
        }




          public static string GetCurrentUserEmail(HttpCookie newCookie)
          {
              if (newCookie != null)
                  if (newCookie.Value != "")
                    {   
                      FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(newCookie.Value);
                if (ticket.Name.Contains("@"))
                  {
                      var userEmail = db.Users.FirstOrDefault(model => model.email == ticket.Name).email;
                      if (userEmail == null) { return null; }
                      else { return userEmail; }
                  }
                return null;
                }
              else { return null; 
             }
          else return null;
         }

          public static string GetCurrentUserBusket(HttpCookie newCookie)
          {
              if (newCookie != null)
              {
                  if (newCookie.Value != "")
                  {
                      FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(newCookie.Value);
                      if (!ticket.Name.Contains("@"))
                      {
                          Guid ticketName = new Guid(ticket.Name);
                          if (db.Buskets.FirstOrDefault(model => model.Id_Busket == ticketName) == null) { return null; }
                          else { return db.Buskets.FirstOrDefault(model => model.Id_Busket == ticketName).Id_Busket.ToString(); }
                      }
                      return null;
                  }
                  else { return null; }
              }
              else { return null; }
         }

        

          public static string GetCurrentUserBusketItems(HttpCookie newCookie)
          {
              if (newCookie != null)
                  if (newCookie.Value != "")
                  {
                      FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(newCookie.Value);
                      if (ticket.Name.Contains("@"))
                      {
                          var busket = db.Users.FirstOrDefault(model => model.email == ticket.Name).busket;
                          if (busket == null) { return null; }
                          else { return busket.ToString(); }
                      }
                      if (!ticket.Name.Contains("@"))
                      {
                          var busket = ticket.Name;
                          if (busket == null) { return null; }
                          else { return busket.ToString(); }
                      }
                      return null;
                  }
                  else { return null; }
              else return null;
          }

          public static string GetCurrentUserBusketItemsNumber(HttpCookie newCookie)
          {
              int generalNumber = 0;
              string result = "";
              if (newCookie != null)
              {
                  if (newCookie.Value != "")
                  {
                      Guid busket = new Guid(CurrentUser.GetCurrentUserBusketItems(newCookie));
                      List<int> quality = new List<int>(db.BusketItemsAsgmts.Where(i => i.id_Busket == busket).Select(i => i.number));
                      foreach (var item in quality)
                      {
                          generalNumber += item;
                      }
                      string generalStringNumber = generalNumber.ToString();
                      string number = generalStringNumber.Substring(generalStringNumber.Length - 1);
                      if (generalStringNumber == "11")
                      {
                          result = generalStringNumber + "_покупок";
                          return result;
                      }
                      else
                      {
                          if (number == "1")
                          {
                              result = generalStringNumber + "_покупка";
                              return result;
                          }
                          else
                          {
                              if (number == "2" || number == "3" || number == "4")
                              {
                                  result = generalNumber + "_покупки";
                                  return result;
                              }
                              else
                              {
                                  result = generalNumber + "_покупок";
                                  return result;
                              }
                              
                          }
                         
                      }
                     
                  }
                  
              }
              result = generalNumber + "_покупок";
              return result;
              
          }

          public static string GetCurrentUserBusketItemsPrice(HttpCookie newCookie)
          {
              int generalPrice = 0;
              if (newCookie != null)
              {
                  if (newCookie.Value != "")
                  {
                      Guid busket = new Guid(CurrentUser.GetCurrentUserBusketItems(newCookie));
                      
                      List<int> quality = new List<int>(db.BusketItemsAsgmts.Where(i => i.id_Busket == busket).Select(i => i.number));
                      List<int> price = new List<int>(db.BusketItemsAsgmts.Where(i => i.id_Busket == busket).Select(i => i.Item.prise));
                      for (int i = 0; i < price.Count; i++)
                      {
                          generalPrice += quality[i] * price[i];
                      }

                      return generalPrice.ToString();
                  }
                  return generalPrice.ToString();
              }
              return generalPrice.ToString();
          }

          //public static bool IsUserAutorized(HttpCookie newCookie)
          //{
          //    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(newCookie.Value);
          //    if(db.Users.FirstOrDefault(model => model.email == ticket.Name)
          //    return userName;
          //}

    }
}