//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyMarket.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public Item()
        {
            this.Buskets = new HashSet<Busket>();
            this.Images = new HashSet<Image>();
        }
    
        public int id_item { get; set; }
        public string name { get; set; }
        public Nullable<decimal> prise { get; set; }
        public string type { get; set; }
        public string color { get; set; }
    
        public virtual ICollection<Busket> Buskets { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual Size Size { get; set; }
    }
}
