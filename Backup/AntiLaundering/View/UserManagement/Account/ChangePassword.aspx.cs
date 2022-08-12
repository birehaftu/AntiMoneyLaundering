using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using System.Text;
using System.Text.RegularExpressions;

namespace AntiLaundering.View.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        UManagement GetUCompany = new UManagement();
        UserAcLog use = new UserAcLog();
        MyUserManagement.SqlMembershipProvider smp = (MyUserManagement.SqlMembershipProvider)Membership.Provider;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["sec"] == null)
            {
                Response.Redirect("~/View/Index.aspx");
            }
        }

        protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            String password = NewPassword.Text; // Substitute with the user input string
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
                    //return;
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
            if (!String.IsNullOrEmpty(User.Identity.Name))
            {


                MyUserManagement.SqlMembershipProvider smp = (MyUserManagement.SqlMembershipProvider)Membership.Provider;
                if (!smp.ValidateUser(User.Identity.Name, CurrentPassword.Text))
                {
                    ErrorMsg.Message = "Current Password is not correct!";
                    return;
                }

                if (NewPassword.Text.Equals(ConfirmNewPassword.Text))
                {
                    bool result = smp.ChangePassword(User.Identity.Name, NewPassword.Text);
                    if (result)
                    {
                        use.InserActionLog(DateTime.Now, User.Identity.Name, "User Password is changed", "User Password changed:" + User.Identity.Name);
                        MessageBox.Message = "Password successfully updated!";
                    }
                    else
                        ErrorMsg.Message = "Password is not changed! Please refer to your software vendor!";
                }
                else
                {
                    ErrorMsg.Message = "Password is not confirmed!";
                }
            }
        }
    }
}
