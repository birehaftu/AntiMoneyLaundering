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
    
    public partial class UserLog
    {
        public System.Guid LodId { get; set; }
        public System.DateTime LogTime { get; set; }
        public string LogUser { get; set; }
        public string LogType { get; set; }
        public string LogDetail { get; set; }
    
        public virtual User User { get; set; }
    }
}
