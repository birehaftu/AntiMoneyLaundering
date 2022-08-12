<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/Site.Master" AutoEventWireup="true" CodeBehind="IllegalAccess.aspx.cs" Inherits="HohomaFMS.GUI.IllegalAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%@ Register Src="~/GUI/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc2" %>
    <%@ Register Src="~/GUI/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <uc2:errormsg ID="ErrorMsg" runat="server" />
        <uc1:MessageBox ID="MessageBox" runat="server" />
    </div>
</asp:Content>
