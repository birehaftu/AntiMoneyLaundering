using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HohomaFMS.GUI
{
    public partial class IllegalAccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(!IsPostBack)
            MessageBox.Message = "You are trying to access pages which are not previlleged to you! You will be responsible for any actions your organization will take to your trial!";
        }
    }
}