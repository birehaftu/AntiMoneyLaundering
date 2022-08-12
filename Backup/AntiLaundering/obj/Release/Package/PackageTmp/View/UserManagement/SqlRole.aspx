<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/View/Site.Master" CodeBehind="SqlRole.aspx.cs" Inherits="CBEBirrManage.View.UserManagement.SqlRole" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc2" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<br />--%>
    <uc1:MessageBox ID="MessageBox1" runat="server" />
    <uc2:errormsg ID="errormsg1" runat="server" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>

    <div style="width: 100%">

        <uc2:errormsg ID="ErrorMsg" runat="server" />
        <uc1:MessageBox ID="MessageBox" runat="server" />
        <fieldset id="Fieldset2">
            <asp:Label ID="Label2" Text="Role Information" runat="server"></asp:Label>
        </fieldset>
        <%-- <br />
     <br />--%>
        <telerik:RadGrid ID="RadGridRoleList" runat="server" Width="100%"
            OnDeleteCommand="RadGridRoleList_DeleteCommand" AutoGenerateColumns="False"
            OnInsertCommand="RadGridRoleList_InsertCommand" OnItemDataBound="RadGridRoleList_ItemDataBound"
            OnNeedDataSource="RadGridRoleList_NeedDataSource" GridLines="None" OnUpdateCommand="RadGridRoleList_UpdateCommand"
            meta:resourcekey="RadGridRoleListResource1" > 
            <ClientSettings AllowDragToGroup="True" AllowColumnHide="True" AllowRowHide="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView DataKeyNames="ROLENAME" AllowFilteringByColumn="true" CommandItemDisplay="Top" EnableHeaderContextAggregatesMenu="True"
                 AllowSorting="true" CommandItemSettings-AddNewRecordText="New Role">
                <CommandItemSettings ExportToPdfText="Export to Pdf" ShowExportToWordButton="false" />
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="Numbers" AllowFiltering="false" ShowFilterIcon="false" HeaderText="No" Resizable="False"
                        Groupable="False" ReadOnly="True">
                        <ItemTemplate>
                            <asp:Label ID="lblbnum" runat="server" Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="RoleName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true" DataField="RoleName" HeaderStyle-Width="160px" UniqueName="RoleName"
                        HeaderText="Role Name">
                        <ItemTemplate>
                            <%#Eval("RoleName")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRoleName" TextMode="MultiLine" runat="server" Text='<%#Bind("RoleName") %>' Columns="30" Rows="5" CssClass="blue"></asp:TextBox>

                        </EditItemTemplate>
                        <HeaderStyle Width="260px"></HeaderStyle>
                    </telerik:GridTemplateColumn>
                      <telerik:GridTemplateColumn SortExpression="COUNTROLE" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" 
                          ShowFilterIcon="false" AllowFiltering="false" DataField="COUNTROLE" HeaderStyle-Width="160px" UniqueName="COUNTROLE"
                        HeaderText="User Count">
                        <ItemTemplate>
                            <%#Eval("COUNTROLE")%>
                        </ItemTemplate>
                       <%-- <EditItemTemplate>
                            <asp:TextBox ID="txtCOUNTROLE" TextMode="MultiLine" runat="server" Text='<%#Bind("COUNTROLE") %>' Columns="30" Rows="5" CssClass="blue"></asp:TextBox>

                        </EditItemTemplate>--%>
                        <HeaderStyle Width="260px"></HeaderStyle>
                    </telerik:GridTemplateColumn>
                    <%--<telerik:GridBoundColumn DataField="COUNTROLE" UniqueName="COUNTROLE" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true" HeaderText="User Count" meta:resourcekey="GridBoundColumnResource2" />--%>
                    <%--<telerik:GridBoundColumn DataField="Description" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false"  AllowFiltering="true"   UniqueName="Description" HeaderText="Description" meta:resourcekey="GridBoundColumnResource3" />--%>
                    <telerik:GridTemplateColumn SortExpression="Description" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" 
                        ShowFilterIcon="false" AllowFiltering="true" DataField="Description" HeaderStyle-Width="160px" UniqueName="Description"
                        HeaderText="Description">
                        <ItemTemplate>
                            <%#Eval("Description")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" Text='<%#Bind("Description") %>' Columns="30" Rows="5" CssClass="blue"></asp:TextBox>

                        </EditItemTemplate>
                        <HeaderStyle Width="360px"></HeaderStyle>
                    </telerik:GridTemplateColumn>
                    <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" Visible="true" EditImageUrl="~/View/images/btn_edit.gif"
                        ButtonType="ImageButton">
                        <HeaderStyle Width="33px" />
                    </telerik:GridEditCommandColumn>
                    <telerik:GridButtonColumn UniqueName="btnDelete" Text="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
                        ConfirmDialogType="RadWindow" ConfirmDialogWidth="250px" ConfirmText="Are you sure to delete?" ConfirmTitle="Delete Confirmation"
                        meta:resourcekey="GridButtonColumnResource1" />
                </Columns>
                 <EditFormSettings  InsertCaption="Add New Role">
                    <EditColumn UniqueName="EditCommandColumn" UpdateImageUrl="~/View/images/updatte.gif"
                        CancelImageUrl="~/View/images/cancel.gif" InsertImageUrl="~/View/images/save.png" ButtonType="ImageButton">
                    </EditColumn>
                    <PopUpSettings Modal="True" Width="550px" Height="130" />
                </EditFormSettings>
                <%--<EditFormSettings UserControlName="ucSqlRole.ascx" EditFormType="WebUserControl" InsertCaption="Add New Role">
                    <EditColumn UniqueName="EditCommandColumn" UpdateImageUrl="~/View/images/updatte.gif"
                        CancelImageUrl="~/View/images/cancel.gif" InsertImageUrl="~/View/images/save.png" ButtonType="ImageButton">
                    </EditColumn>
                    <PopUpSettings Modal="True" Width="550px" Height="130" />
                </EditFormSettings>--%>

                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                <PagerStyle Mode="NextPrevAndNumeric" />
            </MasterTableView>
        </telerik:RadGrid>
    </div>

    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
</asp:Content>
