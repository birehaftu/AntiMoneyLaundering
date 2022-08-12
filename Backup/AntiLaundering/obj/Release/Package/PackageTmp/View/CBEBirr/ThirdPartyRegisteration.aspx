<%@ Page Title="" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="ThirdPartyRegisteration.aspx.cs" Inherits="CBEBirrManage.View.CBEBirr.ThirdPartyRegisteration" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 983px;
        }
        .auto-style2 {
            width: 292px;
        }
        .auto-style3 {
            width: 292px;
            height: 26px;
        }
        .auto-style4 {
            height: 26px;
        }
        .auto-style5 {
            width: 983px;
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <uc1:errormsg ID="ErrorMsg" runat="server" />
        <uc2:MessageBox ID="MessageBox" runat="server" />
    <div class="InputPanel">

        <fieldset id="Fieldset1">
            <asp:Label ID="lbltext" Text="ThirdParty Registration" runat="server"></asp:Label>
            </fieldset>

    </div>
    <div class="InputPanel" style="width: 100%; height: 450px">

        <table class="auto-style1">
            <tr>
                <td class="auto-style2">ThirdParty Name</td>
                <td class="auto-style4">
                    <telerik:RadTextBox ID="txtThirdPartyName" runat="server" Width="150px">
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style3">Short Code</td>
                <td style="margin-left: 40px" class="auto-style1">
                    <telerik:RadTextBox   MaxLength="50" ID="txtShortCode" runat="server" Width="150px">
                    </telerik:RadTextBox>
                               
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Thirdparty URL</td>
                <td class="auto-style6">
                    <telerik:RadTextBox ID="txtThirdPartyURL" runat="server" Width="150px" InputType="Url">
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style7">Receiver URL</td>
                <td class="auto-style1">
                    <telerik:RadTextBox ID="txtReceiverURL" Width="150px" runat="server" InputType="Url" >
                    </telerik:RadTextBox>
                </td>
            </tr>

            <tr>
                <td class="auto-style3">User Name</td>
                <td class="auto-style4">
                    <telerik:RadTextBox ID="txtUserName" runat="server" Width="150px">
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style3">Password</td>
                <td class="auto-style5">
                    <telerik:RadTextBox ID="txtPassword" TextMode="Password"  Width="150px" runat="server" 
                                 MaxLength="13" ></telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">ThirdParty key</td>
                <td class="auto-style4">
                    <telerik:RadTextBox ID="txtThirdPartykey" runat="server" Width="150px">
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style3">Confirm password</td>
                <td class="auto-style1"> 
                    <telerik:RadTextBox ID="txtPassword0" TextMode="Password"  Width="150px" runat="server" 
                                 MaxLength="13" ></telerik:RadTextBox>
                     &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">Confirmition needed</td>
                <td class="auto-style4">
                    <telerik:RadCheckBox ID="chkconfirm" Text="confrim?" runat="server"></telerik:RadCheckBox></td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style1"> 
                    <telerik:RadTextBox ID="txtThirdPartyID" runat="server"
 Visible="false"></telerik:RadTextBox>                   
                     </td>
            </tr>
            </table>
        <div class="InputPanel" >
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <telerik:RadButton ID="btn_Register"  ButtonType="LinkButton" runat="server" Text="Register" OnClick="btn_Register_Click">
            </telerik:RadButton>
            &nbsp;&nbsp;<telerik:RadButton ID="btn_Update"  ButtonType="LinkButton" runat="server" Text="Update"  OnClick="btn_Update_Click">
            </telerik:RadButton>
            <%--<uc2:MessagePanel ID="MessagePanel1" runat="server" />--%></div>


    </div>
</asp:Content>
