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

namespace AntiLaundering.View.AntiLaundering
{
    public partial class BlackMarketRateInformation : System.Web.UI.Page
    {
        UserAcLog use = new UserAcLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                Response.Redirect("~/View/UserManagement/Account/Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["sec"] == null)
                {
                    Response.Redirect("~/View/Index.aspx");
                }
                load_BlackMarketRateInfo(true);
            }
        }
        private void AccesCheck()
        {
            UManagement isInRole = new UManagement();
            int? accessLevel = isInRole.GetAccessLeve("~/View/AntiLaundering/BlackMarketRateInformation.aspx", User.Identity.Name);
            if (accessLevel == 2)
            {
                grdBlackMarketRateInfo.MasterTableView.GetColumn("Dcolumn").Display = false;//prevent delete access
            }
            else if (accessLevel == 3)
            {
                grdBlackMarketRateInfo.MasterTableView.GetColumn("Dcolumn").Display = false;//prevent delete access
                grdBlackMarketRateInfo.ClientSettings.ClientEvents.OnRowDblClick = "";//Prevent Edit Access
            }
            else if (accessLevel == 4)
            {
                grdBlackMarketRateInfo.MasterTableView.GetColumn("Dcolumn").Display = false;//prevent delete access
                grdBlackMarketRateInfo.ClientSettings.ClientEvents.OnRowDblClick = "";//Prevent Edit Access
                if (grdBlackMarketRateInfo.MasterTableView.GetItems(GridItemType.CommandItem).Length >0)
                {
                    GridCommandItem cmd1 = (GridCommandItem)grdBlackMarketRateInfo.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                    RadButton btn1 = cmd1.FindControl("btnRegister") as RadButton;
                    btn1.Enabled = false;//Prevent Insert Access
                }
            }
        }
        protected void grdBlackMarketRateInfo_PreRender(object sender, EventArgs e)
        {
            if (grdBlackMarketRateInfo.EditItems.Count > 0)
            {
                foreach (GridDataItem item in grdBlackMarketRateInfo.MasterTableView.Items)
                {
                    if (item != grdBlackMarketRateInfo.EditItems[0])
                    {
                        item.Visible = false;
                    }
                }
            }
        }
        protected void grdBlackMarketRateInfo_ItemDataBound(object sender, GridItemEventArgs e)
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
        protected void load_BlackMarketRateInfo(Boolean bind)
        {
            BlackMarketRateManagement com = new BlackMarketRateManagement();
            grdBlackMarketRateInfo.DataSource = com.GetBlackMarketRate();
            if(bind)
                grdBlackMarketRateInfo.DataBind();
            AccesCheck();
        }
        protected void grdBlackMarketRateInfo_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Guid Id = Guid.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["BlackMarketRateID"].ToString());
            try
            {
                BlackMarketRateManagement com = new BlackMarketRateManagement();
                if (com.DeleteBlackMarketRate(Id))
                {
                    load_BlackMarketRateInfo(true);
                    MessageBox1.Message = "BlackMarketRate Information Deleted";
                    use.InserActionLog(DateTime.Now, User.Identity.Name, "BlackMarketRate Deleted ", "BlackMarketRate Deleted" + ID.ToString());
                }
                else
                {
                    ErrorMsg1.Message = "This BlackMarketRate is used as foreign key to other informations! Fisrt delete those informations to delete this BlackMarketRate!";
                }
            }

            catch (Exception ex)
            {
                //MessagePanel1.Visible = true;
                ErrorMsg1.Message = ex.Message;

            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            load_BlackMarketRateInfo(true);
        }

        protected void grdBlackMarketRateInfo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            load_BlackMarketRateInfo(false);
        }

    }
}