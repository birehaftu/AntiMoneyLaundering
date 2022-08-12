using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using AntiLaundering.Control;
using AntiLaundering.Control.UserManagement; using AntiLaundering.Control.ResourceManagement;
using AntiLaundering.Control.ResourceManagement;

namespace AntiLaundering.View.ResourceManagement
{
    public partial class CompanyInformation : System.Web.UI.Page
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
                load_companyInfo(true);
            }
        }
        private void AccesCheck()
        {
            UManagement isInRole = new UManagement();
            int? accessLevel = isInRole.GetAccessLeve("~/View/ResourceManagement/CompanyInformation.aspx", User.Identity.Name);
            if (accessLevel == 2)
            {
                grdCompanyInfo.MasterTableView.GetColumn("Dcolumn").Display = false;//prevent delete access
            }
            else if (accessLevel == 3)
            {
                grdCompanyInfo.MasterTableView.GetColumn("Dcolumn").Display = false;//prevent delete access
                grdCompanyInfo.ClientSettings.ClientEvents.OnRowDblClick = "";//Prevent Edit Access
            }
            else if (accessLevel == 4)
            {
                grdCompanyInfo.MasterTableView.GetColumn("Dcolumn").Display = false;//prevent delete access
                grdCompanyInfo.ClientSettings.ClientEvents.OnRowDblClick = "";//Prevent Edit Access
                if (grdCompanyInfo.MasterTableView.GetItems(GridItemType.CommandItem).Length >0)
                {
                    GridCommandItem cmd1 = (GridCommandItem)grdCompanyInfo.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                    RadButton btn1 = cmd1.FindControl("btnRegister") as RadButton;
                    btn1.Enabled = false;//Prevent Insert Access
                }
            }
        }
        protected void grdCompanyInfo_PreRender(object sender, EventArgs e)
        {
            if (grdCompanyInfo.EditItems.Count > 0)
            {
                foreach (GridDataItem item in grdCompanyInfo.MasterTableView.Items)
                {
                    if (item != grdCompanyInfo.EditItems[0])
                    {
                        item.Visible = false;
                    }
                }
            }
        }
        protected void grdCompanyInfo_ItemDataBound(object sender, GridItemEventArgs e)
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
        protected void load_companyInfo(Boolean bind)
        {
            CompanyManagement com = new CompanyManagement();
            grdCompanyInfo.DataSource = com.GetCompany();
            if(bind)
                grdCompanyInfo.DataBind();
            AccesCheck();
        }
        protected void grdCompanyInfo_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Guid Id = Guid.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CompanyId"].ToString());
            try
            {
                CompanyManagement com = new CompanyManagement();
                if (com.DeleteCompany(Id))
                {
                    load_companyInfo(true);
                    MessageBox1.Message = "Company Information Deleted";
                    use.InserActionLog(DateTime.Now, User.Identity.Name, "Company Deleted ", "Company Deleted" + ID.ToString());
                }
                else
                {
                    ErrorMsg1.Message = "This company is used as foreign key to other informations! Fisrt delete those informations to delete this company!";
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
            load_companyInfo(true);
        }

        protected void grdCompanyInfo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            load_companyInfo(false);
        }

    }
}