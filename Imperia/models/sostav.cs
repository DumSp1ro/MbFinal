//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Imperia.models
{
    using System;
    using System.Collections.Generic;
    
    public partial class sostav
    {
        public int id { get; set; }
        public int id_orders { get; set; }
        public int id_merch { get; set; }
        public Nullable<int> count { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<int> total_cost { get; set; }
        public Nullable<int> discount { get; set; }
    
        public virtual orders orders { get; set; }
        public virtual merch merch { get; set; }
    }
}
