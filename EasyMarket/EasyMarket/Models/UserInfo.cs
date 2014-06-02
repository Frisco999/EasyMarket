using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyMarket.Models
{
    public class UserInfo
    {
        public User user { get; set; }
        public List<BusketItem> busketItems{ get; set;}
    }
}