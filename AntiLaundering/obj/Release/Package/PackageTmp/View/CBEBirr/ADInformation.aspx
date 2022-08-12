<%@ Page Title="" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="ADInformation.aspx.cs" Inherits="CBEBirrManage.View.SMS.ADInformation" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script type="text/javascript">
         function FillAD(sender, eventArgs) {
             var ADID = eventArgs.getDataKeyValue("ADID");
             var ADIP = eventArgs.getDataKeyValue("ADIP");
             var ADPort = eventArgs.getDataKeyValue("ADPort");
             var UserName = eventArgs.getDataKeyValue("UserName");
             var Password = eventArgs.getDataKeyValue("Password");
             //alert(email);
             $find('<%=txtADID.ClientID %>').set_value(ADID);
             $find('<%=txtADIP.ClientID %>').set_value(ADIP);
             $find('<%=txtADPort.ClientID %>').set_value(ADPort);
             $find('<%=txtUserName.ClientID %>').set_value(UserName);
             $find('<%=txtPassword.ClientID %>').set_value(Password);
        }
        
    </script>
    <style type="text/css">
        .auto-style2 {
            width: 194px;
        }
        .auto-style5 {
            height: 129px;
        }
        .auto-style6 {
            width: 16px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
   <div>
        <uc1:errormsg ID="ErrorMsg" runat="server" />
        <uc2:MessageBox ID="MessageBox" runat="server" />
       <fieldset id="Fieldset1">    
                <asp:Label ID="lbltext" Text=" AD Integration parameters Management" runat="server"></asp:Label>
        </fieldset>
       <br />
   </div>
     <div class="auto-style5">
         <br />
    <table>
        <tr>
            <td class="auto-style2" colspan="2">IP Address</td>
            <td><telerik:RadTextBox ID="txtADIP" runat="server" ></telerik:RadTextBox></td>
            <td class="auto-style6">Post</td>
            <td><telerik:RadNumericTextBox NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0" ID="txtADPort" runat="server" ></telerik:RadNumericTextBox></td>
        </tr> 
        <tr>
            <td class="auto-style2" colspan="2">User Name</td>
            <td><telerik:RadTextBox ID="txtUserName" runat="server" ></telerik:RadTextBox></td>
            <td class="auto-style6">Password</td>
            <td><telerik:RadTextBox ID="txtPassword" runat="server" ></telerik:RadTextBox></td>
        </tr> 
        <tr>
            <td ><telerik:RadButton ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click"></telerik:RadButton>
               
            </td>
            <td ><telerik:RadButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"></telerik:RadButton>
               
            </td>
            <td><telerik:RadButton ID="btnSend" runat="server" Text="Update" OnClick="btnSend_Click"></telerik:RadButton>
                 <div hidden="hidden"> <telerik:RadTextBox ID="txtADID" runat="server"></telerik:RadTextBox></div></td>

            <td class="auto-style6">&nbsp;</td>
            <td>&nbsp;</td>

        </tr>
    
    </table>
        </div>
    <div>
        <telerik:RadGrid ID="grdADParameter" runat="server"
            AutoGenerateColumns="False" GridLines="None"
             AllowPaging="True"
            AllowSorting="True" ShowStatusBar="True"
            Width="120%"   ClientSettings-Selecting-AllowRowSelect="true"
            AllowMultiRowSelection="false"
            OnItemDataBound="grdADParameter_ItemDataBound" OnDeleteCommand="grdADParameter_DeleteCommand"
            EnableHeaderContextAggregatesMenu="false" OnNeedDataSource="grdADParameter_NeedDataSource" EnableHeaderContextFilterMenu="false"
            EnableHeaderContextMenu="false" PageSize="20" OnPreRender="grdADParameter_PreRender">
       
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="true" EnableHeaderContextAggregatesMenu="False"
                ClientDataKeyNames="ADID,ADIP,ADPort,UserName,Password" DataKeyNames="ADID,ADIP,ADPort,UserName,Password">
                <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="false"></CommandItemSettings>
               
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="Numbers" HeaderText="No" Resizable="False"
                        Groupable="False" AllowFiltering="false" ReadOnly="True">
                        <ItemTemplate>
                            <asp:Label ID="lblpnum" runat="server" Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="20px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn Display="false" UniqueName="ADID" HeaderText="ADID" SortExpression="ADID"
                        AllowFiltering="false">
                        <ItemTemplate>
                            <%#Eval("ADID")%>
                        </ItemTemplate>

                        <HeaderStyle Width="14px" />
                    </telerik:GridTemplateColumn>
                   
                    <telerik:GridTemplateColumn UniqueName="ADIP" HeaderText="AD IP" SortExpression="ADIP"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" DataField="SenderID" AllowFiltering="true">
                        <ItemTemplate>
                            <%#Eval("ADIP")%>
                        </ItemTemplate>

                        <HeaderStyle Width="30px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="ADPort" HeaderText="AD Port" SortExpression="ADPort"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" DataField="ADPort" AllowFiltering="true">
                        <ItemTemplate>
                            <%#Eval("ADPort")%>
                        </ItemTemplate>
                        <HeaderStyle Width="20px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="UserName" HeaderText="User Name" SortExpression="UserName"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" DataField="UserName" AllowFiltering="true">
                        <ItemTemplate>
                            <%#Eval("UserName")%>
                        </ItemTemplate>

                        <HeaderStyle Width="154px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" DataField="CreatedBy" AllowFiltering="true">
                        <ItemTemplate>
                            <%#Eval("CreatedBy")%>
                        </ItemTemplate>

                        <HeaderStyle Width="154px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="UpdatedBy" HeaderText="Updated By" SortExpression="UpdatedBy"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" DataField="UpdatedBy" AllowFiltering="true">
                        <ItemTemplate>
                            <%#Eval("UpdatedBy")%>
                        </ItemTemplate>

                        <HeaderStyle Width="154px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridDateTimeColumn DataField="DateCreated" HeaderText="Created Date" UniqueName="DateCreated" DataType="System.DateTime"  DataFormatString="{0:MM/dd/yyyy HH:mm}"
                        AutoPostBackOnFilter="true" AllowFiltering="true" EnableTimeIndependentFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                        <HeaderStyle Width="54px" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" HeaderText="Delete" ConfirmText="Are You Sure You want to delete the parameter"
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
                <ClientEvents OnRowDblClick="FillAD" />
            </ClientSettings>
            <FilterItemStyle CssClass="HighZindex" />
            <FilterMenu CssClass="HighZindex" EnableRoundedCorners="True">
            </FilterMenu>
            <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default">
            </HeaderContextMenu>

        </telerik:RadGrid>

    </div>
</asp:Content>
