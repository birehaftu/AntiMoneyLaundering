<%@ Page Title="" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="CBEBirrKeyRegisteration.aspx.cs" Inherits="CBEBirrManage.View.CBEBirr.CBEBirrKeyRegisteration" %>


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
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <uc1:errormsg ID="ErrorMsg" runat="server" />
        <uc2:MessageBox ID="MessageBox" runat="server" />
    <div class="InputPanel">

        <fieldset id="Fieldset1">
            <asp:Label ID="lbltext" Text="CBEBirr Key Registration" runat="server"></asp:Label>
            </fieldset>

    </div>
    <div class="InputPanel" style="width: 100%; height: 450px">

        <table class="auto-style1">
            <tr>
                <td class="auto-style2">CBEBirrKey value</td>
                <td class="auto-style4" colspan="2">
                    <telerik:RadTextBox ID="txtCBEBirrKeyValue" runat="server" >
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style3">Key Password</td>
                <td style="margin-left: 40px" class="auto-style1">
                    <telerik:RadTextBox ID="txtKeyPassWord" runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>

            <tr>
                <td class="auto-style2">key Name</td>
                <td class="auto-style4" colspan="2">
                    <telerik:RadTextBox ID="txtKeyName" runat="server" >
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style3">Query Access</td>
                <td style="margin-left: 40px" class="auto-style1">
                    <telerik:RadTextBox ID="txtQueryUserName" runat="server" >
                    </telerik:RadTextBox>
                </td>
            </tr>

            <tr>
                <td class="auto-style2">Query Password</td>
                <td class="auto-style4" colspan="2">
                    <telerik:RadTextBox ID="txtQueryPassword"  runat="server">
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style3">Organization Name</td>
                <td style="margin-left: 40px" class="auto-style1">
                    <telerik:RadTextBox ID="txtOrgName"  runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>

            <tr>
                <td class="auto-style2">Organization Password</td>
                <td class="auto-style4" colspan="2">
                    <telerik:RadTextBox ID="txtOrgPass"  runat="server">
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style3">Organization Short Code</td>
                <td style="margin-left: 40px" class="auto-style1">
                    <telerik:RadTextBox ID="txtOrgCode"  runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>

            <tr>
                <td class="auto-style2">Loyality Name</td>
                <td class="auto-style4" colspan="2">
                    <telerik:RadTextBox ID="txtLoyaName"  runat="server">
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style3">Loyality Code</td>
                <td style="margin-left: 40px" class="auto-style1">
                    <telerik:RadTextBox ID="txtLoyaCode"  runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>

            <tr>
                <td class="auto-style2">Loyality Password</td>
                <td class="auto-style4" colspan="2">
                    <telerik:RadTextBox ID="txtLoyaPass"  runat="server">
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style3">Reciever IP</td>
                <td style="margin-left: 40px" class="auto-style1">
                    <telerik:RadTextBox ID="txtReciverIP" runat="server" >
                    </telerik:RadTextBox>
                </td>
            </tr>

            <tr>
                <td class="auto-style2">Reciever Port</td>
                <td class="auto-style4" colspan="2">
                    <telerik:RadTextBox ID="txtReciverPort" runat="server" >
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style3">&nbsp;</td>
                <td style="margin-left: 40px" class="auto-style1">
                    &nbsp;</td>
            </tr>

            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style4">
            <telerik:RadButton ID="btn_Register"  ButtonType="LinkButton" runat="server" Text="Register" OnClick="btn_Register_Click">
            </telerik:RadButton>
                </td>
                <td class="auto-style4">
                    &nbsp;</td>
                <td class="auto-style3"><telerik:RadButton ID="btn_Update"  ButtonType="LinkButton" runat="server" Text="Update"  OnClick="btn_Update_Click">
            </telerik:RadButton>
                </td>
                <td class="auto-style1"> 
                    <telerik:RadTextBox ID="txtCBEBirrKeyID" runat="server"
 Visible="false"></telerik:RadTextBox>                   
                     </td>
            </tr>
            </table>
        <div class="InputPanel" >
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;<%--<uc2:MessagePanel ID="MessagePanel1" runat="server" />--%></div>


    </div>
</asp:Content>
