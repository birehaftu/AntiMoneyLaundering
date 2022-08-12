using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using AntiLaundering.Control.ResourceManagement;
using System.Configuration;
using System.Data.SqlClient;
using AntiLaundering.Control;
using System.Net;
using System.IO;

namespace AntiLaundering.View
{
    public partial class HomePage : System.Web.UI.MasterPage
    {

        string ip = "", port = "";
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
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Label1.Text = " Detecting Money Laundering System";
            //Label2.Text = "Home";
            ip = HttpContext.Current.Request.Url.Host;//["LOCAL_ADDR"];
            port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (!IsPostBack)
            {
                CompanyManagement com = new CompanyManagement();
                var info = com.GetCompany();
                if (info != null && info.Count > 0)
                {
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
                            string folder = "~/View/images/CompanyLogo/";
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
        }
    }
}