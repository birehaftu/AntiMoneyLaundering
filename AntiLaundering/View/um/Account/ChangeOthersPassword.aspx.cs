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
    public partial class ChangeOthersPassword : System.Web.UI.Page
    {
        UManagement GetUCompany = new UManagement();
        UserAcLog use = new UserAcLog();
        MyUserManagement.SqlMembershipProvider smp = (MyUserManagement.SqlMembershipProvider)Membership.Provider;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["sec"] == null)
                {
                    Response.Redirect("~/View/Index.aspx");
                }
                var comp = Guid.Parse(GetUCompany.GetCompanyId(User.Identity.Name));
                var listuser = GetUCompany.getAllUser(comp);
                if (listuser != null)
                {
                    txtUsers.DataSource = listuser;
                    //txtUsers.DataTextField = "FullName";
                    txtUsers.DataTextField = "UserName";
                    txtUsers.DataValueField = "UserName";
                    txtUsers.DataBind();
                }
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
                // return;
                case PasswordScore.VeryStrong:
                    // Password deemed strong enough, allow user to be added to database etc
                    break;
            }
            if (!String.IsNullOrEmpty(txtUsers.Text))
            {


                MyUserManagement.SqlMembershipProvider smp = (MyUserManagement.SqlMembershipProvider)Membership.Provider;
                if ( NewPassword.Text.Equals(ConfirmNewPassword.Text))
                {
                    bool result = smp.ChangePassword(txtUsers.SelectedValue, NewPassword.Text);
                    if (result)
                    {
                        use.InserActionLog(DateTime.Now, User.Identity.Name, "User Password is changed", "User Password changed:" + txtUsers.SelectedValue);
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
