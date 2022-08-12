<%@ Page Title="" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="AppRegisteration.aspx.cs" Inherits="CBEBirrManage.View.CBEBirr.AppRegisteration" %>


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
            <asp:Label ID="lbltext" Text="Mobile App Version Registration" runat="server"></asp:Label>
            </fieldset>

    </div>
    <div class="InputPanel" style="width: 100%; height: 450px">

        <table class="auto-style1">
            <tr>
                <td class="auto-style2">App Version</td>
                <td class="auto-style4">
                    <telerik:RadTextBox ID="txtAppVersion" runat="server" Width="150px">
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style3">Status</td>
                <td style="margin-left: 40px" class="auto-style1">
                    <telerik:RadComboBox ID="comboStatus" runat="server" >
                        <Items>
                            <telerik:RadComboBoxItem Text="--select--" Value="" />
                            <telerik:RadComboBoxItem Text="New" Value="New" />
                            <telerik:RadComboBoxItem Text="Old" Value="Old" />
                            <telerik:RadComboBoxItem Text="Deprecated" Value="Deprecated" />
                            
                        </Items>
                    </telerik:RadComboBox>
                               
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Start Date</td>
                <td class="auto-style6">
                   <telerik:RadDatePicker ID="dpstart" runat="server"></telerik:RadDatePicker></td>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style1">
                    &nbsp;</td>
            </tr>

            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style4">
            <telerik:RadButton ID="btn_Register"  ButtonType="LinkButton" runat="server" Text="Register" OnClick="btn_Register_Click">
            </telerik:RadButton>
                </td>
                <td class="auto-style3"><telerik:RadButton ID="btn_Update"  ButtonType="LinkButton" runat="server" Text="Update"  OnClick="btn_Update_Click">
            </telerik:RadButton>
                </td>
                <td class="auto-style1"> 
                    <telerik:RadTextBox ID="txtAPPID" runat="server"
 Visible="false"></telerik:RadTextBox>                   
                     </td>
            </tr>
            </table>
        <div class="InputPanel" >
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;<%--<uc2:MessagePanel ID="MessagePanel1" runat="server" />--%></div>


    </div>
</asp:Content>
