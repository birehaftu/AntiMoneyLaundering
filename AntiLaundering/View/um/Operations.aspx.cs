using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using AntiLaundering.Model;
using AntiLaundering.Control.UserManagement; using AntiLaundering.Control.ResourceManagement;
using AntiLaundering.Control.ResourceManagement;

namespace AntiLaundering.View.UserManagement
{
    public partial class Operations : System.Web.UI.Page
    {
        UserAcLog use = new UserAcLog();
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
       

        protected void grdSubsystem_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //VehicleInfoCLS SubSystem = new VehicleInfoCLS();            
            var editedItem = e.Item as GridEditableItem;
            int strId = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
            AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
            var ope = (from l in entities.SubSystems
                       where l.Id == strId
                       select l).FirstOrDefault();
            var upload = (editedItem.FindControl("ULogo") as RadUpload);
            if (upload.InvalidFiles.Count > 0)
            {
                errormsg1.Message = "Invalid Image Extension type. Supported Image Types are .jpeg,.jpg,.png,.tiff,.bit,.bmp,.dib.";
                return;
            }
            if (String.IsNullOrEmpty(ope.SubSystemName))
            {
                errormsg1.Message = "Please choose a valid Module!";
                return;
            }
            if (upload.UploadedFiles.Count > 0)
            {
                ope.Logo = upload.UploadedFiles[0].GetName();
                upload.TargetFolder = Server.MapPath("~/View/images/SubSystemLogo/");
            }
            else
            {
                ope.Logo = "";
                //errormsg1.Message = "Please choose a valid Module!";
                //return;
            }
            ope.Description = (editedItem.FindControl("txtDescription") as TextBox).Text;
            ope.SubSystemName = (editedItem.FindControl("txtSubSystemName") as TextBox).Text;
            try
            {
                entities.SaveChanges();
                use.InserActionLog(DateTime.Now, User.Identity.Name, "Sub System is Updated", "User Deleted Sub System with Sub System Name:" + ope.SubSystemName);
                MessageBox1.Message = "Operation Succeeded. Subsytem successfuly Updated!";
            }
            catch (Exception ex)
            {
                errormsg1.Message = "Operation Failed. Subsytem was not saved" + ex.Message;//clsLogFile.WriteLog("Error:", ex);
            }
        }

        protected void grdSubsystem_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            AntiMoney_UMEntities fleep = new AntiMoney_UMEntities();
            try
            {
                List<SubSystem> operat = new List<SubSystem>();
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var oper = from l in entities.SubSystems
                           orderby l.SubSystemName
                           select l;
                SubSystem op;
                foreach (var l in oper)
                {
                    op = new SubSystem();
                    op.Id = l.Id;
                    op.SubSystemName = l.SubSystemName;
                    //op.Logo = l.SubSystemName;
                    op.Description = l.Description;
                    operat.Add(op);
                }
                this.grdSubsystem.DataSource = operat;
            }
            catch (Exception ex)
            {
                //MessageBox1.Message = ex.Message;
            }
        }

        protected void grdSubsystem_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
        protected void grdSubsystem_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //VehicleInfoCLS SubSystem = new VehicleInfoCLS();
            AntiMoney_UMEntities fleep = new AntiMoney_UMEntities();
            SubSystem ope = new SubSystem();
            var editedItem = e.Item as GridEditableItem;
            ope.SubSystemName = ((editedItem.FindControl("txtSubSystemName") as TextBox).Text);
            var upload=(editedItem.FindControl("ULogo") as RadUpload);
            if (upload.InvalidFiles.Count > 0)
            {
                errormsg1.Message = "Invalid Image Extension type. Supported Image Types are .jpeg,.jpg,.png,.tiff,.bit,.bmp,.dib.";
                return;
            }
            if (String.IsNullOrEmpty(ope.SubSystemName))
            {
                errormsg1.Message = "Please choose a valid Module!";
                return;
            }
            if (upload.UploadedFiles.Count > 0)
            {
                ope.Logo = upload.UploadedFiles[0].GetName();
                upload.TargetFolder = Server.MapPath("~/View/images/SubSystemLogo/");
            }
            else
            {
                ope.Logo = "";
                //errormsg1.Message = "Please choose a valid Module!";
                //return;
            }
            ope.Description = (editedItem.FindControl("txtDescription") as TextBox).Text;
            try
            {
                UManagement exist = new UManagement();
                if (exist.SubSytemExists(ope.SubSystemName))
                {
                    errormsg1.Message = "An Subsytem is already registered with the given Subsytem Name!";
                    return;
                }
                fleep.SubSystems.Add(ope);
                fleep.SaveChanges();
                use.InserActionLog(DateTime.Now, User.Identity.Name, "Sub System is Registered", "User Deleted Sub System with Sub System Name:" + ope.SubSystemName);
                MessageBox1.Message = "SubSystem Succeeded. Subsytem successfuly Saved";
            }
            catch (Exception ex)
            {
                errormsg1.Message = "SubSystem Failed. Subsytem was not saved" + ex.Message;//clsLogFile.WriteLog("Error:", ex);
            }
        }
        protected void grdSubsystem_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //VehicleInfoCLS SubSystem = new VehicleInfoCLS();
            AntiMoney_UMEntities fleep = new AntiMoney_UMEntities();
            SubSystem veh = new SubSystem();
            try
            {
                int strId = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                //Guid GUId = Guid.Parse(strId);
                veh.Id = strId;
                Model.SubSystem Opedelete = fleep.SubSystems.Where(p => p.Id == veh.Id).FirstOrDefault();
                fleep.SubSystems.Remove(Opedelete);
                fleep.SaveChanges();
                use.InserActionLog(DateTime.Now, User.Identity.Name, "Sub System is Registered", "User Deleted Sub System with Sub System ID:" + strId);
                MessageBox1.Message = "Operation Succeeded. Subsytem successfuly Deleted";

            }
            catch (Exception ex)
            {
                errormsg1.Message = "Operation Failed. Subsytem not successfuly Deleted" + ex.Message;//clsLogFile.WriteLog("Error:", ex);
            }
        }




        protected void grdModules_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //VehicleInfoCLS Operation = new VehicleInfoCLS();            
            var editedItem = e.Item as GridEditableItem;
            int strId = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
            AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
            var ops = (from l in entities.Operations
                       where l.Id == strId
                       select l).FirstOrDefault();
            if ((editedItem.FindControl("txtSubSystemName") as RadComboBox).SelectedValue.Equals("Please Select"))
            {
                errormsg1.Message = "Please choose a valid Subsytem!";
                return;
            }
            //ops.SubSystem = Convert.ToInt32(((editedItem.FindControl("txtSubSystemName") as RadComboBox).SelectedValue).ToString());
            ops.ModuleName = ((editedItem.FindControl("txtModuleName") as TextBox).Text);

            ops.OperationName = ((editedItem.FindControl("txtModuleName") as TextBox).Text);
            ops.OperationLink = "";
            ops.Description = (editedItem.FindControl("txtDescription") as TextBox).Text;
            try
            {
                entities.SaveChanges();
                use.InserActionLog(DateTime.Now, User.Identity.Name, "Module is updated", "User updated Module with Module Name:" + ops.ModuleName);
                MessageBox1.Message = "Operation Succeeded. Module successfuly Updated!";
            }
            catch (Exception ex)
            {
                errormsg1.Message = "Operation Failed. Module was not saved" + ex.Message;//clsLogFile.WriteLog("Error:", ex);
            }
        }

        protected void grdModules_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            try
            {
                List<VWModuleSubsystem> operat = new List<VWModuleSubsystem>();
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var oper = from l in entities.VWModuleSubsystems
                           where String.IsNullOrEmpty(l.OperationLink)
                           orderby l.Id
                           select l;
                VWModuleSubsystem op;
                foreach (var l in oper)
                {
                    op = new VWModuleSubsystem();
                    op.Id = l.Id;
                    op.ModuleName = l.ModuleName;
                    op.SubSystemName = l.SubSystemName;
                    op.OperationName = l.OperationName;
                    op.Description = l.Description;
                    operat.Add(op);
                }
                this.grdModules.DataSource = operat;
            }
            catch (Exception ex)
            {
                //MessageBox1.Message = ex.Message;
            }
        }

        protected void grdModules_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
        protected void grdModules_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //VehicleInfoCLS Operation = new VehicleInfoCLS();
            AntiMoney_UMEntities fleep = new AntiMoney_UMEntities();
            Operation ope = new Operation();
            var editedItem = e.Item as GridEditableItem;
            if ((editedItem.FindControl("txtSubSystemName") as RadComboBox).SelectedValue.Equals("Please Select"))
            {
                errormsg1.Message = "Please choose a valid Subsytem!";
                return;
            }
            IndexUserMgnt man = new IndexUserMgnt();
            ope.SubSystem = man.getSubSystemByName(((editedItem.FindControl("txtSubSystemName") as RadComboBox).SelectedValue).ToString());
            ope.SubSystemID = man.getSubSystemIDByName(((editedItem.FindControl("txtSubSystemName") as RadComboBox).SelectedValue).ToString());
            ope.ModuleName = ((editedItem.FindControl("txtModuleName") as TextBox).Text);
            ope.OperationName = ((editedItem.FindControl("txtModuleName") as TextBox).Text);
            ope.OperationLink = "";
            ope.Description = (editedItem.FindControl("txtDescription") as TextBox).Text;
            //Operation.OperationName = (editedItem.FindControl("txtOperationName") as TextBox).Text;
            try
            {
                UManagement exist = new UManagement();
                if (exist.OperationExists(ope.OperationName))
                {
                    errormsg1.Message = "An Module is already registered with the given Module Name!";
                    return;
                }
                ope.OperationID = Guid.NewGuid();
                fleep.Operations.Add(ope);
                fleep.SaveChanges();
                use.InserActionLog(DateTime.Now, User.Identity.Name, "Module is Registered", "User Registered Module with Module Name:" + ope.ModuleName);
                MessageBox1.Message = "Operation Succeeded. Module successfuly Saved";
            }
            catch (Exception ex)
            {
                errormsg1.Message = "Operation Failed. Module was not saved" + ex.Message;//clsLogFile.WriteLog("Error:", ex);
            }
        }
        protected void grdModules_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //VehicleInfoCLS Operation = new VehicleInfoCLS();
            AntiMoney_UMEntities fleep = new AntiMoney_UMEntities();
            Operation veh = new Operation();
            try
            {
                int strId = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                //Guid GUId = Guid.Parse(strId);
                veh.Id = strId;
                Model.Operation Opedelete = fleep.Operations.Where(p => p.Id == veh.Id).FirstOrDefault();
                fleep.Operations.Remove(Opedelete);
                fleep.SaveChanges();
                use.InserActionLog(DateTime.Now, User.Identity.Name, "Module is Deleted", "User Deleted Module with Module ID:" + strId.ToString());
                MessageBox1.Message = "Operation Succeeded. Module successfuly Deleted";

            }
            catch (Exception ex)
            {
                errormsg1.Message = "Operation Failed. Module is used by users currently. It can't be deleted!";// +ex.Message;//clsLogFile.WriteLog("Error:", ex);
            }
        }


        protected void grdOperation_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //VehicleInfoCLS Operation = new VehicleInfoCLS();            
            var editedItem = e.Item as GridEditableItem;
            Guid strId = Guid.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["OperationID"].ToString());
            AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
            var ops = (from l in entities.Operations
                       where l.OperationID == strId
                       select l).FirstOrDefault();
            ops.ModuleName = ((editedItem.FindControl("txtModuleName") as RadComboBox).Text);
            if (String.IsNullOrEmpty(ops.ModuleName))
            {
                errormsg1.Message = "Please choose module Name!";
                return;
            }
            ops.OperationName = ((editedItem.FindControl("txtOperationName") as TextBox).Text);
            ops.OperationLink = (editedItem.FindControl("txtOperationLink") as TextBox).Text;
            ops.Description = (editedItem.FindControl("txtDescription") as TextBox).Text;
            try
            {
                entities.SaveChanges();
                use.InserActionLog(DateTime.Now, User.Identity.Name, "Operation/SubModule is updated", "User updated Operation/SubModule with Operation Name:" + ops.OperationName);
                MessageBox1.Message = "Operation Succeeded. Operation successfuly Updated!";
            }
            catch (Exception ex)
            {
                errormsg1.Message = "Operation Failed. Operation was not saved" + ex.Message;//clsLogFile.WriteLog("Error:", ex);
            }
        }

        protected void grdOperation_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            AntiMoney_UMEntities fleep = new AntiMoney_UMEntities();
            try
            {
                List<Operation> operat = new List<Operation>();
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var oper = from l in entities.Operations
                           where !String.IsNullOrEmpty(l.OperationLink)
                           orderby l.Id
                           select l;
                Operation op;
                foreach (var l in oper)
                {
                    op = new Operation();
                    op.Id = l.Id;
                    op.ModuleName = l.ModuleName;
                    op.OperationID = l.OperationID;
                    op.OperationLink = l.OperationLink;
                    op.OperationName = l.OperationName;
                    op.Description = l.Description;
                    operat.Add(op);
                }
                this.grdOperation.DataSource = operat;
            }
            catch (Exception ex)
            {
                //MessageBox1.Message = ex.Message;
            }
        }

        protected void grdOperation_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
        protected void grdOperation_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //VehicleInfoCLS Operation = new VehicleInfoCLS();
            AntiMoney_UMEntities fleep = new AntiMoney_UMEntities();
            Operation ope = new Operation();
            var editedItem = e.Item as GridEditableItem;
            ope.ModuleName = ((editedItem.FindControl("txtModuleName") as RadComboBox).Text);
            if (String.IsNullOrEmpty(ope.ModuleName))
            {
                errormsg1.Message = "Please choose module Name!";
                return;
            }
            ope.OperationName = (editedItem.FindControl("txtOperationName") as TextBox).Text;
            ope.OperationLink = ope.OperationLink = (editedItem.FindControl("txtOperationLink") as TextBox).Text;
            ope.Description = (editedItem.FindControl("txtDescription") as TextBox).Text;
            
            //Operation.OperationName = (editedItem.FindControl("txtOperationName") as TextBox).Text;
            try
            {
                UManagement exist = new UManagement();
                if (exist.OperationExists(ope.OperationName))
                {
                    errormsg1.Message = "A Submodule is already registered with the given Submodule Name!";
                    return;
                }
                Guid sub=exist.getSubSytemByName(ope.ModuleName);
                if (sub!=null && sub!=Guid.Empty)
                    ope.SubSystemID = sub;
                ope.OperationID = Guid.NewGuid();
                fleep.Operations.Add(ope);
                fleep.SaveChanges();
                use.InserActionLog(DateTime.Now, User.Identity.Name, "Operation/SubModule is Registered", "User Deleted Operation/SubModule with Operation Name:" + ope.OperationName);
                MessageBox1.Message = "Operation Succeeded. Operation successfuly Saved";
            }
            catch (Exception ex)
            {
                errormsg1.Message = "Operation Failed. Operation was not saved" + ex.Message;//clsLogFile.WriteLog("Error:", ex);
            }
        }
        protected void grdOperation_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //VehicleInfoCLS Operation = new VehicleInfoCLS();
            AntiMoney_UMEntities fleep = new AntiMoney_UMEntities();
            Operation veh = new Operation();
            try
            {
                Guid strId = Guid.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["OperationID"].ToString());
                //Guid GUId = Guid.Parse(strId);
                veh.OperationID = strId;
                Model.Operation Opedelete = fleep.Operations.Where(p => p.OperationID == veh.OperationID).FirstOrDefault();
                fleep.Operations.Remove(Opedelete);
                fleep.SaveChanges();
                use.InserActionLog(DateTime.Now, User.Identity.Name, "Operation/SubModule is Deleted", "User Registered Operation/SubModule with Operation ID:" + strId.ToString());
                MessageBox1.Message = "Operation Succeeded. Operation successfuly Deleted";

            }
            catch (Exception ex)
            {
                errormsg1.Message = "Operation Failed.  Operation is used by users currently. It can't be deleted!";// +ex.Message;//clsLogFile.WriteLog("Error:", ex);
            }
        }
    }
}