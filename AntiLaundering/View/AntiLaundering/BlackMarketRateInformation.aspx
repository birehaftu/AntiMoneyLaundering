<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/View/Site.Master" CodeBehind="BlackMarketRateInformation.aspx.cs" Inherits="AntiLaundering.View.AntiLaundering.BlackMarketRateInformation" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/um/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="~/View/um/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RegisterBlackMarketRate() {
                var radload = radopen("BlackMarketRateRegisteration.aspx?sec=test", "UpdateRegisteration", 800, 600, 20, 20);
                radload.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Minimize + Telerik.Web.UI.WindowBehaviors.Maximize);
               
            }
            function UpdateBlackMarketRate(sender, eventArgs) {

                var compId = eventArgs.getDataKeyValue("BlackMarketRateID");
                var radload = radopen("BlackMarketRateRegisteration.aspx?gt=update&compId=" + compId, "UpdateRegisteration", 800, 600, 20, 20);
                radload.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Minimize + Telerik.Web.UI.WindowBehaviors.Maximize);
            }
            function RefreshParentPage()//function in parent page
            {
                window.location.href = window.location.href;
            }
            
        </script>
    </telerik:RadCodeBlock>
    <script type="text/javascript">
        function onRadWindowShow(sender, arg) {
            sender.maximize(true);
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <uc1:errormsg ID="ErrorMsg1" runat="server" />
        <uc2:MessageBox ID="MessageBox1" runat="server" />
    <div class="InputPanel" >
        
        <telerik:RadWindowManager ID="RadWindowManager1" VisibleRecordedDatebar="false"  OnClientShow="onRadWindowShow"  VisibleOnPageLoad="true" OnClientClose="RefreshParentPage" runat="server"></telerik:RadWindowManager>
        <fieldset id="Fieldset1">    
                <asp:Label ID="lbltext" Text=" Black Market Rate Information" runat="server"></asp:Label>
        </fieldset>
    </div>
    <div class="InputPanel" >

        <telerik:RadGrid ID="grdBlackMarketRateInfo" runat="server"
            AutoGenerateColumns="False" GridLines="None"
             AllowPaging="True"
            AllowSorting="True" ShowRecordedDateBar="True"
            Width="100%" ClientSettings-Selecting-AllowRowSelect="true"
            AllowMultiRowSelection="false" AllowFilteringByColumn="true"
            OnItemDataBound="grdBlackMarketRateInfo_ItemDataBound" OnDeleteCommand="grdBlackMarketRateInfo_DeleteCommand"
            EnableHeaderContextAggregatesMenu="false" OnNeedDataSource="grdBlackMarketRateInfo_NeedDataSource" EnableHeaderContextFilterMenu="false"
            EnableHeaderContextMenu="false" PageSize="20" OnPreRender="grdBlackMarketRateInfo_PreRender" >
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView CommandItemDisplay="Top" EnableHeaderContextAggregatesMenu="False"
                  ClientDataKeyNames="BlackMarketRateID" DataKeyNames="BlackMarketRateID">
                <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="false"></CommandItemSettings>
                <CommandItemTemplate>
                            <telerik:RadButton ID="btnRegister" ButtonType="LinkButton" BackColor="#a4bbc1" OnClientClicked="RegisterBlackMarketRate" runat="server" Text="New BlackMarketRate" AutoPostBack="false"
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
                    <telerik:GridTemplateColumn Display="false" UniqueName="BlackMarketRateID" HeaderText="BlackMarketRateID" SortExpression="BlackMarketRateID"
                        AllowFiltering="true">
                        <ItemTemplate>
                            <%#Eval("BlackMarketRateID")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="ExchangeName" HeaderText="ExchangeName" SortExpression="ExchangeName"
                        DataField="ExchangeName" AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("ExchangeName")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="ExchangeCode" HeaderText="ExchangeCode" SortExpression="ExchangeCode"
                        AllowFiltering="true" DataField="ExchangeCode" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("ExchangeCode")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="Country" HeaderText="Country" SortExpression="Country"
                        AllowFiltering="true" DataField="Country" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("Country")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridDateTimeColumn UniqueName="RateDate" HeaderText="RateDate" SortExpression="RateDate" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}"
                        DataField="RateDate"                         
                       AutoPostBackOnFilter="true" AllowFiltering="true" EnableTimeIndependentFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                        <HeaderStyle Width="184px" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridTemplateColumn UniqueName="RateAmountInBirr" HeaderText="Black Market Rate" SortExpression="RateAmountInBirr"
                        AllowFiltering="false" DataField="RateAmountInBirr" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("RateAmountInBirr")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="BankRate" HeaderText="Bank Rate" SortExpression="BankRate"
                        AllowFiltering="false" DataField="BankRate" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("BankRate")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="RecorderBy" HeaderText="RecorderBy" SortExpression="RecorderBy"
                        DataField="RecorderBy" FilterControlAltText="" AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <%#Eval("RecorderBy")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridDateTimeColumn UniqueName="RecordedDate" HeaderText="RecordedDate" SortExpression="RecordedDate" DataField="RecordedDate"   DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}"
                       AutoPostBackOnFilter="true" AllowFiltering="true" EnableTimeIndependentFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                       <HeaderStyle Width="184px" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" HeaderText="Delete" ConfirmText="Are You Sure You want to delete the ExchangeName?"
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
                <ClientEvents OnRowDblClick="UpdateBlackMarketRate" />
            </ClientSettings>
            <FilterItemStyle CssClass="HighZindex" />
            <FilterMenu CssClass="HighZindex" EnableRoundedCorners="True">
            </FilterMenu>
            <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default">
            </HeaderContextMenu>
            
        </telerik:RadGrid>

    </div>


</asp:Content>
