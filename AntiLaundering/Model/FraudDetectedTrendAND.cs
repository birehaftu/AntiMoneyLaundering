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
    
    public partial class FraudDetectedTrendAND
    {
        public int TXN_ID { get; set; }
        public Nullable<int> TOTAL_TNX { get; set; }
        public Nullable<int> SUSPECTFRAUD { get; set; }
        public string DETECTED_BY { get; set; }
        public Nullable<int> DEBIT_CUSTOMER { get; set; }
        public Nullable<decimal> USD_EQUIV_BALANCE { get; set; }
        public Nullable<decimal> CREDIT_AMOUNT { get; set; }
        public Nullable<int> AGE { get; set; }
        public string GENDER { get; set; }
        public string REGIONNAME { get; set; }
        public string DISTRICTNAME { get; set; }
    }
}
