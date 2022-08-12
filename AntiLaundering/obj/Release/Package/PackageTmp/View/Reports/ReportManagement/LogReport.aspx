<%@ Page Title="" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="LogReport.aspx.cs" Inherits="CBEBirrManage.View.Reports.ReportManagement.LogReport" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=10.0.16.113, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerikview" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <uc1:errormsg ID="ErrorMsg1" runat="server" />
        <uc2:MessageBox ID="MessageBox1" runat="server" />
    <div style="background-color: #B3D1FF; height: 50px;">
        <table>
            <tr>
                <td>
                    <telerik:RadComboBox ID="RadComboType" runat="server" Label="Type" OnSelectedIndexChanged="RadComboType_SelectedIndexChanged" AutoPostBack="True">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="All Log" Value="All" />
                            <telerik:RadComboBoxItem runat="server" Text="By User" Value="User" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td>
                    <telerik:RadComboBox ID="RadComboRange" AutoPostBack="true" runat="server" Label="Range" OnSelectedIndexChanged="RadComboRange_SelectedIndexChanged">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Daily" Value="Daily" />
                            <telerik:RadComboBoxItem runat="server" Text="Weekly" Value="Weekly" />
                            <telerik:RadComboBoxItem runat="server" Text="Monthly" Value="Monthly" />
                            <telerik:RadComboBoxItem runat="server" Text="Anually" Value="Anually" />
                           <telerik:RadComboBoxItem runat="server" Text="Custom" Value="Custom" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td>
                    <telerik:RadComboBox ID="RadComboUserName" Filter="Contains" runat="server" Labe="User Name"></telerik:RadComboBox>
                </td>
                <td>
                    <telerik:RadDatePicker ID="RadDateFrom" runat="server" DateInput-Label="From"></telerik:RadDatePicker>
                </td>
                <td>
                    <telerik:RadDatePicker ID="RadDateTo" runat="server" DateInput-Label="To"></telerik:RadDatePicker>
                </td>
                <td>
                    <telerik:RadButton ID="RadButtonPreview" runat="server" Text="Show" OnClick="RadButtonPreview_Click"></telerik:RadButton>
                </td>
            </tr>

        </table>
    </div>
    <div style="background-color: #F6F6F6">
        <telerikview:ReportViewer ID="ReportViewerActionLog" runat="server" Width="1082px" Height="673px" ></telerikview:ReportViewer>
    </div>
</asp:Content>
