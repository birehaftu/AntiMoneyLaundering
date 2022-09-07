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
            string ExistedStudents = "";
            string NullStudents = "";
            int StudentExisted = 0;
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
                ExcelApp._Worksheet excelSheet = excelBook.Sheets[1];
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
                    DateTime dt=RecordedDate;
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
                            StudentExisted++;
                            ExistedStudents += countexisted + ", ";
                        }
                    }
                    else
                    {
                        errorMessages += "All Fields should be filled. Some fields are empty in the excel in line " + countexisted + ".";
                    }
                    countexisted++;
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
            if (!String.IsNullOrEmpty(ExistedStudents))
                ExistedStudents = "Lines " + ExistedStudents + " Already exists.";
            if (count > 0)
            {
                if (StudentExisted > 0)
                    return count + " Customer Transactions Registered succefully." + errorMessages + ";" + StudentExisted + " Record/s " + ExistedStudents;
                else
                    return count + " Customer Transactions Registered succefully." + errorMessages + ";";

            }
            else
            {
                if (StudentExisted > 0)
                    return errorMessages + StudentExisted + " Record/s- " + ExistedStudents;
                else
                    return errorMessages + ExistedStudents;
            }

        }

    }
}