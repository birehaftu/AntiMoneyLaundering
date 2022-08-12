<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/View/Site.Master" CodeBehind="listusers.aspx.cs" Inherits="AntiLaundering.View.UserManagement.listusers" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc2" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

   
 <div style="width:100%">
  
 
        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" 
         meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
       <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Default" 
         InitialBehavior="None" meta:resourcekey="RadWindowManager1Resource1" />
         <%--<br />--%>
        

<%--<div class="column" style="width: 100%;">--%>
    <uc2:errormsg ID="ErrorMsg1" runat="server" />
    <uc1:MessageBox ID="MessageBox1" runat="server" />
     
    <fieldset id="Fieldset2">
            <asp:Label ID="Label2" Text="User List" runat="server"></asp:Label>
        <br />

    </fieldset><table class="ui-accordion">
            <tr>
                <td class="auto-style1">
                 <asp:Label  ID="Literal1" AssociatedControlID="UserRoles" Text="Role Filter:" runat="server" 
                         meta:resourcekey="Literal1Resource1"/> 

                </td>
                <td class="auto-style2"> 

                <telerik:RadComboBox ID="UserRoles" runat="server"  Filter="Contains" AppendDataBoundItems="True" 
                        AutoPostBack="True" meta:resourcekey="UserRolesResource1" OnSelectedIndexChanged="UserRoles_SelectedIndexChanged">
                        <Items>
                        <telerik:RadComboBoxItem Text="Show All" Value="" runat="server" 
                                 />
                        </Items>
                        </telerik:RadComboBox>

                    </td>
            </tr>
        </table>
&nbsp;
     <telerik:RadGrid ID="RadGridUsers" runat="server" Width="100%" 
                        onupdatecommand="RadGridUsers_UpdateCommand"
                        onneeddatasource="RadGridUsers_NeedDataSource"   OnItemDataBound="RadGridUsers_ItemDataBound" 
                        ondeletecommand="RadGridUsers_DeleteCommand" GridLines="None" 
                         meta:resourcekey="RadGridUsersResource1" >
                          <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True"/> 
                        <GroupingSettings CaseSensitive="false" /><MasterTableView AutoGenerateColumns="False" AllowFilteringByColumn="True" 
                            DataKeyNames="USERNAME" CommandItemDisplay="Top" EditMode="PopUp" >
                          <CommandItemSettings AddNewRecordText="Add New User" ShowAddNewRecordButton="false"  ShowExportToWordButton="true"
                                ExportToPdfText="Export to Pdf" />
                          <Columns>
                               <telerik:GridTemplateColumn UniqueName="Numbers" AllowFiltering="false" ShowFilterIcon="false" HeaderText="No" Resizable="False"
                            Groupable="False" ReadOnly="True">
                            <ItemTemplate>
                                <asp:Label ID="lblbnum" runat="server" Width="30px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                        </telerik:GridTemplateColumn>     
                          <telerik:GridEditCommandColumn UniqueName="btnEdit" ButtonType="ImageButton" meta:resourcekey="GridEditCommandColumnResource1"/>   
                         
                               <telerik:GridBoundColumn DataField="FullName" UniqueName="FullName" 
                                  HeaderText="Full Name"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false"  AllowFiltering="true"  /> 
                              <%--<telerik:GridCheckBoxColumn DataField="IsLockedOut" UniqueName="IsLockedOut" HeaderText="Lockedout" AllowFiltering="false"></telerik:GridCheckBoxColumn>--%>
                               <telerik:GridBoundColumn DataField="UserName" UniqueName="UserName" 
                                  HeaderText="User Name"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false"  AllowFiltering="true"  />                   
                          <telerik:GridBoundColumn DataField="Email" UniqueName="Email" HeaderText="Email"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false"  AllowFiltering="true"  
                                  Visible="true"/>
                              <telerik:GridBoundColumn DataField="EmployeeId" UniqueName="EmployeeId" HeaderText="Employee Id"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false"  AllowFiltering="true"  
                                  Visible="true"/>
                              
                          <telerik:GridCheckBoxColumn DataField="IsLockedOut" UniqueName="IsLockedOut" Visible="true"
                                  HeaderText="Locked" AllowFiltering="false"/>
                              
                               <telerik:GridBoundColumn DataField="Address" UniqueName="Address" 
                                  HeaderText="Address" AllowFiltering="false"/> 
                              
                               <telerik:GridBoundColumn DataField="Description" UniqueName="Description" 
                                  HeaderText="comment" AllowFiltering="false"/> 
                          <telerik:GridCheckBoxColumn DataField="IsOnline" UniqueName="IsOnline" 
                                  HeaderText="Online" AllowFiltering="false" Visible="false"/>                          
                    <telerik:GridTemplateColumn HeaderText="Last Activity" UniqueName="colLastActivityDate" 
                            DataField="LastActivityDate" ReadOnly="True" AllowFiltering="false" meta:resourcekey="GridcolLastActivityDate" >
                            <ItemTemplate>  
                           <%--<asp:Label  Font-Italic="True" id="lblLastActivityDate" runat="server" Text='<%#ConvertedDate(Container.DataItem,"LastActivityDate") %>'  />  --%>
                                <asp:Label  Font-Italic="True" id="lblLastActivityDate" runat="server" Text='<%#Eval("LastActivityDate") %>'  /> 
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>  
                   <telerik:GridTemplateColumn HeaderText="Last Login" UniqueName="colLastLoginDate" 
                            DataField="LastLoginDate" ReadOnly="True" AllowFiltering="false" meta:resourcekey="GridcolLastLoginDate">
                            <ItemTemplate>  
                            <asp:Label  Font-Italic="True" id="lblLastLoginDate" runat="server" Text='<%# Eval("LastLoginDate") %>'  />  
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>  
                <telerik:GridButtonColumn UniqueName="btnDelete" Text="Delete" 
                                  ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px" 
                                  ConfirmDialogType="Classic" ConfirmDialogWidth="250px" 
                                  ConfirmText="Are you sure to delete?" ConfirmTitle="Delete Confirmation" 
                                  meta:resourcekey="GridButtonColumnResource1" />
                           </Columns>
                         <EditFormSettings UserControlName="EditUser.ascx" EditFormType="WebUserControl" InsertCaption="Add New User" >
                            <PopUpSettings Modal="True" Width="600px"/>
                          </EditFormSettings>
                        </MasterTableView>
                        
                    </telerik:RadGrid>
           
    </div>

 
<%--</div>--%>      

</asp:Content>



    <asp:Content ID="Content2" runat="server" contentplaceholderid="HeadContent">
        <style type="text/css">
            .auto-style1 {
                width: 87px;
                height: 2px;
            }
            .auto-style2 {
                height: 2px;
            }
        </style>
</asp:Content>




    