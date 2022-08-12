<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/View/Site.Master" CodeBehind="CompanyInformation.aspx.cs" Inherits="CBEBirrManage.View.ResourceManagement.CompanyInformation" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function Registercompany() {
                var radload = radopen("CompanyRegisteration.aspx?sec=test", "UpdateRegisteration", 800, 600, 20, 20);
                radload.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Minimize + Telerik.Web.UI.WindowBehaviors.Maximize);
               
            }
            function Updatecompany(sender, eventArgs) {

                var compId = eventArgs.getDataKeyValue("CompanyId");
                var radload = radopen("CompanyRegisteration.aspx?gt=update&compId=" + compId, "UpdateRegisteration", 800, 600, 20, 20);
                radload.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Minimize + Telerik.Web.UI.WindowBehaviors.Maximize);
            }
            function RefreshParentPage()//function in parent page
            {
                window.location.href = window.location.href;
            }
            
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <uc1:errormsg ID="ErrorMsg1" runat="server" />
        <uc2:MessageBox ID="MessageBox1" runat="server" />
    <div class="InputPanel" >
        
        <telerik:RadWindowManager ID="RadWindowManager1" VisibleStatusbar="false" OnClientClose="RefreshParentPage" runat="server"></telerik:RadWindowManager>
        <fieldset id="Fieldset1">    
                <asp:Label ID="lbltext" Text=" Company Information" runat="server"></asp:Label>
        </fieldset>
    </div>
    <div class="InputPanel" >

        <telerik:RadGrid ID="grdCompanyInfo" runat="server"
            AutoGenerateColumns="False" GridLines="None"
             AllowPaging="True"
            AllowSorting="True" ShowStatusBar="True"
            Width="100%" ClientSettings-Selecting-AllowRowSelect="true"
            AllowMultiRowSelection="false"
            OnItemDataBound="grdCompanyInfo_ItemDataBound" OnDeleteCommand="grdCompanyInfo_DeleteCommand"
            EnableHeaderContextAggregatesMenu="false" OnNeedDataSource="grdCompanyInfo_NeedDataSource" EnableHeaderContextFilterMenu="false"
            EnableHeaderContextMenu="false" PageSize="20" OnPreRender="grdCompanyInfo_PreRender" >
            <GroupingSettings CaseSensitive="false" /><MasterTableView CommandItemDisplay="Top" EnableHeaderContextAggregatesMenu="False"
                  ClientDataKeyNames="CompanyId" DataKeyNames="CompanyId">
                <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="false"></CommandItemSettings>
                <CommandItemTemplate>
                            <telerik:RadButton ID="btnRegister" ButtonType="LinkButton" BackColor="#a4bbc1" OnClientClicked="Registercompany" runat="server" Text="New Company" AutoPostBack="false"
                            Enabled="true">
                            </telerik:RadButton>
                               <%-- &nbsp;&nbsp;
                         <telerik:RadButton ID="btnRefresh" ButtonType="LinkButton" BackColor="#a4bbc1" OnClick="btnRefresh_Click" runat="server" Text="Refresh" AutoPostBack="true"
                             Enabled="true">
                         </telerik:RadButton>--%>
                </CommandItemTemplate>
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="Numbers" HeaderText="No" Resizable="False"
                        Groupable="False" ReadOnly="True">
                        <ItemTemplate>
                            <asp:Label ID="lblpnum" runat="server" Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn Display="false" UniqueName="CompanyId" HeaderText="CompanyId" SortExpression="CompanyId"
                        AllowFiltering="true">
                        <ItemTemplate>
                            <%#Eval("CompanyId")%>
                        </ItemTemplate>

                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="CompanyName" HeaderText="Company Name" SortExpression="CompanyName"
                        AllowFiltering="true" DataField="CompanyName" FilterControlAltText="Filter by CompanyName">
                        <ItemTemplate>
                            <%#Eval("CompanyName")%>
                        </ItemTemplate>

                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="CompanyType" HeaderText="Company Type" SortExpression="CompanyType"
                        AllowFiltering="false" DataField="CompanyType" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("CompanyType")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="Tel" HeaderText="Telephone" SortExpression="Tel"
                        AllowFiltering="false" DataField="Tel" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("Tel")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="Fax" HeaderText="Fax" SortExpression="Fax"
                        AllowFiltering="false" DataField="Fax" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("Fax")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="Mobile" HeaderText="Mobile" SortExpression="Mobile"
                        AllowFiltering="false" DataField="Mobile" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("Mobile")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn UniqueName="ManagerName" HeaderText="Manager Name" SortExpression="ManagerName"
                        AllowFiltering="false" DataField="ManagerName" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("ManagerName")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="Email" HeaderText="Email" SortExpression="Email"
                        AllowFiltering="false" DataField="Email" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("Email")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="CompanyLogo" HeaderText="Company Logo" SortExpression="CompanyLogo"
                        AllowFiltering="false">
                        <ItemTemplate>
                         <asp:Image ID="ImageAlarmIconGridL" runat="server" Height="70px" Width="70px" ImageUrl='<%# Eval("CompanyLogo") %>'/>
                        </ItemTemplate>
                        <HeaderStyle Width="160px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="Descrbition" HeaderText="Descrpition" SortExpression="Descrbition"
                        AllowFiltering="false" DataField="Descrbition" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("Descrbition")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" HeaderText="Delete" ConfirmText="Are You Sure You want to delete the company"
                        ConfirmDialogType="Classic" ConfirmTitle="Delete Confirmation" CommandName="Delete"
                        Text="Delete" UniqueName="Dcolumn">
                        <HeaderStyle Width="33px" />
                    </telerik:GridButtonColumn>
                </Columns>
                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                <PagerStyle Mode="NextPrevAndNumeric" />
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnRowDblClick="Updatecompany" />
            </ClientSettings>
            <FilterItemStyle CssClass="HighZindex" />
            <FilterMenu CssClass="HighZindex" EnableRoundedCorners="True">
            </FilterMenu>
            <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default">
            </HeaderContextMenu>
            
        </telerik:RadGrid>

    </div>


</asp:Content>
