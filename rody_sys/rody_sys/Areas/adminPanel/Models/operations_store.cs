//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace rody_sys.Areas.adminPanel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class operations_store
    {
        public int id { get; set; }
        public Nullable<int> storeId { get; set; }
        public Nullable<double> get_value { get; set; }
        public Nullable<int> clientId { get; set; }
        public string date { get; set; }
        public Nullable<int> supplierId { get; set; }
        public Nullable<double> give_value { get; set; }
    
        public virtual client client { get; set; }
        public virtual stores stores { get; set; }
        public virtual supplier supplier { get; set; }
    }
}
