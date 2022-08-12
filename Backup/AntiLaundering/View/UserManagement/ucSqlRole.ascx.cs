using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AntiLaundering.View.UserManagement
{
    public partial class ucSqlRole : System.Web.UI.UserControl
    {
        private object _dataItem = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (User.Identity.IsAuthenticated == false)
            //{
            //    Response.Redirect("~/View/UserManagement/Account/Login.aspx");
            //}
        }
        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }
    }
}