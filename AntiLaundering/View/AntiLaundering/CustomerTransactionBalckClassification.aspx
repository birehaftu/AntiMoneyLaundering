<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/View/Site.Master" CodeBehind="CustomerTransactionBalckClassification.aspx.cs" Inherits="AntiLaundering.View.AntiLaundering.CustomerTransactionBalckClassification" %>

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
                <asp:Label ID="lbltext" Text=" Customer Transaction Classified By black market rate " runat="server"></asp:Label>
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
            <MasterTableView CommandItemDisplay="Top" EnableHeaderContextAggregatesMenu="False"
                  ClientDataKeyNames="TXN_ID" DataKeyNames="TXN_ID">
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
                    <telerik:GridTemplateColumn UniqueName="EXCHANGERATE" HeaderText="EXCHANGE RATE" SortExpression="EXCHANGERATE"
                        DataField="EXCHANGERATE" AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("EXCHANGERATE")%>
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
                    <telerik:GridTemplateColumn UniqueName="CREDIT_CUSTOMER" HeaderText="CREDIT_CUSTOMER" SortExpression="CREDIT_CUSTOMER"
                        AllowFiltering="true" DataField="CREDIT_CUSTOMER" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("CREDIT_CUSTOMER")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="DEBIT_ACCT_NO" HeaderText="DEBIT_ACCT_NO" SortExpression="DEBIT_ACCT_NO"
                        AllowFiltering="true" DataField="DEBIT_ACCT_NO" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("DEBIT_ACCT_NO")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="CREDIT_ACCT_NO" HeaderText="CREDIT_ACCT_NO" SortExpression="CREDIT_ACCT_NO"
                        AllowFiltering="true" DataField="CREDIT_ACCT_NO" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("CREDIT_ACCT_NO")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridDateTimeColumn UniqueName="BUSINESS_DATE" HeaderText="BUSINESS_DATE" SortExpression="BUSINESS_DATE" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}"
                        DataField="BUSINESS_DATE"                         
                       AutoPostBackOnFilter="true" AllowFiltering="true" EnableTimeIndependentFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                        <HeaderStyle Width="184px" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridTemplateColumn UniqueName="TRANSACTION_BY" HeaderText="TRANSACTION_BY" SortExpression="TRANSACTION_BY"
                        AllowFiltering="true" DataField="TRANSACTION_BY" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("TRANSACTION_BY")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="DEBIT_CURRENCY" HeaderText="DEBIT_CURRENCY" SortExpression="DEBIT_CURRENCY"
                        AllowFiltering="true" DataField="DEBIT_CURRENCY" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("DEBIT_CURRENCY")%>
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
