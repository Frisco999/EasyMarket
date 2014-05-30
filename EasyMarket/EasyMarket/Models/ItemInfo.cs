using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyMarket.Models;
using System.Web.Security;
using System.Web.UI;

namespace EasyMarket.Models
{
    public class ItemInfo
    {
        public Item Item { get; set; }
        public List<Image> Images { get; set; }
        public List<Size> Sizes { get; set; }
    }
}