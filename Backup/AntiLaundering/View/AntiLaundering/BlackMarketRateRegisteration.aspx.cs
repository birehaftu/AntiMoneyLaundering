using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using AntiLaundering.Control;
using AntiLaundering.Control.ResourceManagement;
using AntiLaundering.Control.AntiLaundering;
using System.Net;
using System.IO;

namespace AntiLaundering.View.AntiLaundering
{
    public partial class BlackMarketRateRegisteration : System.Web.UI.Page
    {
        UserAcLog use = new UserAcLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                Response.Redirect("~/View/UserManagement/Account/Login.aspx");
            }
            if (!IsPostBack)
            {
                String type = "";
                if (Request.QueryString["gt"] != null)
                {
                    type = Convert.ToString(Request.QueryString["gt"]);
                    if (type.Equals("update") && Request.QueryString["compId"]!=null)
                    {
                        Register_Controls(false);
                        Update_Controls(true);
                        lbltext.Text = "BlackMarketRate Update";
                        FillControls(Guid.Parse(Request.QueryString["compId"].ToString()));
                    }
                    else
                    {
                        Register_Controls(true);
                        Update_Controls(false);
                    }
                }
                else
                {
                    if (Request.QueryString["sec"] == null)
                    {
                        Response.Redirect("~/View/Index.aspx");
                    }
                    Register_Controls(true);
                    Update_Controls(false);
                }
                
            }
            //Telerik.Web.UI.WindowBehaviors.Maximize
        }
      
        private void FillControls(Guid ID)
        {
            BlackMarketRateManagement com = new BlackMarketRateManagement();
            var info = com.GetBlackMarketRateByID(ID);//
            txtBlackMarketRateID.Text=info.BlackMarketRateID.ToString();
            txtBlackMarketRateName.Text = info.BlackMarketRateName;
            txtBlackMarketRateName_AM.Text = info.BlackMarketRateName_AM;
            txtBlackMarketRateName_OR.Text = info.BlackMarketRateName_OR;
            txtBlackMarketRateName_TG.Text = info.BlackMarketRateName_TG;
            txtBlackMarketRateName_SM.Text = info.BlackMarketRateName_SM;
            txtBlackMarketRateName_AF.Text = info.BlackMarketRateName_AF;
            txtAccount.Text= info.Account;
            txtCategory.Text = info.Category;
            txtCategory_AM.Text = info.Category_AM;
            txtCategory_OR.Text = info.Category_OR;
            txtCategory_TG.Text = info.Category_TG;
            txtCategory_SM.Text = info.Category_SM;
            txtCategory_AF.Text = info.Category_AF;
            Register_Controls(false);
            Update_Controls(true);
        }
        private void Update_Controls(Boolean value)
        {
            btn_Update.Enabled = value;
        }

        private void Register_Controls(Boolean value)
        {
            btn_Register.Enabled = value;
        }
        public void clearField()
        {
            txtBlackMarketRateID.Text = "";
            txtBlackMarketRateName.Text = "";
            txtBlackMarketRateName_AM.Text = "";
            txtBlackMarketRateName_OR.Text = "";
            txtBlackMarketRateName_TG.Text = "";
            txtBlackMarketRateName_SM.Text = "";
            txtBlackMarketRateName_AF.Text = "";
            txtAccount.Text = "";
            txtCategory.Text = "";
            txtCategory_AM.Text = "";
            txtCategory_OR.Text = "";
            txtCategory_TG.Text = "";
            txtCategory_SM.Text = "";
            txtCategory_AF.Text = "";
        }
        protected void btn_Register_Click(object sender, EventArgs e)
        {
            Guid id = Guid.NewGuid();
            try
            {
                string str = "";
                if (String.IsNullOrEmpty(txtBlackMarketRateName.Text))
                {
                    str +="BlackMarketRate Name , ";
                }
                if (String.IsNullOrEmpty(txtCategory.Text))
                {
                    str += "Category , ";
                }
                if (String.IsNullOrEmpty(txtAccount.Text))
                {
                    str += "Account , ";
                }
                if (!String.IsNullOrEmpty(str))
                {
                    ErrorMsg.Message = str+" can't be empty!";
                    return;
                }
                BlackMarketRateManagement exist = new BlackMarketRateManagement();
                if (exist.BlackMarketRateExistsByBlackMarketRateName(txtBlackMarketRateName.Text))
                {
                    ErrorMsg.Message = "A BlackMarketRate is already registered with the given BlackMarketRateName!";
                    return;
                }
                if (exist.InsertBlackMarketRate(id,txtBlackMarketRateName.Text,
                    txtBlackMarketRateName_AM.Text, txtBlackMarketRateName_OR.Text, txtBlackMarketRateName_TG.Text, txtBlackMarketRateName_SM.Text, txtBlackMarketRateName_AF.Text,
                    DateTime.Now,txtAccount.Text,User.Identity.Name, txtCategory.Text
                    , txtCategory_AM.Text, txtCategory_OR.Text, txtCategory_TG.Text, txtCategory_SM.Text, txtCategory_AF.Text))
                {
                    MessageBox.Message = "BlackMarketRate Information successfuly saved";
                    use.InserActionLog(DateTime.Now, User.Identity.Name, "BlackMarketRate Inserted ", "Thirdparth Insereted" + txtBlackMarketRateName.Text.ToString());
                    clearField();
                }
                else
                {
                    ErrorMsg.Message = "BlackMarketRate Information is not registered!";
                }
            }

            catch (Exception ex)
            {
                ErrorMsg.Message = ex.Message;
            }
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                Guid id = Guid.Parse(txtBlackMarketRateID.Text);

                string str = "";
                if (String.IsNullOrEmpty(txtBlackMarketRateName.Text))
                {
                    str += "BlackMarketRate Name , ";
                }
                if (String.IsNullOrEmpty(txtCategory.Text))
                {
                    str += "Category , ";
                }
                if (String.IsNullOrEmpty(txtAccount.Text))
                {
                    str += "Account , ";
                }
                BlackMarketRateManagement com = new BlackMarketRateManagement();
                if (com.BlackMarketRateExistsByBlackMarketRateNameUpdate(txtBlackMarketRateName.Text,id))
                {
                    ErrorMsg.Message = "A BlackMarketRate is already registered with the given BlackMarketRateName, use different BlackMarketRateName!";
                    return;
                }
                if (com.UpdateBlackMarketRate(id, txtBlackMarketRateName.Text,
                    txtBlackMarketRateName_AM.Text, txtBlackMarketRateName_OR.Text, txtBlackMarketRateName_TG.Text, txtBlackMarketRateName_SM.Text, txtBlackMarketRateName_AF.Text,
                    DateTime.Now, txtAccount.Text, User.Identity.Name, txtCategory.Text
                    , txtCategory_AM.Text, txtCategory_OR.Text, txtCategory_TG.Text, txtCategory_SM.Text, txtCategory_AF.Text))
                {
                    MessageBox.Message = "BlackMarketRate Information Updated";
                    use.InserActionLog(DateTime.Now, User.Identity.Name, "BlackMarketRate Updated ", "BlackMarketRate Updated" + txtBlackMarketRateName.Text.ToString());
                    //clearField();
                }
                else
                {
                    ErrorMsg.Message = "BlackMarketRate Information is not Updated";
                }
            }
            catch (Exception ex)
            {
                ErrorMsg.Message = ex.Message;
            }
        }
    }
}