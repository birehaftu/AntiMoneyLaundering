using System;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Security;
using System.IO;
using System.Web.UI.WebControls;
using System.Collections;
using Telerik.Web.UI;
using MyUserManagement;

namespace AntiLaundering.View.UserManagement
{
    public partial class AccessRule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateTree();
            }

        }
        private const string VirtualImageRoot = "~/";
        string selectedFolderName;

        protected void Page_Init()
        {

            UserRoles.DataSource = Roles.GetAllRoles();
            UserRoles.DataBind();

            UserList.DataSource = Membership.GetAllUsers();
            UserList.DataBind();

            if (IsPostBack)
            {
                selectedFolderName = "";
            }
            else
            {
                selectedFolderName = Request.QueryString["selectedFolderName"];
            }
        }


        protected void Page_PreRender()
        {

            if (FolderTree.SelectedNode != null)
            {
                DisplayAccessRules(FolderTree.SelectedValue);
                SecurityInfoSection.Visible = true;
            }

        }

        protected void PopulateTree()
        {
            // Populate the tree based on the subfolders of the specified VirtualImageRoot
            DirectoryInfo rootFolder = new DirectoryInfo(Server.MapPath(VirtualImageRoot));
            TreeNode root = AddNodeAndDescendents(rootFolder, null);

            FolderTree.Nodes.Add(root);

            try
            {
                FolderTree.SelectedNode.ImageUrl = "~/View/images/Target-Folder.png";

            }
            catch { }
        }

        protected TreeNode AddNodeAndDescendents(DirectoryInfo folder, TreeNode parentNode)
        {
            string virtualFolderPath;
            if (parentNode == null)
            {
                virtualFolderPath = VirtualImageRoot;
            }
            else
            {
                virtualFolderPath = parentNode.Value + folder.Name + "/";
            }

            TreeNode node = new TreeNode(folder.Name, virtualFolderPath);
            node.Selected = (folder.Name == selectedFolderName);

            // Recurse through this folder's subfolders
            DirectoryInfo[] subFolders = folder.GetDirectories();
            foreach (DirectoryInfo subFolder in subFolders)
            {
                if (subFolder.Name != "_controls" && subFolder.Name != "App_Data")
                {
                    TreeNode child = AddNodeAndDescendents(subFolder, node);
                    node.ChildNodes.Add(child);
                }
            }
            return node; // Return the new TreeNode
        }

        protected void FolderTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            ActionDeny.Checked = true;
            ActionAllow.Checked = false;
            ApplyRole.Checked = true;
            ApplyUser.Checked = false;
            ApplyAllUsers.Checked = false;
            ApplyAnonUser.Checked = false;
            UserRoles.SelectedIndex = 0;
            UserList.SelectedIndex = 0;

            RuleCreationError.Visible = false;

            ResetFolderImageUrls(FolderTree.Nodes[0]); // Restore previously selected folder's ImageUrl.
            FolderTree.SelectedNode.ImageUrl = "~/View/images/Target-Folder.png"; // Set the newly selected folder's ImageUrl.
        }
        protected void ResetFolderImageUrls(RadTreeNode parentNode)
        {
            parentNode.ImageUrl = "/Simple/i/Folder-yellow.png";
            RadTreeNodeCollection nodes = parentNode.Nodes;
            foreach (RadTreeNode childNode in nodes)
            {
                ResetFolderImageUrls(childNode);
            }
        }

        protected void ResetFolderImageUrls(TreeNode parentNode)
        {

            parentNode.ImageUrl = "~/View/images/Folder-yellow.png";

            TreeNodeCollection nodes = parentNode.ChildNodes;
            foreach (TreeNode childNode in nodes)
            {
                ResetFolderImageUrls(childNode);
            }
        }

        protected void DisplayAccessRules(string virtualFolderPath)
        {
            if (!virtualFolderPath.StartsWith(VirtualImageRoot) || virtualFolderPath.IndexOf("..") >= 0)
            {
                throw new ApplicationException("An attempt to access a folder outside of the website directory has been detected and blocked.");
            }
            Configuration config = WebConfigurationManager.OpenWebConfiguration(virtualFolderPath);
            SystemWebSectionGroup systemWeb = (SystemWebSectionGroup)config.GetSectionGroup("system.web");
            AuthorizationRuleCollection authorizationRules = systemWeb.Authorization.Rules;

            RadGridRuleDisplay.DataSource = authorizationRules;
            RadGridRuleDisplay.DataBind();
            TitleOne.InnerText = "Rules applied to " + virtualFolderPath;
            TitleTwo.InnerText = "Create new rule for " + virtualFolderPath;
        }

        protected string GetAction(AuthorizationRule rule)
        {
            return rule.Action.ToString();
        }
        protected string GetRole(AuthorizationRule rule)
        {
            return rule.Roles.ToString();
        }
        protected string GetUser(AuthorizationRule rule)
        {
            return rule.Users.ToString();
        }
        protected void DeleteRule(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            GridDataItem item = (GridDataItem)button.Parent.Parent;
            string virtualFolderPath = FolderTree.SelectedValue;
            Configuration config = WebConfigurationManager.OpenWebConfiguration(virtualFolderPath);
            SystemWebSectionGroup systemWeb = (SystemWebSectionGroup)config.GetSectionGroup("system.web");
            AuthorizationSection section = (AuthorizationSection)systemWeb.Sections["authorization"];
            section.Rules.RemoveAt(item.DataSetIndex);
            config.Save();
        }

        protected void CreateRule(object sender, EventArgs e)
        {
            AuthorizationRule newRule;
            if (ActionAllow.Checked) newRule = new AuthorizationRule(AuthorizationRuleAction.Allow);
            else newRule = new AuthorizationRule(AuthorizationRuleAction.Deny);

            if (ApplyRole.Checked && UserRoles.SelectedIndex > 0)
            {
                newRule.Roles.Add(UserRoles.Text);
                AddRule(newRule);
            }
            else if (ApplyUser.Checked && UserList.SelectedIndex > 0)
            {
                newRule.Users.Add(UserList.Text);
                AddRule(newRule);
            }
            else if (ApplyAllUsers.Checked)
            {
                newRule.Users.Add("*");
                AddRule(newRule);
            }
            else if (ApplyAnonUser.Checked)
            {
                newRule.Users.Add("?");
                AddRule(newRule);
            }
        }

        protected void AddRule(AuthorizationRule newRule)
        {
            string virtualFolderPath = FolderTree.SelectedValue;
            Configuration config = WebConfigurationManager.OpenWebConfiguration(virtualFolderPath);
            SystemWebSectionGroup systemWeb = (SystemWebSectionGroup)config.GetSectionGroup("system.web");
            AuthorizationSection section = (AuthorizationSection)systemWeb.Sections["authorization"];
            section.Rules.Add(newRule);
            try
            {
                config.Save();
                RuleCreationError.Visible = false;
            }
            catch (Exception ex)
            {
                RuleCreationError.Visible = true;
                RuleCreationError.Text = "<div class=\"alert\"><br />An error occurred and the rule was not added. I saw this happen during testing when I attempted to create a rule that the ASP.NET infrastructure realized was redundant. Specifically, I had the rule <i>DENY ALL USERS</i> in one folder, then attempted to add the same rule in a subfolder, which caused ASP.NET to throw an exception.<br /><br />Here's the error message that was thrown just now:<br /><br /><i>" + ex.Message + "</i></div>";
            }
        }


        protected void RadGridRuleDisplay_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                AuthorizationRule rule = (AuthorizationRule)e.Item.DataItem;
                if (!rule.ElementInformation.IsPresent)
                {
                    e.Item.Cells[5].Text = "Inherited from parent";
                    e.Item.Cells[6].Text = "Inherited from parent";
                }
            }

        }
        //Rearranging folders
        protected void MoveUp(object sender, EventArgs e)
        {
            MoveRule(sender, e, "up");
        }
        protected void MoveDown(object sender, EventArgs e)
        {
            MoveRule(sender, e, "down");
        }

        protected void MoveRule(object sender, EventArgs e, string upOrDown)
        {
            /*
             * Dan Clem, 3/17/2007
             */
            upOrDown = upOrDown.ToLower();

            if (upOrDown == "up" || upOrDown == "down")
            {

                ImageButton button = (ImageButton)sender;
                GridDataItem item = (GridDataItem)button.Parent.Parent;
                int selectedIndex = item.DataSetIndex;
                if ((selectedIndex > 0 && upOrDown == "up") || (upOrDown == "down"))
                {
                    string virtualFolderPath = FolderTree.SelectedValue;
                    Configuration config = WebConfigurationManager.OpenWebConfiguration(virtualFolderPath);
                    SystemWebSectionGroup systemWeb = (SystemWebSectionGroup)config.GetSectionGroup("system.web");
                    AuthorizationSection section = (AuthorizationSection)systemWeb.Sections["authorization"];

                    // Pull the local rules out of the authorization section, deleting them from same:
                    ArrayList rulesArray = PullLocalRulesOutOfAuthorizationSection(section);
                    if (upOrDown == "up")
                    {
                        LoadRulesInNewOrder(section, rulesArray, selectedIndex, upOrDown);
                    }
                    else if (upOrDown == "down")
                    {
                        if (selectedIndex < rulesArray.Count - 1)
                        {
                            LoadRulesInNewOrder(section, rulesArray, selectedIndex, upOrDown);
                        }
                        else
                        {
                            // DOWN button in last row was pressed. Load the rules array back in without resorting.
                            for (int x = 0; x < rulesArray.Count; x++)
                            {
                                section.Rules.Add((AuthorizationRule)rulesArray[x]);
                            }
                        }
                    }
                    config.Save();
                }
            }
        }
        protected void LoadRulesInNewOrder(AuthorizationSection section,
            ArrayList rulesArray, int selectedIndex, string upOrDown)
        {
            /*
             * Dan Clem, 3/17/2007.
             * I hope this is simple enough.
             * Imagine you have five local rules and you click a button to move the middle one.
             * In that scenario, all three of these methods will add rules.
             * If, however, there are only two local rules to start with, then only the middle method will add rules.
             * The first and third methods won't do anything, because their FOR loops will never execute.
             */

            AddFirstGroupOfRules(section, rulesArray, selectedIndex, upOrDown);
            AddTheTwoSwappedRules(section, rulesArray, selectedIndex, upOrDown);
            AddFinalGroupOfRules(section, rulesArray, selectedIndex, upOrDown);
        }
        protected void AddFirstGroupOfRules(AuthorizationSection section,
            ArrayList rulesArray, int selectedIndex, string upOrDown)
        {
            int adj;
            if (upOrDown == "up") adj = 1;
            else adj = 0;
            for (int x = 0; x < selectedIndex - adj; x++)
            {
                section.Rules.Add((AuthorizationRule)rulesArray[x]);
            }
        }
        protected void AddTheTwoSwappedRules(AuthorizationSection section,
            ArrayList rulesArray, int selectedIndex, string upOrDown)
        {
            if (upOrDown == "up")
            {
                section.Rules.Add((AuthorizationRule)rulesArray[selectedIndex]);
                section.Rules.Add((AuthorizationRule)rulesArray[selectedIndex - 1]);
            }
            else if (upOrDown == "down")
            {
                section.Rules.Add((AuthorizationRule)rulesArray[selectedIndex + 1]);
                section.Rules.Add((AuthorizationRule)rulesArray[selectedIndex]);
            }
        }
        protected void AddFinalGroupOfRules(AuthorizationSection section,
            ArrayList rulesArray, int selectedIndex, string upOrDown)
        {
            int adj;
            if (upOrDown == "up") adj = 1;
            else adj = 2;
            for (int x = selectedIndex + adj; x < rulesArray.Count; x++)
            {
                section.Rules.Add((AuthorizationRule)rulesArray[x]);
            }
        }
        protected ArrayList PullLocalRulesOutOfAuthorizationSection(AuthorizationSection section)
        {
            // Dan Clem, 3/17/2007.
            // First load the local rules into an ArrayList.

            ArrayList rulesArray = new ArrayList();
            foreach (AuthorizationRule rule in section.Rules)
            {
                if (rule.ElementInformation.IsPresent)
                {
                    rulesArray.Add(rule);
                }
            }

            // Next delete the rules from the section.
            foreach (AuthorizationRule rule in rulesArray)
            {
                section.Rules.Remove(rule);
            }
            return rulesArray;
        }
    }
}