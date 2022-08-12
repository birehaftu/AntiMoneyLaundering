using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntiLaundering.Model;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace AntiLaundering.Control.AntiLaundering
{
    public class BlackMarketRateManagement
    {
        AntiLaunderingEntities CBE = new AntiLaunderingEntities();
        public Boolean BlackMarketRateExistsByBlackMarketRate(String ExchangeCode, DateTime date)
        {
            var rop = (from l in CBE.BlackMarketRates where l.ExchangeCode == ExchangeCode & l.RateDate==date select l).FirstOrDefault();
            if (rop != null)
                return true;
            else
            {
                return false;
            }
        }
        public Boolean BlackMarketRateExistsByExchangeNameUpdate(String ExchangeCode, DateTime date, Guid id)
        {
            var rop = (from l in CBE.BlackMarketRates where l.ExchangeCode == ExchangeCode & l.RateDate == date && l.BlackMarketRateID!=id select l).FirstOrDefault();
            if (rop != null)
                return true;
            else
            {
                return false;
            }
        }
        public Boolean InsertBlackMarketRate(Guid BlackMarketRateID, String ExchangeName, String ExchangeCode, String Country,
            DateTime RateDate, Decimal RateAmountInBirr, String RecorderBy, DateTime RecordedDate)
        {
            try
            {
                CBE = new AntiLaunderingEntities();
                BlackMarketRate com = new BlackMarketRate();
                com.BlackMarketRateID = BlackMarketRateID;
                com.ExchangeName = ExchangeName;
                com.ExchangeCode = ExchangeCode;
                com.Country = Country;
                com.RateDate = RateDate;
                com.RateAmountInBirr = RateAmountInBirr;
                com.RecorderBy = RecorderBy;
                com.RecordedDate = RecordedDate;
                CBE.BlackMarketRates.Add(com);
                int change = CBE.SaveChanges();
                if (change > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
                return false;
            }
        }
        public Boolean UpdateBlackMarketRate(Guid BlackMarketRateID, String ExchangeName, String ExchangeCode, String Country,
            DateTime RateDate, Decimal RateAmountInBirr, String RecorderBy, DateTime RecordedDate)
        {
            //EnrollPalmInfo com = new EnrollPalmInfo();
            var com = (from d in CBE.BlackMarketRates where d.BlackMarketRateID == BlackMarketRateID select d).FirstOrDefault();            
            com.ExchangeName = ExchangeName;
            com.ExchangeCode = ExchangeCode;
            com.Country = Country;
            com.RateDate = RateDate;
            com.RateAmountInBirr = RateAmountInBirr;
            com.RecorderBy = RecorderBy;
            com.RecordedDate = RecordedDate;
            int change = CBE.SaveChanges();
            if (change > 0)
            {
                return true;
            }
            else
                return false;
        }

        
        public Boolean DeleteBlackMarketRate(Guid Id)
        {
            try
            {
                var BookInfo = (from c in CBE.BlackMarketRates where c.BlackMarketRateID == Id select c).FirstOrDefault();
                CBE.BlackMarketRates.Remove(BookInfo);
                int change = CBE.SaveChanges();
                if (change > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Boolean DeleteBlackMarketRateByExchangeName(String ExchangeName)
        {
            try
            {
                var BookInfo = (from c in CBE.BlackMarketRates where c.ExchangeName == ExchangeName select c).FirstOrDefault();
                CBE.BlackMarketRates.Remove(BookInfo);
                int change = CBE.SaveChanges();
                if (change > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public BlackMarketRate GetBlackMarketRateExchangeName(string ExchangeName)
        {
            var comp_data = (from v in CBE.BlackMarketRates where v.ExchangeName == ExchangeName select v).FirstOrDefault();
            return comp_data;
        }

        public BlackMarketRate GetBlackMarketRateByID(Guid id)
        {
            var comp_data = (from v in CBE.BlackMarketRates where v.BlackMarketRateID == id select v).FirstOrDefault();
            return comp_data;
        }
        

        public List<BlackMarketRate> GetBlackMarketRate()
        {
            var comp_data = (from v in CBE.BlackMarketRates select v).ToList();
            return comp_data;
        }
       

    }
}