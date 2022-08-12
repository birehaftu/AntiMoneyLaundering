<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/View/Site.Master" CodeBehind="CustomerInformation.aspx.cs" Inherits="CBEBirrManage.View.CBEBirr.CustomerInformation" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
          
            
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <uc1:errormsg ID="ErrorMsg1" runat="server" />
        <uc2:MessageBox ID="MessageBox1" runat="server" />
    <div class="InputPanel" >
        
        <fieldset id="Fieldset1">    
                <asp:Label ID="lbltext" Text=" Customer Status Information" runat="server"></asp:Label>
        </fieldset>
    </div>
    <div class="InputPanel" >

        <telerik:RadGrid ID="CustomerRegistration" runat="server"
            AutoGenerateColumns="False" GridLines="None"
             AllowPaging="True"
            AllowSorting="True" ShowStatusBar="True"
            Width="100%" ClientSettings-Selecting-AllowRowSelect="true"
            AllowMultiRowSelection="false" AllowFilteringByColumn="true"
            OnItemDataBound="CustomerRegistration_ItemDataBound" OnDeleteCommand="CustomerRegistration_DeleteCommand"
            EnableHeaderContextAggregatesMenu="false" OnNeedDataSource="CustomerRegistration_NeedDataSource" EnableHeaderContextFilterMenu="false"
            EnableHeaderContextMenu="false" PageSize="20" OnPreRender="CustomerRegistration_PreRender" >
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView CommandItemDisplay="Top" EnableHeaderContextAggregatesMenu="False"
                  ClientDataKeyNames="CustomerRegistrationID,Status" DataKeyNames="CustomerRegistrationID,Status">
                <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="false"></CommandItemSettings>
                <CommandItemTemplate>
                       
                </CommandItemTemplate>
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="Numbers" HeaderText="No" Resizable="False"
                        Groupable="False" AllowFiltering="false" ReadOnly="True">
                        <ItemTemplate>
                            <asp:Label ID="lblpnum" runat="server" Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn Display="false" UniqueName="CustomerRegistrationID" HeaderText="CustomerRegistrationID" SortExpression="CustomerRegistrationID"
                        AllowFiltering="true">
                        <ItemTemplate>
                            <%#Eval("CustomerRegistrationID")%>
                        </ItemTemplate>

                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="PhoneNumber" HeaderText="Phone Number" SortExpression="PhoneNumber"
                        AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="PhoneNumber" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("PhoneNumber")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="RegistrationDate" HeaderText="Start Date" SortExpression="RegistrationDate"
                        AllowFiltering="false" DataField="RegistrationDate" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("RegistrationDate")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="Status" HeaderText="Status" SortExpression="Status"
                        AllowFiltering="true"  CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="Status" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("Status")%>
                        </ItemTemplate>
                        <HeaderStyle Width="64px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" HeaderText="Change Status" ConfirmText="Are You Sure You want to change the Customer Status?"
                        ConfirmDialogType="Classic" ConfirmTitle="Change Confirmation" CommandName="Delete"
                        Text="Change Status" UniqueName="Dcolumn">
                        <HeaderStyle Width="33px" />
                    </telerik:GridButtonColumn>
                </Columns>
                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                <PagerStyle Mode="NextPrevAndNumeric" />
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <FilterItemStyle CssClass="HighZindex" />
            <FilterMenu CssClass="HighZindex" EnableRoundedCorners="True">
            </FilterMenu>
            <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default">
            </HeaderContextMenu>
            
        </telerik:RadGrid>

    </div>


</asp:Content>
