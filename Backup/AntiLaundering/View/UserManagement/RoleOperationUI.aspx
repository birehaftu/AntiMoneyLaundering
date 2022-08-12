<%@ Page Title="" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="RoleOperationUI.aspx.cs" Inherits="AntiLaundering.View.UserManagement.RoleOperationUI" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc2" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 564px;
        }
        .auto-style2 {
            width: 231px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc2:errormsg ID="ErrorMsg" runat="server" />
    <uc1:MessageBox ID="MessageBox" runat="server" />
    
    <fieldset id="Fieldset2">
            <asp:Label ID="Label2" Text="Role Operation Assignment" runat="server"></asp:Label>
            </fieldset>
    <%--<br />
    <br />--%>
    <asp:Label ID="lblId" Visible="false" runat="server"></asp:Label>
    <table class="auto-style1">
        <tr>
            <td class="auto-style3">Role Name</td>
            <td class="auto-style2">
                <telerik:RadComboBox ID="rdRoles" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdRoles_SelectedIndexChanged">
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadComboBox ID="txtModuleName" Filter="Contains" runat="server" DataSourceID="ModuleDS" DataTextField="ModuleName"
                    DataValueField="ModuleName" Skin="Telerik" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="txtModuleName_SelectedIndexChanged">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Please Select"
                            Value="" />
                    </Items>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>Rank</td>
            <td class="auto-style2">
                <telerik:RadNumericTextBox ID="txtRank" NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0" runat="server">
                </telerik:RadNumericTextBox>
            </td>
            <td class="auto-style6" rowspan="4">Operation Name<div style="OVERFLOW-Y: scroll; background-color: #CCCCFF; WIDTH: 200px; HEIGHT: 200px">
                <asp:CheckBoxList ID="rdOperation" runat="server"></asp:CheckBoxList>
            </div>
            </td>

        </tr>
        <tr>
            <td class="auto-style6">Access Level </td>
            <td class="auto-style2">

                <telerik:RadComboBox ID="rdAccesLeve" runat="server">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Choose Access Level" Value="" />
                        <telerik:RadComboBoxItem runat="server" Text="CRUD" Value="1" />
                        <telerik:RadComboBoxItem runat="server" Text="CRU" Value="2" />
                        <telerik:RadComboBoxItem runat="server" Text="CR" Value="3" />
                        <telerik:RadComboBoxItem runat="server" Text="R" Value="4" />

                    </Items>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style6">&nbsp;</td>
            <td class="auto-style2">

                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">
                <telerik:RadButton ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" Height="27px" Width="107px">
                </telerik:RadButton>
            </td>
            <td class="auto-style5">
                &nbsp;<telerik:RadButton ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click1"></telerik:RadButton></td>
        </tr>
    </table>
    <br />
    <br />
    <telerik:RadGrid ID="grdRoleOperation" runat="server"
        AutoGenerateColumns="False" GridLines="None"
         AllowPaging="True"
        AllowSorting="True" ShowStatusBar="True"
        Width="100%"
        OnItemDataBound="grdRoleOperation_ItemDataBound" OnDeleteCommand="grdRoleOperation_DeleteCommand"
        EnableHeaderContextAggregatesMenu="false" EnableHeaderContextFilterMenu="false"  OnItemCommand="grdRoleOperation_ItemCommand"
        EnableHeaderContextMenu="false" PageSize="20" OnPreRender="grdRoleOperation_PreRender"
        OnNeedDataSource="grdRoleOperation_NeedDataSource">
        <GroupingSettings CaseSensitive="false" /><MasterTableView AllowFilteringByColumn="true" CommandItemDisplay="Top" EnableHeaderContextAggregatesMenu="False"
            Caption="All Role Operation" DataKeyNames="Id,OperationID">
            <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="false"></CommandItemSettings>
            <AlternatingItemStyle BackColor="#FF90EE90" />
            <Columns>
                <telerik:GridTemplateColumn UniqueName="Numbers" AllowFiltering="false" HeaderText="No" Resizable="False"
                    Groupable="False" ReadOnly="True">
                    <ItemTemplate>
                        <asp:Label ID="lblpnum" runat="server" Width="30px"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn SortExpression="Roles" HeaderStyle-Width="160px" UniqueName="Roles"
                    HeaderText="Role"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="Roles"  ShowFilterIcon="false"  AllowFiltering="true"  >
                    <ItemTemplate>
                        <%#Eval("Roles") %>
                    </ItemTemplate>

                    <HeaderStyle Width="160px"></HeaderStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="OperationName" HeaderText="Operation" SortExpression="OperationName"
                     AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false" DataField="OperationName"  AllowFiltering="true"  >
                    <ItemTemplate>
                        <%#Eval("OperationName")%>
                    </ItemTemplate>

                    <HeaderStyle Width="184px" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Rank" HeaderText="Rank" SortExpression="Rank"
                     AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false" DataField="Rank"  AllowFiltering="true"  >
                    <ItemTemplate>
                        <%#Eval("Rank")%>
                    </ItemTemplate>
                    <HeaderStyle Width="284px" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Description" HeaderText="Description" SortExpression="Description"
                    AllowFiltering="false">
                    <ItemTemplate>
                        <%#Eval("Description")%>
                    </ItemTemplate>
                    <HeaderStyle Width="284px" />
                </telerik:GridTemplateColumn>
                 <telerik:GridButtonColumn ButtonType="ImageButton" HeaderText="Edit" CommandName="Fill" UniqueName="Fill"
                            Text="Edit">
                        </telerik:GridButtonColumn>
                <telerik:GridButtonColumn ButtonType="ImageButton" HeaderText="Delete" ConfirmText="Are You Sure You want to delete"
                    ConfirmDialogType="Classic" ConfirmTitle="Delete Confirmation" CommandName="Delete"
                    Text="Delete" UniqueName="Dcolumn">
                    <HeaderStyle Width="33px" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
            <PagerStyle Mode="NextPrevAndNumeric" />
        </MasterTableView>
        <FilterItemStyle CssClass="HighZindex" />
        <FilterMenu CssClass="HighZindex" EnableRoundedCorners="True">
        </FilterMenu>
        <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default">
        </HeaderContextMenu>
    </telerik:RadGrid>
    <asp:ObjectDataSource ID="ModuleDS" runat="server" SelectMethod="getModueses"
        TypeName="AntiLaundering.Control.UserManagement.ModulesLists"></asp:ObjectDataSource>
</asp:Content>
