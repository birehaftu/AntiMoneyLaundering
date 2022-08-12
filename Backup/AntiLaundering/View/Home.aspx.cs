using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AntiLaundering.Control.UserManagement;

namespace AntiLaundering.View
{
    public partial class Home : System.Web.UI.Page
    {
        IndexUserMgnt man = new IndexUserMgnt();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LoginId"] == null)
                    Response.Redirect("~/View/UserManagement/Account/Login.aspx");
                else
                {
                    Response.ClearHeaders();
                    Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                    Response.AddHeader("Pragma", "no-cache");
                }
                FillNodes();
            }
        }
        protected void FillNodes()
        {
            try
            {
                String Uname = User.Identity.Name;
                //get modules by username
                var rop = man.getOperationsByUserName(User.Identity.Name);
                int i = 0;
                foreach (var l in rop)
                {
                    string modulename = l.OperationName;
                    string description = l.Description;
                    System.Web.UI.HtmlControls.HtmlTableRow row = new System.Web.UI.HtmlControls.HtmlTableRow();
                    System.Web.UI.HtmlControls.HtmlTableCell cell = new System.Web.UI.HtmlControls.HtmlTableCell();
                    //cell.InnerHtml = "<fieldset id=\"Fieldset1\"><b>" + modulename + "</b> </fieldset>";
                    ////cell.BorderColor = "blue";
                    //cell.Width = "40%";
                    //row.Cells.Add(cell);
                    System.Web.UI.HtmlControls.HtmlTableCell cellDescrition = new System.Web.UI.HtmlControls.HtmlTableCell();
                    cellDescrition.InnerHtml = "<b>" + modulename + ": </b>" + description + " <br>";
                    row.Cells.Add(cellDescrition);
                    tblMainHome.Rows.Add(row);


                }
            }
            catch (Exception ex)
            {
                errormsg1.Message = "Erro:" + ex.Message;
                return;
            }

        }

    }
}