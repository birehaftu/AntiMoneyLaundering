<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true"
    CodeBehind="ChangeOthersPassword.aspx.cs" Inherits="AntiLaundering.View.Account.ChangeOthersPassword" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc2" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 682px;
        }
        .auto-style2 {
            width: 642px;
        }
    </style>
     <script type="text/javascript">
         function CheckPasswordStrength(password) {
             var password_strength = document.getElementById("password_strength");

             //TextBox left blank.
             if (password.length == 0) {
                 password_strength.innerHTML = "";
                 return;
             }

             //Regular Expressions.
             var regex = new Array();
             regex.push("[A-Z]"); //Uppercase Alphabet.
             regex.push("[a-z]"); //Lowercase Alphabet.
             regex.push("[0-9]"); //Digit.
             regex.push("[$@$!%*#?&]"); //Special Character.

             var passed = 0;

             //Validate for each Regular Expression.
             for (var i = 0; i < regex.length; i++) {
                 if (new RegExp(regex[i]).test(password)) {
                     passed++;
                 }
             }

             //Validate for length of Password.
             if (passed > 2 && password.length >= 6) {
                 passed++;
             }

             //Display status.
             var color = "";
             var strength = "";
             if (password.length >= 6) {
                 switch (passed) {
                     case 0:
                     case 1:
                         strength = "Weak";
                         color = "red";
                         break;
                     case 2:
                         strength = "Good";
                         color = "darkorange";
                         break;
                     case 3:
                     case 4:
                         strength = "Strong";
                         color = "green";
                         break;
                     case 5:
                         strength = "Very Strong";
                         color = "darkgreen";
                         break;
                 }
             }
             else {
                 strength = "Weak";
                 color = "red";

             }
             password_strength.innerHTML = strength;
             password_strength.style.color = color;
         }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <uc2:errormsg ID="ErrorMsg" runat="server" />
    <uc1:MessageBox ID="MessageBox" runat="server" />
    <div class="column" style="width: 100%; margin-left: 4px;">

        <fieldset id="Fieldset2">
            <asp:Label ID="Label2" Text="Other Users Password Change" runat="server"></asp:Label>
        </fieldset>
    </div>
    <br />
    <br />
    <div>
    <table>
        <tr>
            <td>
                <asp:Label ID="txtUsersLabel" runat="server" AssociatedControlID="txtUsers">User Name:</asp:Label></td>
            <td class="auto-style1">
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <telerik:RadComboBox ID="txtUsers" Filter="Contains" AutoPostBack="false" runat="server" Skin="Telerik">
                </telerik:RadComboBox>

            </td>
        </tr>


        <table>

            <td>
                <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
            </td>
            <td class="auto-style2">
                <asp:TextBox ID="NewPassword" AutoCompleteType="Disabled" runat="server" CssClass="passwordEntry" TextMode="Password"  onkeyup="CheckPasswordStrength(this.value)" Width="147px"></asp:TextBox>
                                <span id="password_strength"></span>
                <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" CssClass="failureNotification" ErrorMessage="Password is required." ForeColor="Red" ToolTip="New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
            </td>
            </tr>
                   
                    
            <tr>
                <td>
                    <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm Password:</asp:Label></td>
                <td class="auto-style2">
                    <asp:TextBox AutoCompleteType="Disabled" ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password" Width="147px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                        CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                        ToolTip="Confirm New Password is required." ForeColor="Red" ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                        CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                        ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator></td>
            </tr>
            <tr>
                <td></td>
                <td class="auto-style2"></td>
            </tr>
            <tr>
                <td>&nbsp;&nbsp;&nbsp;</td>
                <td class="auto-style2">
                    <asp:Button runat="server" ID="ChangePasswordPushButton" CommandName="ChangePassword" Text="Change Password"
                        ValidationGroup="ChangeUserPasswordValidationGroup" OnClick="ChangePasswordPushButton_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
