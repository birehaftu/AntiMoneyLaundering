using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntiLaundering.Model;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace AntiLaundering.Control.AntiLaundering
{
    public class FraudDetectedBlackCountManagement
    {
        AntiLaunderingEntities CBE = new AntiLaunderingEntities();
       
        public List<FraudDetectedBlackCount> GetFraudDetectedBlack()
        {
            var comp_data = (from v in CBE.FraudDetectedBlackCounts orderby v.TXN_ID select v).ToList();
            return comp_data;
        }
       

    }
}