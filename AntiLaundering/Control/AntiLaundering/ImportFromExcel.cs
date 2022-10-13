using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Collections;
using System.Configuration;
using ExcelApp = Microsoft.Office.Interop.Excel;
namespace AntiLaundering.Control.AntiLaundering
{
    public class ImportFromExcel

    {

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public string ImportCustomerTransactFromExcel(String fileName, String user)
        {
            ExcelApp.Application excelApp = new ExcelApp.Application();
            String aYear = "";
            int count = 0;
            Boolean success = true;
            //DEADAL objDEADAL = new DEADAL();
            //deaBO objdeaBO;
            string ExistedTransactions = "";
            string NullTransactions = "";
            int TransactionExisted = 0;
            string errorMessages = null;
            try
            {

                //Create COM Objects.
                if (excelApp == null)
                {
                    return "Excel is not installed!!";
                }

                //Notice: Change this path to your real excel file path
                ExcelApp.Workbook excelBook = excelApp.Workbooks.Open(fileName);
                for (int s = 1; s > 11; s++)//10 sheets
                {
                    ExcelApp._Worksheet excelSheet = excelBook.Sheets[s];
                    ExcelApp.Range excelRange = excelSheet.UsedRange;

                    int rows = excelRange.Rows.Count;
                    int cols = excelRange.Columns.Count;
                    int countexisted = 1;
                    DateTime RecordedDate = DateTime.Now;
                    //first row using for heading, start second row for data
                    for (int i = 2; i <= rows; i++)
                    {
                        int CustomerID = 0;
                        int.TryParse(excelRange.Cells[i, 1].Value2.ToString(), out CustomerID);
                        int CustomerAccount = 0;
                        int.TryParse(excelRange.Cells[i, 2].Value2.ToString(), out CustomerAccount);
                        object value = excelRange.Cells[i, 3].Value2;
                        DateTime dt = RecordedDate;
                        if (value != null)
                        {
                            if (value is double)
                            {
                                dt = DateTime.FromOADate((double)value);
                            }
                            else
                            {
                                DateTime.TryParse((string)value, out dt);
                            }
                        }
                        DateTime BusinessDate = dt;
                        value = excelRange.Cells[i, 4].Value2;
                        dt = RecordedDate;
                        if (value != null)
                        {
                            if (value is double)
                            {
                                dt = DateTime.FromOADate((double)value);
                            }
                            else
                            {
                                DateTime.TryParse((string)value, out dt);
                            }
                        }
                        DateTime DebitedDateTime = dt;
                        Decimal TransactionAmount = 0;
                        Decimal.TryParse(excelRange.Cells[i, 5].Value2.ToString(), out TransactionAmount);
                        String TransactionType = excelRange.Cells[i, 6].Value2.ToString();
                        //int 
                        if (CustomerID != 0 && CustomerAccount != 0 && BusinessDate != RecordedDate
                            && DebitedDateTime != RecordedDate && TransactionAmount != 0
                             && !String.IsNullOrEmpty(TransactionType))
                        {
                            CustomerTransactManagement transact = new CustomerTransactManagement();
                            if (!transact.CustomerTransactExistsByCustomerTransact(CustomerAccount, TransactionAmount, DebitedDateTime))
                            {
                                if (transact.InsertCustomerTransact(Guid.NewGuid(), CustomerID, CustomerAccount, BusinessDate, DebitedDateTime, TransactionAmount
                                    , TransactionType, user, RecordedDate))
                                {
                                    count++;
                                }
                                else
                                {
                                    errorMessages += "Registration Failed at line " + countexisted + ". Please contact your system administrator";
                                }
                            }
                            else
                            {
                                TransactionExisted++;
                                ExistedTransactions += countexisted + ", ";
                            }
                        }
                        else
                        {
                            errorMessages += "All Fields should be filled. Some fields are empty in the excel in line " + countexisted + ".";
                        }
                        countexisted++;
                    }
                }
            }
            catch (Exception ex)
            {
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                if (count > 0)
                    return count + " registed " + ex.Message;
                else
                    return "The Excel file doesnot have correct data.";

            }
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            if (!String.IsNullOrEmpty(ExistedTransactions))
                ExistedTransactions = "Lines " + ExistedTransactions + " Already exists.";
            if (count > 0)
            {
                if (TransactionExisted > 0)
                    return count + " Customer Transactions Registered succefully." + errorMessages + ";" + TransactionExisted + " Record/s " + ExistedTransactions;
                else
                    return count + " Customer Transactions Registered succefully." + errorMessages + ";";

            }
            else
            {
                if (TransactionExisted > 0)
                    return errorMessages + TransactionExisted + " Record/s- " + ExistedTransactions;
                else
                    return errorMessages + ExistedTransactions;
            }

        }

        public string ImportCustomerRowDataFromExcel(String fileName)
        {
            ExcelApp.Application excelApp = new ExcelApp.Application();
            String aYear = "";
            int count = 0;
            Boolean success = true;
            //DEADAL objDEADAL = new DEADAL();
            //deaBO objdeaBO;
            string ExistedTransactions = "";
            string NullTransactions = "";
            int TransactionExisted = 0;
            string errorMessages = null;
            try
            {

                //Create COM Objects.
                if (excelApp == null)
                {
                    return "Excel is not installed!!";
                }

                //Notice: Change this path to your real excel file path
                ExcelApp.Workbook excelBook = excelApp.Workbooks.Open(fileName);
                for (int s = 1; s < 11; s++)//10 sheets
                {
                    ExcelApp._Worksheet excelSheet = excelBook.Sheets[s];
                    ExcelApp.Range excelRange = excelSheet.UsedRange;

                    int rows = excelRange.Rows.Count;
                    int cols = excelRange.Columns.Count;
                    int countexisted = 1;
                    DateTime RecordedDate = DateTime.Now;
                    //first row using for heading, start second row for data
                    for (int i = 2; i <= rows; i++)
                    {
                        int DEBIT_CUSTOMER = 0;
                        int.TryParse(excelRange.Cells[i, 2].Value2.ToString(), out DEBIT_CUSTOMER);
                        int CREDIT_CUSTOMER = 0;
                        int.TryParse(excelRange.Cells[i, 3].Value2.ToString(), out CREDIT_CUSTOMER);
                        int DEBIT_ACCT_NO = 0;
                        int.TryParse(excelRange.Cells[i, 4].Value2.ToString(), out DEBIT_ACCT_NO);
                        int CREDIT_ACCT_NO = 0;
                        int.TryParse(excelRange.Cells[i, 5].Value2.ToString(), out CREDIT_ACCT_NO);
                        object value = excelRange.Cells[i, 6].Value2;
                        DateTime dt = RecordedDate;
                        if (value != null)
                        {
                            if (value is double)
                            {
                                dt = DateTime.FromOADate((double)value);
                            }
                            else
                            {
                                DateTime.TryParse((string)value, out dt);
                            }
                        }
                        DateTime BUSINESS_DATE = dt;
                        String TRANSACTION_BY = excelRange.Cells[i, 7].Value2.ToString();
                        String DEBIT_CURRENCY = excelRange.Cells[i, 8].Value2.ToString();
                        Decimal USD_EQUIV_BALANCE = 0;
                        Decimal.TryParse(excelRange.Cells[i, 9].Value2.ToString(), out USD_EQUIV_BALANCE);
                        Decimal CREDIT_AMOUNT = 0;
                        Decimal.TryParse(excelRange.Cells[i, 10].Value2.ToString(), out CREDIT_AMOUNT);
                        int AGE = 0;
                        if (excelRange.Cells[i, 11].Value2 != null)
                            int.TryParse(excelRange.Cells[i, 11].Value2.ToString(), out AGE);
                        String GENDER = string.Empty;
                        if (excelRange.Cells[i, 12].Value2 != null)
                            GENDER = excelRange.Cells[i, 12].Value2.ToString();
                        String REGIONNAME= string.Empty;
                        if(excelRange.Cells[i, 13].Value2!=null)
                            REGIONNAME = excelRange.Cells[i, 13].Value2.ToString();
                        String DISTRICTNAME = string.Empty;
                        if (excelRange.Cells[i, 14].Value2 != null)
                            DISTRICTNAME = excelRange.Cells[i, 14].Value2.ToString();
                        //int 
                        if (DEBIT_CUSTOMER != 0 && CREDIT_CUSTOMER != 0)
                        {
                            RowDataManagement transact = new RowDataManagement();
                                if (transact.InsertRowData(DEBIT_CUSTOMER,CREDIT_CUSTOMER,DEBIT_ACCT_NO,CREDIT_ACCT_NO,
                                    BUSINESS_DATE,TRANSACTION_BY,DEBIT_CURRENCY,USD_EQUIV_BALANCE,CREDIT_AMOUNT,AGE,GENDER,REGIONNAME,DISTRICTNAME))
                                {
                                    count++;
                                }
                                else
                                {
                                    errorMessages += "Registration Failed at line " + countexisted + ". Please contact your system administrator";
                                }
                        }
                        else
                        {
                            errorMessages += "All Fields should be filled. Some fields are empty in the excel in line " + countexisted + ".";
                        }
                        countexisted++;
                    }
                }
            }
            catch (Exception ex)
            {
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                if (count > 0)
                    return count + " registed " + ex.Message;
                else
                    return "The Excel file doesnot have correct data.";

            }
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            if (!String.IsNullOrEmpty(ExistedTransactions))
                ExistedTransactions = "Lines " + ExistedTransactions + " Already exists.";
            if (count > 0)
            {
                if (TransactionExisted > 0)
                    return count + " Customer Transactions Registered succefully." + errorMessages + ";" + TransactionExisted + " Record/s " + ExistedTransactions;
                else
                    return count + " Customer Transactions Registered succefully." + errorMessages + ";";

            }
            else
            {
                if (TransactionExisted > 0)
                    return errorMessages + TransactionExisted + " Record/s- " + ExistedTransactions;
                else
                    return errorMessages + ExistedTransactions;
            }

        }

    }
}