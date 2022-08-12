using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AntiLaundering.Control.Report.ReportLib;
using AntiLaundering.Control.ResourceManagement;
using AntiLaundering.Control.UserManagement;
using AntiLaundering.Control.UserManagement;

namespace AntiLaundering.View.Reports.ReportManagement
{
    public partial class UserLogInReportUI : System.Web.UI.Page
    {
        UserLogInReport useractRep = new UserLogInReport();
        UserRegiManagement userMng = new UserRegiManagement();
        UserAcLog use = new UserAcLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["sec"] == null)
                {
                    Response.Redirect("~/View/Index.aspx");
                }
                RadButtonPreview.Visible = true;
                RadComboRange.Visible = true;
                RadComboType.Visible = true;
                RadComboUserName.Visible = false;
                RadDateFrom.Visible = false;
                RadDateTo.Visible = true;
                DateTime dateTo = DateTime.Now;
                RadDateTo.SelectedDate = DateTime.Now; ;
                RadDateFrom.SelectedDate = DateTime.Now; 
                DateTime dateFrom = dateTo.AddDays(-365);
                useractRep.table1.DataSource = use.getAllLogInRange(dateTo, dateFrom);
                ReportViewerActionLog.ReportSource = useractRep;
            }

        }
        public void loadUser()
        {
            RadComboUserName.DataSource = userMng.getAllUsers();
            RadComboUserName.DataTextField = "USERNAME";
            RadComboUserName.DataValueField = "USERNAME";
            RadComboUserName.DataBind();
        }
        protected void RadComboType_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadComboType.SelectedValue.ToString() == "All")
            {
                RadButtonPreview.Visible = true;
                RadComboRange.Visible = true;
                RadComboType.Visible = true;
                RadComboUserName.Visible = false;
                RadDateFrom.Visible = false;
                RadDateTo.Visible = true;
            }
            if (RadComboType.SelectedValue.ToString() == "User")
            {
                RadButtonPreview.Visible = true;
                RadComboRange.Visible = true;
                RadComboType.Visible = true;
                RadComboUserName.Visible = true ;
                RadDateFrom.Visible = false;
                RadDateTo.Visible = true;
                loadUser();
            }

        }

        protected void RadComboRange_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadComboRange.SelectedValue.ToString() == "Custom")
            {
                RadDateFrom.Visible = true;
                RadDateTo.Visible = true;
            }
            else
            {
                RadDateTo.Visible = true;
            }
        }

        protected void RadButtonPreview_Click(object sender, EventArgs e)
        {
            ReportViewerActionLog.ReportSource = null;
            UserAcLog use = new UserAcLog();
            if (RadDateTo.SelectedDate != null)
            {
                if (RadDateTo.SelectedDate.Value > DateTime.Now)
                {
                    ErrorMsg1.Message = "End date should be less or equal to Current date!";
                    ReportViewerActionLog.ReportSource = null;
                    return;
                }
            }
            
            if (RadDateFrom.SelectedDate != null && RadDateTo.SelectedDate != null)
            {
                if (RadDateTo.SelectedDate.Value < RadDateFrom.SelectedDate.Value)
                {
                    ErrorMsg1.Message = "Start date should be less or equal to end date!";
                    ReportViewerActionLog.ReportSource = null;
                    return;
                }
            }
            use.InserActionLog(DateTime.Now, User.Identity.Name, "User Logged In Report Generated", "By Type:" + RadComboType.SelectedValue);
             
            if (RadComboType.SelectedValue.ToString() == "All")
            {
                if (RadComboRange.SelectedValue.ToString() == "Daily")
                {
                    if (!string.IsNullOrEmpty(RadDateTo.SelectedDate.ToString()))
                    {
                        DateTime dateTo = RadDateTo.SelectedDate.Value;
                        useractRep.table1.DataSource = use.getAllLogInDaily(dateTo);
                        ReportViewerActionLog.ReportSource = useractRep;
                    }
                }
                if (RadComboRange.SelectedValue.ToString() == "Weekly")
                {
                    if (!string.IsNullOrEmpty(RadDateTo.SelectedDate.ToString()))
                    {
                        DateTime dateTo = RadDateTo.SelectedDate.Value;
                        DateTime dateFrom = dateTo.AddDays(-7);
                        useractRep.table1.DataSource = use.getAllLogInRange(dateTo, dateFrom);
                        ReportViewerActionLog.ReportSource = useractRep;
                    }
                }
                if (RadComboRange.SelectedValue.ToString() == "Monthly")
                {
                    if (!string.IsNullOrEmpty(RadDateTo.SelectedDate.ToString()))
                    {
                        DateTime dateTo = RadDateTo.SelectedDate.Value;
                        DateTime dateFrom = dateTo.AddDays(-30);
                        useractRep.table1.DataSource = use.getAllLogInRange(dateTo, dateFrom);
                        ReportViewerActionLog.ReportSource = useractRep;
                    }
                }
                if (RadComboRange.SelectedValue.ToString() == "Anually")
                {
                    if (!string.IsNullOrEmpty(RadDateTo.SelectedDate.ToString()))
                    {
                        DateTime dateTo = RadDateTo.SelectedDate.Value;
                        DateTime dateFrom = dateTo.AddDays(-365);
                        useractRep.table1.DataSource = use.getAllLogInRange(dateTo, dateFrom);
                        ReportViewerActionLog.ReportSource = useractRep;
                    }
                }
                if (RadComboRange.SelectedValue.ToString() == "Custom")
                {
                    if (!string.IsNullOrEmpty(RadDateTo.SelectedDate.ToString()))
                    {
                        if (!string.IsNullOrEmpty(RadDateFrom.SelectedDate.ToString()))
                        {
                            DateTime dateTo = RadDateTo.SelectedDate.Value;
                            DateTime dateFrom = RadDateFrom.SelectedDate.Value;
                            useractRep.table1.DataSource = use.getAllLogInRange(dateTo, dateFrom);
                            ReportViewerActionLog.ReportSource = useractRep;
                        }
                    }
                }
            }
            if (RadComboType.SelectedValue.ToString() == "User")
            {
                if (RadComboRange.SelectedValue.ToString() == "Daily")
                {
                    if (!string.IsNullOrEmpty(RadDateTo.SelectedDate.ToString()))
                    {
                        if (!string.IsNullOrEmpty(RadComboUserName.SelectedValue.ToString()))
                        {
                            DateTime dateTo = RadDateTo.SelectedDate.Value;
                            useractRep.table1.DataSource = use.getAllLogInDailyByUser(dateTo, RadComboUserName.SelectedValue.ToString());
                            ReportViewerActionLog.ReportSource = useractRep;
                        }
                    }
                }
                if (RadComboRange.SelectedValue.ToString() == "Weekly")
                {
                    if (!string.IsNullOrEmpty(RadDateTo.SelectedDate.ToString()))
                    {
                        if (!string.IsNullOrEmpty(RadComboUserName.SelectedValue.ToString()))
                        {
                            DateTime dateTo = RadDateTo.SelectedDate.Value;
                            DateTime dateFrom = dateTo.AddDays(-7);
                            useractRep.table1.DataSource = use.getAllLogInRangeByUser(dateTo, dateFrom, RadComboUserName.SelectedValue.ToString());
                            ReportViewerActionLog.ReportSource = useractRep;
                        }
                    }
                }
                if (RadComboRange.SelectedValue.ToString() == "Monthly")
                {
                    if (!string.IsNullOrEmpty(RadDateTo.SelectedDate.ToString()))
                    {
                        if (!string.IsNullOrEmpty(RadComboUserName.SelectedValue.ToString()))
                        {
                            DateTime dateTo = RadDateTo.SelectedDate.Value;
                            DateTime dateFrom = dateTo.AddDays(-30);
                            useractRep.table1.DataSource = use.getAllLogInRangeByUser(dateTo, dateFrom, RadComboUserName.SelectedValue.ToString());
                            ReportViewerActionLog.ReportSource = useractRep;
                        }
                    }
                }
                if (RadComboRange.SelectedValue.ToString() == "Anually")
                {
                    if (!string.IsNullOrEmpty(RadDateTo.SelectedDate.ToString()))
                    {
                        if (!string.IsNullOrEmpty(RadComboUserName.SelectedValue.ToString()))
                        {
                            DateTime dateTo = RadDateTo.SelectedDate.Value;
                            DateTime dateFrom = dateTo.AddDays(3657);
                            useractRep.table1.DataSource = use.getAllLogInRangeByUser(dateTo, dateFrom, RadComboUserName.SelectedValue.ToString());
                            ReportViewerActionLog.ReportSource = useractRep;
                        }
                    }
                }
                if (RadComboRange.SelectedValue.ToString() == "Custom")
                {
                    if (!string.IsNullOrEmpty(RadDateTo.SelectedDate.ToString()))
                    {
                        if (!string.IsNullOrEmpty(RadDateFrom.SelectedDate.ToString()))
                        {
                            if (!string.IsNullOrEmpty(RadComboUserName.SelectedValue.ToString()))
                            {
                                DateTime dateTo = RadDateTo.SelectedDate.Value;
                                DateTime dateFrom = RadDateFrom.SelectedDate.Value;
                                useractRep.table1.DataSource = use.getAllLogInRangeByUser(dateTo, dateFrom, RadComboUserName.SelectedValue.ToString());
                                ReportViewerActionLog.ReportSource = useractRep;
                            }
                        }
                    }
                }
            }
        }
    }
}