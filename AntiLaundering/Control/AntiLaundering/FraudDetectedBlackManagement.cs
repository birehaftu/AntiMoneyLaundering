using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntiLaundering.Model;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace AntiLaundering.Control.AntiLaundering
{
    public class FraudDetectedBlackManagement
    {
        AntiLaunderingEntities CBE = new AntiLaunderingEntities();
        public Boolean FraudDetectedBlackExistsByFraudDetectedBlack(int customerAccount,Decimal TransactionAmount, DateTime date)
        {
            var rop = (from l in CBE.FraudDetectedBlacks  select l).FirstOrDefault();
            if (rop != null)
                return true;
            else
            {
                return false;
            }
        }

        public List<FraudDetectedBlack> GetFraudDetectedBlack()
        {
            var comp_data = (from v in CBE.FraudDetectedBlacks orderby v.TXN_ID select v).Take(2000000).ToList();
            return comp_data;
        }
       

    }
}