<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/View/Site.Master" CodeBehind="Operations.aspx.cs" Inherits="AntiLaundering.View.UserManagement.Operations" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/um/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>
<%@ Register Src="~/View/um/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<asp:Content ID="bodycontent" ContentPlaceHolderID="MainContent" runat="server">
    <uc2:MessageBox ID="MessageBox1" runat="server" />
    <uc1:errormsg ID="errormsg1" runat="server" />

    <%--<br />--%>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server"
        MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Web20" Height="26px"
        Width="100%">
        <Tabs>
            <%--<telerik:RadTab Visible="false" runat="server" Text="SubSystem"
                PageViewID="RadPageViewSubSystem">
            </telerik:RadTab>--%>
            <telerik:RadTab runat="server" Text="Modules"
                PageViewID="RadPageViewModuleRegistration" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Submodules"
                PageViewID="RadPageViewSubmodule">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="1" Width="100%">
        <telerik:RadPageView ID="RadPageViewSubSystem" runat="server" Width="100%" Enabled="true">
           
             <fieldset id="Fieldset4">
            <asp:Label ID="lbltext" Text="SubSystem Management" runat="server"></asp:Label>
            </fieldset>
            <telerik:RadGrid Visible="false" ID="grdSubsystem" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="False"
                GridLines="None" EnableEmbeddedSkins="True" AllowPaging="True" AllowSorting="True" ShowStatusBar="True"
                Width="100%" OnItemDataBound="grdSubsystem_ItemDataBound"
                OnUpdateCommand="grdSubsystem_UpdateCommand" OnDeleteCommand="grdSubsystem_DeleteCommand"
                EnableHeaderContextAggregatesMenu="False" EnableHeaderContextFilterMenu="False"
                EnableHeaderContextMenu="True" PageSize="20"
                OnNeedDataSource="grdSubsystem_NeedDataSource" OnInsertCommand="grdSubsystem_InsertCommand" CellSpacing="0" >
                <ClientSettings AllowDragToGroup="True" AllowColumnHide="True" AllowRowHide="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <AlternatingItemStyle BackColor="#FF90EE90" />
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView AllowFilteringByColumn="true" CommandItemDisplay="Top" EnableHeaderContextAggregatesMenu="True"
                    Caption="SubSystems" DataKeyNames="Id,SubSystemID">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="New SubSystem"></CommandItemSettings>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="Numbers" AllowFiltering="false" ShowFilterIcon="false" HeaderText="No" Resizable="False"
                            Groupable="False" ReadOnly="True">
                            <ItemTemplate>
                                <asp:Label ID="lblbnum" runat="server" Width="30px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn SortExpression="SubSystemName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false"  AllowFiltering="true"  DataField="SubSystemName" HeaderStyle-Width="160px" UniqueName="SubSystemName"
                            HeaderText="Module Name">
                            <ItemTemplate>
                                <%#Eval("SubSystemName")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSubSystemName" runat="server" Text='<%#Bind("SubSystemName") %>' Rows="5" CssClass="blue"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn SortExpression="Logo"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false"  AllowFiltering="true"   HeaderStyle-Width="160px" UniqueName="Logo"
                            HeaderText="Logo">
                            <%-- <ItemTemplate>
                                <%#Eval("OperationName")%>
                            </ItemTemplate>--%>
                            <EditItemTemplate>
                                <asp:Label ID="txtOperationName" runat="server" Text='Logo' CssClass="blue"></asp:Label>
                                <telerik:RadUpload ID="ULogo" runat="server" AllowedFileExtensions=".jpeg,.jpg,.png,.tiff,.bit,.bmp,.dib" ControlObjectsVisibility="None"></telerik:RadUpload>
                            </EditItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn SortExpression="Description" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false"  AllowFiltering="true" DataField="Description" HeaderStyle-Width="160px" UniqueName="Description"
                            HeaderText="Description">
                            <ItemTemplate>
                                <%#Eval("Description")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" Text='<%#Bind("Description") %>' Columns="30" Rows="5" CssClass="blue"></asp:TextBox>

                            </EditItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" Visible="true" EditImageUrl="~/View/images/btn_edit.gif"
                            ButtonType="ImageButton">
                            <HeaderStyle Width="33px" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn ButtonType="ImageButton" ConfirmText="Are You Sure You want to delete"
                            ConfirmDialogType="Classic" ConfirmTitle="Delete Confirmation" CommandName="Delete"
                            Text="Delete" UniqueName="Dcolumn">
                            <HeaderStyle Width="33px" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings InsertCaption="New SubSystem">
                        <EditColumn UniqueName="EditCommandColumn" UpdateImageUrl="~/View/images/updatte.gif"
                            CancelImageUrl="~/View/images/cancel.gif" InsertImageUrl="~/View/images/save.png" ButtonType="ImageButton">
                        </EditColumn>
                        <PopUpSettings Modal="True" ZIndex="300"></PopUpSettings>
                    </EditFormSettings>
                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                    <PagerStyle Mode="NextPrevAndNumeric" />
                </MasterTableView>
                <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default">
                </HeaderContextMenu>
            </telerik:RadGrid>

        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageViewModuleRegistration" runat="server" Width="100%" Enabled="true">
            
            <fieldset id="Fieldset3">
            <asp:Label ID="Label1" Text="Module Management" runat="server"></asp:Label>
            </fieldset>
            <%--<br />--%>
            <telerik:RadGrid ID="grdModules" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="False"
                GridLines="None" EnableEmbeddedSkins="True" AllowPaging="True" AllowSorting="True" ShowStatusBar="True"
                Width="100%" OnItemDataBound="grdModules_ItemDataBound"
                OnUpdateCommand="grdModules_UpdateCommand" OnDeleteCommand="grdModules_DeleteCommand"
                EnableHeaderContextAggregatesMenu="False" EnableHeaderContextFilterMenu="False"
                EnableHeaderContextMenu="True" PageSize="20"
                OnNeedDataSource="grdModules_NeedDataSource" OnInsertCommand="grdModules_InsertCommand" CellSpacing="0" >
                <ClientSettings AllowDragToGroup="True" AllowColumnHide="True" AllowRowHide="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <AlternatingItemStyle BackColor="#FF90EE90" />
                <GroupingSettings CaseSensitive="false" /><MasterTableView AllowFilteringByColumn="true" CommandItemDisplay="Top" EnableHeaderContextAggregatesMenu="True"
                    Caption="Modules" DataKeyNames="Id,OperationID">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="New Modules"></CommandItemSettings>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="Numbers" AllowFiltering="false" ShowFilterIcon="false" HeaderText="No" Resizable="False"
                            Groupable="False" ReadOnly="True">
                            <ItemTemplate>
                                <asp:Label ID="lblbnum" runat="server" Width="30px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn SortExpression="SubSystemName" HeaderStyle-Width="160px" UniqueName="SubSystemName"
                            HeaderText="SubSystem Name" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false" DataField="SubSystemName"  AllowFiltering="true"  >
                            <ItemTemplate>
                                <%#Eval("SubSystemName")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadComboBox ID="txtSubSystemName" Filter="Contains" runat="server" DataSourceID="SubSystemDS" DataTextField="SubSystemName"
                                    DataValueField="SubSystemName" Skin="Telerik" SelectedValue='<%#Eval("SubSystemName") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="Please Select"
                                            Value="Please Select" />
                                    </Items>
                                </telerik:RadComboBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="60px"></HeaderStyle>

                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Display="false"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataField="ModuleName"  ShowFilterIcon="false"  AllowFiltering="true"  >
                            <ItemTemplate>
                                <asp:TextBox ID="txtModuleName" runat="server" Text='<%#Bind("ModuleName") %>' Rows="5" CssClass="blue"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn SortExpression="ModuleName"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false"  AllowFiltering="true"   DataField="ModuleName" HeaderStyle-Width="160px" UniqueName="ModuleName"
                            HeaderText="Module Name">
                            <ItemTemplate>
                                <%#Eval("ModuleName")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtModuleName" runat="server" Text='<%#Bind("ModuleName") %>' Rows="5" CssClass="blue"></asp:TextBox>
                                
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4txtModuleName" runat="server" 
                                    ControlToValidate="txtModuleName" ErrorMessage="Please enter a valid Module Name" ForeColor="Red" 
                                    ValidationExpression="^([a-zA-Z ]*?)([a-zA-Z]*)$"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>

                        <%-- <telerik:GridTemplateColumn SortExpression="OperationName" AllowFiltering="true" ShowFilterIcon="false" DataField="OperationName" HeaderStyle-Width="160px" UniqueName="OperationName"
                            HeaderText="Operation Name">
                            <ItemTemplate>
                                <%#Eval("OperationName")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOperationName" runat="server" Text='<%#Bind("OperationName") %>' Rows="5" CssClass="blue"></asp:TextBox>

                            </EditItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>--%>
                        <%-- <telerik:GridTemplateColumn SortExpression="OperationLink" AllowFiltering="true" ShowFilterIcon="false" DataField="OperationLink" HeaderStyle-Width="160px" UniqueName="OperationLink"
                            HeaderText="Operation Link">
                            <ItemTemplate>
                                <%#Eval("OperationLink")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOperationLink" runat="server" Text='<%#Bind("OperationLink") %>' Rows="5" CssClass="blue"></asp:TextBox>

                            </EditItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridTemplateColumn SortExpression="Description" AllowFiltering="false" ShowFilterIcon="false" DataField="Description" HeaderStyle-Width="160px" UniqueName="Description"
                            HeaderText="Description">
                            <ItemTemplate>
                                <%#Eval("Description")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" Text='<%#Bind("Description") %>' Columns="30" Rows="5" CssClass="blue"></asp:TextBox>
                                
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4txtDescription" runat="server" 
                                    ControlToValidate="txtDescription" ErrorMessage="Please enter a valid Description" ForeColor="Red" 
                                    ValidationExpression="^([a-zA-Z ]*?)([a-zA-Z]*)$"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" Visible="true" EditImageUrl="~/View/images/btn_edit.gif"
                            ButtonType="ImageButton">
                            <HeaderStyle Width="33px" />
                        </telerik:GridEditCommandColumn>
                        <%--<telerik:GridButtonColumn ButtonType="ImageButton" ConfirmText="Are You Sure You want to delete"
                            ConfirmDialogType="Classic" ConfirmTitle="Delete Confirmation" CommandName="Delete"
                            Text="Delete" UniqueName="Dcolumn">
                            <HeaderStyle Width="33px" />
                        </telerik:GridButtonColumn>--%>
                    </Columns>
                    <EditFormSettings InsertCaption="New Modules">
                        <EditColumn UniqueName="EditCommandColumn" UpdateImageUrl="~/View/images/updatte.gif"
                            CancelImageUrl="~/View/images/cancel.gif" InsertImageUrl="~/View/images/save.png" ButtonType="ImageButton">
                        </EditColumn>
                        <PopUpSettings Modal="True" ZIndex="300"></PopUpSettings>
                    </EditFormSettings>
                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                    <PagerStyle Mode="NextPrevAndNumeric" />
                </MasterTableView>
                <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default">
                </HeaderContextMenu>
            </telerik:RadGrid>

        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageViewSubmodule" runat="server" Width="100%" Enabled="true">
            
            <fieldset id="Fieldset1">
            <asp:Label ID="Label2" Text="Operation Management" runat="server"></asp:Label>
            </fieldset>
            <telerik:RadGrid ID="grdOperation" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="False"
                GridLines="None" EnableEmbeddedSkins="True" AllowPaging="True" AllowSorting="True" ShowStatusBar="True"
                Width="100%" OnItemDataBound="grdOperation_ItemDataBound"
                OnUpdateCommand="grdOperation_UpdateCommand" OnDeleteCommand="grdOperation_DeleteCommand"
                EnableHeaderContextAggregatesMenu="False" EnableHeaderContextFilterMenu="False"
                EnableHeaderContextMenu="True" PageSize="20"
                OnNeedDataSource="grdOperation_NeedDataSource" OnInsertCommand="grdOperation_InsertCommand" CellSpacing="0" >
                <ClientSettings AllowDragToGroup="True" AllowColumnHide="True" AllowRowHide="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <AlternatingItemStyle BackColor="#FF90EE90" />
                <GroupingSettings CaseSensitive="false" /><MasterTableView AllowFilteringByColumn="true" CommandItemDisplay="Top" EnableHeaderContextAggregatesMenu="True"
                    Caption="Operations" DataKeyNames="Id,OperationID">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="New Operations"></CommandItemSettings>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="Numbers" AllowFiltering="false" ShowFilterIcon="false" HeaderText="No" Resizable="False"
                            Groupable="False" ReadOnly="True">
                            <ItemTemplate>
                                <asp:Label ID="lblbnum" runat="server" Width="30px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Display="false" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:TextBox ID="txtModuleName" runat="server" Text='<%#Bind("ModuleName") %>' Rows="5" CssClass="blue"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn SortExpression="ModuleName" HeaderStyle-Width="160px" UniqueName="ModuleName"
                            HeaderText="Module Name" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false" DataField="ModuleName"  AllowFiltering="true"  >
                            <ItemTemplate>
                                <%#Eval("ModuleName")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadComboBox ID="txtModuleName"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false"  AllowFiltering="true"  runat="server" DataSourceID="ModuleDS" DataTextField="ModuleName"
                                    DataValueField="ModuleName" Filter="Contains" Skin="Telerik" SelectedValue='<%#Eval("ModuleName") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="Please Select"
                                            Value="Please Select" />
                                    </Items>
                                </telerik:RadComboBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="60px"></HeaderStyle>

                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn SortExpression="OperationName"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false"  AllowFiltering="true"   DataField="OperationName" HeaderStyle-Width="160px" UniqueName="OperationName"
                            HeaderText="Operation Name">
                            <ItemTemplate>
                                <%#Eval("OperationName")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOperationName" runat="server" Text='<%#Bind("OperationName") %>' Rows="5" CssClass="blue"></asp:TextBox>
                                
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4txtOperationName" runat="server" 
                                    ControlToValidate="txtOperationName" ErrorMessage="Please enter a valid Operation Name" ForeColor="Red" 
                                    ValidationExpression="^([a-zA-Z ]*?)([a-zA-Z]*)$"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn SortExpression="OperationLink"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  ShowFilterIcon="false"  AllowFiltering="true"   DataField="OperationLink" HeaderStyle-Width="160px" UniqueName="OperationLink"
                            HeaderText="Operation Link">
                            <ItemTemplate>
                                <%#Eval("OperationLink")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOperationLink" runat="server" Text='<%#Bind("OperationLink") %>' Rows="5" CssClass="blue"></asp:TextBox>

                            </EditItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn SortExpression="Description" AllowFiltering="false" ShowFilterIcon="false" DataField="Description" HeaderStyle-Width="160px" UniqueName="Description"
                            HeaderText="Description">
                            <ItemTemplate>
                                <%#Eval("Description")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" Text='<%#Bind("Description") %>' Columns="30" Rows="5" CssClass="blue"></asp:TextBox>
                                
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4txttxtDescription" runat="server" 
                                    ControlToValidate="txtDescription" ErrorMessage="Please enter a valid Description" ForeColor="Red" 
                                    ValidationExpression="^([a-zA-Z ]*?)([a-zA-Z]*)$"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" Visible="true" EditImageUrl="~/View/images/btn_edit.gif"
                            ButtonType="ImageButton">
                            <HeaderStyle Width="33px" />
                        </telerik:GridEditCommandColumn>
                        <%--<telerik:GridButtonColumn ButtonType="ImageButton" ConfirmText="Are You Sure You want to delete"
                            ConfirmDialogType="Classic" ConfirmTitle="Delete Confirmation" CommandName="Delete"
                            Text="Delete" UniqueName="Dcolumn">
                            <HeaderStyle Width="33px" />
                        </telerik:GridButtonColumn>--%>
                    </Columns>
                    <EditFormSettings InsertCaption="New Operation">
                        <EditColumn UniqueName="EditCommandColumn" UpdateImageUrl="~/View/images/updatte.gif"
                            CancelImageUrl="~/View/images/cancel.gif" InsertImageUrl="~/View/images/save.png" ButtonType="ImageButton">
                        </EditColumn>
                        <PopUpSettings Modal="True" ZIndex="300"></PopUpSettings>
                    </EditFormSettings>
                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
                    <PagerStyle Mode="NextPrevAndNumeric" />
                </MasterTableView>
                <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default">
                </HeaderContextMenu>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>    
    <asp:ObjectDataSource ID="ModuleDS" runat="server" SelectMethod="getModueses"
        TypeName="AntiLaundering.Control.UserManagement.ModulesLists"></asp:ObjectDataSource>    
    <asp:ObjectDataSource ID="SubSystemDS" runat="server" SelectMethod="getSubSytems"
        TypeName="AntiLaundering.Control.UserManagement.ModulesLists"></asp:ObjectDataSource>
</asp:Content>


