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
    
    public partial class BusketItemsAsgmt
    {
        public System.Guid id_Asgmt { get; set; }
        public System.Guid id_Item { get; set; }
        public System.Guid id_Busket { get; set; }
        public int number { get; set; }
    
        public virtual Busket Busket { get; set; }
        public virtual Item Item { get; set; }
    }
}
