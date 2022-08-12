<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucSqlRole.ascx.cs" Inherits="CBEBirrManage.View.UserManagement.ucSqlRole" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<br />
<table>
    <tr>
        <td>
            <asp:Label ID="lblRoleName" runat="server" AssociatedControlID="txtNewRole"
                meta:resourcekey="lblRoleNameResource1">New Role:</asp:Label>

        </td>
        <td>
            <asp:TextBox runat="server" ID="txtNewRole" Width="200px"
                meta:resourcekey="txtNewRoleResource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RFVtxtNewRole" runat="server"
                ControlToValidate="txtNewRole" ErrorMessage="Role Name is required." ToolTip="Role Name is required."
                ValidationGroup="RegisterUserValidationGroup"
                meta:resourcekey="RFVtxtNewRoleResource1">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Label ID="Label1" runat="server" AssociatedControlID="txtNewRole"
                meta:resourcekey="lblRoleNameResource1">Description:</asp:Label>

        </td>
        <td>
            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescrption" Height="50px" Width="200px"
                meta:resourcekey="txtDescrptionResource1"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:ImageButton ID="btnInsert" ToolTip="Insert"
                AlternateText="Insert new Equipment" runat="server"
                CommandName="PerformInsert" TabIndex="0" ValidationGroup="RegisterUserValidationGroup"
                
                ImageUrl='<%# RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Grid.Update.gif") %>'
                meta:resourcekey="btnInsertResource1" />
           </td>
        <td>
                <asp:ImageButton ID="ImageButton2" ToolTip="Cancel"
                    AlternateText="Cancel" runat="server" CausesValidation="False" TabIndex="0"
                    CommandName="Cancel"
                    ImageUrl='<%# RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Grid.Cancel.gif") %>'
                    meta:resourcekey="ImageButton2Resource1" />

        <td></td>
        </td>
        <td></td>
    </tr>
</table>
