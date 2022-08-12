using System;
using System.Web.UI;
using Telerik.Web.UI;
using System.Resources;
using System.Reflection;
using System.Web.Security;
using MyUserManagement;
using AntiLaundering.Model;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;

using AntiLaundering.Control.UserManagement; using AntiLaundering.Control.ResourceManagement;
using AntiLaundering.Control.ResourceManagement;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
namespace AntiLaundering.View.UserManagement
{
    public partial class SqlRole : System.Web.UI.Page
    {
        UserAcLog use = new UserAcLog();
        //ResourceManager rm = new ResourceManager("Resources.CommonResource", Assembly.Load("App_GlobalResources"));
        OurTeleric ourTeleric = new OurTeleric();
        MyUserManagement.SqlRoleProvider srp = (MyUserManagement.SqlRoleProvider)Roles.Provider;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (User.Identity.IsAuthenticated == false)
            {
                Response.Redirect("~/View/um/Account/Login.aspx");
            }
            if (Request.QueryString["sec"] == null)
            {
                Response.Redirect("~/View/Index.aspx");
            }
        }
        protected void RadGridRoleList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            string RoleName = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ROLENAME"].ToString();
            try
            {
               Boolean flg= System.Web.Security.Roles.DeleteRole(RoleName, false);
               if (flg)
                   MessageBox.Message = "Role is Deleted.";
               else
                   ErrorMsg.Message = "This role is referenced by users. First you need to delete these users!";
                //RadGridRoleList.Controls.Add(new LiteralControl(RoleName + ": " + rm.GetString("AfterDelete")));

            }
            catch (Exception ex)
            {
                ErrorMsg.Message="This role is assigned to a users please avaoid these users first.";

            }
        }
        protected void RadGridRoleList_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //ourTeleric.userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

            try
            {

                string rolename = (editedItem.FindControl("txtRoleName") as TextBox).Text.Trim();
                string Description = (editedItem.FindControl("txtDescription") as TextBox).Text;
                //ourTeleric.controlName = "txtNewRole";
                //String RoleName = ourTeleric.getAspTextBoxValue();
                //ourTeleric.controlName = "txtDescrption";
                //String Descrption = ourTeleric.getAspTextBoxValue();
                //srp.Description = Descrption;
                srp.Description = Description;
                UManagement exist = new UManagement();
                if (exist.RoleExists(rolename))
                {
                    ErrorMsg.Message = "A Role is already registered with the given Role Name!";
                    return;
                }
                if (String.IsNullOrEmpty(rolename))
                {
                    errormsg1.Message = "RoleName can't be empty!";
                    return;
                }
                srp.CreateRole(rolename);
                use.InserActionLog(DateTime.Now, User.Identity.Name, "Role is Created", "User created Role with RoleName:" + rolename);
                MessageBox.Message = "Role is Created.";
                //RadGridRoleList.Controls.Add(new LiteralControl(rm.GetString("AfterInsert")));
                //createRoleSuccess = true;
            }
            catch (Exception ex)
            {
                //RadGridRoleList.Controls.Add(new LiteralControl(ex.Message));
                ErrorMsg.Message = ""+ex.Message;
                //createRoleSuccess = false;
            }
        }
        protected void RadGridRoleList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RadGridRoleList.DataSource = srp.GetRoles_UserRoles();
        }

        protected void RadGridRoleList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item is GridItem)
            {
                var lbl = e.Item.FindControl("lblbnum") as Label;
                if (lbl != null)
                {
                    lbl.Text = (e.Item.ItemIndex + 1).ToString();
                }
            }
        }

        protected void RadGridRoleList_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            string role = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ROLENAME"].ToString();
           string rolename = (editedItem.FindControl("txtRoleName") as TextBox).Text;
            if (!rolename.Equals(role))
            {
                errormsg1.Message = "Role Name can't be modified!";
                return;
            }
            if (String.IsNullOrEmpty(role))
            {
                errormsg1.Message = "RoleName can't be empty!";
                return;
            }
            AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
            var ope = (from r in entities.Roles where r.ROLENAME==role select r).FirstOrDefault();
            ope.Description = (editedItem.FindControl("txtDescription") as TextBox).Text;
            try
            {
                entities.SaveChanges();
                use.InserActionLog(DateTime.Now, User.Identity.Name, "Role is Updated", "User Updated Role with RoleName:" + role);
                MessageBox1.Message = "Operation Succeeded. Role successfuly Updated!";
            }
            catch (Exception ex)
            {
                errormsg1.Message = "Operation Failed. Role was not updated " + ex.Message;//clsLogFile.WriteLog("Error:", ex);
            }
        }
    }
}