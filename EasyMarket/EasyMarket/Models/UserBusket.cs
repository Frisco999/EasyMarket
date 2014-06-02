using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyMarket.Models
{
    public class UserBusket
    {
        public Guid temporaryBusketId { get; set; }
        public List<BusketItem> temporaryBusketItems { get; set; }
    }
}