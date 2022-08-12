using System;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace AntiLaundering.View.UserManagement
{
    public partial class EditUser : System.Web.UI.UserControl
    {
        private object _dataItem = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }
        protected void Page_PreRender()
        {
            cblUserRoles.DataSource = Roles.GetAllRoles();
            cblUserRoles.DataBind();
            // Bind these checkboxes to the User's own set of roles.
            string[] userRoles = Roles.GetRolesForUser(txtUsername.Text);
            foreach (string role in userRoles)
            {
                ListItem checkbox = cblUserRoles.Items.FindByValue(role);
                checkbox.Selected = true;
            }
            
            //if (!(DataItem is Telerik.Web.UI.GridInsertionObject))
            //{
            //    if (!Membership.GetUser(txtUsername.Text).IsLockedOut)
            //    {
            //        cbIsLocked.Checked = false;
            //        cbIsLocked.Enabled = true;
            //    }
            //    else
            //    {
            //        cbIsLocked.Checked = true;
            //        cbIsLocked.Enabled = true;
                
            //    }

            //    if (!Membership.GetUser(txtUsername.Text).IsApproved)
            //    {
            //        cbIsapproved.Checked = false;
            //    }
            //    else
            //    {
            //        cbIsapproved.Checked = true;
            //        cbIsapproved.Enabled = true;

            //    }
            //}
            //else
            //{
            //    trlock.Visible = false;
            //}
        }
    }
}