//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AntiLaundering.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerTransact
    {
        public System.Guid CustomerTransactID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> CustomerAccount { get; set; }
        public Nullable<System.DateTime> BusinessDate { get; set; }
        public Nullable<System.DateTime> DebitedDateTime { get; set; }
        public Nullable<decimal> TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public string RecorderBy { get; set; }
        public Nullable<System.DateTime> RecordedDate { get; set; }
    }
}
