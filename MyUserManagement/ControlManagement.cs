using System;
using System.Data.SqlClient;
using System.Data;
using SqlDataAccess;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;
using System.Reflection;

/// <summary>
/// Summary description for ControlManagement
/// </summary>

namespace MyUserManagement
{
    public class ControlManagement
    {
        public Connection Conn = new Connection();


        #region private Member variables
        private string _text;
        private string[] _texts;
        private string _value;
        private string _procedureName;
        private string _selectedText;
        private string[] _arrayData;
        private string[] _parameterName;
        private object[] _objectValue;
        private object[] _parameterDirection;
        private SqlDbType[] _parameterType;
        SqlDBManagerClass dbManager;

        #endregion
        public ControlManagement()
        {
            dbManager = new SqlDBManagerClass();
        }
        #region properties
        public string text
        {
            get { return _text; }
            set { _text = value; }
        }
        public string[] texts
        {
            get { return _texts; }
            set { _texts = value; }
        }
        public string value
        {
            get { return _value; }
            set { _value = value.ToUpper(); }
        }
        public string procedureName
        {
            get { return _procedureName; }
            set { _procedureName = value.ToUpper(); }
        }
        public string selectedText
        {
            get { return _selectedText; }
            set { _selectedText = value.ToUpper(); }
        }
        public string[] arrayData
        {
            get { return _arrayData; }
            set { _arrayData = value; }
        }
        public string[] parameterName
        {
            get { return _parameterName; }
            set { _parameterName = value; }
        }
        public object[] objectValue
        {
            get { return _objectValue; }
            set { _objectValue = value; }
        }
        public object[] parameterDirection
        {
            get { return _parameterDirection; }
            set { _parameterDirection = value; }
        }
        public SqlDbType[] parameterType
        {
            get { return _parameterType; }
            set { _parameterType = value; }
        }
        #endregion

        ///<summary>
        ///Fill DropDownList, RadComboBox Methods with diffrent Parameteres Properties
        ///</summary>
        ///
        #region fill combo boxes
        public void fillCombo(RadComboBox ddl)
        {

            if (Conn.OpenConnection())
            {
                dbManager.ConnectionString = Conn.ConnectionString;
                try
                {
                    dbManager.Open();
                    dbManager.CreateParameters(_parameterName.Length);
                    dbManager.AddParameters(_parameterName, _objectValue, _parameterDirection, _parameterType);
                    SqlDataReader dr = dbManager.ExecuteReader(CommandType.StoredProcedure, _procedureName);
                    while (dr.Read())
                    {
                        ddl.Items.Add(new RadComboBoxItem(dr[_text].ToString(), dr[_value].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dbManager.Dispose();
                    _parameterType = null;
                    _parameterName = null;
                    _parameterDirection = null;
                    _parameterName = null;
                }
            }
        }
        public void fillCombo(DropDownList ddl)
        {
            if (Conn.OpenConnection())
            {
                dbManager.ConnectionString = Conn.ConnectionString;
                try
                {
                    dbManager.Open();
                    dbManager.CreateParameters(_parameterName.Length);
                    dbManager.AddParameters(_parameterName, _objectValue, _parameterDirection, _parameterType);
                    SqlDataReader dr = dbManager.ExecuteReader(CommandType.StoredProcedure, _procedureName);
                    while (dr.Read())
                    {
                        ddl.Items.Add(new ListItem(dr[_text].ToString(), dr[_value].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dbManager.Dispose();
                    _parameterType = null;
                    _parameterName = null;
                    _parameterDirection = null;
                    _parameterName = null;
                }
            }
        }
        public void fillComboListArray(RadComboBox ddl)
        {
            if (Conn.OpenConnection())
            {
                dbManager.ConnectionString = Conn.ConnectionString;
                try
                {
                    dbManager.Open();
                    dbManager.CreateParameters(_parameterName.Length);
                    dbManager.AddParameters(parameterName, _objectValue, _parameterDirection, _parameterType);
                    SqlDataReader dr = dbManager.ExecuteReader(CommandType.StoredProcedure, _procedureName);
                    while (dr.Read())
                    {
                        //ddl.Items.Add(new RadComboBoxItem(dr[_text].ToString(), dr[_value].ToString()));
                        RadComboBoxItem item = new RadComboBoxItem(dr[_text].ToString(), dr[_value].ToString());
                        for (int i = 0; i < _arrayData.Length; i++)
                        {
                            item.Attributes.Add(_arrayData[i], dr[_arrayData[i]].ToString());
                        }
                        ddl.Items.Add(item);
                        item.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dbManager.Dispose();
                    _parameterType = null;
                    _parameterName = null;
                    _parameterDirection = null;
                    _parameterName = null;
                    _objectValue = null;
                }
            }
        }
        public void getSelectedComboItemValue(RadComboBox ddl)
        {
            foreach (RadComboBoxItem item in ddl.Items)
            {
                if (_selectedText.ToLower() == item.Value.ToLower())
                {
                    item.Selected = true;
                    break;
                }
            }
        }
        public void getSelectedComboItemText(RadComboBox ddl)
        {
            foreach (RadComboBoxItem item in ddl.Items)
            {
                if (_selectedText.ToLower() == item.Text.ToLower())
                {
                    item.Selected = true;
                    break;
                }
            }
        }
        public void CheckedAllItems(RadListBox rlb)
        {
            foreach (RadListBoxItem RLBI in rlb.Items)
            {
                RLBI.Checked = true;
            }
        }
        #endregion

        #region Fill List Box
        /// <summary>
        /// Fill List Box
        /// </summary>
        /// <param name="ddl"></param>
        public void fillListBox(RadListBox ddl)
        {

            if (Conn.OpenConnection())
            {
                dbManager.ConnectionString = Conn.ConnectionString;
                try
                {
                    dbManager.Open();
                    dbManager.CreateParameters(_parameterName.Length);
                    dbManager.AddParameters(_parameterName, _objectValue, _parameterDirection, _parameterType);
                    SqlDataReader dr = dbManager.ExecuteReader(CommandType.StoredProcedure, _procedureName);
                    while (dr.Read())
                    {
                        ddl.Items.Add(new RadListBoxItem(dr[_text].ToString(), dr[_value].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dbManager.Dispose();
                    _parameterType = null;
                    _parameterName = null;
                    _parameterDirection = null;
                    _parameterName = null;
                }
            }
        }
        public void fillListBoxArray(RadListBox ddl)
        {

            if (Conn.OpenConnection())
            {
                dbManager.ConnectionString = Conn.ConnectionString;
                try
                {
                    dbManager.Open();
                    dbManager.CreateParameters(_parameterName.Length);
                    dbManager.AddParameters(_parameterName, _objectValue, _parameterDirection, _parameterType);
                    SqlDataReader dr = dbManager.ExecuteReader(CommandType.StoredProcedure, _procedureName);
                    while (dr.Read())
                    {
                        string tempText = "";
                        for (int i = 0; i < _texts.Length; i++)
                            tempText += _texts[i] + " ";
                        ddl.Items.Add(new RadListBoxItem(dr[_text].ToString(), tempText));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dbManager.Dispose();
                    _parameterType = null;
                    _parameterName = null;
                    _parameterDirection = null;
                    _parameterName = null;
                }
            }
        }
        #endregion
        ///<summary>
        ///Grid View Methods with diffrent Parameteres Properties
        ///</summary>
        ///
        #region fill grid views
        public void fillGridView(RadGrid radgrid)
        {
            if (Conn.OpenConnection())
            {
                dbManager.ConnectionString = Conn.ConnectionString;
                try
                {
                    dbManager.Open();
                    dbManager.CreateParameters(_parameterName.Length);
                    dbManager.AddParameters(_parameterName, _objectValue, _parameterDirection, _parameterType);
                    radgrid.DataSource = dbManager.ExecuteDataTable(CommandType.StoredProcedure, _procedureName);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dbManager.Dispose();
                    _parameterType = null;
                    _parameterName = null;
                    _parameterDirection = null;
                    _parameterName = null;
                }
            }
        }
        #endregion

        ///<summary>
        ///Get data table Methods with diffrent Parameteres Properties
        ///</summary>
        ///
        #region getDataTable
        public DataTable getDataTable()
        {
            DataTable dt = new DataTable();
            if (Conn.OpenConnection())
            {
                dbManager.ConnectionString = Conn.ConnectionString.ToString();
                try
                {
                    dbManager.Open();
                    dbManager.CreateParameters(_parameterName.Length);
                    dbManager.AddParameters(_parameterName, _objectValue, _parameterDirection, _parameterType);
                    dt = dbManager.ExecuteDataTable(CommandType.StoredProcedure, _procedureName);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dbManager.Dispose();
                    _parameterType = null;
                    _parameterName = null;
                    _parameterDirection = null;
                    _parameterName = null;
                }
            }
            return dt;
        }
        public void executeNoneQuery()
        {
            if (Conn.OpenConnection())
            {
                dbManager.ConnectionString = Conn.ConnectionString;
                try
                {
                    dbManager.Open();
                    dbManager.CreateParameters(_parameterName.Length);
                    if (_parameterType == null && _parameterDirection == null)
                    {
                        dbManager.AddParameters(_parameterName, _objectValue);
                    }
                    //remark
                    else if (_parameterName != null && _parameterType == null && _parameterDirection != null)
                    {

                        dbManager.AddParameters(_parameterName, _objectValue, _parameterDirection);
                    }
                    else if (_parameterType.Length == _parameterName.Length && _parameterDirection == null)
                    {
                        dbManager.AddParameters(_parameterType, _parameterName, _objectValue);
                    }
                    else if (_parameterType.Length == _parameterName.Length && _parameterDirection.Length == _parameterName.Length)
                    {
                        dbManager.AddParameters(_parameterName, _objectValue, _parameterDirection, _parameterType);
                    }
                    dbManager.ExecuteNonQuery(CommandType.StoredProcedure, _procedureName);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dbManager.Dispose();
                    _parameterType = null;
                    _parameterName = null;
                    _parameterDirection = null;
                    _parameterName = null;
                    _objectValue = null;

                }
            }
        }
        public int executeNoneQueryReturn()
        {
            int x = 0;
            if (Conn.OpenConnection())
            {
                dbManager.ConnectionString = Conn.ConnectionString;
                try
                {
                    dbManager.Open();
                    dbManager.CreateParameters(_parameterName.Length);
                    if (_parameterType == null && _parameterDirection == null)
                    {
                        dbManager.AddParameters(_parameterName, _objectValue);
                    }
                    //remark
                    else if (_parameterName != null && _parameterType == null && _parameterDirection != null)
                    {
                        dbManager.AddParameters(_parameterName, _objectValue, _parameterDirection);
                    }
                    else if (_parameterType.Length == _parameterName.Length && _parameterDirection == null)
                    {
                        dbManager.AddParameters(_parameterType, _parameterName, _objectValue);
                    }
                    else if (_parameterType.Length == _parameterName.Length && _parameterDirection.Length == _parameterName.Length)
                    {
                        dbManager.AddParameters(_parameterName, _objectValue, _parameterDirection, _parameterType);
                    }

                    x = dbManager.ExecuteNonQuery(CommandType.StoredProcedure, _procedureName);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dbManager.Dispose();
                    _parameterType = null;
                    _parameterName = null;
                    _parameterDirection = null;
                    _parameterName = null;
                    _objectValue = null;
                }
            }
            return x;
        }
        #endregion

        /// <summary>
        /// Localizes Gridview filter options
        /// </summary>
        /// <param name="rgv"></param>
        public void SetFilterText(RadGrid rgv)
        {
            //ResourceManager rm = new ResourceManager("Resources.CommonResource", Assembly.Load("App_GlobalResources"));
            //GridFilterMenu menu = rgv.FilterMenu;
            //foreach (RadMenuItem item in menu.Items)
            //{
            //    //change the text for the "StartsWith" menu item
            //    if (item.Value == "StartsWith")
            //    {
            //        item.Text = rm.GetString("StartsWith");

            //    }
            //    if (item.Value == "Contains")
            //    {
            //        item.Text = rm.GetString("Contains");

            //    }
            //    if (item.Value == "EndsWith")
            //    {
            //        item.Text = rm.GetString("EndsWith");

            //    }
            //    if (item.Value == "EqualTo")
            //    {
            //        item.Text = rm.GetString("EqualTo");

            //    }
            //    if (item.Value == "NoFilter")
            //    {
            //        item.Text = rm.GetString("NoFilter");

            //    }
            //    if (item.Value == "DoesNotContain")
            //    {
            //        item.Text = rm.GetString("DoesNotContain");

            //    }
            //    if (item.Value == "NotEqualTo")
            //    {
            //        item.Text = rm.GetString("NotEqualTo");

            //    }
            //    if (item.Value == "GreaterThan")
            //    {
            //        item.Text = rm.GetString("GreaterThan");

            //    }
            //    if (item.Value == "LessThan")
            //    {
            //        item.Text = rm.GetString("LessThan");

            //    }
            //    if (item.Value == "GreaterThanOrEqualTo")
            //    {
            //        item.Text = rm.GetString("GreaterThanOrEqualTo");
            //    }
            //    if (item.Value == "LessThanOrEqualTo")
            //    {
            //        item.Text = rm.GetString("LessThanOrEqualTo");
            //    }
            //    if (item.Value == "Between")
            //    {
            //        item.Text = rm.GetString("Between");
            //    }
            //    if (item.Value == "NotBetween")
            //    {
            //        item.Text = rm.GetString("NotBetween");
            //    }
            //    if (item.Value == "IsEmpty")
            //    {
            //        item.Text = rm.GetString("IsEmpty");
            //    }
            //    if (item.Value == "NotIsEmpty")
            //    {
            //        item.Text = rm.GetString("NotIsEmpty");
            //    }
            //    if (item.Value == "IsNull")
            //    {
            //        item.Text = rm.GetString("IsNull");
            //    }
            //    if (item.Value == "NotIsNull")
            //    {
            //        item.Text = rm.GetString("NotIsNull");
            //    }
            //}

        }
        /// <summary>
        /// Decreases the number of filter options to 5
        /// </summary>
        /// <param name="grd">Gridview needed to have decreased filter menu options </param>
        public void FilterOptions(RadGrid grd)
        {
            //this.LiteralMessage.Text = rm.GetString("LiteralMessageResource1");
            GridFilterMenu menu = grd.FilterMenu;
            int i = 0;
            while (i < menu.Items.Count)
            {
                if (menu.Items[i].Value == "NoFilter" || menu.Items[i].Value == "StartsWith" || menu.Items[i].Value == "Contains" ||
                   menu.Items[i].Value == "EndsWith" ||
                   menu.Items[i].Value == "EqualTo")
                {
                    i++;
                }
                else
                {
                    menu.Items.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// THIS FUNCTION ACCEPTS GC DAY,MONTH AND YEAR,
        /// THEN IT CONVERTED TO EC AND RETURNTO CALLER 
        /// FUNCTION ON ET DD/MM/YYYY FORMAT, THE CALLER
        /// FUNCTION MAY NEED TO SPLIT THE STRING TO GET DAY,MONTH AND YEAR SEPARATELY
        /// </summary>
        /// <param name="pdate"></param>
        /// <returns></returns>
        ///
        public string ConvertFromGCtoEC(int day, int month, int year)
        {
            int cday, cmonth, cyear;
            int ecLeapEffect, gcLeapEffect;
            string convertedDate = "";
            ecLeapEffect = IsLeapYear(year - 9);
            gcLeapEffect = IsLeapYearGC(year);
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            if (month == 0)//jan
            {
                cyear = year - 8;
                //alert(ecLeapEffect);
                if (day <= (8 + ecLeapEffect))
                {
                    cmonth = month + 4; //tahissas
                    cday = (day + 22 - ecLeapEffect);
                }
                else
                {
                    cmonth = month + 5; //thir
                    if (ecLeapEffect == 1)
                        cday = day - 9;
                    else
                        cday = day - 8;
                }
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            else if (month == 1)//feb
            {
                cyear = year - 8;
                if (day <= (7 + ecLeapEffect))
                {
                    cmonth = month + 4; //thir
                    cday = (day + 23 - ecLeapEffect);
                }
                else
                {//this else statment doesn't need to consider GC Leap year, since it doesn't make any diffrence on conversion
                    cmonth = month + 5; //yekatit
                    if (ecLeapEffect == 1)
                        cday = day - 8;
                    else                    //1ce in 4 year feb leap it self and be 29 rather 28 in this else statment
                        cday = day - 7;
                }
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            if (month == 2)//mar
            {
                //both ec and gc leapeffects returns one here so either feb 29 or 28 it ends with ec 21 and march starts from 22
                //so in this case the ec leap effect affects no more month before this end of year, since it is rejected by gc leap effect
                //alert("ec leap = " + ecLeapEffect + " gc leap = " + gcLeapEffect);
                //alert(gcLeapEffect);
                cyear = year - 8;
                if (day <= 9)
                {
                    cmonth = month + 4; //yekatit
                    cday = (day + 21);
                }
                else
                {
                    cmonth = month + 5; //megabit         
                    cday = day - 9;
                }
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            if (month == 3)//apr
            {
                cyear = year - 8;
                if (day <= 8)
                {
                    cmonth = month + 4; //megabit
                    cday = (day + 22);
                }
                else
                {
                    cmonth = month + 5; //miyaziya         
                    cday = day - 8;
                }
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            if (month == 4)//may
            {
                cyear = year - 8;
                if (day <= 8)
                {
                    cmonth = month + 4; //miyaziya
                    cday = (day + 22);
                }
                else
                {
                    cmonth = month + 5; //ginbot         
                    cday = day - 8;
                }
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            if (month == 5)//jun
            {
                cyear = year - 8;
                if (day <= 7)
                {
                    cmonth = month + 4; //ginbot
                    cday = (day + 23);
                }
                else
                {
                    cmonth = month + 5; //sene         
                    cday = day - 7;
                }
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            if (month == 6)//jul
            {
                cyear = year - 8;
                if (day <= 7)
                {
                    cmonth = month + 4; //sene
                    cday = (day + 23);
                }
                else
                {
                    cmonth = month + 5; //hamle         
                    cday = day - 7;
                }
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            if (month == 7)//aug
            {
                cyear = year - 8;
                if (day <= 6)
                {
                    cmonth = month + 4; //hamle
                    cday = (day + 24);
                }
                else
                {
                    cmonth = month + 5; //nehasse         
                    cday = day - 6;
                }
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }

            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            if (month == 8)//sep
            {
                int ecLeapEffect2 = IsLeapYear(year - 8); // this is not the same leap check as the global peer, rather it checks if the current
                if (day <= 5)
                {                         //year is leap or not, the global checks if the current-1 is leap or not.
                    cyear = year - 8;
                    cmonth = month + 4; //nehasse
                    cday = (day + 25);
                }
                else if (day >= 6 && day <= (10 + ecLeapEffect2))
                {
                    cyear = year - 8;
                    cmonth = month + 5; //Puagme
                    cday = day - 5;
                }
                else
                {
                    cyear = year - 7;
                    cmonth = month - 7; //Meskerem
                    if (ecLeapEffect2 == 1)
                        cday = day - 11;
                    else
                        cday = day - 10;
                }
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }

            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            if (month == 9)//oct
            {
                int ecLeapEffect2 = IsLeapYear(year - 8); // check if last ethiopian year is leap or not, bc it affects months after puagme 5 or 6
                cyear = year - 7;                       // and consider that there is no gc leap arround this month so it will continue until it gets it.
                if (day <= (10 + ecLeapEffect2))
                {
                    cmonth = month - 8;  //meskerem
                    if (ecLeapEffect2 == 1)
                        cday = day + 19;
                    else
                        cday = day + 20;
                }
                else
                {
                    cmonth = month - 7;  //tikimt
                    if (ecLeapEffect2 == 1)
                        cday = day - 11;
                    else
                        cday = day - 10;
                }
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);


            }
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            if (month == 10)//nov
            {
                int ecLeapEffect2 = IsLeapYear(year - 8); // check if last ethiopian year is leap or not, bc it affects months after puagme 5 or 6
                cyear = year - 7;                       // and consider that there is no gc leap arround this month so it will continue until it gets it.
                if (day <= (9 + ecLeapEffect2))
                {
                    cmonth = month - 8;  //tikimt
                    if (ecLeapEffect2 == 1)
                        cday = day + 20;
                    else
                        cday = day + 21;
                }
                else
                {
                    cmonth = month - 7;  //hidar 
                    if (ecLeapEffect2 == 1)
                        cday = day - 10;
                    else
                        cday = day - 9;
                }
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);


            }
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            if (month == 11)//dec
            {
                //alert(day+" " + month + " " + year);
                int ecLeapEffect2 = IsLeapYear(year - 8); // check if last ethiopian year is leap or not, bc it affects months after puagme 5 or 6
                cyear = year - 7;                       // and consider that there is no gc leap arround this month so it will continue until it gets it.
                if (day <= (9 + ecLeapEffect2))
                {
                    cmonth = month - 8;  //hidar
                    if (ecLeapEffect2 == 1)
                        cday = day + 20;
                    else
                        cday = day + 21;
                }
                else
                {
                    cmonth = month - 7;  //tahissas 
                    if (ecLeapEffect2 == 1)
                        cday = day - 10;
                    else
                        cday = day - 9;
                }
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);
                //alert(convertedDate);


            }
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            return convertedDate;
        }
        /// <summary>
        /// THIS FUNCTION ACCEPTS EC DAY,MONTH AND YEAR, THEN IT CONVERTED 
        /// TO GC AND RETURN TO CALLER FUNCTION ON GC DD/MM/YYYY FORMAT,
        /// THE CALLER FUNCTION MAY NEED TO SPLIT  THE STRING TO GET DAY,MONTH AND YEAR SEPARATELY
        /// </summary>
        /// <param name="pdate"></param>
        /// <returns></returns>
        public string ConvertFromECtoGC(int day, int month, int year)
        {
            int cday, cmonth, cyear;
            int leapEffect = IsLeapYear(year - 1);
            int gcLeapEffect;
            string convertedDate = "";

            if (month == 0) //if Meskerem
            {
                cyear = year + 7;
                if (day <= (20 - leapEffect))
                {
                    cmonth = month + 9; //sep
                    cday = day + 10 + leapEffect;
                }
                else
                {
                    cmonth = month + 10; //oct
                    if (leapEffect == 1)
                        cday = day - 19;
                    else
                        cday = day - 20;
                }
                //alert(cday + "/" + cmonth + "/" + cyear);
                convertedDate = cday + "-" + cmonth + "-" + cyear;

            }
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (month == 1) //if Tikimt
            {
                cyear = year + 7;
                if (day <= (21 - leapEffect))
                {
                    cmonth = month + 9; //oct
                    cday = day + 10 + leapEffect;
                }
                else
                {
                    cmonth = month + 10; //nov
                    if (leapEffect == 1)
                        cday = day - 20;
                    else
                        cday = day - 21;
                }
                //alert(cday + "/" + cmonth + "/" + cyear);
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (month == 2) //if Hidar
            {
                cyear = year + 7;
                if (day <= (21 - leapEffect))
                {
                    cmonth = month + 9; //nov
                    cday = day + 9 + leapEffect;
                }
                else
                {
                    cmonth = month + 10; //dec
                    if (leapEffect == 1)
                        cday = day - 20;
                    else
                        cday = day - 21;
                }
                //alert(cday + "/" + cmonth + "/" + cyear);
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (month == 3) //if Tahissas
            {
                //cyear = year + 7;
                if (day <= (22 - leapEffect))
                {
                    cyear = year + 7; //year is ready to switch
                    cmonth = month + 9; //dec
                    cday = day + 9 + leapEffect;
                }
                else
                {
                    cyear = year + 8; //year is switched
                    cmonth = month - 2;  //JAN /*HAPPY NEW YEAR*/
                    if (leapEffect == 1)
                        cday = day - 21;
                    else
                        cday = day - 22;
                }
                //alert(cday + "/" + cmonth + "/" + cyear);
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++           
            if (month == 4) //if Thir
            {
                cyear = year + 8;
                if (day <= (23 - leapEffect))
                {
                    cmonth = month - 3; //jan
                    cday = day + 8 + leapEffect;
                }
                else
                {
                    cmonth = month - 2; //feb /*April the fool*/
                    if (leapEffect == 1)
                        cday = day - 22;
                    else
                        cday = day - 23;
                }
                //alert(cday + "/" + cmonth + "/" + cyear);
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++      
            if (month == 5) //if Yekatit
            {
                cyear = year + 8;
                gcLeapEffect = IsLeapYearGC(cyear);
                if (day <= ((21 + gcLeapEffect) - leapEffect))
                {
                    cmonth = month - 3; //feb
                    cday = day + 7 + leapEffect;
                }
                else
                {
                    cmonth = month - 2; //mar
                    if (leapEffect == 1)
                        cday = day - (20 + gcLeapEffect);
                    else
                        cday = day - (21 + gcLeapEffect);
                }
                //alert(cday + "/" + cmonth + "/" + cyear);
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (month == 6) //if Megabit
            {
                cyear = year + 8;
                if (day <= 22)
                {
                    cmonth = month - 3;  //mar
                    cday = day + 9;
                }
                else
                {
                    cmonth = month - 2;  //apr                              
                    cday = day - 22;
                }
                //alert(cday + "/" + cmonth + "/" + cyear);
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (month == 7) //if Miyazia
            {
                cyear = year + 8;
                if (day <= 22)
                {
                    cmonth = month - 3;  //apr
                    cday = day + 8;
                }
                else
                {
                    cmonth = month - 2;  //may    
                    cday = day - 22;
                }
                //alert(cday + "/" + cmonth + "/" + cyear);
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (month == 8) //if Ginbot
            {
                cyear = year + 8;
                if (day <= 23)
                {
                    cmonth = month - 3;  //may
                    cday = day + 8;
                }
                else
                {
                    cmonth = month - 2;  //jun                              
                    cday = day - 23;
                }
                //alert(cday + "/" + cmonth + "/" + cyear);
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (month == 9) //if Sene
            {
                cyear = year + 8;
                if (day <= 23)
                {
                    cmonth = month - 3;  //jun
                    cday = day + 7;
                }
                else
                {
                    cmonth = month - 2;  //jul                              
                    cday = day - 23;
                }
                //alert(cday + "/" + cmonth + "/" + cyear);
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (month == 10) //if Hamle
            {
                cyear = year + 8;
                if (day <= 24)
                {
                    cmonth = month - 3;  //jul
                    cday = day + 7;
                }
                else
                {
                    cmonth = month - 2;  //aug                              
                    cday = day - 24;
                }
                //alert(cday + "/" + cmonth + "/" + cyear);
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (month == 11) //if Nehasse
            {
                cyear = year + 8;
                if (day <= 25)
                {

                    cmonth = month - 3;  //aug
                    cday = day + 6;
                }
                else
                {
                    //cyear = year + 7;
                    cmonth = month - 2;  //sep                              
                    cday = day - 25;
                }
                //alert(cday + "/" + cmonth + "/" + cyear);
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (month == 12) //if Puagme /*Ethiopian's alone month*/
            {
                cyear = year + 8;
                cmonth = month - 3;  //sep
                cday = day + 5;
                //alert(cday + "/" + cmonth + "/" + cyear);
                convertedDate = cyear + "-" + appendZero(cmonth) + "-" + appendZero(cday);

            }
            return convertedDate;
        }
        public int IsLeapYear(int yearToBeChecked)
        {
            int initialYear = 1899;
            for (var year = initialYear; year <= yearToBeChecked; year += 4)
            {
                if (yearToBeChecked == year)
                    return 1;
            }
            return 0;
        }
        /// <summary>
        /// CHECKS WHETHER A YEAR IS LEAP OR NOT (FEB 29 OR 28) FOR GREGORIAN CALENDAR
        /// </summary>
        /// <param name="yearToBeChecked"></param>
        /// <returns></returns>
        public int IsLeapYearGC(int yearToBeChecked)
        {
            if (yearToBeChecked % 4 == 0)
            {
                if (yearToBeChecked % 100 != 0)
                    return 1;
                else if (yearToBeChecked % 400 == 0)
                    return 1;
                else
                    return 0;
            }
            return 0;
        }
        /// <summary>
        /// Append Zero
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string appendZero(int str)
        {
            return str < 10 ? "0" + str.ToString() : str.ToString();
        }
    }
}