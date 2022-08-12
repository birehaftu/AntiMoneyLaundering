<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/View/Site.Master" CodeBehind="ThirdPartyInformation.aspx.cs" Inherits="CBEBirrManage.View.CBEBirr.ThirdPartyInformation" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RegisterThirdParty() {
                var radload = radopen("ThirdPartyRegisteration.aspx?sec=test", "UpdateRegisteration", 800, 600, 20, 20);
                radload.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Minimize + Telerik.Web.UI.WindowBehaviors.Maximize);
               
            }
            function UpdateThirdParty(sender, eventArgs) {

                var compId = eventArgs.getDataKeyValue("ThirdPartyID");
                var radload = radopen("ThirdPartyRegisteration.aspx?gt=update&compId=" + compId, "UpdateRegisteration", 800, 600, 20, 20);
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
                <asp:Label ID="lbltext" Text=" ThirdParty Information" runat="server"></asp:Label>
        </fieldset>
    </div>
    <div class="InputPanel" >

        <telerik:RadGrid ID="grdThirdPartyInfo" runat="server"
            AutoGenerateColumns="False" GridLines="None"
             AllowPaging="True"
            AllowSorting="True" ShowStatusBar="True"
            Width="100%" ClientSettings-Selecting-AllowRowSelect="true"
            AllowMultiRowSelection="false"
            OnItemDataBound="grdThirdPartyInfo_ItemDataBound" OnDeleteCommand="grdThirdPartyInfo_DeleteCommand"
            EnableHeaderContextAggregatesMenu="false" OnNeedDataSource="grdThirdPartyInfo_NeedDataSource" EnableHeaderContextFilterMenu="false"
            EnableHeaderContextMenu="false" PageSize="20" OnPreRender="grdThirdPartyInfo_PreRender" >
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView CommandItemDisplay="Top" EnableHeaderContextAggregatesMenu="False"
                  ClientDataKeyNames="ThirdPartyID" DataKeyNames="ThirdPartyID">
                <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="false"></CommandItemSettings>
                <CommandItemTemplate>
                            <telerik:RadButton ID="btnRegister" ButtonType="LinkButton" BackColor="#a4bbc1" OnClientClicked="RegisterThirdParty" runat="server" Text="New ThirdParty" AutoPostBack="false"
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
                    <telerik:GridTemplateColumn Display="false" UniqueName="ThirdPartyID" HeaderText="ThirdPartyID" SortExpression="ThirdPartyID"
                        AllowFiltering="true">
                        <ItemTemplate>
                            <%#Eval("ThirdPartyID")%>
                        </ItemTemplate>

                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="ThirdPartyName" HeaderText="ThirdParty Name" SortExpression="ThirdPartyName"
                        AllowFiltering="true" DataField="ThirdPartyName" FilterControlAltText="Filter by ThirdPartyName">
                        <ItemTemplate>
                            <%#Eval("ThirdPartyName")%>
                        </ItemTemplate>

                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="ShortCode" HeaderText="Short Code" SortExpression="ShortCode"
                        AllowFiltering="false" DataField="ShortCode" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("ShortCode")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="ThirdPartyURL" HeaderText="ThirdParty URL" SortExpression="ThirdPartyURL"
                        AllowFiltering="false" DataField="ThirdPartyURL" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("ThirdPartyURL")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="ReceiverURL" HeaderText="Receiver URL" SortExpression="ReceiverURL"
                        AllowFiltering="false" DataField="ReceiverURL" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("ReceiverURL")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="UserName" HeaderText="UserName" SortExpression="UserName"
                        AllowFiltering="false" DataField="UserName" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("UserName")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn UniqueName="SendConfimation" HeaderText="SendConfimation" SortExpression="SendConfimation"
                        AllowFiltering="false" DataField="SendConfimation" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("SendConfimation")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>

                    <telerik:GridButtonColumn ButtonType="ImageButton" HeaderText="Delete" ConfirmText="Are You Sure You want to delete the ThirdParty"
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
                <ClientEvents OnRowDblClick="UpdateThirdParty" />
            </ClientSettings>
            <FilterItemStyle CssClass="HighZindex" />
            <FilterMenu CssClass="HighZindex" EnableRoundedCorners="True">
            </FilterMenu>
            <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default">
            </HeaderContextMenu>
            
        </telerik:RadGrid>

    </div>


</asp:Content>
