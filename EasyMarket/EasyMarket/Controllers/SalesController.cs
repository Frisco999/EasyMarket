using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyMarket.Models;
using EasyMarket.Classes;
using System.Web.Security;
using System.Web.UI;

namespace EasyMarket.Controllers
{
    public class SalesController : Controller
    {
        Easy_MarketEntities db = new Easy_MarketEntities();
        //
        // GET: /Sales/
        public ActionResult Busket()
        {
            ViewBag.price = CurrentUser.GetCurrentUserBusketItemsPrice(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);
            List<Guid> itemsId;

                    ViewBag.name = CurrentUser.GetCurrentUserName(Request.Cookies[FormsAuthentication.FormsCookieName]);
                    string currentBusketId = CurrentUser.GetCurrentUserBusket(Request.Cookies[FormsAuthentication.FormsCookieName]);
                    string currentUserEmail = CurrentUser.GetCurrentUserEmail(Request.Cookies[FormsAuthentication.FormsCookieName]);
                    if (currentUserEmail == null)
                    {
                        if (currentBusketId != null)
                        {
                            UserBusket newTemporaryUserBusket = new UserBusket();
                            Guid temporaryUserBusketId = new Guid(currentBusketId);
                            itemsId = new List<Guid>(db.BusketItemsAsgmts.Where(i => i.id_Busket == temporaryUserBusketId).Select(i => i.id_Item));
                            List<BusketItem> busketItem = new List<BusketItem>();
                            foreach (Guid id in itemsId)
                            {
                                Item itemsBusket = db.Items.Where(i => i.id_item == id).FirstOrDefault();
                                List<Image> images = new List<Image>(db.Images.Where(i => i.item == id));
                                List<Size> sizes = new List<Size>(db.Sizes.Where(s => s.item == id));
                                ItemInfo itemInfoBusket = new ItemInfo { Images = images, Sizes = sizes, Item = itemsBusket };
                                int number = db.BusketItemsAsgmts.Where(i => i.id_Busket == temporaryUserBusketId && i.id_Item == id).Select(i => i.number).FirstOrDefault();
                                busketItem.Add(new BusketItem { iteminfo = itemInfoBusket, number = number });
                            }
                            newTemporaryUserBusket.temporaryBusketId = temporaryUserBusketId;
                            newTemporaryUserBusket.temporaryBusketItems = busketItem;
                            return View(newTemporaryUserBusket);
                        }
                        else
                        {
                            return View();
                        }
                    }
                    else
                    {
                            
                            UserBusket viewBusket = new UserBusket();
                            User user = db.Users.FirstOrDefault(u => u.email == currentUserEmail);
                            itemsId = new List<Guid>(db.BusketItemsAsgmts.Where(i => i.id_Busket == user.busket).Select(i => i.id_Item));
                            List<BusketItem> busketItem = new List<BusketItem>();

                            foreach (Guid id in itemsId)
                            {
                                Item itemsBusket = db.Items.Where(i => i.id_item == id).FirstOrDefault();
                                List<Image> images = new List<Image>(db.Images.Where(i => i.item == id));
                                List<Size> sizes = new List<Size>(db.Sizes.Where(s => s.item == id));
                                ItemInfo itemInfoBusket = new ItemInfo { Images = images, Sizes = sizes, Item = itemsBusket };
                                int number = db.BusketItemsAsgmts.Where(i => i.id_Busket == user.busket && i.id_Item == id).Select(i => i.number).FirstOrDefault();
                                busketItem.Add(new BusketItem { iteminfo = itemInfoBusket, number = number });
                            }
                            viewBusket.temporaryBusketId = (Guid)user.busket;
                            viewBusket.temporaryBusketItems = busketItem;
                            return View(viewBusket);
                            
                    }

        }

        
        public ActionResult addBusket(Guid itemId)
        {
            string errorMessage = "Ошибка!";
            Guid temporaryBusketId;
            Guid temporaryItemId = itemId;
            if (Request.Cookies[FormsAuthentication.FormsCookieName] == null)
            {
                temporaryBusketId = Guid.NewGuid();
                Busket newBusket = new Busket { Id_Busket = temporaryBusketId };
                db.Buskets.Add(newBusket);
                string newUserBusketId = newBusket.Id_Busket.ToString();
                FormsAuthentication.SetAuthCookie(newUserBusketId, false);
                BusketItemsAsgmt newAsgmt = new BusketItemsAsgmt { id_Asgmt = Guid.NewGuid(), id_Busket = temporaryBusketId, id_Item = temporaryItemId, number = 1 };
                db.BusketItemsAsgmts.Add(newAsgmt);
                db.SaveChanges();
                return null;
            }
             if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    string result = "ok";
                    string currentBusketId = CurrentUser.GetCurrentUserBusket(Request.Cookies[FormsAuthentication.FormsCookieName]);
                    string currentUserEmail = CurrentUser.GetCurrentUserEmail(Request.Cookies[FormsAuthentication.FormsCookieName]);
                    if (currentUserEmail == null)
                    {
                        if (currentBusketId != null)
                        {
                            temporaryBusketId = new Guid(currentBusketId);
                            List<BusketItemsAsgmt> checkItems = new List<BusketItemsAsgmt>(db.BusketItemsAsgmts.Where(b => b.id_Busket == temporaryBusketId));
                            foreach (var item in checkItems)
                            {
                                if (item.id_Item == itemId)
                                {
                                    item.number += 1;
                                    db.SaveChanges();
                                    return Json(result);
                                }
                            }
                            BusketItemsAsgmt newAsgmt = new BusketItemsAsgmt { id_Asgmt = Guid.NewGuid(), id_Busket = temporaryBusketId, id_Item = temporaryItemId, number = 1 };
                            db.BusketItemsAsgmts.Add(newAsgmt);
                            db.SaveChanges();
                            return Json(result);
                        }
                    }
                    else
                    {
                        if (currentUserEmail != null)
                        {
                            temporaryBusketId = (Guid)db.Users.Where(u => u.email == currentUserEmail).FirstOrDefault().busket;
                            List<BusketItemsAsgmt> checkItems = new List<BusketItemsAsgmt>(db.BusketItemsAsgmts.Where(b => b.id_Busket == temporaryBusketId));
                            foreach (var item in checkItems)
                            {
                                if (item.id_Item == itemId)
                                {
                                    item.number += 1;
                                    db.SaveChanges();
                                    return Json(result);
                                }
                            }
                            BusketItemsAsgmt newAsgmt = new BusketItemsAsgmt { id_Asgmt = Guid.NewGuid(), id_Busket = temporaryBusketId, id_Item = temporaryItemId, number = 1 };
                            db.BusketItemsAsgmts.Add(newAsgmt);
                            db.SaveChanges();
                            return Json(result);
                        }
                    }
                }
             return Json(errorMessage);
            }
	}
}