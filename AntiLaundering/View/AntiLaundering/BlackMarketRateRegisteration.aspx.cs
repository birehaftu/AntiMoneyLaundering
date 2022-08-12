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
                Response.Redirect("~/View/um/Account/Login.aspx");
            }
            if (!IsPostBack)
            {
                dpRateDate.SelectedDate = DateTime.Now.Date;
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
            comboCountry.SelectedValue = info.Country;
            comboExchangeCode.SelectedValue = info.ExchangeCode;
            comboExchangeName.SelectedValue = info.ExchangeName;
            txtRateAmountInBirr.Text = info.RateAmountInBirr.ToString();
            txtBankRate.Text = info.BankRate.ToString();
            dpRateDate.SelectedDate = info.RateDate;
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
            comboExchangeName.SelectedValue = "";
            comboExchangeCode.SelectedValue = "";
            txtRateAmountInBirr.Text = "";
            comboCountry.SelectedValue = "";
            txtBankRate.Text = "";
            dpRateDate.SelectedDate = null;
        }
        protected void btn_Register_Click(object sender, EventArgs e)
        {
            Guid id = Guid.NewGuid();
            try
            {
                string str = "";
                if (String.IsNullOrEmpty(comboExchangeName.SelectedValue))
                {
                    str +="Exchange Name , ";
                }
                if (String.IsNullOrEmpty(comboExchangeCode.SelectedValue))
                {
                    str += "Exchange code , ";
                }
                if (String.IsNullOrEmpty(comboCountry.SelectedValue))
                {
                    str += "Country , ";
                }
                if (String.IsNullOrEmpty(txtBankRate.Text))
                {
                    str += "Bank Rate , ";
                }
                if (String.IsNullOrEmpty(txtRateAmountInBirr.Text))
                {
                    str += "Balck Rate , ";
                }
                if (dpRateDate.SelectedDate==null)
                {
                    str += "RateDate , ";
                }
                if (!String.IsNullOrEmpty(str))
                {
                    ErrorMsg.Message = str+" can't be empty!";
                    return;
                }
                BlackMarketRateManagement exist = new BlackMarketRateManagement();
                if (exist.BlackMarketRateExistsByBlackMarketRate(comboExchangeCode.SelectedValue,dpRateDate.SelectedDate.Value))
                {
                    ErrorMsg.Message = "A BlackMarketRate is already registered with the given BlackMarketRateName!";
                    return;
                }
                if (exist.InsertBlackMarketRate(id,comboExchangeName.SelectedValue,comboExchangeCode.SelectedValue,comboCountry.SelectedValue,dpRateDate.SelectedDate.Value,
                    decimal.Parse(txtRateAmountInBirr.Text), decimal.Parse(txtBankRate.Text),
                    User.Identity.Name,DateTime.Now))
                {
                    MessageBox.Message = "BlackMarketRate Information successfuly saved";
                    use.InserActionLog(DateTime.Now, User.Identity.Name, "BlackMarketRate Inserted ", "BlackMarketRate Insereted" + comboExchangeName.SelectedValue.ToString());
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
                if (String.IsNullOrEmpty(comboExchangeName.SelectedValue))
                {
                    str += "Exchange Name , ";
                }
                if (String.IsNullOrEmpty(comboExchangeCode.SelectedValue))
                {
                    str += "Exchange code , ";
                }
                if (String.IsNullOrEmpty(comboCountry.SelectedValue))
                {
                    str += "Country , ";
                }
                if (String.IsNullOrEmpty(txtBankRate.Text))
                {
                    str += "Bank Rate , ";
                }
                if (String.IsNullOrEmpty(txtRateAmountInBirr.Text))
                {
                    str += "Balck Rate , ";
                }
                if (dpRateDate.SelectedDate == null)
                {
                    str += "RateDate , ";
                }
                if (!String.IsNullOrEmpty(str))
                {
                    ErrorMsg.Message = str + " can't be empty!";
                    return;
                }
                BlackMarketRateManagement com = new BlackMarketRateManagement();
                if (com.BlackMarketRateExistsByExchangeNameUpdate(comboExchangeCode.SelectedValue, dpRateDate.SelectedDate.Value, id))
                {
                    ErrorMsg.Message = "A BlackMarketRate is already registered with the given BlackMarketRateName, use different BlackMarketRateName!";
                    return;
                }
                if (com.UpdateBlackMarketRate(id, comboExchangeName.SelectedValue, comboExchangeCode.SelectedValue, comboCountry.SelectedValue, dpRateDate.SelectedDate.Value,
                    decimal.Parse(txtRateAmountInBirr.Text), decimal.Parse(txtBankRate.Text),
                    User.Identity.Name, DateTime.Now))
                {
                    MessageBox.Message = "BlackMarketRate Information Updated";
                    use.InserActionLog(DateTime.Now, User.Identity.Name, "BlackMarketRate Updated ", "BlackMarketRate Updated" + comboExchangeName.SelectedValue.ToString());
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

        protected void comboExchangeName_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            comboExchangeCode.SelectedIndex = comboExchangeName.SelectedIndex;
            comboCountry.SelectedIndex = comboExchangeName.SelectedIndex;
        }

        protected void comboExchangeCode_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            comboExchangeName.SelectedIndex= comboExchangeCode.SelectedIndex  ;
            comboCountry.SelectedIndex = comboExchangeCode.SelectedIndex;
        }

        protected void comboCountry_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            comboExchangeName.SelectedIndex = comboCountry.SelectedIndex;
            comboExchangeCode.SelectedIndex = comboCountry.SelectedIndex;
        }
    }
}