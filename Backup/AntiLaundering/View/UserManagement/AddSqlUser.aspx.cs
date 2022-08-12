using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Resources;
using System.Reflection;
using MyUserManagement;
using AntiLaundering.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntiLaundering.Control;
using AntiLaundering.Control.ResourceManagement;
using AntiLaundering.Control.UserManagement;
using AntiLaundering.Control.ResourceManagement;
using System.Text;
using System.Text.RegularExpressions;

namespace AntiLaundering.View.UserManagement
{
    public enum PasswordScore
    {
        Blank = 0,
        VeryWeak = 2,
        Weak = 3,
        Medium = 4,
        Strong = 5,
        VeryStrong = 6
    }

    public class PasswordAdvisor
    {
        public static PasswordScore CheckStrength(string password)
        {
            int score = 0;

            if (password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length < 4)
                return PasswordScore.VeryWeak;

            if (password.Length >= 5)
                score = 1;
            if (password.Length >= 6)
                score++;
            if (Regex.IsMatch(password, @"[0-9]", RegexOptions.ECMAScript))
                score++;
            if (Regex.IsMatch(password, @"[A-Z]", RegexOptions.ECMAScript))
                score++;
            if (Regex.IsMatch(password, @"[a-z]", RegexOptions.ECMAScript))
                score++;
            if (Regex.IsMatch(password, @"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript))
                score++;

            return (PasswordScore)score;
        }
    }
    public partial class AddSqlUser : System.Web.UI.Page
    {

        ControlManagement cmgt = new ControlManagement();
        UserAcLog use = new UserAcLog();
        AntiMoney_UMEntities ctx = new AntiMoney_UMEntities();
        UManagement GetUCompany = new UManagement();
        MyUserManagement.SqlMembershipProvider smp = (MyUserManagement.SqlMembershipProvider)Membership.Provider;
        private void load_Company()
        {
            var comp_data = (from v in ctx.Companies select v).ToList();
            txtOwnerName.DataSource = comp_data;
            txtOwnerName.DataTextField = "CompanyName";
            txtOwnerName.DataValueField = "CompanyId";
            txtOwnerName.SelectedValue = GetUCompany.GetCompanyId(User.Identity.Name);
            txtOwnerName.Enabled = false;
            txtOwnerName.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                Response.Redirect("~/View/UserManagement/Account/Login.aspx");
            }
            if (Request.QueryString["sec"] == null)
            {
                Response.Redirect("~/View/Index.aspx");
            }
            load_Company();
            //LoadBranch();
            if (User.IsInRole("Super Admin"))
            {
                txtOwnerName.Enabled = true;
            }
            //cmgt.SetFilterText(RadGridUsers);
            //cmgt.FilterOptions(RadGridUsers);
        }

        protected void Page_Init()
        {
            // UserRoles.DataSource = Roles.GetAllRoles();
            //UserRoles.DataBind();
        }
        /// <summary>
        /// Page Pre_Render event
        /// Here a checkbox list used to show user roles are constructed.
        /// If the admin is filtering users by their role the gridview 
        /// that contains user list also re-binded 
        /// </summary>
        protected void Page_PreRender()
        {
            if (!IsPostBack)
            {
                cblUserRoles.DataSource = Roles.GetAllRoles();
                cblUserRoles.DataBind();
                // Bind these checkboxes to the User's own set of roles.
                string[] userRoles = Roles.GetRolesForUser(txtUsername.Text);
                foreach (string role in userRoles)
                {
                    ListItem checkbox = cblUserRoles.Items.FindByValue(role);
                    checkbox.Selected = true;
                }
                MembershipUserCollection allUsers = Membership.GetAllUsers();
                MembershipUserCollection filteredUsers = new MembershipUserCollection();
            }

        }

        /// <summary>
        /// This event passes object instances to actual user registering method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void LoadBranch()
        //{
        //    BranchManagement com = new BranchManagement();
        //    ComboBranch.DataTextField = "BranchName";
        //    ComboBranch.DataValueField = "BranchId";
        //    ComboBranch.DataSource = com.GetBranch();
        //    ComboBranch.DataBind();
        //}
        //private void Employee_Load()
        //{
        //    EmployeeInformationManagement com = new EmployeeInformationManagement();
        //    var list_data = new List<Model.VWEmployeeInformationBasic>();
        //    if (String.IsNullOrEmpty(ComboBranch.SelectedValue))
        //        list_data = com.GetEmployeeInformationAllByBranchAll();
        //    else
        //        list_data = com.GetEmployeeInformationAllByBranch(Guid.Parse(ComboBranch.SelectedValue));

        //    comboEmployee.Items.Clear();
        //    foreach (Model.VWEmployeeInformationBasic dataRow in list_data)
        //    {
        //        RadComboBoxItem item = new RadComboBoxItem();
        //        item.Text = dataRow.FullName;
        //        item.Value = dataRow.EmployeeId.ToString();
        //        String name = dataRow.FullName;
        //        String code = dataRow.EmployeeCode ?? "";
        //        item.Attributes.Add("FullName", name.ToString());
        //        item.Attributes.Add("EmployeeCode", code.ToString());
        //        comboEmployee.Items.Add(item);
        //        item.DataBind();
        //    }

        //    RadComboBoxItem item2 = new RadComboBoxItem();
        //    item2.Text = "select";
        //    item2.Value = "";
        //    String name2 = "select";
        //    String code2 = "";
        //    item2.Attributes.Add("FullName", name2.ToString());
        //    item2.Attributes.Add("EmployeeCode", code2.ToString());
        //    comboEmployee.Items.Insert(0, item2);
        //    item2.DataBind();
        //}
        //protected void comboEmployee_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        //{
        //    EmployeeInformationManagement com = new EmployeeInformationManagement();
        //    var list_data = new List<Model.VWEmployeeInformationBasic>();
        //    if (String.IsNullOrEmpty(ComboBranch.SelectedValue))
        //        list_data = com.GetEmployeeInformationAllByBranchAll();
        //    else
        //        list_data = com.GetEmployeeInformationAllByBranch(Guid.Parse(ComboBranch.SelectedValue));
        //    comboEmployee.Items.Clear();
        //    foreach (Model.VWEmployeeInformationBasic dataRow in list_data)
        //    {
        //        RadComboBoxItem item = new RadComboBoxItem();
        //        item.Text = dataRow.FullName;
        //        item.Value = dataRow.EmployeeId.ToString();
        //        String name = dataRow.FullName;
        //        String code = dataRow.EmployeeCode ?? "";
        //        item.Attributes.Add("FullName", name.ToString());
        //        item.Attributes.Add("EmployeeCode", code.ToString());
        //        comboEmployee.Items.Add(item);
        //        item.DataBind();
        //    }

        //    RadComboBoxItem item2 = new RadComboBoxItem();
        //    item2.Text = "select";
        //    item2.Value = "";
        //    String name2 = "select";
        //    String code2 = "";
        //    item2.Attributes.Add("FullName", name2.ToString());
        //    item2.Attributes.Add("EmployeeCode", code2.ToString());
        //    comboEmployee.Items.Insert(0, item2);
        //    item2.DataBind();
        //}

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(comboEmployee.Text))
            {
                //just letters and digits.
                ErrorMsg.Message = "Please provide or write full name.";
                return;
            }
            if (String.IsNullOrEmpty(txtUsername.Text))
            {
                //just letters and digits.
                ErrorMsg.Message = "Please enter a valid user name.";
                return;
            }
            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                //just letters and digits.
                ErrorMsg.Message = "Please enter a valid Password.";
                return;
            }
            if (String.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                //just letters and digits.
                ErrorMsg.Message = "Please enter Password Confirmation.";
                return;
            }
            if (!txtPassword.Text.Equals(txtConfirmPassword.Text))
            {
                ErrorMsg.Message = "Password Did not match. please re-enter again.";
                return;
            }
            if (txtUsername.Text.Length > 15)
            {
                ErrorMsg.Message = "Maximum User Name length should not exceed 15 characters!";
                return;
            }
            if (txtUsername.Text.Length < 5)
            {
                ErrorMsg.Message = "Minimum User Name length must be 5 characters!";
                return;
            }
            if (!txtUsername.Text.All(char.IsLetterOrDigit))
            {
                //just letters and digits.
                ErrorMsg.Message = "Username Must be letters and/or digits only.";
                return;
            }
            //if (String.IsNullOrEmpty(txtPalmIP.Text))
            //{
            //    //just letters and digits.
            //    ErrorMsg.Message = "Please provide a valid Device IP Address.";
            //    return;
            //}
            //if (!Regex.IsMatch(txtFullName.Text, @"^[a-zA-Z ]+$"))
            //{
            //    //just letters and digits.
            //    ErrorMsg.Message = "Full Name can be only letters and white space.";
            //    return;

            //}
            if (dpDOB.SelectedDate != null)
            {
                if (((DateTime.Now.Subtract(dpDOB.SelectedDate.Value)).TotalDays / 365.25) < 18)
                {
                    ErrorMsg.Message = "Can't Register User. User is under age.";
                    return;
                }
            }
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                //just letters and digits.
                ErrorMsg.Message = "Email Address can't be empty string.";
                return;
            }
            String password = txtPassword.Text; // Substitute with the user input string
            PasswordScore passwordStrengthScore = PasswordAdvisor.CheckStrength(password);

            switch (passwordStrengthScore)
            {
                case PasswordScore.Blank:
                    ErrorMsg.Message = "Password can't be Blank!";
                    return;
                case PasswordScore.VeryWeak:
                    ErrorMsg.Message = "Password is VeryWeak! Please make it small and capital characters, number and symbol with lengith of 6 and above!";
                    return;
                case PasswordScore.Weak:
                    //ErrorMsg.Message = "Password is Weak! Please make it small and capital characters, number and symbol with lengith of 6 and above!";
                   // return;
                case PasswordScore.Medium:
                    //ErrorMsg.Message = "Password is Good! Please make it small and capital characters and number with lengith of 6 and above!";
                    //return;
                case PasswordScore.Strong:
                //ErrorMsg.Message = "Password is Strong! Please make it small and capital characters, number and symbol with lengith of 6 and above!";
                //return;
                case PasswordScore.VeryStrong:
                    // Password deemed strong enough, allow user to be added to database etc
                    break;
            }
            MembershipCreateStatus status = MembershipCreateStatus.Success;
            //string emaila = ddlEmployee.SelectedItem.Value;
            bool approved = true;// (bool)cbIsapproved.Checked;
            smp.Address = txtAddress.Text;
            //smp.PalmIP = txtPalmIP.Text;
            if (dpDOB.SelectedDate == null)
            {
                ErrorMsg.Message = "Please fill users DOB";
                return;
            }
            if (String.IsNullOrEmpty(txtUsername.Text))
            {
                ErrorMsg.Message = "Please fill A Valid User Name!";
                return;
            }
            if (smp.UserExits(txtUsername.Text))
            {
                ErrorMsg.Message = "This username is already taken. Please use different username!";
                return;
            }
            if (!String.IsNullOrEmpty(comboEmployee.Text))
            {
                if (smp.EmployeeExits(comboEmployee.Text))
                {
                    ErrorMsg.Message = "This Employee is already have active user name taken. if you want to change the user you need to deactivate the previous username!";
                    return;
                }
            }
            smp.DOB = dpDOB.SelectedDate.Value;
            //smp.EmployeeId = txtEmployeeId.Text;
            smp.Address = txtAddress.Text;
            Boolean RoleSelected = false;
            foreach (ListItem rolebox in cblUserRoles.Items)
            {
                if (rolebox.Selected)
                {
                    RoleSelected = true;
                }
            }
            if (!RoleSelected)
            {
                ErrorMsg.Message = "Please select a role!";
                return;
            }

            //Exists exist = new Exists();
            //if (exist.UserExists(txtUsername.Text))
            //{
            //    ErrorMsg.Message = "A User is already registered with the given User Name!";
            //    return;
            //}
            smp.CompanyId = Guid.Parse(txtOwnerName.SelectedValue);
            smp.PalmIP = txtPalmIP.Text;
            smp.Department = ComboDepartment.SelectedValue;
            string empid ="";

            //if (!String.IsNullOrEmpty(comboEmployee.Text))
            //{
            //    empid = Guid.Parse(comboEmployee.Text);
            //    EmployeeInformationManagement empman = new EmployeeInformationManagement();
            //    var empinfo = empman.GetEmployeeInformationAllById(empid);

            //}
            MyCustomMembershipUser user = smp.CreateUser(comboEmployee.Text, txtUsername.Text, txtPassword.Text, this.txtEmail.Text.Trim(), txtComment.Text, txtPhone.Text, approved, null, empid, out status,txtPalmIP.Text,ComboDepartment.SelectedValue);

            if (user != null)
            {
                smp.UpdateUser(smp.GetUser(txtUsername.Text, false), txtPalmIP.Text, ComboDepartment.SelectedValue);
                foreach (ListItem rolebox in cblUserRoles.Items)
                {
                    if (rolebox.Selected)
                    {
                        Roles.AddUserToRole(txtUsername.Text, rolebox.Text);
                    }
                }
                this.txtEmail.Text = txtUsername.Text = String.Empty;
                use.InserActionLog(DateTime.Now, User.Identity.Name, "User Information is Registed", "User registered User Information with User Name:" + txtUsername);
                MessageBox.Message = "User has been successfuly registered";
                Clear();
            }
            else
            {
                ErrorMsg.Message = "Operation failed. User was not registered. " + status;
                //Inform user that user was not created"
            }
        }
        /// <summary>
        /// A method used to slice some portion of comment value if it is more than 
        /// 8 characters long.
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
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


            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            string txtUsername = (userControl.FindControl("txtUsername") as TextBox).Text;
            string txtPassword = (userControl.FindControl("txtPassword") as TextBox).Text;
            string txtPassowrdQuestion = (userControl.FindControl("txtPassowrdQuestion") as TextBox).Text;
            string txtPassowrdAnswer = (userControl.FindControl("txtPassowrdAnswer") as TextBox).Text;
            string txtComment = (userControl.FindControl("txtComment") as RadTextBox).Text;
            string txtPalmIP = (userControl.FindControl("txtPalmIP") as RadTextBox).Text;
            string combodepartment = (userControl.FindControl("ComboDepartment") as RadComboBox).SelectedValue;
            CheckBoxList cblUserRoles = (userControl.FindControl("cblUserRoles") as CheckBoxList);
            CheckBox cbIsapproved = (userControl.FindControl("cbIsapproved") as CheckBox);
            CheckBox cbIsLocked = (userControl.FindControl("cbIsLocked") as CheckBox);

            MyUserManagement.MyCustomMembershipUser user = (MyUserManagement.MyCustomMembershipUser)smp.GetUser(txtUsername, false);
            bool approved = (bool)cbIsapproved.Checked;
            user.Comment = (string)txtComment;
            user.PalmIp = (String)txtPalmIP;
            user.Department = (String)combodepartment;
            user.IsApproved = approved;
            char isap = (user.IsApproved) ? '1' : '0';

            try
            {
                smp.UpdateUser(user, (String)txtPalmIP,  (String)combodepartment);
                use.InserActionLog(DateTime.Now, User.Identity.Name, "User Information is Updated", "User updated User Information with User Name:" + txtUsername);
                MessageBox.Message = "Operation succeeded. User was updated";
                if (cbIsLocked.Checked == false)
                {
                    if (user.UnlockUser())
                    {
                        MessageBox.Message = "Operation succeeded. User was unlocked";

                    }

                }
                // Update user roles:
                foreach (ListItem rolebox in cblUserRoles.Items)
                {
                    if (rolebox.Selected)
                    {
                        if (!Roles.IsUserInRole(txtUsername, rolebox.Text))
                        {
                            Roles.AddUserToRole(txtUsername, rolebox.Text);
                            use.InserActionLog(DateTime.Now, User.Identity.Name, "User Role is Added", "User Role added with Role Name:" + rolebox.Text + " for User:" + txtUsername);
                            Clear();
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

            }
            catch (Exception ex)
            {
                //RadGridUsers.Controls.Add(new LiteralControl(ex.Message));
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
            //RadGridUsers.DataSource = Membership.GetAllUsers();
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
            use.InserActionLog(DateTime.Now, User.Identity.Name, "User Information is Deleted", "User Deleted User Information with User Name:" + username);
            Membership.DeleteUser(username, true);
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
        /// <summary>
        /// A user defined method neccessary for displaying windows alert
        /// </summary>
        /// <param name="txt">Message of the alert window</param>
        /// <param name="title">title of the alert window</param>
        private void ShowMessage(string txt, string title)
        {
            RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + txt + "\", 330, 100,\"" + title + "\")"));
        }

        protected void Clear()
        {
            this.txtEmail.Text = txtUsername.Text = String.Empty;
            txtAddress.Text = String.Empty;
            txtComment.Text = String.Empty;
            txtConfirmPassword.Text = String.Empty;
            txtEmail.Text = String.Empty;
            //comboEmployee.SelectedIndex = 0;
            txtPassword.Text = String.Empty;
            txtPhone.Text = String.Empty;
            txtUsername.Text = String.Empty;
            dpDOB.Clear();

        }

        protected void ComboBranch_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        
    }
}