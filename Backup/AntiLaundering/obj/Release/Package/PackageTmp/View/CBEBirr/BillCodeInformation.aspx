<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/View/Site.Master" CodeBehind="BillCodeInformation.aspx.cs" Inherits="CBEBirrManage.View.CBEBirr.BillCodeInformation" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RegisterBillCode() {
                var radload = radopen("BillCodeRegisteration.aspx?sec=test", "UpdateRegisteration", 800, 600, 20, 20);
                radload.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Minimize + Telerik.Web.UI.WindowBehaviors.Maximize);
               
            }
            function UpdateBillCode(sender, eventArgs) {

                var compId = eventArgs.getDataKeyValue("BillCodeID");
                var radload = radopen("BillCodeRegisteration.aspx?gt=update&compId=" + compId, "UpdateRegisteration", 800, 600, 20, 20);
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
        
        <telerik:RadWindowManager ID="RadWindowManager1" VisibleAccountbar="false" OnClientClose="RefreshParentPage" runat="server"></telerik:RadWindowManager>
        <fieldset id="Fieldset1">    
                <asp:Label ID="lbltext" Text=" Mobile BillCode Information" runat="server"></asp:Label>
        </fieldset>
    </div>
    <div class="InputPanel" >

        <telerik:RadGrid ID="grdBillCodeInfo" runat="server"
            AutoGenerateColumns="False" GridLines="None"
             AllowPaging="True"
            AllowSorting="True" ShowAccountBar="True"
            Width="100%" ClientSettings-Selecting-AllowRowSelect="true"
            AllowMultiRowSelection="false"
            OnItemDataBound="grdBillCodeInfo_ItemDataBound" OnDeleteCommand="grdBillCodeInfo_DeleteCommand"
            EnableHeaderContextAggregatesMenu="false" OnNeedDataSource="grdBillCodeInfo_NeedDataSource" EnableHeaderContextFilterMenu="false"
            EnableHeaderContextMenu="false" PageSize="20" OnPreRender="grdBillCodeInfo_PreRender" >
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView CommandItemDisplay="Top" EnableHeaderContextAggregatesMenu="False"
                  ClientDataKeyNames="BillCodeID" DataKeyNames="BillCodeID">
                <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="false"></CommandItemSettings>
                <CommandItemTemplate>
                            <telerik:RadButton ID="btnRegister" ButtonType="LinkButton" BackColor="#a4bbc1" OnClientClicked="RegisterBillCode" runat="server" Text="New BillCode" AutoPostBack="false"
                            Enabled="true">
                            </telerik:RadButton>
                </CommandItemTemplate>
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="Numbers" HeaderText="No" Resizable="False"
                        Groupable="False" ReadOnly="True">
                        <ItemTemplate>
                            <asp:Label ID="lblpnum" runat="server" Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn Display="false" UniqueName="BillCodeID" HeaderText="BillCodeID" SortExpression="BillCodeID"
                        AllowFiltering="true">
                        <ItemTemplate>
                            <%#Eval("BillCodeID")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="BillCodeName" HeaderText="BillCodeName" SortExpression="BillCodeName"
                        AllowFiltering="false" DataField="BillCodeName" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("BillCodeName")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="BillCodeName_AM" HeaderText="BillCodeName_AM" SortExpression="BillCodeName_AM"
                        AllowFiltering="false" DataField="BillCodeName_AM" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("BillCodeName_AM")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="BillCodeName_OR" HeaderText="BillCodeName_OR" SortExpression="BillCodeName_OR"
                        AllowFiltering="false" DataField="BillCodeName_OR" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("BillCodeName_OR")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="BillCodeName_TG" HeaderText="BillCodeName_TG" SortExpression="BillCodeName_TG"
                        AllowFiltering="false" DataField="BillCodeName_TG" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("BillCodeName_TG")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="BillCodeName_SM" HeaderText="BillCodeName_SM" SortExpression="BillCodeName_SM"
                        AllowFiltering="false" DataField="BillCodeName_SM" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("BillCodeName_SM")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="BillCodeName_AF" HeaderText="BillCodeName_AF" SortExpression="BillCodeName_AF"
                        AllowFiltering="false" DataField="BillCodeName_AF" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("BillCodeName_AF")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="Account" HeaderText="Account" SortExpression="Account"
                        AllowFiltering="false" DataField="Account" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("Account")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" HeaderText="Delete" ConfirmText="Are You Sure You want to delete the BillCodeName?"
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
                <ClientEvents OnRowDblClick="UpdateBillCode" />
            </ClientSettings>
            <FilterItemStyle CssClass="HighZindex" />
            <FilterMenu CssClass="HighZindex" EnableRoundedCorners="True">
            </FilterMenu>
            <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default">
            </HeaderContextMenu>
            
        </telerik:RadGrid>

    </div>


</asp:Content>
