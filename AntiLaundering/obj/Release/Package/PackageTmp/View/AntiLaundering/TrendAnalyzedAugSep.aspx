<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/View/Site.Master" CodeBehind="TrendAnalyzedAugSep.aspx.cs" Inherits="AntiLaundering.View.AntiLaundering.TrendAnalyzedAugSep" %>

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
                <asp:Label ID="lbltext" Text=" Customers Classified Trended By or Operator" runat="server"></asp:Label>
        </fieldset>
    </div>
    <div class="InputPanel" >

        <telerik:RadGrid ID="grdCustomerTransactInfo" runat="server"
            AutoGenerateColumns="False" GridLines="None"
             AllowPaging="True"
            AllowSorting="True" ShowRecordedDateBar="True"
            Width="100%" ClientSettings-Selecting-AllowRowSelect="true"
            AllowMultiRowSelection="false" AllowFilteringByColumn="true"
            OnItemDataBound="grdCustomerTransactInfo_ItemDataBound" 
            EnableHeaderContextAggregatesMenu="false" OnNeedDataSource="grdCustomerTransactInfo_NeedDataSource" EnableHeaderContextFilterMenu="false"
            EnableHeaderContextMenu="false" PageSize="20" OnPreRender="grdCustomerTransactInfo_PreRender" >
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView CommandItemDisplay="Top"   EnableHeaderContextAggregatesMenu="False"
                  ClientDataKeyNames="TXN_ID" DataKeyNames="TXN_ID">
                <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="false"></CommandItemSettings>
                
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="Numbers" HeaderText="No" Resizable="False"
                        Groupable="False" AllowFiltering="false" ReadOnly="True" ShowFilterIcon="false">
                        <ItemTemplate>
                            <asp:Label ID="lblpnum" runat="server" Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn Display="false" UniqueName="TXN_ID" HeaderText="TXN_ID" SortExpression="TXN_ID"
                        AllowFiltering="true">
                        <ItemTemplate>
                            <%#Eval("TXN_ID")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="SUSPECTFRAUD" HeaderText="SUSPECT FRAUD" SortExpression="SUSPECTFRAUD"
                        DataField="SUSPECTFRAUD" AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("SUSPECTFRAUD")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="DETECTED_BY" HeaderText="DETECTED BY" SortExpression="DETECTED_BY"
                        DataField="DETECTED_BY" AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("DETECTED_BY")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="TOTAL_TNX" HeaderText="TOTAL_TNX" SortExpression="TOTAL_TNX"
                        DataField="TOTAL_TNX" AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("TOTAL_TNX")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="DEBIT_CUSTOMER" HeaderText="DEBIT_CUSTOMER" SortExpression="DEBIT_CUSTOMER"
                        DataField="DEBIT_CUSTOMER" AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("DEBIT_CUSTOMER")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="USD_EQUIV_BALANCE" HeaderText="USD_EQUIV_BALANCE" SortExpression="USD_EQUIV_BALANCE"
                        AllowFiltering="false" DataField="USD_EQUIV_BALANCE" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("USD_EQUIV_BALANCE")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="CREDIT_AMOUNT" HeaderText="CREDIT_AMOUNT" SortExpression="CREDIT_AMOUNT"
                        AllowFiltering="false" DataField="CREDIT_AMOUNT" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("CREDIT_AMOUNT")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="AGE" HeaderText="AGE" SortExpression="AGE"
                        AllowFiltering="false" DataField="AGE" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("AGE")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="GENDER" HeaderText="GENDER" SortExpression="GENDER"
                        AllowFiltering="false" DataField="GENDER" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("GENDER")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="REGIONNAME" HeaderText="REGION NAME" SortExpression="REGIONNAME"
                        AllowFiltering="false" DataField="REGIONNAME" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("REGIONNAME")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="DISTRICTNAME" HeaderText="DISTRICT NAME" SortExpression="DISTRICTNAME"
                        AllowFiltering="false" DataField="DISTRICTNAME" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("DISTRICTNAME")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
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
