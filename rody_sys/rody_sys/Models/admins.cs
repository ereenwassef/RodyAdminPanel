//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace rody_sys.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class admins
    {
        public admins()
        {
            this.adminOperations = new HashSet<adminOperations>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string pic { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Nullable<int> adType { get; set; }
        public Nullable<int> isActive { get; set; }
    
        public virtual ICollection<adminOperations> adminOperations { get; set; }
    }
}