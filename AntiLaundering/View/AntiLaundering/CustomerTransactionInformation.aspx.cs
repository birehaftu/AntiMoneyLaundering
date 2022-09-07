using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using AntiLaundering.Control;
using AntiLaundering.Control.UserManagement;
using AntiLaundering.Control.ResourceManagement;
using AntiLaundering.Control.AntiLaundering;
using System.IO;

namespace AntiLaundering.View.AntiLaundering
{
    public partial class CustomerTransactInformation : System.Web.UI.Page
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
                if (Request.QueryString["sec"] == null)
                {
                    Response.Redirect("~/View/Index.aspx");
                }
                load_CustomerTransactInfo(true);
            }
        }
        private void AccesCheck()
        {
            UManagement isInRole = new UManagement();
            int? accessLevel = isInRole.GetAccessLeve("~/View/AntiLaundering/CustomerTransactInformation.aspx", User.Identity.Name);
           
        }
        protected void grdCustomerTransactInfo_PreRender(object sender, EventArgs e)
        {
            if (grdCustomerTransactInfo.EditItems.Count > 0)
            {
                foreach (GridDataItem item in grdCustomerTransactInfo.MasterTableView.Items)
                {
                    if (item != grdCustomerTransactInfo.EditItems[0])
                    {
                        item.Visible = false;
                    }
                }
            }
        }
        protected void grdCustomerTransactInfo_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridItem)
            {
                var lbl = e.Item.FindControl("lblpnum") as Label;
                if (lbl != null)
                {
                    lbl.Text = (e.Item.ItemIndex + 1).ToString();
                }
            }
        }
        protected void load_CustomerTransactInfo(Boolean bind)
        {
            CustomerTransactManagement com = new CustomerTransactManagement();
            grdCustomerTransactInfo.DataSource = com.GetCustomerTransact();
            if(bind)
                grdCustomerTransactInfo.DataBind();
            AccesCheck();
        }
        protected void grdCustomerTransactInfo_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Guid Id = Guid.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CustomerTransactID"].ToString());
            try
            {
                CustomerTransactManagement com = new CustomerTransactManagement();
                if (com.DeleteCustomerTransact(Id))
                {
                    load_CustomerTransactInfo(true);
                    MessageBox1.Message = "CustomerTransact Information Deleted";
                    use.InserActionLog(DateTime.Now, User.Identity.Name, "CustomerTransact Deleted ", "CustomerTransact Deleted" + ID.ToString());
                }
                else
                {
                    ErrorMsg1.Message = "This CustomerTransact is used as foreign key to other informations! Fisrt delete those informations to delete this CustomerTransact!";
                }
            }

            catch (Exception ex)
            {
                //MessagePanel1.Visible = true;
                ErrorMsg1.Message = ex.Message;

            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            load_CustomerTransactInfo(true);
        }

        protected void grdCustomerTransactInfo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            load_CustomerTransactInfo(false);
        }

        protected void DownloadTemplate(object sender, EventArgs e)
        {
            try
            {
                string FilePath = "~\\View\\Download\\CustomerTransact.xlsx";
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(FilePath));
                Response.WriteFile(FilePath);
                Response.End();
            }
            catch (Exception ex)
            {
                ErrorMsg1.Message = "Please Check the excel file. Either it is openned or doesnot found";
                return;
            }
        }

        protected void ImportBookInfo(object sender, EventArgs e)
        {
            try
            {
                if (Path.GetExtension(FileUpload1.FileName) == ".xlsx" || Path.GetExtension(FileUpload1.FileName) == ".xls")
                {
                    if (FileUpload1.PostedFile.ContentLength > 1000e+7)
                    {
                        ErrorMsg1.Message = "It is not allowed to upload A file size more than 10,000 Mega Byte!";
                        return;
                    }
                    if ((this.FileUpload1.PostedFile != null) && (this.FileUpload1.PostedFile.ContentLength > 0))
                    {

                        String path = System.IO.Path.GetFileName(this.FileUpload1.PostedFile.FileName);
                        String saveLocation = Server.MapPath("~/View/Temp") + "\\" + path;
                        path = "~/View/Temp/" + path;
                        this.FileUpload1.PostedFile.SaveAs(saveLocation);

                        ImportFromExcel transact = new ImportFromExcel();
                        string message = transact.ImportCustomerTransactFromExcel(saveLocation, User.Identity.Name);
                        if (message.Contains("registered succefully."))
                        {
                            MessageBox1.Message = message;
                            load_CustomerTransactInfo(true);
                        }
                        else
                        {
                            ErrorMsg1.Message = message;
                            return;
                        }
                        if (File.Exists(saveLocation))
                        {
                            try
                            {
                                System.IO.File.Delete(saveLocation);
                            }
                            catch (Exception ex)
                            {
                                return;
                            }
                        }

                    }
                    else
                    {
                        ErrorMsg1.Message = "Please upload a file";
                        return;
                    }
                }
                else
                {
                    ErrorMsg1.Message = "Please select  excel file with extension .xlsx or .xls";
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMsg1.Message = "Please Check the excel file";
                return;
            }
        }
    }
}