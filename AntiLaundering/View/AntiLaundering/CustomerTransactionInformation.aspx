<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/View/Site.Master" CodeBehind="CustomerTransactionInformation.aspx.cs" Inherits="AntiLaundering.View.AntiLaundering.CustomerTransactInformation" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/um/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="~/View/um/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <uc1:errormsg ID="ErrorMsg1" runat="server" />
        <uc2:MessageBox ID="MessageBox1" runat="server" />
    <div class="InputPanel" >
        
        <telerik:RadWindowManager ID="RadWindowManager1" VisibleRecordedDatebar="false"  VisibleOnPageLoad="true" runat="server"></telerik:RadWindowManager>
        <fieldset id="Fieldset1">    
                <asp:Label ID="lbltext" Text=" Customer Transaction Information " runat="server"></asp:Label>
        </fieldset>
        <table><tr>
        <td colspan="3">
                    &nbsp;&nbsp;
                    <telerik:RadButton ID="RadButtonDownLoad" runat="server" Text="Download Template" OnClick="DownloadTemplate">
                    </telerik:RadButton>
                    
                </td>
            </tr>
            <tr >
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                <td>
                    <telerik:RadButton ID="RadButtonImport" runat="server" Text="Import" OnClick="ImportBookInfo">
                    </telerik:RadButton>
                    
                </td>
            </tr>
    </table>
    </div>
    <div class="InputPanel" >

        <telerik:RadGrid ID="grdCustomerTransactInfo" runat="server"
            AutoGenerateColumns="False" GridLines="None"
             AllowPaging="True"
            AllowSorting="True" ShowRecordedDateBar="True"
            Width="100%" ClientSettings-Selecting-AllowRowSelect="true"
            AllowMultiRowSelection="false" AllowFilteringByColumn="true"
            OnItemDataBound="grdCustomerTransactInfo_ItemDataBound" OnDeleteCommand="grdCustomerTransactInfo_DeleteCommand"
            EnableHeaderContextAggregatesMenu="false" OnNeedDataSource="grdCustomerTransactInfo_NeedDataSource" EnableHeaderContextFilterMenu="false"
            EnableHeaderContextMenu="false" PageSize="20" OnPreRender="grdCustomerTransactInfo_PreRender" >
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView CommandItemDisplay="Top" EnableHeaderContextAggregatesMenu="False"
                  ClientDataKeyNames="CustomerTransactID" DataKeyNames="CustomerTransactID">
                <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="false"></CommandItemSettings>
                <CommandItemTemplate>
                            <telerik:RadButton ID="btnRegister" ButtonType="LinkButton" BackColor="#a4bbc1" OnClientClicked="RegisterCustomerTransact" runat="server" Text="New CustomerTransact" AutoPostBack="false"
                            Enabled="true">
                            </telerik:RadButton>
                </CommandItemTemplate>
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="Numbers" HeaderText="No" Resizable="False"
                        Groupable="False" AllowFiltering="false" ReadOnly="True" ShowFilterIcon="false">
                        <ItemTemplate>
                            <asp:Label ID="lblpnum" runat="server" Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn Display="false" UniqueName="CustomerTransactID" HeaderText="CustomerTransactID" SortExpression="CustomerTransactID"
                        AllowFiltering="true">
                        <ItemTemplate>
                            <%#Eval("CustomerTransactID")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="CustomerID" HeaderText="CustomerID" SortExpression="CustomerID"
                        DataField="CustomerID" AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("CustomerID")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="CustomerAccount" HeaderText="CustomerAccount" SortExpression="CustomerAccount"
                        AllowFiltering="true" DataField="CustomerAccount" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("CustomerAccount")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridDateTimeColumn UniqueName="BusinessDate" HeaderText="BusinessDate" SortExpression="BusinessDate" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}"
                        DataField="BusinessDate"                         
                       AutoPostBackOnFilter="true" AllowFiltering="true" EnableTimeIndependentFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                        <HeaderStyle Width="184px" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn UniqueName="DebitedDateTime" HeaderText="DebitedDateTime" SortExpression="DebitedDateTime" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}"
                        DataField="DebitedDateTime"                         
                       AutoPostBackOnFilter="true" AllowFiltering="true" EnableTimeIndependentFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                        <HeaderStyle Width="184px" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridTemplateColumn UniqueName="TransactionAmount" HeaderText="Transaction Amount" SortExpression="TransactionAmount"
                        AllowFiltering="false" DataField="TransactionAmount" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("TransactionAmount")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="TransactionType" HeaderText="Transaction Type" SortExpression="TransactionType"
                        AllowFiltering="false" DataField="TransactionType" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("TransactionType")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="RecorderBy" HeaderText="Recorder By" SortExpression="RecorderBy"
                        AllowFiltering="false" DataField="RecorderBy" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("RecorderBy")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridDateTimeColumn UniqueName="RecordedDate" HeaderText="RecordedDate" SortExpression="RecordedDate" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}"
                        DataField="RecordedDate"                         
                       AutoPostBackOnFilter="true" AllowFiltering="true" EnableTimeIndependentFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                        <HeaderStyle Width="184px" />
                    </telerik:GridDateTimeColumn>
                </Columns>
                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                <PagerStyle Mode="NextPrevAndNumeric" />
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnRowDblClick="UpdateCustomerTransact" />
            </ClientSettings>
            <FilterItemStyle CssClass="HighZindex" />
            <FilterMenu CssClass="HighZindex" EnableRoundedCorners="True">
            </FilterMenu>
            <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default">
            </HeaderContextMenu>
            
        </telerik:RadGrid>

    </div>


</asp:Content>
