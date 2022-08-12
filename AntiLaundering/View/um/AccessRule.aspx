<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/View/Site.Master" CodeBehind="AccessRule.aspx.cs" Inherits="AntiLaundering.View.UserManagement.AccessRule" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="System.Web.Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<br />

    <div style="margin-left:50px">
<table style="width:100%">
<tr>
	<th>
    <asp:Localize ID="Localize1" runat="server" 
            Text="Access Rule Management" 
             />
   </th>
</tr>
<tr>
	<td class="details" valign="top">
		<p>
		<i>
         <asp:Localize ID="Localize2" runat="server" 
                Text="  Use this page to manage access rules for the system users. Rules are applied to folders." 
                 />
       </i>
		</p>



		<table style="width:100%">
		<tr>
			<td valign="top" style="padding-right: 30px;">
				<div class="treeview">
                   
				<asp:TreeView runat="server" ID="FolderTree" 
					OnSelectedNodeChanged="FolderTree_SelectedNodeChanged" ExpandDepth="1" 
                        meta:resourcekey="FolderTreeResource1">
                    
					<RootNodeStyle ImageUrl="~/View/images/Folder-yellow.png" />
					<ParentNodeStyle ImageUrl="~/View/images/Folder-yellow.png" />
					<LeafNodeStyle ImageUrl="~/View/images/Folder-yellow.png" />
					<SelectedNodeStyle Font-Underline="True" ForeColor="#A21818" />
				</asp:TreeView>
				</div> 
			</td>

			<td valign="top" style="padding-left: 30px; border-left: 1px solid #999;width:80%">
			<asp:Panel runat="server" ID="SecurityInfoSection" Visible="False" 
                    meta:resourcekey="SecurityInfoSectionResource1">
				<h2 runat="server" id="TitleOne" class="alert"></h2>
				
				<p>
             <asp:Localize ID="Localize3" runat="server" 
                        Text=" Rules are applied in order. The first rule that matches applies, and the permission in each rule overrides the permissions in all following rules. Use the Move Up and Move Down buttons to change the order of the selected rule. Rules that appear dimmed are inherited from the parent and cannot be changed at this level. " 
                        meta:resourcekey="Localize3Resource1" />
				
				</p>
				<telerik:RadGrid ID="RadGridRuleDisplay" runat="server"  Width="100%" 
                    onitemdatabound="RadGridRuleDisplay_ItemDataBound" GridLines="None" 
                    meta:resourcekey="RadGridRuleDisplayResource1" >
                <GroupingSettings CaseSensitive="false" /><MasterTableView AutoGenerateColumns="False">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                <Columns>
                <telerik:GridTemplateColumn HeaderText="Action" UniqueName="Action" 
                        meta:resourcekey="GridTemplateColumnResource1">
                <ItemTemplate>
                  <%# GetAction((AuthorizationRule)Container.DataItem) %>
                </ItemTemplate>
                </telerik:GridTemplateColumn>
                 <telerik:GridTemplateColumn HeaderText="Roles" UniqueName="Roles" 
                        meta:resourcekey="GridTemplateColumnResource2">
                <ItemTemplate>
                  <%# GetRole((AuthorizationRule)Container.DataItem)%>
                </ItemTemplate>
                </telerik:GridTemplateColumn>
                 <telerik:GridTemplateColumn HeaderText="Users" UniqueName="Users" 
                        meta:resourcekey="GridTemplateColumnResource3">
                <ItemTemplate>
                  <%# GetUser((AuthorizationRule)Container.DataItem)%>
                </ItemTemplate>
                </telerik:GridTemplateColumn>
                 <telerik:GridTemplateColumn HeaderText="Delete Rule" UniqueName="DeleteRule" 
                        meta:resourcekey="GridTemplateColumnResource4">
                <ItemTemplate>
                   <asp:ImageButton ID="Button1" ToolTip="Delete Rule" AlternateText="Delete Rule" 
                        runat="server" 
                        ImageUrl='<%# RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Grid.Delete.gif") %>' 
                        CommandArgument="<%# (AuthorizationRule)Container.DataItem %>" 
                        OnClick="DeleteRule" 
                        OnClientClick="return confirm('Click OK to delete this rule.')" 
                        meta:resourcekey="Button1Resource1"/> 
				</ItemTemplate>
                </telerik:GridTemplateColumn>
                 <telerik:GridTemplateColumn HeaderText="Move" UniqueName="Move" 
                        meta:resourcekey="GridTemplateColumnResource5">
                <ItemTemplate>
								<asp:ImageButton ID="Button2" runat="server" AlternateText="  Up  " 
                                    CommandArgument="<%# (AuthorizationRule)Container.DataItem %>" OnClick="MoveUp" 
                                    ImageUrl="~/View/images/Arrow-Up.png" meta:resourcekey="Button2Resource1" /> &nbsp;&nbsp;&nbsp;&nbsp;
								<asp:ImageButton ID="Button3" runat="server" AlternateText="Down" 
                                    CommandArgument="<%# (AuthorizationRule)Container.DataItem %>" 
                                    OnClick="MoveDown" ImageUrl="~/View/images/Arrow-Down.png" 
                                    meta:resourcekey="Button3Resource1" />
							</ItemTemplate>
                </telerik:GridTemplateColumn>
                </Columns>
                </MasterTableView>
                </telerik:RadGrid>
				

				<br />
				<hr />
				<h2 runat="server" id="TitleTwo" class="alert"></h2>
				<b><asp:Literal ID="Literal1" runat="server" Text="Action:" 
                    meta:resourcekey="Literal1Resource1" /></b>
				<asp:RadioButton runat="server" ID="ActionDeny" GroupName="action" 
					Text="Deny" Checked="True"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:RadioButton runat="server" ID="ActionAllow" GroupName="action" 
					Text="Allow" />
				
				<br /><br />
				<b><asp:Literal ID="Literal2" runat="server" Text="Rule applies to:" 
                    meta:resourcekey="Literal2Resource1" /></b>
				<br />
				<asp:RadioButton runat="server" ID="ApplyRole" GroupName="applyto"
					Text="This Role:" Checked="True" meta:resourcekey="ApplyRoleResource1" />
                <telerik:RadComboBox  ID="UserRoles" runat="server" AppendDataBoundItems="True" 
                    Filter="Contains">
                <Items>
                <telerik:RadComboBoxItem Text="Select Role" Value="Select Role" runat="server" 
                         Owner=""/>
                </Items>
                </telerik:RadComboBox> 
				<br />
					
				<asp:RadioButton runat="server" ID="ApplyUser" GroupName="applyto"
					Text="This User:" meta:resourcekey="ApplyUserResource1" />
                <telerik:RadComboBox  ID="UserList" runat="server" AppendDataBoundItems="True" 
                    Filter="Contains" meta:resourcekey="UserListResource1">
                <Items>
                <telerik:RadComboBoxItem Text="Select User" Value="Select User" runat="server" 
                         Owner=""/>
                </Items>
                </telerik:RadComboBox> 
				<br />
				
				
				<asp:RadioButton runat="server" ID="ApplyAllUsers" GroupName="applyto"
					Text="All Users (*)" meta:resourcekey="ApplyAllUsersResource1"  />
				<br />
				
				
				<asp:RadioButton runat="server" ID="ApplyAnonUser" GroupName="applyto"
					Text="Anonymous Users (?)"  />
				<br /><br />
				
				<asp:Button ID="Button4" runat="server" Text="Create Rule" OnClick="CreateRule"
					OnClientClick="return confirm('Click OK to create this rule.');" 
                    meta:resourcekey="Button4Resource1" />
					
				<asp:Literal runat="server" ID="RuleCreationError" 
                    ></asp:Literal>
			</asp:Panel>
			</td>
		</tr>
		</table>
	</td>
</tr>
</table>
    </div>

</asp:Content>
