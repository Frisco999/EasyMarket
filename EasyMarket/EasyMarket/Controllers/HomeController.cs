using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyMarket.Models;
using System.Web.Security;
using System.Web.UI;
using System.Data.Entity;
using EasyMarket.Classes;

namespace EasyMarket.Controllers
{
    public class HomeController : Controller
    {
        Easy_MarketEntities db = new Easy_MarketEntities();
        public ActionResult Index(string error = "", bool modal = false)
        {
            ViewBag.price = CurrentUser.GetCurrentUserBusketItemsPrice(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.name = CurrentUser.GetCurrentUserName(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.modal = modal;
            if (CurrentUser.GetCurrentUserEmail(Request.Cookies[FormsAuthentication.FormsCookieName]) != null) { ViewBag.Autorized = true; }
            else
            {
                ViewBag.Autorized = false;
            }
            ViewBag.errMessage = error;
            return View();
        }

        public ActionResult Goods()
        {
            ViewBag.name = CurrentUser.GetCurrentUserName(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.price = CurrentUser.GetCurrentUserBusketItemsPrice(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);
            if (CurrentUser.GetCurrentUserEmail(Request.Cookies[FormsAuthentication.FormsCookieName]) != null) { ViewBag.Autorized = true; }
            else
            {
                ViewBag.Autorized = false;
            }
            List<ItemInfo> itemsInfo = new List<ItemInfo>();
            List<Item> items = new List<Item>(db.Items);
            foreach (Item item in items)
            {
                List<Image> images = new List<Image>(db.Images.Where(i => i.item == item.id_item));
                List<Size> sizes = new List<Size>(db.Sizes.Where(s => s.item == item.id_item));
                itemsInfo.Add(new ItemInfo { Images = images, Sizes = sizes, Item = item });
            }

            return View(itemsInfo);

        }

       public ActionResult NewGoods()
        {
            ViewBag.name = CurrentUser.GetCurrentUserName(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.price = CurrentUser.GetCurrentUserBusketItemsPrice(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);
            List<ItemInfo> itemsInfo = new List<ItemInfo>();
            List<Item> items = new List<Item>(db.Items);
            items = items.Skip(items.Count()-3).ToList();
            foreach (Item item in items)
            {
                List<Image> images = new List<Image>(db.Images.Where(i => i.item == item.id_item));
                List<Size> sizes = new List<Size>(db.Sizes.Where(s => s.item == item.id_item));
                itemsInfo.Add(new ItemInfo { Images = images, Sizes = sizes, Item = item });
            }

            return View(itemsInfo);
        }

        public ActionResult GraphicalSearch()
        {
            ViewBag.name = CurrentUser.GetCurrentUserName(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.price = CurrentUser.GetCurrentUserBusketItemsPrice(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);


            return View();
        }

        public ActionResult Delivery()
        {
            ViewBag.name = CurrentUser.GetCurrentUserName(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.price = CurrentUser.GetCurrentUserBusketItemsPrice(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.name = CurrentUser.GetCurrentUserName(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.price = CurrentUser.GetCurrentUserBusketItemsPrice(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);
            

            return View();
        }

        
        public ActionResult Tshirts()
        {
            ViewBag.name = CurrentUser.GetCurrentUserName(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.price = CurrentUser.GetCurrentUserBusketItemsPrice(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);
            List<ItemInfo> itemsInfo = new List<ItemInfo>();
            List<Item> items = new List<Item>(db.Items.Where(m => m.type == "tshirt"));
            foreach (Item item in items)
            {
                List<Image> images = new List<Image>(db.Images.Where(i => i.item == item.id_item));
                List<Size> sizes = new List<Size>(db.Sizes.Where(s => s.item == item.id_item));
                itemsInfo.Add(new ItemInfo { Images = images, Sizes = sizes, Item = item });
            }

            return View(itemsInfo);
        }


        public ActionResult Singlets()
        {
            ViewBag.name = CurrentUser.GetCurrentUserName(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.price = CurrentUser.GetCurrentUserBusketItemsPrice(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);
            List<ItemInfo> itemsInfo = new List<ItemInfo>();
            List<Item> items = new List<Item>(db.Items.Where(m => m.type == "singlet"));
            foreach (Item item in items)
            {
                List<Image> images = new List<Image>(db.Images.Where(i => i.item == item.id_item));
                List<Size> sizes = new List<Size>(db.Sizes.Where(s => s.item == item.id_item));
                itemsInfo.Add(new ItemInfo { Images = images, Sizes = sizes, Item = item });
            }

            return View(itemsInfo);
        }

        public ActionResult Shirts()
        {
            ViewBag.name = CurrentUser.GetCurrentUserName(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.price = CurrentUser.GetCurrentUserBusketItemsPrice(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);
            List<ItemInfo> itemsInfo = new List<ItemInfo>();
            List<Item> items = new List<Item>(db.Items.Where(m => m.type == "shirt"));
            foreach (Item item in items)
            {
                List<Image> images = new List<Image>(db.Images.Where(i => i.item == item.id_item));
                List<Size> sizes = new List<Size>(db.Sizes.Where(s => s.item == item.id_item));
                itemsInfo.Add(new ItemInfo { Images = images, Sizes = sizes, Item = item });
            }

            return View(itemsInfo);

        }

        public ActionResult Jumpers()
        {
            ViewBag.name = CurrentUser.GetCurrentUserName(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.price = CurrentUser.GetCurrentUserBusketItemsPrice(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);
            List<ItemInfo> itemsInfo = new List<ItemInfo>();
            List<Item> items = new List<Item>(db.Items.Where(m => m.type == "cardigan"));
            foreach (Item item in items)
            {
                List<Image> images = new List<Image>(db.Images.Where(i => i.item == item.id_item));
                List<Size> sizes = new List<Size>(db.Sizes.Where(s => s.item == item.id_item));
                itemsInfo.Add(new ItemInfo { Images = images, Sizes = sizes, Item = item });
            }

            return View(itemsInfo);
        }

        public ActionResult Polos()
        {
            ViewBag.name = CurrentUser.GetCurrentUserName(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.price = CurrentUser.GetCurrentUserBusketItemsPrice(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);
            List<ItemInfo> itemsInfo = new List<ItemInfo>();
            List<Item> items = new List<Item>(db.Items.Where(m => m.type == "polo"));
            foreach (Item item in items)
            {
                List<Image> images = new List<Image>(db.Images.Where(i => i.item == item.id_item));
                List<Size> sizes = new List<Size>(db.Sizes.Where(s => s.item == item.id_item));
                itemsInfo.Add(new ItemInfo { Images = images, Sizes = sizes, Item = item });
            }

            return View(itemsInfo);
        }

            public ActionResult Hoodies()
        {
            ViewBag.name = CurrentUser.GetCurrentUserName(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.price = CurrentUser.GetCurrentUserBusketItemsPrice(Request.Cookies[FormsAuthentication.FormsCookieName]);
            ViewBag.number = CurrentUser.GetCurrentUserBusketItemsNumber(Request.Cookies[FormsAuthentication.FormsCookieName]);
            List<ItemInfo> itemsInfo = new List<ItemInfo>();
            List<Item> items = new List<Item>(db.Items.Where(m => m.type == "smock"));
            foreach (Item item in items)
            {
                List<Image> images = new List<Image>(db.Images.Where(i => i.item == item.id_item));
                List<Size> sizes = new List<Size>(db.Sizes.Where(s => s.item == item.id_item));
                itemsInfo.Add(new ItemInfo { Images = images, Sizes = sizes, Item = item });
            }

            return View(itemsInfo);
        }

        
    }
}