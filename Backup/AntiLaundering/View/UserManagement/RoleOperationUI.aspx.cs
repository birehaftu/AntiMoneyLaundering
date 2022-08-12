using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AntiLaundering.Model;
using Telerik.Web.UI;
using AntiLaundering.Control.ResourceManagement;
namespace AntiLaundering.View.UserManagement
{
    public partial class RoleOperationUI : System.Web.UI.Page
    {

        UserAcLog use = new UserAcLog();
        AntiMoney_UMEntities ctx = new AntiMoney_UMEntities();
        
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
                load_Role();
                load_operation();
            }
        }
        protected void grdRoleOperation_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                AntiMoney_UMEntities fleep = new AntiMoney_UMEntities();
                var ID = Int32.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                var loc = new RoleOperation();
                loc.Id = ID;
                Model.RoleOperation locdelete = fleep.RoleOperations.Where(p => p.Id == loc.Id).FirstOrDefault();
                fleep.RoleOperations.Remove(locdelete);
                fleep.SaveChanges();
                use.InserActionLog(DateTime.Now, User.Identity.Name, "Role permission assignment is deleted", "User deleted assignment with ID:" + ID);
                MessageBox.Message = "Operation Succeeded. Role Operation Information successfuly Deleted";
                //DataSource(true);
            }
            catch (Exception ex)
            {
                //ErrorMsg.Type = errormsg.ShowType.Error;
                ErrorMsg.Message = "An error occured while deleting";
            }
        }



        protected void grdRoleOperation_ItemDataBound(object sender, GridItemEventArgs e)
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
        protected void DataSource(Boolean bind)
        {
            AntiMoney_UMEntities fleep = new AntiMoney_UMEntities();
            RoleOperation loc = new RoleOperation();
            this.grdRoleOperation.DataSource = null;
            try
            {                
                List<VWRoleOperationAssign> RoleOps = new List<VWRoleOperationAssign>();
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var rop=new List<VWROLEOp>();
                if(!String.IsNullOrEmpty(txtModuleName.SelectedValue))
                    rop = (from l in entities.VWROLEOps where l.ROLENAME == rdRoles.SelectedValue && l.ModuleName == txtModuleName.SelectedValue select l).ToList();
                else
                    rop = (from l in entities.VWROLEOps where l.ROLENAME == rdRoles.SelectedValue select l).ToList();
                this.grdRoleOperation.DataSource = rop;
                if (bind)
                    this.grdRoleOperation.DataBind();
            }
            catch (Exception ex)
            {
                ErrorMsg.Message = ex.Message;
            }
        }
        protected void grdRoleOperation_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataSource(false);
        }

        protected void grdRoleOperation_PreRender(object sender, EventArgs e)
        {
            if (grdRoleOperation.EditItems.Count > 0)
            {
                foreach (GridDataItem item in grdRoleOperation.MasterTableView.Items)
                {
                    if (item != grdRoleOperation.EditItems[0])
                    {
                        item.Visible = false;
                    }
                }
            }
        }
        private void load_operation()
        {
            var Operation_data = (from v in ctx.Operations
                                  where (v.ModuleName == txtModuleName.SelectedValue)
                                 select v).ToList();

            rdOperation.DataSource = Operation_data;
            rdOperation.DataTextField = "OperationName";
            rdOperation.DataValueField = "OperationID";
            rdOperation.DataBind();
        }

        private void load_Role()
        {
            var role_data = (from v in ctx.Roles select v).ToList();
            rdRoles.DataSource = role_data;
            rdRoles.DataTextField = "ROLENAME";
            rdRoles.DataValueField = "ROLENAME";
            rdRoles.DataBind();
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            AntiMoney_UMEntities entity = new AntiMoney_UMEntities();
            int count = 0;
            int nocount = 0;
            if (String.IsNullOrEmpty(rdRoles.SelectedValue))
            {
                ErrorMsg.Message = "Please choose a valid Role!";
                return;
            }
            if (String.IsNullOrEmpty(rdAccesLeve.SelectedValue))
            {
                ErrorMsg.Message = "Please choose a Access Level!";
                return;
            }
            bool selectedlist = false;
            foreach (ListItem operItems in rdOperation.Items)
            {
                if (operItems.Selected)
                {
                    selectedlist = true;
                    break;
                }
            }
            if (!selectedlist)
            {
                ErrorMsg.Message = "Please select operations to be assigned!";
                return;
            }
            int rank = Convert.ToInt32(txtRank.Value);
            foreach (ListItem operItems in rdOperation.Items)
            {
                if (operItems.Selected)
                {
                    RoleOperation rop = new RoleOperation();
                    Guid opid = Guid.Parse(operItems.Value);
                    Model.Operation Operation = entity.Operations.Where(p =>  p.OperationID == opid).FirstOrDefault();                    
                    rop.Roles = rdRoles.SelectedValue;
                    rop.Rank = rank;
                    rop.Operation = Operation.Id;
                    rop.RoleOperationID = Guid.NewGuid();
                    rop.OperationID = Operation.OperationID;
                    rop.AccessLevel = Convert.ToInt32(rdAccesLeve.SelectedValue);
                    Model.RoleOperation ROperation = entity.RoleOperations.Where(p => p.Roles == rop.Roles && p.OperationID == rop.OperationID).FirstOrDefault();
                    try
                    {
                        if (ROperation == null)
                        {
                            entity.RoleOperations.Add(rop);
                            use.InserActionLog(DateTime.Now, User.Identity.Name, "Role permission assignment is Registered", "User updated Role permission assignment with ID:" + rop.Id);
                            entity.SaveChanges();
                            count++;

                        }
                        else
                        {
                            if (btnRegister.Text.Equals("Update"))
                            {
                                ROperation.Rank = int.Parse(txtRank.Text);
                                ROperation.AccessLevel = Convert.ToInt32(rdAccesLeve.SelectedValue);
                                entity.SaveChanges();
                                use.InserActionLog(DateTime.Now, User.Identity.Name, "Role permission assignment is Updated", "User updated Role permission assignment with ID:" + rop.Id);
                                MessageBox.Message = "Data is successfully Updated.";
                                return;
                            }
                            else
                            {
                                nocount++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg.Message = "Operation Failed. Role Operations assignement was not successful. " + ex.InnerException.InnerException;//clsLogFile.WriteLog("Error:", ex);
                        return;
                    }
                    rank++;
                }
            }
            if (count > 0 && nocount > 0)
            {
                MessageBox.Message = "Operation Succeeded. " + count + " Role Operations are assigned successfuly. AND The selected " + nocount + " Role Operations was already assigned.";
            }
            else if (count > 0)
            {
                MessageBox.Message = "Operation Succeeded. " + count + " Role Operations are assigned successfuly.";
            }
            else if (nocount > 0)
            {
                MessageBox.Message = "The selected " + nocount + " Role Operations was already registered.";
            }
            else
            {
                ErrorMsg.Message = "Operation Failed. No Assigned was made.";
            }
            DataSource(true);
        }
        protected void grdRoleOperation_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName.Equals("Fill"))
            {
                Guid Id = Guid.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["OperationID"].ToString());
                btnRegister.Text = "Update";
                var rop = (from l in ctx.VWRoleOperations where l.OperationID == Id select l).FirstOrDefault();
                if (rop != null)
                {
                    lblId.Text = rop.Id.ToString();
                    txtRank.Text = rop.Rank.ToString();
                    rdRoles.SelectedValue = rop.Roles.ToString();
                    rdAccesLeve.SelectedValue = rop.AccessLevel.ToString();
                    txtModuleName.SelectedValue = rop.ModuleName;
                    rdOperation.SelectedValue = rop.OperationID.ToString();
                    load_operation();

                }
            }
        }
        protected void btnClear_Click1(object sender, EventArgs e)
        {
            btnRegister.Text = "Register";
            txtRank.Text = "";
            rdAccesLeve.SelectedIndex = 0;
            txtModuleName.SelectedIndex = 0;
            rdOperation.Items.Clear();
        } 
        protected void rdRoles_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DataSource(true);
            load_operation();
        }

        protected void txtModuleName_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DataSource(true);
            load_operation();
        } 
    }
}