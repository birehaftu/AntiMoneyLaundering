using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntiLaundering.Model;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace AntiLaundering.Control.AntiLaundering
{
    public class RowDataManagement
    {
        AntiLaunderingEntities CBE = new AntiLaunderingEntities();
        public Boolean RowDataExistsByRowData(int customerAccount,Decimal TransactionAmount, DateTime date)
        {
            var rop = (from l in CBE.RowDatas  select l).FirstOrDefault();
            if (rop != null)
                return true;
            else
            {
                return false;
            }
        }
        public Boolean InsertRowData(int DEBIT_CUSTOMER, int CREDIT_CUSTOMER, int DEBIT_ACCT_NO,int CREDIT_ACCT_NO,
            DateTime BusinessDate,string TRANSACTION_BY,string DEBIT_CURRENCY, decimal CREDIT_AMOUNT, decimal USD_EQUIV_BALANCE, int AGE, string GENDER,
            string REGIONNAME,string DISTRICTNAME )
        {
            try
            {
                CBE = new AntiLaunderingEntities();
                RowData com = new RowData();
                com.DEBIT_CUSTOMER = DEBIT_CUSTOMER;
                com.CREDIT_CUSTOMER = CREDIT_CUSTOMER;
                com.DEBIT_ACCT_NO = DEBIT_ACCT_NO;
                com.CREDIT_ACCT_NO = CREDIT_ACCT_NO;
                com.BUSINESS_DATE = BusinessDate;
                com.BUSINESS_DATE = BusinessDate;
                com.TRANSACTION_BY = TRANSACTION_BY;
                com.DEBIT_CURRENCY = DEBIT_CURRENCY;
                com.USD_EQUIV_BALANCE = USD_EQUIV_BALANCE;
                com.CREDIT_AMOUNT = CREDIT_AMOUNT;
                com.AGE = AGE;
                com.GENDER = GENDER;
                com.REGIONNAME = REGIONNAME;
                com.DISTRICTNAME = DISTRICTNAME;
                CBE.RowDatas.Add(com);
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
       
        

        public List<RowData> GetRowData()
        {
            var comp_data = (from v in CBE.RowDatas orderby v.TXN_ID select v).Take(2000000).ToList();
            return comp_data;
        }
       

    }
}