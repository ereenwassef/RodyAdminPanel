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
    
    public partial class agent
    {
        public agent()
        {
            this.delegator = new HashSet<delegator>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public Nullable<int> areaId { get; set; }
    
        public virtual area area { get; set; }
        public virtual ICollection<delegator> delegator { get; set; }
    }
}
