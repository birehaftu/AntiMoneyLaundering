using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntiLaundering.Model;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace AntiLaundering.Control.AntiLaundering
{
    public class FraudDetectedBlackCountFilteredManagement
    {
        AntiLaunderingEntities CBE = new AntiLaunderingEntities();
       
        public List<FraudDetectedBlackCountSepAND> GetFraudDetectedBlack()
        {
            var comp_data = (from v in CBE.FraudDetectedBlackCountSepANDs orderby v.TXN_ID select v).ToList();
            return comp_data;
        }
       

    }
}