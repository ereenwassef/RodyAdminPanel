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
    
    public partial class operations_client
    {
        public int id { get; set; }
        public Nullable<double> debtor { get; set; }
        public Nullable<double> creditor { get; set; }
        public string actual_date { get; set; }
        public string get_date { get; set; }
        public Nullable<int> clientId { get; set; }
        public Nullable<int> phone_id { get; set; }
        public Nullable<double> charge { get; set; }
        public string notes { get; set; }
        public Nullable<int> serviceId { get; set; }
        public string declaration { get; set; }
        public string time { get; set; }
        public string otherPhoneNum { get; set; }
        public Nullable<int> saleOpId { get; set; }
    
        public virtual sales sales { get; set; }
        public virtual services services { get; set; }
        public virtual phones phones { get; set; }
    }
}
