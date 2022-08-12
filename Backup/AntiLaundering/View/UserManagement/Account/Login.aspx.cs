using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using AntiLaundering.Control.UserManagement; using AntiLaundering.Control.ResourceManagement;
using MyUserManagement;
using System.Net;
using System.IO;
namespace AntiLaundering.View.Account
{
    public partial class Login : System.Web.UI.Page
    {
        MyUserManagement.SqlMembershipProvider smp = (MyUserManagement.SqlMembershipProvider)Membership.Provider;
        protected global::System.Web.UI.WebControls.TextBox UserName;
        protected global::System.Web.UI.WebControls.TextBox Password;
        protected global::System.Web.UI.WebControls.Login log;
        string ip = "", port = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ip = HttpContext.Current.Request.Url.Host;//["LOCAL_ADDR"];
            port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            Session["LoginId"] = null;
            Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            TextBox txtUser = LoginUser.FindControl("UserName") as TextBox;
            Button btnlogin = LoginUser.FindControl("LoginButton") as Button;
            RadCheckBox chremember = LoginUser.FindControl("RememberMe") as RadCheckBox;
            TextBox txtPassword = LoginUser.FindControl("Password") as TextBox;
            if (!IsPostBack)
            {
                if (Request.Cookies["INFONANSCookie"] != null)
                {
                    HttpCookie cookie = Request.Cookies.Get("INFONANSCookie");
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
            CompanyManagement com= new CompanyManagement();
            var info = com.GetCompany();
            if (info != null && info.Count > 0) {
                if (!String.IsNullOrEmpty(info[0].CompanyLogo) && testRequest("http://" + ip + ":" + port + info[0].CompanyLogo.Remove(0, 1)))
                {
                    Image2.ImageUrl = info[0].CompanyLogo;
                }
                else
                {
                    if (!String.IsNullOrEmpty(info[0].Photo))
                    {
                        string photo = "";
                        string fpath = info[0].CompanyId.ToString() + ".jpg";
                        string folder = "~/View/images/CompanyLogo/" ;
                        bool exists = System.IO.Directory.Exists(Server.MapPath(folder));
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(folder));
                        }
                        string imagePath = folder + fpath;
                        // To copy a file to another location and 
                        // overwrite the destination file if it already exists.
                        photo = folder + fpath;
                        System.Drawing.Image img = Base64ToImage(info[0].Photo);
                        img.Save(Server.MapPath(photo), System.Drawing.Imaging.ImageFormat.Jpeg);
                        Image2.ImageUrl = photo;
                    }
                }
            }
             
        }

        public string ImageToBase64(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Convert Image to byte[]
                    image.Save(ms, format);
                    byte[] imageBytes = ms.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public System.Drawing.Image Base64ToImage(string base64String)
        {
            try
            {
                // Convert Base64 String to byte[]
                byte[] imageBytes = Convert.FromBase64String(base64String);
                MemoryStream ms = new MemoryStream(imageBytes, 0,
                  imageBytes.Length);

                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                return image;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private static byte[] ConvertHexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
        private bool testRequest(string urlToCheck)
        {
            var wreq = (HttpWebRequest)WebRequest.Create(urlToCheck);

            //wreq.KeepAlive = true;
            wreq.Method = "HEAD";

            HttpWebResponse wresp = null;

            try
            {
                wresp = (HttpWebResponse)wreq.GetResponse();

                return (wresp.StatusCode == HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                //System.Diagnostics.Debug.WriteLine(String.Format("url: {0} not found", urlToCheck));
                return false;
            }
            finally
            {
                if (wresp != null)
                {
                    wresp.Close();
                }
            }
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
            if (rm.Checked == true)
            {
                HttpCookie myCookie = new HttpCookie("INFONANSCookie");
                Response.Cookies.Remove("INFONANSCookie");
                Response.Cookies.Add(myCookie);
                myCookie.Values.Add("username", txtUser.Text);
                myCookie.Values.Add("password", txtPassword.Text);
                DateTime dtExpiry = DateTime.Now.AddDays(15); //you can add years and months too here
                Response.Cookies["myCookie"].Expires = dtExpiry;
            }
            else
            {
                HttpCookie myCookie = new HttpCookie("INFONANSCookie");
                Response.Cookies.Remove("INFONANSCookie");
                Response.Cookies.Add(myCookie);
                myCookie.Values.Add("username", txtUser.Text);
                myCookie.Values.Add("password", txtPassword.Text);
                DateTime dtExpiry = DateTime.Now.AddSeconds(1); //you can add years and months too here
                Response.Cookies["INFONANSCookie"].Expires = dtExpiry;


            }
        }
        protected void LoginUser_LoginError(object sender, EventArgs e)
        {
            // Determine why the user could not login...
            //LoginUser.FailureText = "Your login attempt was not successful. Please try again.";

            // Does there exist a User account for this user?
            TextBox txtUser = LoginUser.FindControl("UserName") as TextBox;
            MyCustomMembershipUser usrInfo = (MyCustomMembershipUser)smp.GetUser(txtUser.Text, true);
            if (usrInfo != null)
            {
                // Is this user locked out?
                if (usrInfo.IsLockedOut || usrInfo.FAILEDPASSWORDATTEMPTCOUNT >= 10)
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
                    LoginUser.FailureText = "Your login was not successful. Your account will be blocked after " + (10 - usrInfo.FAILEDPASSWORDATTEMPTCOUNT) + " trials.";
                    //else
                    //LoginUser.FailureText = "Your login was not successful. Your account will be locked you have exceeded maximum allowed trials.";


                }
            }
            else
            {
                LoginUser.FailureText = "Your login attempt was not successful. Please try again.";
            }
        }

    }
}
