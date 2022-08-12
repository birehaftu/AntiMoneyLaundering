<%@  Page Title="Student Management System" Language="C#" MasterPageFile="~/View/Site.Master" 
    AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="AntiLaundering.View.Home"  %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc2:MessageBox ID="MessageBox1" runat="server" />
    <uc1:errormsg ID="errormsg1" runat="server" />
     <fieldset id="Fieldset1">    
                <asp:Label ID="lbltext" Text=" Dashboard" runat="server"></asp:Label>
        </fieldset>
    <table class="ui-accordion" style="background-color:#ffffff;" id="tblMainHome" border="0" runat="server">
        
    </table>

</asp:Content>
