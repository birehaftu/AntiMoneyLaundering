using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Resources;
using System.Reflection;
using MyUserManagement;
using AntiLaundering.Control.UserManagement; using AntiLaundering.Control.ResourceManagement;
using AntiLaundering.Model;
using AntiLaundering.Control.ResourceManagement;

namespace AntiLaundering.View.UserManagement
{
    public partial class listusers : System.Web.UI.Page
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
       
        MyUserManagement.SqlMembershipProvider smp = (MyUserManagement.SqlMembershipProvider)Membership.Provider;
        ControlManagement cmgt = new ControlManagement();
        UserRegiManagement um = new UserRegiManagement();

        protected void Page_Init()
        {
            UserRoles.DataSource = Roles.GetAllRoles();
            UserRoles.DataBind();
        }
        protected void Page_PreRender()
        {

        }

       
        protected string TrimRemark(object dataItem)
        {
            string Commentval = DataBinder.Eval(dataItem, "Comment").ToString();
            if (Commentval.Length > 8)
            {
                return string.Concat(Commentval.Substring(0, 8), "...");
            }
            return Commentval;
        }
        /// <summary>
        /// This event calls and passes object instances to actual user updater method.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RadGridUsers_UpdateCommand(object source, GridCommandEventArgs e)
        {
            AntiMoney_UMEntities userEnt = new AntiMoney_UMEntities();
            User fineUser = new Model.User();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            string txtUsername = (userControl.FindControl("txtUsername") as TextBox).Text;
            string txtEmail = (userControl.FindControl("txtEmail") as TextBox).Text;
            string txtFullName = (userControl.FindControl("txtFullName") as TextBox).Text;
            string txtComment = (userControl.FindControl("txtComment") as TextBox).Text;
            string txtAddress = (userControl.FindControl("txtAddress") as TextBox).Text;
            string txtPhone = (userControl.FindControl("txtPhone") as RadTextBox).Text;
            DateTime dbDOD = DateTime.Now.AddYears(-18);
            if((userControl.FindControl("dpDOB") as RadDatePicker).SelectedDate!=null)
                dbDOD = (userControl.FindControl("dpDOB") as RadDatePicker).SelectedDate.Value;
            RadioButtonList cblUserRoles = (userControl.FindControl("cblUserRoles") as RadioButtonList);
            CheckBox cbIsLocked = (userControl.FindControl("cbIsLocked") as CheckBox);

            MyUserManagement.MyCustomMembershipUser user = (MyUserManagement.MyCustomMembershipUser)smp.GetUser(txtUsername, false);
            fineUser=userEnt.Users.Find(txtUsername.ToString());
            fineUser.EMAIL = txtEmail.ToString();
            fineUser.Address = txtAddress;
            fineUser.DOB = dbDOD;
            fineUser.Description = txtComment;
            //fineUser.PalmIP = txtPalmIP;
            fineUser.Phone = txtPhone;
            //fineUser.FullName = txtFullName.ToString();
            fineUser.ISLOCKEDOUT = cbIsLocked.Checked;
            int x=userEnt.SaveChanges();
            //if (!user.IsApproved && approved)
            //{
            //    if (!User.IsInRole("Main Registrar"))
            //    {
            //        ShowMessage("Only main registrar can approve users","Notification");
            //        //ErrorMsg1.Message = "Only main registrar can approve users";
            //        return;

            //    }
            //}
            
            //user.IsApproved = approved;
            char isap = (user.IsApproved) ? '1' : '0';

            try
            {
                // Update user info: 

                //smp.UpdateUser(user,txtFullName,txtEmployeeID);
                if (cbIsLocked.Checked == false)
                {
                    user.UnlockUser();
                    use.InserActionLog(DateTime.Now, User.Identity.Name, "User Information is Unlocked", "User Unlocked User Information with User Name:" + txtUsername);
                }
                else
                {
                    use.InserActionLog(DateTime.Now, User.Identity.Name, "User Information is Locked", "User Locked User Information with User Name:" + txtUsername);
                    
                }
                // Update user roles:
                foreach (ListItem rolebox in cblUserRoles.Items)
                {
                    if (rolebox.Selected)
                    {
                        if (!Roles.IsUserInRole(txtUsername, rolebox.Text))
                        {
                            Roles.AddUserToRole(txtUsername, rolebox.Text);
                            use.InserActionLog(DateTime.Now, User.Identity.Name, "User Role is Added", "User Role added with Role Name:" + rolebox.Text+ " for User:"+txtUsername);
                        }
                    }
                    else
                    {
                        if (Roles.IsUserInRole(txtUsername, rolebox.Text))
                        {
                            Roles.RemoveUserFromRole(txtUsername, rolebox.Text);
                            use.InserActionLog(DateTime.Now, User.Identity.Name, "User Role is Removed", "User Role Removed with Role Name:" + rolebox.Text + " for User:" + txtUsername);
                        }
                    }
                }

                if (x > 0)
                    MessageBox1.Message = "User is successfully Updated!";
                else
                    ErrorMsg1.Message = "User is not updated no change is made!";
            }
            catch (Exception ex)
            {
                //RadGridUsers.Controls.Add(new LiteralControl());
                ErrorMsg1.Message = ex.Message;
            }
            //ShowMessage(rm.GetString("AfterUpdate"), rm.GetString("AfterSuccessTitle"));
        }
        /// <summary>
        /// An event used by gridview to load users data whenever it is needed.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RadGridUsers_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (String.IsNullOrEmpty(UserRoles.SelectedValue))
                RadGridUsers.DataSource = um.getAllUsers();//um.getAllUsers();Membership.GetAllUsers();
            else
                RadGridUsers.DataSource = um.getAllUsers(UserRoles.SelectedValue);//um.getAllUsers();Membership.GetAllUsers();
        }
        /// <summary>
        /// An event used for deleting users
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RadGridUsers_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            string username = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["USERNAME"].ToString();
            if (!Membership.DeleteUser(username, true))
            {
                ErrorMsg1.Message = "User is not deleted. If user have accessed the system before it can only be blocked!";
                return;
            }
            else
            {
                use.InserActionLog(DateTime.Now, User.Identity.Name, "User Information is Deleted", "User Deleted User Information with User Name:" + username);
                MessageBox1.Message = "User is succesfully deleted!";
                return;
            }
        }
        /// <summary>
        /// Returns converted date from GC to EC
        /// </summary>
        /// <param name="dataItem">Data Item object which is gridview row</param>
        /// <param name="column">Column name on the gridview</param>
        /// <returns></returns>
        protected string ConvertedDate(object dataItem, string column)
        {
            DateTime DateTobeConverted = DateTime.Parse(DataBinder.Eval(dataItem, column).ToString());
            return cmgt.ConvertFromGCtoEC(DateTobeConverted.Day, DateTobeConverted.Month - 1, DateTobeConverted.Year);

        }

        protected void UserRoles_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if(String.IsNullOrEmpty(UserRoles.SelectedValue))
                RadGridUsers.DataSource = um.getAllUsers();//um.getAllUsers();Membership.GetAllUsers();
            else
                RadGridUsers.DataSource = um.getAllUsers(UserRoles.SelectedValue);//um.getAllUsers();Membership.GetAllUsers();
            RadGridUsers.DataBind();
        }

        protected void RadGridUsers_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
        

        /// <summary>
        /// A user defined method neccessary for displaying windows alert
        /// </summary>
        /// <param name="txt">Message of the alert window</param>
        /// <param name="title">title of the alert window</param>


    }
}