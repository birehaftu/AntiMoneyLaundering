using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using AntiLaundering.Control.UserManagement;
using System.Web.Services;
using System.Data;
using AntiLaundering.Control.ResourceManagement;
using System.Configuration;
using System.Data.SqlClient;
using AntiLaundering.Control;
using System.Net;
using System.IO;
namespace AntiLaundering.View
{
    public partial class Index : System.Web.UI.Page
    {
        static int selectednode = -1;
        static int selectednodeParent = -1;
        IndexUserMgnt man = new IndexUserMgnt();
        protected void Page_Load(object sender, EventArgs e)
        {
            //connect();
            if (User.Identity.IsAuthenticated == false)
            {
                Response.Redirect("~/View/um/Account/Login.aspx");
            }
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                // errormsg1.Message = "Please check your database connection";
                Response.Redirect("~/View/um/Account/Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
               
                if (Session["LoginId"] == null)
                    Response.Redirect("~/View/um/Account/Login.aspx");
                else
                {
                    Response.ClearHeaders();
                    Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                    Response.AddHeader("Pragma", "no-cache");
                }
                FillNodes();
                //DataSource();
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "msg", " showAlert()", true);
            }
        }
        protected void RadTreeView1_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
        //    if (User.IsInRole("Administrator") || User.IsInRole("Admin") || User.IsInRole("Super Admin") || User.IsInRole("Palm"))
        //    {
        //        RadTreeView1.ExpandAllNodes();
        //    }
        //    else
        //    {
                RadTreeView1.CollapseAllNodes();
            //}
            var Opera = man.getOperationByOPName(RadTreeView1.SelectedValue);
            var ope = man.getFirstOperationsByModuleName(RadTreeView1.SelectedValue);
            try
            {
                if (Opera == null)
                {
                    //errormsg1.Message = "Please check your database connection";
                    return;
                }
                selectednode = RadTreeView1.SelectedNode.Index;


                if (RadTreeView1.SelectedNode.ParentNode != null)
                {

                    selectednodeParent = RadTreeView1.SelectedNode.ParentNode.Index;
                }

            }
            catch (Exception ex)
            {
                //errormsg1.Message = ex.InnerException.Message;
            }
            if (!String.IsNullOrEmpty(Opera.OperationLink))
            {
                var id = Opera.Id.ToString();
                String url = "~/View/Index.aspx?gt=" + id;
                Response.Redirect(url);
                //contentPane.ContentUrl = Opera.OperationLink;
            }
            if (string.IsNullOrEmpty(Opera.OperationLink) && ope != null)
            {

                if (!String.IsNullOrEmpty(ope.OperationLink))
                {
                    var id = ope.OperationID;
                    selectednodeParent = selectednode;
                    String url = "~/View/Index.aspx?gt=" + id;
                    Response.Redirect(url);
                    //contentPane.ContentUrl = ope.OperationLink;

                }

            }
            //contentPane

        }
        protected void FillNodes()
        {
            //RadTreeView1.Nodes.Clear();
            try
            {
                String Uname = User.Identity.Name;
                var rop = man.getOperationsByUserName(User.Identity.Name);
                int i = 0;
                foreach (var l in rop)
                {
                    if (String.IsNullOrEmpty(l.OperationLink))
                    {
                        RadTreeNode root1 = new RadTreeNode(l.OperationName);
                        RadTreeView1.Nodes.Add(root1);

                        //root1.ImageUrl = "~/View/images/ic_launcher_filemanager_512.png";
                        //root1.ExpandedImageUrl = "~/View/images/open_folder.png";
                        root1.Style.Add("border-size", "1px");
                        root1.Style.Add("margin", "3px 3px 3px 3px");
                        root1.Style.Add("border-color", "#4e8264");
                        root1.Style.Add("width", "100%");
                        root1.Style.Add("background-color", "#800080");
                        //root1.Style.Add("background-color", "#1b4e00");
                        var submodule = man.getOperationsByModuleName(l.ModuleName, User.Identity.Name);
                        foreach (var li in submodule)
                        {
                            RadTreeNode root2 = new RadTreeNode(li.OperationName);
                            //root2.ImageUrl
                            //li.Id photo_2019-06-26_08-22-56.jpg
                            //root2.ImageUrl = "~/View/images/Icon_Disk_256x256.png";
                            //root2.SelectedImageUrl = "~/View/images/Icon_New_File_256x256.png";
                            //background - color:#1b4e00
                            root2.Style.Add("border-size", "1px");
                            root2.Style.Add("border-color", "#4e8264");
                            root2.Style.Add("margin", "3px 3px 3px 3px");
                            root2.Style.Add("width", "100%");
                            root2.Style.Add("background-color", "#FFD700");
                            //root2.Style.Add("background-color", "#1b4e08");
                            if (!String.IsNullOrEmpty(li.OperationLink))
                            {
                                RadTreeView1.Nodes[i].Nodes.Add(root2);
                                var id = li.OperationID.ToString();
                                String url = "~/View/Index.aspx?gt=" + id;
                                RadTreeView1.Nodes[i].Nodes.FindNodeByText(li.OperationName).NavigateUrl = url;
                            }

                        }
                        i++;
                    }
                    else if (l.OperationName.Equals(l.ModuleName))
                    {
                        RadTreeNode root2 = new RadTreeNode(l.OperationName);
                        //root2.ImageUrl = "~/View/images/Icon_Disk_256x256.png";
                        //root2.SelectedImageUrl = "~/View/images/Icon_New_File_256x256.png";

                        root2.Style.Add("border-size", "1px");
                        root2.Style.Add("border-color", "#4e8264");
                        root2.Style.Add("margin", "3px 3px 3px 3px");
                        root2.Style.Add("width", "100%");
                        root2.Style.Add("background-color", "#800080");//#FFD700
                        //root2.Style.Add("background-color", "#1b4e00");
                        //background-color:#800080
                        RadTreeView1.Nodes.Add(root2);
                        var id = l.OperationID.ToString();
                        String url = "~/View/Index.aspx?gt=" + id;
                        RadTreeView1.Nodes.FindNodeByText(l.OperationName).NavigateUrl = url;
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                //errormsg1.Message = "Erro:" + ex.Message;
                return;
            }
            if (Request.QueryString["gt"] != null)
            {
                Guid OpId = Guid.Parse(Request.QueryString["gt"].ToString());
                try
                {
                    var Ope = man.getOperationsByOperarionID(OpId);
                    if (!String.IsNullOrEmpty(Ope.OperationLink))
                    {
                        var Opera = man.getOperationByOPName(Ope.ModuleName);
                        if (Ope.ModuleName.Equals(Ope.OperationName))
                        {
                            selectednode = RadTreeView1.Nodes.FindNodeByText(Ope.OperationName.ToString()).Index;
                            selectednodeParent = -1;
                        }
                        else
                        {
                            int moduleIndex = RadTreeView1.Nodes.FindNodeByText(Opera.ModuleName).Index;
                            selectednode = RadTreeView1.Nodes[moduleIndex].Nodes.FindNodeByText(Ope.OperationName.ToString()).Index;
                            if (RadTreeView1.Nodes[moduleIndex].Nodes.FindNodeByText(Ope.OperationName.ToString()).ParentNode != null)
                            {

                                selectednodeParent = RadTreeView1.Nodes[moduleIndex].Nodes.FindNodeByText(Ope.OperationName.ToString()).ParentNode.Index;
                            }
                        }
                        if (selectednode != -1)
                        {
                            if (selectednodeParent != -1)
                            {
                                //if (!User.IsInRole("Administrator") && !User.IsInRole("Admin") && !User.IsInRole("Super Admin"))
                                //{
                                    RadTreeView1.CollapseAllNodes();
                                //}
                                //else
                                //    RadTreeView1.ExpandAllNodes();
                                RadTreeView1.Nodes[selectednodeParent].Expanded = true;
                                RadTreeView1.Nodes[selectednodeParent].Nodes[selectednode].Selected = true;
                                //RadTreeView1.Nodes[selectednodeParent].Nodes[selectednode].Style.Add("background-color:", "#1b4e00");
                            }
                            else
                            {
                                //if (!User.IsInRole("Administrator") && !User.IsInRole("Admin") && !User.IsInRole("Super Admin"))
                                //{
                                    RadTreeView1.CollapseAllNodes();
                                //}
                                //else
                                    //RadTreeView1.ExpandAllNodes();
                                RadTreeView1.Nodes[selectednode].Expanded = true;
                                RadTreeView1.Nodes[selectednode].Selected = true;
                                //RadTreeView1.Nodes[selectednode].Style.Add("background-color:", "#1b4e00");
                            }
                        }
                        UManagement isInRole = new UManagement();
                        if (isInRole.ModuleInRoleExists(Ope.OperationID, User.Identity.Name))
                        {
                            contentPane.ContentUrl = Ope.OperationLink + "?sec=test";
                        }
                        else
                        {
                            Response.Redirect("~/View/Index.aspx");
                            //contentPane.ContentUrl = "~/View/IllegalAccess.aspx";
                        }
                        //contentPane.ContentUrl = Ope.OperationLink;
                    }
                }
                catch (Exception ex)
                {
                    //errormsg1.Message = "Error: " + ex.Message;
                }
            }
            else
            {
                //contentPane.ContentUrl = "Home.aspx";
                if (RadTreeView1.Nodes.Count > 0)
                {
                    var Opera = man.getOperationByOPName(RadTreeView1.Nodes[0].Text.ToString());// entities.Operations.Where(p => p.OperationName == RadTreeView1.SelectedValue).FirstOrDefault();
                    var ope = man.getFirstOperationsByModuleName(RadTreeView1.Nodes[0].Text.ToString());
                    try
                    {
                        if (Opera == null)
                        {
                            //errormsg1.Message = "Please check your database connection";
                            return;
                        }
                        selectednode = RadTreeView1.Nodes[0].Index;
                        RadTreeView1.Nodes[selectednode].Selected = true;


                        if (RadTreeView1.Nodes[0].ParentNode != null)
                        {

                            selectednodeParent = RadTreeView1.Nodes[0].ParentNode.Index;
                            RadTreeView1.Nodes[selectednodeParent].Nodes[selectednode].Selected = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        //errormsg1.Message = ex.InnerException.Message;
                    }
                    if (!String.IsNullOrEmpty(Opera.OperationLink))
                    {
                        var id = Opera.OperationID.ToString();
                        String url = "~/View/Index.aspx?gt=" + id;
                        Response.Redirect(url);
                        //contentPane.ContentUrl = Opera.OperationLink;
                    }
                    if (string.IsNullOrEmpty(Opera.OperationLink) && ope != null)
                    {

                        if (!String.IsNullOrEmpty(ope.OperationLink))
                        {
                            var id = ope.OperationID.ToString();
                            selectednodeParent = selectednode;
                            String url = "~/View/Index.aspx?gt=" + id;
                            Response.Redirect(url);
                            //contentPane.ContentUrl = ope.OperationLink;

                        }

                    }
                }
                //contentPane
            }

            //if (User.IsInRole("Administrator") || User.IsInRole("Admin") || User.IsInRole("Super Admin"))
            //{
            //    RadTreeView1.ExpandAllNodes();
            //}

            //if (User.IsInRole("Administrator") || User.IsInRole("Admin") || User.IsInRole("Super Admin") || User.IsInRole("Palm"))
            //{
            //    RadTreeView1.ExpandAllNodes();
            //}
            //else
            //{
            //    RadTreeView1.CollapseAllNodes();
            //}
            //contentPane.Style.Add("padding", "3px 3px 3px 3px");

        }
        public void connect()
        {
            //SocketConnect sock = new SocketConnect();
            //sock.ConnectionListening(65000);
        }
        private void DataSource()
        {
            //StrConnection conne = new StrConnection();
            //SqlDependency.Start(conne.constr);
            //NotificationsHub nHub = new NotificationsHub();
            //nHub.NotifyNotificationPopUp("False");
            ////grdNotification.DataSource = list;
            ////if (bo)
            ////grdNotification.DataBind();
        }
    }
}