<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditUser.ascx.cs" Inherits="AntiLaundering.View.UserManagement.EditUser" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    .auto-style1 {
        height: 63px;
    }
</style>
<table style="width: auto; height: auto">
    <tr>
        <td colspan="2">
            <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server"
                meta:resourcekey="RegisterUserValidationSummaryResource1" />
        </td>
    </tr>
    <tr>

        <td valign="top">

            <h3>
                <asp:Literal ID="Literal1" Text="Main Info:" runat="server"
                    meta:resourcekey="Literal1Resource1" /></h3>
            <table>


                <tr hidden="hidden">
                    <td align="right">
                        <asp:Literal ID="Literal2" Text="Active User:" runat="server"
                            meta:resourcekey="Literal2Resource1" /></td>
                    <td>
                        <asp:CheckBox ID="cbIsapproved" runat="server" Checked="True"
                            meta:resourcekey="cbIsapprovedResource1" />
                    </td>
                </tr>
                <tr runat="server" id="trlock">
                    <td align="right">
                        <asp:Literal ID="Literal3" Text="Is Locked out?" runat="server"
                            meta:resourcekey="Literal3Resource1" /></td>
                    <td>
                        <asp:CheckBox ID="cbIsLocked" runat="server" Checked='<%# DataBinder.Eval( Container, "DataItem.ISLOCKEDOUT") %>'
                            meta:resourcekey="cbIsLockedResource1" />
                    </td>
                </tr>
                <tr id="Tr1" runat="server">
                    <td>
                        <asp:Label ID="lblUserName" runat="server"
                            AssociatedControlID="txtUsername" meta:resourcekey="lblUserNameResource1">User Name:</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtUsername" runat="server" Width="150px" Enabled="false"
                            Text='<%# DataBinder.Eval( Container, "DataItem.username") %>'
                            meta:resourcekey="txtUsernameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server"
                            ControlToValidate="txtUsername" ErrorMessage="User Name is required." ToolTip="User Name is required."
                            ValidationGroup="RegisterUserValidationGroup"
                            meta:resourcekey="UserNameRequiredResource1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>


                <tr>
                    <td>
                        <asp:Label ID="lblEmail" runat="server"
                            AssociatedControlID="txtEmail" meta:resourcekey="lblEmailResource1">E-mail:</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Width="150px"
                            Text='<%# DataBinder.Eval( Container, "DataItem.Email") %>'
                            meta:resourcekey="txtEmailResource1"></asp:TextBox>
                    </td>
                </tr>


              <tr>
                    <td>
                        Device IP</td>
                    <td>
                        <asp:TextBox ID="txtPalmIP" runat="server" Width="150px"
                            Text='<%# DataBinder.Eval( Container, "DataItem.PalmIP") %>'
                            meta:resourcekey="txtPalmIPResource1"></asp:TextBox>
                    </td>
                </tr>
                
              

                <tr>
                    <td>
                        Department</td>
                    <td>
                        <telerik:RadComboBox ID="ComboDepartment" SelectedValue='<%# DataBinder.Eval( Container, "DataItem.Department") %>' runat="server">
                            <Items>
                                <telerik:RadComboBoxItem Text="All" Value="" />
                                <telerik:RadComboBoxItem Text="Natural Science" Value="Natural Science" />
                                <telerik:RadComboBoxItem Text="Business" Value="Business" />
                                <telerik:RadComboBoxItem Text="Social Science" Value="Social Science" />
                                <telerik:RadComboBoxItem Text="Agriculture" Value="Agriculture" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>


                <tr>
                    <%--<td>
                        <asp:Label ID="Label1" runat="server"
                            AssociatedControlID="txtEmployeeId" meta:resourcekey="lblEmployeeIdResource1">Employee ID:</asp:Label>

                    </td>
                    <td>
                        <asp:TextBox ID="txtEmployeeId" runat="server" Width="150px"
                            Text='<%# DataBinder.Eval( Container, "DataItem.EmployeeId") %>'
                            meta:resourcekey="txtEmailResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="txtEmployeeId" ErrorMessage="Employee Id is required." ToolTip="Employee Id  is required."
                            ValidationGroup="RegisterUserValidationGroup"
                            meta:resourcekey="EmailRequiredResource1">*</asp:RequiredFieldValidator>
                    </td>--%>

                </tr>
                <tr>

                    <td class="auto-style2">
                        <asp:Label ID="lblPhone" Text="Phone:" runat="server"
                            AssociatedControlID="txtPhone"
                            meta:resourcekey="lblPhoneResource1"></asp:Label>
                    </td>
                    <td class="auto-style5">
                        <telerik:RadTextBox ID="txtPhone" Mask="###-######" ValidationGroup="Group1" Width="160px" runat="server"
                            Text='<%# DataBinder.Eval( Container, "DataItem.Phone") %>'
                            meta:resourcekey="txtPhoneResource1">
                        </telerik:RadTextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhone" ErrorMessage="Please enter the correct phone number" ForeColor="Red" ValidationExpression="^([0-9\(\)\/\+ \-]*)$"></asp:RegularExpressionValidator>

                    </td>
                </tr>
                <tr>

                    <td class="auto-style2">
                        <asp:Label ID="lblDOB" runat="server"
                            AssociatedControlID="dpDOB"
                            meta:resourcekey="lblDOBResource1">Date of Birth:</asp:Label>
                    </td>
                    <td class="auto-style5">
                        <telerik:RadDatePicker ID="dpDOB" SelectedDate='<%# DataBinder.Eval( Container, "DataItem.DOB") %>' runat="server" MinDate="1900-01-01">
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" ControlToValidate="dpDOB" Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server"
                            AssociatedControlID="txtFullName" meta:resourcekey="lblFullNameResource1">Full Name:</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtFullName" runat="server" Width="150px"
                            Text='<%# DataBinder.Eval( Container, "DataItem.FullName") %>'
                            meta:resourcekey="txtFullNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="txtFullName" ErrorMessage="Full Name is required." ToolTip="Full Name  is required."
                            ValidationGroup="RegisterUserValidationGroup"
                            meta:resourcekey="EmailRequiredResource1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>



                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblAddress" runat="server"
                            AssociatedControlID="txtAddress"
                            meta:resourcekey="lblPassowrdAnswerResource1">Address:</asp:Label>
                    </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtAddress" Width="160px" runat="server"
                            Text='<%# DataBinder.Eval( Container, "DataItem.Address") %>'
                            meta:resourcekey="txtAddressResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr id="Tr6" runat="server">
                    <td class="auto-style1">
                        <asp:Label ID="lblComment" runat="server"
                            AssociatedControlID="txtComment" meta:resourcekey="lblCommentResource1">Description:</asp:Label>

                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtComment" runat="server" Width="329px"
                            Text='<%# DataBinder.Eval( Container, "DataItem.Description") %>'
                            TextMode="MultiLine" meta:resourcekey="txtCommentResource1" Height="55px" />

                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <div id="ConfirmationMessage" runat="server" class="alert"></div>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:ImageButton ID="ImageButton1" AlternateText="Update User"
                            ToolTip="Update User" runat="server" CommandName="Update" TabIndex="3" ValidationGroup="RegisterUserValidationGroup"
                            Visible='<%# !(DataItem is Telerik.Web.UI.GridInsertionObject) %>'
                            ImageUrl='<%# RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Grid.Update.gif") %>'
                            meta:resourcekey="ImageButton1Resource1" />
                        &nbsp;&nbsp;&nbsp
                 <asp:ImageButton ID="btnInsert" ToolTip="Insert" AlternateText="Insert new User"
                     runat="server" CommandName="PerformInsert" TabIndex="3" ValidationGroup="RegisterUserValidationGroup"
                     Visible='<%# DataItem is Telerik.Web.UI.GridInsertionObject %>'
                     ImageUrl='<%# RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Grid.Update.gif") %>'
                     meta:resourcekey="btnInsertResource1" />
                        &nbsp;&nbsp;&nbsp
                <asp:ImageButton ID="ImageButton2" ToolTip="Cancel" AlternateText="Cancel"
                    runat="server" CausesValidation="False" TabIndex="4"
                    CommandName="Cancel"
                    ImageUrl='<%# RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Grid.Cancel.gif") %>'
                    meta:resourcekey="ImageButton2Resource1" />

                    </td>
                </tr>

            </table>
        </td>
        <td valign="top" style="border-left: 1px solid #999">
            <h3>
                <asp:Literal ID="Literal4" Text="Roles:" runat="server"
                    meta:resourcekey="Literal4Resource1" /></h3>
            <asp:RadioButtonList ID="cblUserRoles" runat="server"
                meta:resourcekey="cblUserRolesResource1">
            </asp:RadioButtonList>
        </td>
    </tr>

</table>
