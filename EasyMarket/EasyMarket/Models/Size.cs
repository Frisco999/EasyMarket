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
    
    public partial class Size
    {
        public System.Guid item { get; set; }
        public string size1 { get; set; }
    
        public virtual Item Item1 { get; set; }
    }
}
