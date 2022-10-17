using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using AntiLaundering.Control;
using AntiLaundering.Control.UserManagement;
using AntiLaundering.Control.ResourceManagement;
using AntiLaundering.Control.AntiLaundering;
using System.IO;

namespace AntiLaundering.View.AntiLaundering
{
    public partial class CustomerTransactionBalckCountClassificationFiltered : System.Web.UI.Page
    {
        UserAcLog use = new UserAcLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                Response.Redirect("~/View/um/Account/Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["sec"] == null)
                {
                    Response.Redirect("~/View/Index.aspx");
                }
                load_CustomerTransactInfo(true);
            }
        }
        private void AccesCheck()
        {
            UManagement isInRole = new UManagement();
            int? accessLevel = isInRole.GetAccessLeve("~/View/AntiLaundering/CustomerTransactionBalckCountClassificationFiltered.aspx", User.Identity.Name);
           
        }
        protected void grdCustomerTransactInfo_PreRender(object sender, EventArgs e)
        {
            if (grdCustomerTransactInfo.EditItems.Count > 0)
            {
                foreach (GridDataItem item in grdCustomerTransactInfo.MasterTableView.Items)
                {
                    if (item != grdCustomerTransactInfo.EditItems[0])
                    {
                        item.Visible = false;
                    }
                }
            }
        }
        protected void grdCustomerTransactInfo_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridItem)
            {
                var lbl = e.Item.FindControl("lblpnum") as Label;
                if (lbl != null)
                {
                    lbl.Text = (e.Item.ItemIndex + 1).ToString();
                }
            }
        }
        protected void load_CustomerTransactInfo(Boolean bind)
        {
            FraudDetectedBlackCountFilteredManagement com = new FraudDetectedBlackCountFilteredManagement();
            grdCustomerTransactInfo.DataSource = com.GetFraudDetectedBlack();
            if(bind)
                grdCustomerTransactInfo.DataBind();
            AccesCheck();
        }
        protected void grdCustomerTransactInfo_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
           
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            load_CustomerTransactInfo(true);
        }

        protected void grdCustomerTransactInfo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            load_CustomerTransactInfo(false);
        }

        

    }
}