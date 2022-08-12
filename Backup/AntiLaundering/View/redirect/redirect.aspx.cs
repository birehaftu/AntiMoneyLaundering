using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AntiLaundering.View.redirect
{
    public partial class redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["LoginId"] = "Login";
            if (User.IsInRole("Operator"))
            {
                Response.Redirect("~/View/Index.aspx");
            }
            else if (User.IsInRole("Administrator"))
            {
                Response.Redirect("~/View/Index.aspx");
            }
            else
            {
                Response.Redirect("~/View/Index.aspx");
            }

            //else
            //{
            //    Response.Redirect("~/View/UserManagement/Account/Login.aspx");
            //}  
        }
        private void MessageBox(String msg)
        {
            Response.Write(@"<script language='javascript'>alert('Error Message : \n" + msg + " .');</script>");
        }
    }
}