<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/View/Site.Master" CodeBehind="RestoreBackup.aspx.cs" Inherits="CBEBirrManage.View.UserManagement.RestoreBackup" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:errormsg ID="ErrorMsg" runat="server" />
    <uc2:MessageBox ID="MessageBox" runat="server" />
    <div>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td align="center">
                    <table cellpadding="0" cellspacing="0" border="0" width="50%">
                        <tr>
                            <td>
                                <h3>
                                    <b>Backup and Restore the Database</b></h3>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px;"></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="right">
                                            <b>Select Database:</b>
                                        </td>
                                        <td align="left">&nbsp;&nbsp;<asp:DropDownList ID="ddlDatabases" runat="server" AutoPostBack="false"
                                            Height="23px" Width="197px">
                                        </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btnBackup" runat="server" Text="Backup..." OnClick="btnBackup_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px;"></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMessage" ForeColor="Blue" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px;"></td>
                        </tr>
                        <tr>
                            <td>
                                <b>Backup Databases List:</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px;"></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="lstBackupfiles" runat="server" Height="236px" Width="922px"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px;"></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <%--<telerik:RadUpload ID="upload"  runat="server" MaxFileSize="1024000" ControlObjectsVisibility="None" Width="243px" Height="21px" MaxFileInputsCount="1"></telerik:RadUpload>--%>
                                <telerik:RadUpload ID="upload" runat="server" AllowedFileExtensions=".bak" ControlObjectsVisibility="None" Width="288px"></telerik:RadUpload>
                                <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="btnUpload_Click" />

                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnRestore" runat="server" Text="Restore..."
                                    OnClick="btnRestore_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px;"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
