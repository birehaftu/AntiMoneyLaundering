using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntiLaundering.Model;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace AntiLaundering.Control.AntiLaundering
{
    public class FraudDetectedTrendManagement
    {
        AntiLaunderingEntities CBE = new AntiLaunderingEntities();
       
        public List<FraudDetectedTrend> GetFraudDetectedBlack()
        {
            var comp_data = (from v in CBE.FraudDetectedTrends orderby v.TXN_ID select v).ToList();
            return comp_data;
        }
       

    }
}