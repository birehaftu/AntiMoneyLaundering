using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntiLaundering.Model;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace AntiLaundering.Control.AntiLaundering
{
    public class CustomerTransactManagement
    {
        AntiLaunderingEntities CBE = new AntiLaunderingEntities();
        public Boolean CustomerTransactExistsByCustomerTransact(int customerAccount,Decimal TransactionAmount, DateTime date)
        {
            var rop = (from l in CBE.CustomerTransacts where l.CustomerAccount == customerAccount & l.TransactionAmount==TransactionAmount & l.DebitedDateTime==date select l).FirstOrDefault();
            if (rop != null)
                return true;
            else
            {
                return false;
            }
        }
        public Boolean InsertCustomerTransact(Guid CustomerTransactID, int CustomerID, int CustomerAccount, DateTime BusinessDate,
            DateTime DebitedDateTime, Decimal TransactionAmount, String TransactionType, String RecorderBy, DateTime RecordedDate)
        {
            try
            {
                CBE = new AntiLaunderingEntities();
                CustomerTransact com = new CustomerTransact();
                com.CustomerTransactID = CustomerTransactID;
                com.CustomerID = CustomerID;
                com.CustomerAccount = CustomerAccount;
                com.BusinessDate = BusinessDate;
                com.DebitedDateTime = DebitedDateTime;
                com.TransactionAmount = TransactionAmount;
                com.TransactionType = TransactionType;
                com.RecorderBy = RecorderBy;
                com.RecordedDate = RecordedDate;
                CBE.CustomerTransacts.Add(com);
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
        public Boolean UpdateCustomerTransact(Guid CustomerTransactID, int CustomerID, int CustomerAccount, DateTime BusinessDate,
            DateTime DebitedDateTime, Decimal TransactionAmount, String TransactionType, String RecorderBy, DateTime RecordedDate)
        {
            try
            {
                var com = (from d in CBE.CustomerTransacts where d.CustomerTransactID == CustomerTransactID select d).FirstOrDefault();

                com.CustomerID = CustomerID;
                com.CustomerAccount = CustomerAccount;
                com.BusinessDate = BusinessDate;
                com.DebitedDateTime = DebitedDateTime;
                com.TransactionAmount = TransactionAmount;
                com.TransactionType = TransactionType;
                com.RecorderBy = RecorderBy;
                com.RecordedDate = RecordedDate;
                CBE.CustomerTransacts.Add(com);
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

            public Boolean DeleteCustomerTransact(Guid Id)
        {
            try
            {
                var BookInfo = (from c in CBE.CustomerTransacts where c.CustomerTransactID == Id select c).FirstOrDefault();
                CBE.CustomerTransacts.Remove(BookInfo);
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
        public Boolean DeleteCustomerTransactByBusinessDate(DateTime BusinessDate)
        {
            try
            {
                var BookInfo = (from c in CBE.CustomerTransacts where c.BusinessDate == BusinessDate select c).FirstOrDefault();
                CBE.CustomerTransacts.Remove(BookInfo);
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


        public CustomerTransact GetCustomerTransactByID(Guid id)
        {
            var comp_data = (from v in CBE.CustomerTransacts where v.CustomerTransactID == id select v).FirstOrDefault();
            return comp_data;
        }
        

        public List<CustomerTransact> GetCustomerTransact()
        {
            var comp_data = (from v in CBE.CustomerTransacts orderby v.DebitedDateTime ascending select v).ToList();
            return comp_data;
        }
       

    }
}