<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/View/Site.Master" CodeBehind="CBEBirrKeyInformation.aspx.cs" Inherits="CBEBirrManage.View.CBEBirr.CBEBirrKeyInformation" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RegisterCBEBirrKey() {
                var radload = radopen("CBEBirrKeyRegisteration.aspx?sec=test", "UpdateRegisteration", 800, 600, 20, 20);
                radload.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Minimize + Telerik.Web.UI.WindowBehaviors.Maximize);
               
            }
            function UpdateCBEBirrKey(sender, eventArgs) {

                var compId = eventArgs.getDataKeyValue("CBEBirrKeyID");
                var radload = radopen("CBEBirrKeyRegisteration.aspx?gt=update&compId=" + compId, "UpdateRegisteration", 800, 600, 20, 20);
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
                <asp:Label ID="lbltext" Text="CBEBirr Key Information" runat="server"></asp:Label>
        </fieldset>
    </div>
    <div class="InputPanel" >

        <telerik:RadGrid ID="grdCBEBirrKeyInfo" runat="server"
            AutoGenerateColumns="False" GridLines="None"
             AllowPaging="True"
            AllowSorting="True" ShowStatusBar="True"
            Width="100%" ClientSettings-Selecting-AllowRowSelect="true"
            AllowMultiRowSelection="false"
            OnItemDataBound="grdCBEBirrKeyInfo_ItemDataBound" OnDeleteCommand="grdCBEBirrKeyInfo_DeleteCommand"
            EnableHeaderContextAggregatesMenu="false" OnNeedDataSource="grdCBEBirrKeyInfo_NeedDataSource" EnableHeaderContextFilterMenu="false"
            EnableHeaderContextMenu="false" PageSize="20" OnPreRender="grdCBEBirrKeyInfo_PreRender" >
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView CommandItemDisplay="Top" EnableHeaderContextAggregatesMenu="False"
                  ClientDataKeyNames="CBEBirrKeyID" DataKeyNames="CBEBirrKeyID">
                <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="false"></CommandItemSettings>
                <CommandItemTemplate>
                            <telerik:RadButton ID="btnRegister" ButtonType="LinkButton" BackColor="#a4bbc1" OnClientClicked="RegisterCBEBirrKey" runat="server" Text="New CBEBirrKey" AutoPostBack="false"
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
                    <telerik:GridTemplateColumn Display="false" UniqueName="CBEBirrKeyID" HeaderText="CBEBirrKeyID" SortExpression="CBEBirrKeyID"
                        AllowFiltering="true">
                        <ItemTemplate>
                            <%#Eval("CBEBirrKeyID")%>
                        </ItemTemplate>

                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="CBEBirrKeyValue" HeaderText="CBEBirrKey " SortExpression="CBEBirrKeyValue"
                        AllowFiltering="false" DataField="CBEBirrKeyValue" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("CBEBirrKeyValue")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="KeyName" HeaderText="KeyName " SortExpression="KeyName"
                        AllowFiltering="false" DataField="KeyName" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("KeyName")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="DateCreated" HeaderText="Start Date" SortExpression="DateCreated"
                        AllowFiltering="false" DataField="DateCreated" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("DateCreated")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="RecieveIP" HeaderText="Recieve IP" SortExpression="RecieveIP"
                        AllowFiltering="false" DataField="RecieveIP" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("RecieveIP")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="RecievePort" HeaderText="Recieve Port" SortExpression="RecievePort"
                        AllowFiltering="false" DataField="RecievePort" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("RecievePort")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="CreatedBy" HeaderText="Changed By" SortExpression="CreatedBy"
                        AllowFiltering="false" DataField="CreatedBy" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("CreatedBy")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="ChangedBy" HeaderText="Changed By" SortExpression="ChangedBy"
                        AllowFiltering="false" DataField="ChangedBy" FilterControlAltText="">
                        <ItemTemplate>
                            <%#Eval("ChangedBy")%>
                        </ItemTemplate>
                        <HeaderStyle Width="184px" />
                    </telerik:GridTemplateColumn>
                     <telerik:GridButtonColumn ButtonType="ImageButton" HeaderText="Delete" ConfirmText="Are You Sure You want to delete the CBEBirrKey Version?"
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
                <ClientEvents OnRowDblClick="UpdateCBEBirrKey" />
            </ClientSettings>
            <FilterItemStyle CssClass="HighZindex" />
            <FilterMenu CssClass="HighZindex" EnableRoundedCorners="True">
            </FilterMenu>
            <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default">
            </HeaderContextMenu>
            
        </telerik:RadGrid>

    </div>


</asp:Content>
