using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;
using MyUserManagement;
namespace AntiLaundering.View.UserManagement
{
    public partial class RestoreBackup : System.Web.UI.Page
    {
        MyUserManagement.SqlMembershipProvider smp = (MyUserManagement.SqlMembershipProvider)Membership.Provider;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["sec"] == null)
                {
                    Response.Redirect("~/View/Index.aspx");
                }
                if (User.IsInRole("admin") || User.IsInRole("Admin") || User.IsInRole("Administrator"))
                {
                    btnRestore.Visible = true;
                }
                else
                {
                    btnRestore.Visible = false;
                }
                FillDatabases();
                ReadBackupFiles();
            }
        }

        private void FillDatabases()
        {
            try
            {
                string _ConnectionString = smp.connstr;
                SqlConnection sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = _ConnectionString;
                sqlConnection.Open();
                string sqlQuery = "SELECT * FROM sys.databases";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                ddlDatabases.DataSource = dataSet.Tables[0];
                ddlDatabases.DataTextField = "name";
                ddlDatabases.DataValueField = "name";
                ddlDatabases.DataBind();
                if (dataSet.Tables.Count > 0)
                {
                    ddlDatabases.SelectedValue = "HR";
                    ddlDatabases.Text = "HR";
                }
            }
            catch (SqlException sqlException)
            {
                lblMessage.Text = sqlException.Message.ToString();
            }
            catch (Exception exception)
            {
                lblMessage.Text = exception.Message.ToString();
            }
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string _ConnectionString = smp.connstr;
                string _DatabaseName = ddlDatabases.SelectedValue.ToString();
                string _BackupName = _DatabaseName + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".bak";

                SqlConnection sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = _ConnectionString;
                sqlConnection.Open();
                string sqlQuery = "BACKUP DATABASE " + _DatabaseName + " TO DISK = '" + Server.MapPath("~/View/um/Backup/") + "" + _BackupName + "' WITH FORMAT, MEDIANAME = 'Z_SQLServerBackups', NAME = '" + _BackupName + "';";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                int iRows = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                lblMessage.Text = "The " + _DatabaseName + " database Backup with the name " + _BackupName + " successfully...";
                ReadBackupFiles();
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script language=javascript>window.location.href = 'Backup/" + _BackupName + "';</script>");

            }
            catch (SqlException sqlException)
            {
                lblMessage.Text = sqlException.Message.ToString();
            }
            catch (Exception exception)
            {
                lblMessage.Text = exception.Message.ToString();
            }
        }

        private void ReadBackupFiles()
        {
            try
            {
                if (!Directory.Exists(@Server.MapPath("~/View/um/Backup/")))
                {
                    Directory.CreateDirectory(@Server.MapPath("~/View/um/Backup/"));
                }

                string[] files = Directory.GetFiles(@Server.MapPath("~/View/um/Backup/"), "*.bak");
                if (files.Length > 0)
                {
                    lstBackupfiles.DataSource = files;
                    lstBackupfiles.DataBind();
                    lstBackupfiles.SelectedIndex = 0;
                }
            }
            catch (Exception exception)
            {
                lblMessage.Text = exception.Message.ToString();
            }
        }


        protected void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstBackupfiles.SelectedIndex == null || String.IsNullOrEmpty(lstBackupfiles.SelectedIndex.ToString()))
                {
                    lblMessage.Text = "There is no selected file to be restored.";
                    return;
                    //lblMessage.Style=
                }
                if (lstBackupfiles.SelectedIndex > 0)
                {
                    string _ConnectionString = smp.connstr;
                    string _DatabaseName = ddlDatabases.SelectedItem.Text.ToString();
                    string _BackupName = lstBackupfiles.SelectedItem.Text.ToString();

                    SqlConnection sqlConnection = new SqlConnection();
                    sqlConnection.ConnectionString = _ConnectionString;
                    sqlConnection.Open();
                    string sqlQuery = "RESTORE DATABASE " + _DatabaseName + " FROM DISK ='" + _BackupName + "' WITH REPLACE";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlCommand.CommandType = CommandType.Text;
                    int iRows = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    MessageBox.Message = "The " + _DatabaseName + " database restored with the name " + _BackupName + " successfully...";
                }
            }
            catch (SqlException sqlException)
            {
               ErrorMsg.Message = sqlException.Message.ToString();
            }
            catch (Exception exception)
            {
                ErrorMsg.Message = exception.Message.ToString();
            }
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (upload.InvalidFiles.Count > 0)
            {
                ErrorMsg.Message = "Invalid File Extension type. Supported File Types are .bak or .BAK Only.";
                return;
            }
            else
            {
                if (upload.UploadedFiles.Count > 0)
                {
                    string fpath = "~/View/um/Backup/" + upload.UploadedFiles[0].GetName();
                    upload.UploadedFiles[0].SaveAs(Server.MapPath(fpath), true);
                    ReadBackupFiles();
                    MessageBox.Message = "File is successfully uploaded";
                }
                else
                {
                    ErrorMsg.Message = "Please choose file to upload or You can restore from the available list.";
                    return;
                }
            }

        }

    }
}
