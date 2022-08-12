using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using AntiLaundering.Control;
using AntiLaundering.Control.ResourceManagement;
using System.Net;
using System.IO;

namespace AntiLaundering.View.ResourceManagement
{
    public partial class CompanyRegisteration : System.Web.UI.Page
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
                        lbltext.Text = "Company Update";
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

        private void FillControls(Guid ID)
        {
            CompanyManagement com = new CompanyManagement();
            var info = com.GetCompanyById(ID);//
            txtCompanyId.Text=info.CompanyId.ToString();
            txtWoreda.Text = info.Woreda;
            txtTele.Text = info.Tel;
            txtFax.Text = info.Fax;
            txtMobile.Text = info.Mobile;
            txtManagerName.Text = info.ManagerName;
            txtDescription.Text = info.Descrbition;
            txtCity.Text = info.City;
            txtSubCity.Text = info.SubCity;
            txtPOBOX.Text = info.SubCity;
            txtCompanyName.Text = info.CompanyName;
            //txtCompanyName.Enabled = false;
            //txtNumberOfVehicles.Text = info.NumberOfVehicles.ToString();
            comboComanyType.SelectedValue = info.CompanyType;
            txtEmail.Text = info.Email;
            CompLogo.ImageUrl = info.CompanyLogo;
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
            txtWoreda.Text = "";
            txtCompanyName.Text = "";
            txtDescription.Text = "";
            txtEmail.Text = "";
            txtFax.Text = "";
            txtManagerName.Text = "";
            txtMobile.Text = "";
            txtTele.Text = "";
            txtCity.Text = "";
            txtSubCity.Text = "";
            txtPOBOX.Text = "";
            CompLogo.ImageUrl = "";
            txtNumberOfVehicles.Text = "";
            comboComanyType.SelectedIndex = 0;
        }
        protected void btn_Register_Click(object sender, EventArgs e)
        {
            int NumberOfVehicles=0;
            if (!String.IsNullOrEmpty(txtNumberOfVehicles.Text))
                NumberOfVehicles = Convert.ToInt32(txtNumberOfVehicles.Text);
            if (String.IsNullOrEmpty(comboComanyType.SelectedValue))
            {
                ErrorMsg.Message = "Please choose a valid company type!";
                return;
            }
            String CompanyLogo = "";
            var upload = ULogo;
            Guid id = Guid.NewGuid();
            if (upload.InvalidFiles.Count > 0)
            {
                ErrorMsg.Message = "Invalid Image Extension type. Supported Image Types are .jpeg,.jpg,.JPEG,.JPG,gif,GIF,.png,.tiff,.bit,.bmp,.dib";
                return;
            }
            if (upload.UploadedFiles.Count > 0)
            {
                CompanyLogo = "~/View/images/CompanyLogo/" + id.ToString() + upload.UploadedFiles[0].GetExtension();
                // upload.TargetFolder = Server.MapPath("~/View/images/CompanyLogo/");
                upload.UploadedFiles[0].SaveAs(Server.MapPath(CompanyLogo), true);           
            }            
            else
            {
                CompanyLogo = "";
                //errormsg1.Message = "Please choose a valid Module!";
                //return;
            }
            try
            {
                string imagePhoto = "";
                if (!String.IsNullOrEmpty(CompanyLogo))
                {
                    imagePhoto = ImageToBase64(System.Drawing.Image.FromFile(Server.MapPath(CompanyLogo)), System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                CompanyManagement exist = new CompanyManagement();
                if (exist.CompanyExists(txtCompanyName.Text))
                {
                    ErrorMsg.Message = "A Company is already registered with the given Company ID!";
                    return;
                }
                if (exist.InsertCompany(id, CompanyLogo,imagePhoto, txtCompanyName.Text, comboComanyType.SelectedValue, txtDescription.Text, txtFax.Text, txtManagerName.Text,
                    txtMobile.Text, NumberOfVehicles, txtPOBOX.Text, txtSubCity.Text, txtTele.Text, txtWoreda.Text, txtCity.Text,txtEmail.Text))
                {
                    MessageBox.Message = "Company Information successfuly saved";
                    use.InserActionLog(DateTime.Now, User.Identity.Name, "Company Inserted ", "Comapany Insereted" + txtCompanyName.Text.ToString());
                    clearField();
                }
                else
                {
                    ErrorMsg.Message = "Company Information is not registered!";
                }
                CompLogo.ImageUrl = "~/View/images/CompanyLogo/" + CompanyLogo;
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
                Guid id = Guid.Parse(txtCompanyId.Text);
                int NumberOfVehicles = 0;
                if (!String.IsNullOrEmpty(txtNumberOfVehicles.Text))
                    NumberOfVehicles = Convert.ToInt32(txtNumberOfVehicles.Text);
                if (String.IsNullOrEmpty(comboComanyType.SelectedValue))
                {
                    ErrorMsg.Message = "Please choose a valid company type!";
                    return;
                }
                String CompanyLogo = "";
                var upload = ULogo;
                if (upload.InvalidFiles.Count > 0)
                {
                    ErrorMsg.Message = "Invalid Image Extension type. Supported Image Types are .jpeg,.jpg,.JPEG,.JPG,gif,GIF,.png,.tiff,.bit,.bmp,.dib";
                    return;
                }
                if (upload.UploadedFiles.Count > 0)
                {
                    CompanyLogo = "~/View/images/CompanyLogo/"+ id.ToString() + upload.UploadedFiles[0].GetExtension();
                    upload.UploadedFiles[0].SaveAs(Server.MapPath(CompanyLogo), true);
                }
                else
                {
                    CompanyLogo = CompLogo.ImageUrl;
                }

                string imagePhoto = "";
                if (!String.IsNullOrEmpty(CompanyLogo))
                {
                    imagePhoto = ImageToBase64(System.Drawing.Image.FromFile(Server.MapPath(CompanyLogo)), System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                CompanyManagement com = new CompanyManagement();
                if (com.UpdateCompany(id, CompanyLogo, imagePhoto, txtCompanyName.Text, comboComanyType.SelectedValue, txtDescription.Text, txtFax.Text, txtManagerName.Text,
                        txtMobile.Text, NumberOfVehicles, txtPOBOX.Text, txtSubCity.Text, txtTele.Text, txtWoreda.Text, txtCity.Text, txtEmail.Text))
                {
                    MessageBox.Message = "Company Information Updated";
                    use.InserActionLog(DateTime.Now, User.Identity.Name, "Company Updated ", "Comapany Updated" + txtCompanyName.Text.ToString());
                    //clearField();
                }
                else
                {
                    ErrorMsg.Message = "Company Information is not Updated";
                }
            }
            catch (Exception ex)
            {
                ErrorMsg.Message = ex.Message;
            }
        }
    }
}