using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using HohomaERP.Control.UserManagement; using HohomaERP.Control.ResourceManagement;
using MyUserManagement;
namespace HohomaERP.View.Account
{
    public partial class Aboutus : System.Web.UI.Page
    {
        MyUserManagement.SqlMembershipProvider smp = (MyUserManagement.SqlMembershipProvider)Membership.Provider;
        protected global::System.Web.UI.WebControls.TextBox UserName;
        protected global::System.Web.UI.WebControls.TextBox Password;
        protected global::System.Web.UI.WebControls.Login log;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["LoginId"] = null;
            Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            TextBox txtUser = LoginUser.FindControl("UserName") as TextBox;
            Button btnlogin= LoginUser.FindControl("LoginButton") as Button;
            RadCheckBox chremember = LoginUser.FindControl("RememberMe") as RadCheckBox;
            TextBox txtPassword = LoginUser.FindControl("Password") as TextBox;
            if (!IsPostBack)
            {
                if (Request.Cookies["RestTigrayCookie"] != null)
                {
                    HttpCookie cookie = Request.Cookies.Get("RestTigrayCookie");
                    txtUser.Text = cookie.Values["username"].ToString();
                    //txtPassword.Text = cookie.Values["password"].ToString();
                    txtPassword.Attributes.Add("value", cookie.Values["password"].ToString());
                    chremember.Checked = true;
                    //chremember.Attributes.Add("Checked", "true");
                    this.SetFocus(btnlogin);
                }
                else
                    this.SetFocus(txtUser);
            }
           
           // RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {

            UserRegiManagement userMng = new UserRegiManagement();
            if (userMng.UserExists(this.LoginUser.UserName.ToString()))
            {
                userMng.updateUserLogIn(this.LoginUser.UserName.ToString(), DateTime.Now);
            }
            RadCheckBox rm = (RadCheckBox)LoginUser.FindControl("RememberMe");
            TextBox txtUser = LoginUser.FindControl("UserName") as TextBox;
            TextBox txtPassword = LoginUser.FindControl("Password") as TextBox;
            if (rm.Checked==true)
            {
                HttpCookie myCookie = new HttpCookie("RestTigrayCookie");
                Response.Cookies.Remove("RestTigrayCookie");
                Response.Cookies.Add(myCookie);
                myCookie.Values.Add("username", txtUser.Text);
                myCookie.Values.Add("password", txtPassword.Text);
                DateTime dtExpiry = DateTime.Now.AddDays(15); //you can add years and months too here
                Response.Cookies["myCookie"].Expires = dtExpiry;
            }
            else
            {
                HttpCookie myCookie = new HttpCookie("RestTigrayCookie");
                Response.Cookies.Remove("RestTigrayCookie");
                Response.Cookies.Add(myCookie);
                myCookie.Values.Add("username", txtUser.Text);
                myCookie.Values.Add("password", txtPassword.Text);
                DateTime dtExpiry = DateTime.Now.AddSeconds(1); //you can add years and months too here
                Response.Cookies["RestTigrayCookie"].Expires = dtExpiry;


            }
        }
        protected void LoginUser_LoginError(object sender, EventArgs e)
        {
            // Determine why the user could not login...
            LoginUser.FailureText = "Your login attempt was not successful. Please try again.";

            // Does there exist a User account for this user?
            TextBox txtUser = LoginUser.FindControl("UserName") as TextBox;
            MyCustomMembershipUser usrInfo = (MyCustomMembershipUser)smp.GetUser(txtUser.Text, true);
            if (usrInfo != null)
            {
                // Is this user locked out?
                if (usrInfo.IsLockedOut || usrInfo.FAILEDPASSWORDATTEMPTCOUNT==5)
                {
                    LoginUser.FailureText = "Your account has been locked out because of too many invalid login attempts or you have been terminated from employment. Please contact the administrator to have your account unlocked.";
                }
                else if (!usrInfo.IsApproved)
                {
                    LoginUser.FailureText = "Your account has not yet been approved. You cannot login until an administrator has approved your account.";
                }
                else if (usrInfo.FAILEDPASSWORDATTEMPTCOUNT > 0)
                {
                    //if(usrInfo.FAILEDPASSWORDATTEMPTCOUNT>=5)
                        LoginUser.FailureText = "Your login was not successful. Your account will be blocked after "+(5- usrInfo.FAILEDPASSWORDATTEMPTCOUNT) + " trials.";
                    //else
                        //LoginUser.FailureText = "Your login was not successful. Your account will be locked you have exceeded maximum allowed trials.";


                }
            }
        }

    }
}
