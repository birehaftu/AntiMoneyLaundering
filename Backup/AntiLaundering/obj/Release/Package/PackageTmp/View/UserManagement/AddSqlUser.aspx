<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/View/Site.Master" CodeBehind="AddSqlUser.aspx.cs" Inherits="CBEBirrManage.View.UserManagement.AddSqlUser" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="ErrorMsg" TagPrefix="uc1" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
     <style type="text/css">
        .auto-style1 {
            width: 845px;
        }

        .auto-style2 {
            width: 146px;
        }

        .auto-style3 {
            width: 692px;
        }

        .auto-style4 {
            width: 221px;
        }

        .auto-style5 {
            width: 206px;
        }

        .auto-style6 {
            width: 115px;
        }

        .auto-style11 {
            width: 115px;
            height: 25px;
        }

        .auto-style12 {
            width: 221px;
            height: 25px;
        }

        .auto-style13 {
            width: 146px;
            height: 25px;
        }

        .auto-style14 {
            width: 206px;
            height: 25px;
        }
        .auto-style15 {
            margin-top: 0;
        }
    </style>
    <script type="text/javascript">
        function UpdateEmployeeInfo(sender, eventArgs) {
             var username = eventArgs.getDataKeyValue("UserName"); 
             var fullname = eventArgs.getDataKeyValue("DisplayName");
             var EmployeeID = eventArgs.getDataKeyValue("EmployeeID");
             var email = eventArgs.getDataKeyValue("Email");
             //alert(email);
             $find('<%=txtUsername.ClientID %>').set_value(username);
             $find('<%=txtEmployeeID.ClientID %>').set_value(EmployeeID);
             $find('<%=txtEmail.ClientID %>').set_value(email);
             $find('<%=comboEmployee.ClientID %>').set_value(fullname);
        }
    </script>
    <script type="text/javascript">
         
        function CheckPasswordStrength(password) {
            var password_strength = document.getElementById("password_strength");

            //TextBox left blank.
            if (password.length == 0) {
                password_strength.innerHTML = "";
                return;
            }

            //Regular Expressions.
            var regex = new Array();
            regex.push("[A-Z]"); //Uppercase Alphabet.
            regex.push("[a-z]"); //Lowercase Alphabet.
            regex.push("[0-9]"); //Digit.
            regex.push("[$@$!%*#?&]"); //Special Character.

            var passed = 0;

            //Validate for each Regular Expression.
            for (var i = 0; i < regex.length; i++) {
                if (new RegExp(regex[i]).test(password)) {
                    passed++;
                }
            }

            //Validate for length of Password.
            if (passed > 2 && password.length >= 6) {
                passed++;
            }

            //Display status.
            var color = "";
            var strength = "";
            if (password.length >= 6) {
                switch (passed) {
                    case 0:
                    case 1:
                        strength = "Weak";
                        color = "red";
                        break;
                    case 2:
                        strength = "Good";
                        color = "darkorange";
                        break;
                    case 3:
                    case 4:
                        strength = "Strong";
                        color = "green";
                        break;
                    case 5:
                        strength = "Very Strong";
                        color = "darkgreen";
                        break;
                }
            }
            else {
                strength = "Weak";
                color = "red";

            }
            password_strength.innerHTML = strength;
            password_strength.style.color = color;
        }
    </script>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function rowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            var popUp;
            function PopUpShowing(sender, eventArgs) {
                popUp = eventArgs.get_popUp();
                var gridWidth = sender.get_element().offsetWidth;
                var gridHeight = sender.get_element().offsetHeight;
                var popUpWidth = popUp.style.width.substr(0, popUp.style.width.indexOf("px"));
                var popUpHeight = popUp.style.height.substr(0, popUp.style.height.indexOf("px"));
                popUp.style.left = ((gridWidth - popUpWidth) / 2 + sender.get_element().offsetLeft).toString() + "px";
                popUp.style.top = ((gridHeight - popUpHeight) / 20 + sender.get_element().offsetTop).toString() + "px";
            }
            function onRequestStart(sender, eventArgs) {
                // get the control's UniqueID that initiated the
                // request.
                var target = eventArgs.get_eventTarget();
                if (target.indexOf("ExportToExcelButton") >= 0 ||
                target.indexOf("ExportToWordButton") >= 0 ||
                target.indexOf("ExportToPdfButton") >= 0 ||
                target.indexOf("ExportTCBEBirrManage.Button") >= 0) {
                    // if any of the export buttons is clicked,
                    // perform a regular postback
                    eventArgs.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    
    <div style="width: 100%">

        <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1"
            meta:resourcekey="RadAjaxManager1Resource1"
            DefaultLoadingPanelID="RadAjaxLoadingPanel1">
            <ClientEvents OnRequestStart="onRequestStart" />
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rsIndividualRequest">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rsIndividualRequest"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="UserRoles">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="UserRoles" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="RadGridUsers" UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridUsers">
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cmbDepartment">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cmbDepartment" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="cmbEmployee" UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cmbEmployee">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cmbEmployee" UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1"
            meta:resourcekey="RadAjaxLoadingPanel1Resource1" />


        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Default"
            InitialBehavior="None" meta:resourcekey="RadWindowManager1Resource1" />
        <%--<br />--%>
        <uc1:ErrorMsg ID="ErrorMsg" runat="server" />
        <uc2:MessageBox ID="MessageBox" runat="server" />

        <div class="column" style="width: 100%;">
            <fieldset id="Fieldset6">

                <asp:Label ID="Label2" Text="User Registration" runat="server"></asp:Label>
            </fieldset>
        </div>
      
        <table class="auto-style1">
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server"
                        meta:resourcekey="RegisterUserValidationSummaryResource1" />
                </td>
            </tr>
            <tr>

                <td valign="top" class="auto-style3">

                    <table>
                        <tr>
                            <td align="left" class="auto-style6">
                                <asp:Label ID="Literal3" AssociatedControlID="cbIsapproved" Visible="false" Text="Active User:" runat="server"
                                    meta:resourcekey="Literal2Resource1" /></td>
                            <td align="left" class="auto-style4">
                                <asp:CheckBox ID="cbIsapproved" runat="server" Checked="True" Visible="false" meta:resourcekey="cbIsapprovedResource1" />
                            </td>
                            <td align="left" class="auto-style2">&nbsp;</td>
                            <td align="left" class="auto-style5">&nbsp;</td>
                        </tr>
                        <tr runat="server" id="trlock">
                            <td align="left" class="auto-style6">
                                <asp:Label AssociatedControlID="cbIsLocked" ID="Literal4" Visible="false" Text="Is Locked out?" runat="server"
                                    meta:resourcekey="Literal3Resource1" /></td>
                            <td align="left" class="auto-style4">
                                <asp:CheckBox ID="cbIsLocked" runat="server" Visible="false" meta:resourcekey="cbIsLockedResource1" />
                            </td>
                            <td align="left" class="auto-style2">&nbsp;</td>
                            <td align="left" class="auto-style5">&nbsp;</td>
                        </tr>

                        <tr>
                            <td>Company Name</td>
                            <td class="auto-style4">
                                <telerik:RadComboBox ID="txtOwnerName" runat="server">
                                </telerik:RadComboBox>

                            </td>
                            <td class="auto-style2">Full Name<br />
                            </td>
                            <td class="auto-style5">
                                <telerik:RadTextBox ID="comboEmployee" runat="server"></telerik:RadTextBox>
                                
                                <%--<telerik:RadComboBox runat="server" AutoPostBack="true" ID="comboEmployee" Filter="Contains" AppendDataBoundItems="true"  DropDownWidth="360px"
                                    CssClass="selectlst" EmptyMessage="select"
                                    EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" EnableScreenBoundaryDetection="false"
                                    OnItemsRequested="comboEmployee_ItemsRequested" Enabled="true"
                                    ShowToggleImage="false" Width="181px" OnSelectedIndexChanged="comboEmployee_SelectedIndexChanged" style="margin-top: 0">
                                    <HeaderTemplate>
                                        <table style="width: 315px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 200px">Employee Name
                                                </td>
                                                <td style="width: 110px">Employee Id 
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 315px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 200px">
                                                    <%# DataBinder.Eval(Container,"Attributes['FullName']")%>
                                                </td>
                                                <td style="width: 80px">
                                                    <%#DataBinder.Eval(Container, "Attributes['EmployeeCode']")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>--%>

                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style6" align="left">
                                <asp:Label ID="lblUserName" runat="server"
                                    AssociatedControlID="txtUsername" meta:resourcekey="lblUserNameResource1">User Name:</asp:Label>
                                

                            </td>
                            <td class="auto-style4">
                                <telerik:RadTextBox ID="txtUsername" runat="server" Width="160px"></telerik:RadTextBox>
                                
                            </td>
                            <td class="auto-style2">
                                <asp:Label ID="lblDOB" runat="server"
                                    AssociatedControlID="dpDOB"
                                    meta:resourcekey="lblDOBResource1">Date of Birth:</asp:Label>
                            </td>
                            <td class="auto-style5">
                                <telerik:RadDatePicker ID="dpDOB" runat="server" CssClass="auto-style15" MaxDate="9999-12-31" MinDate="1212-12-31">
                                   
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11" align="left">
                                <asp:Label ID="lblPassword" runat="server"
                                    AssociatedControlID="txtPassword" meta:resourcekey="lblPasswordResource1">Password:</asp:Label></td>
                            <td class="auto-style12">
                                <asp:TextBox ID="txtPassword" runat="server" Width="160px" TextMode="Password" onkeyup="CheckPasswordStrength(this.value)"
                                    meta:resourcekey="txtPasswordResource1" CssClass="auto-style15"></asp:TextBox>
                            </td>
                            <td class="auto-style13">
                                <asp:Label ID="lblPhone" Text="Phone:" runat="server"
                                    AssociatedControlID="txtPhone"
                                    meta:resourcekey="lblPhoneResource1"></asp:Label>
                            </td>
                            <td class="auto-style14">
                                <telerik:RadTextBox ID="txtPhone" ValidationGroup="Group1" Width="160px" runat="server"
                                    meta:resourcekey="txtPhoneResource1">
                                </telerik:RadTextBox>

                            </td>
                        </tr>
                        <tr id="Tr1" runat="server">
                            <td align="left" class="auto-style6">
                                <asp:Label ID="lblConfirmPassword" runat="server"
                                    AssociatedControlID="txtConfirmPassword"
                                    meta:resourcekey="lblConfirmPasswordResource1">Confirm Password:</asp:Label></td>
                            <td align="left" class="auto-style4">
                                <asp:TextBox ID="txtConfirmPassword" runat="server" Width="160px"
                                    TextMode="Password" meta:resourcekey="txtConfirmPasswordResource1"></asp:TextBox>
                                <asp:CompareValidator ID="PasswordCompare"
                                    runat="server" ControlToCompare="txtPassword"
                                    ControlToValidate="txtConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                    ValidationGroup="cmb"
                                    meta:resourcekey="PasswordCompareResource1" ForeColor="Red">*</asp:CompareValidator>
                                
                                <span id="password_strength">
                                

                                </span>
                            </td>
                            <td align="left" class="auto-style2">
                                <asp:Label ID="lblAddress" runat="server"
                                    AssociatedControlID="txtAddress"
                                    meta:resourcekey="lblPassowrdAnswerResource1">Address:</asp:Label>
                            </td>
                            <td align="left" class="auto-style5">
                                <asp:TextBox ID="txtAddress" Width="160px" runat="server"
                                    meta:resourcekey="txtAddressResource1"></asp:TextBox>

                            </td>
                        </tr>
                        <tr id="Tr3" runat="server">
                            <td class="auto-style6">
                                Employee ID</td>
                            <td align="left" class="auto-style4">
                                <telerik:RadTextBox ID="txtEmployeeID" Width="160px" runat="server"></telerik:RadTextBox>

                            </td>
                            <td align="left" class="auto-style2" rowspan="2">
                                <asp:Label ID="lblComment" runat="server"
                                    AssociatedControlID="txtComment" meta:resourcekey="lblCommentResource1">Description:</asp:Label>
                            </td>
                            <td align="left" class="auto-style5" rowspan="2">
                                <asp:TextBox ID="txtComment" runat="server" Width="329px"
                                    TextMode="MultiLine" meta:resourcekey="txtCommentResource1" Height="55px" />

                            </td>
                        </tr>
                        <tr id="Tr2" runat="server">
                            <td align="left" class="auto-style6">
                                <asp:Label ID="lblEmail" runat="server"
                                    AssociatedControlID="txtEmail">Email:</asp:Label>

                            </td>
                            <td align="left" class="auto-style4">
                                <telerik:RadTextBox ID="txtEmail" runat="server" Width="160px"></telerik:RadTextBox>


                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="txtEmail" ErrorMessage="Email is required." ToolTip="Email is required."
                                    ValidationGroup="cmb"
                                    meta:resourcekey="EmailRequiredResource1" ForeColor="Red">*</asp:RequiredFieldValidator>



                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="auto-style6"></td>
                            <td align="left" class="auto-style4">
                                <asp:Button ID="btnRegister" runat="server" Text="Register" meta:resourcekey="btnRegisterResource1" ValidationGroup="cmb"
                                    OnClick="btnRegister_Click" />
                            </td>
                            <td align="left" class="auto-style2">
                            </td>
                            <td align="left" class="auto-style5">&nbsp;</td>
                        </tr>

                        </table>
                </td>
                <td valign="top" style="border-left: 1px solid #999; border-style: solid;">
                    <h3>
                        <asp:Label ID="Literal5" AssociatedControlID="cblUserRoles" Text="Roles:" runat="server"
                            meta:resourcekey="Literal4Resource1" /></h3>
                    <asp:RadioButtonList ID="cblUserRoles" runat="server"
                       >

                    </asp:RadioButtonList>
                </td>
            </tr>

        </table>
        <div>
             <telerik:RadGrid ID="RadGridUsers" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="true"
                GridLines="None" AllowPaging="true" AllowSorting="True" ShowStatusBar="True"
                Width="100%" OnItemDataBound="RadGridUsers_ItemDataBound"  
                                 EnableHeaderContextAggregatesMenu="False" EnableHeaderContextFilterMenu="False"
                EnableHeaderContextMenu="false"  ClientSettings-Selecting-AllowRowSelect="true"
            AllowMultiRowSelection="true" PageSize="20"
                OnNeedDataSource="RadGridUsers_NeedDataSource"  CellSpacing="0" >
            <ClientSettings AllowDragToGroup="False" AllowColumnHide="False" AllowRowHide="False">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
            <GroupingSettings CaseSensitive="false" />
                 <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" CommandItemDisplay="Top" EnableHeaderContextAggregatesMenu="False"
                    Caption="UserName" ClientDataKeyNames="UserName,DisplayName,Email,EmployeeID" DataKeyNames="UserName,DisplayName,Email,EmployeeID">
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
                         <telerik:GridTemplateColumn AutoPostBackOnFilter="true" SortExpression="UserName" CurrentFilterFunction="Contains" HeaderText="User Name" DataField="UserName" 
                              ShowFilterIcon="false"  AllowFiltering="true"  AllowSorting="true"  >
                            <ItemTemplate>
                                <%#Eval("UserName")%>
                            </ItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>     
                         <telerik:GridTemplateColumn AutoPostBackOnFilter="true" SortExpression="UserName" CurrentFilterFunction="Contains" HeaderText="Full Name" DataField="DisplayName" 
                              ShowFilterIcon="false"  AllowFiltering="true" AllowSorting="true"  >
                            <ItemTemplate>
                                <%#Eval("DisplayName")%>
                            </ItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>
                               
                         <telerik:GridTemplateColumn AutoPostBackOnFilter="true" SortExpression="Email" CurrentFilterFunction="Contains" HeaderText="Email" DataField="Email" 
                              ShowFilterIcon="false"  AllowFiltering="true" AllowSorting="true"   >
                            <ItemTemplate>
                                <%#Eval("Email")%>
                            </ItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>
                             
                               
                         <telerik:GridTemplateColumn AutoPostBackOnFilter="true" SortExpression="EmployeeID" CurrentFilterFunction="Contains" HeaderText="Employee ID" DataField="EmployeeID" 
                              ShowFilterIcon="false"  AllowFiltering="true"  AllowSorting="true"  >
                            <ItemTemplate>
                                <%#Eval("EmployeeID")%>
                            </ItemTemplate>
                            <HeaderStyle Width="160px"></HeaderStyle>
                        </telerik:GridTemplateColumn>
                             
                           </Columns>
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" HorizontalAlign="Left" Wrap="True" />
            <PagerStyle Mode="NextPrevAndNumeric" />
                        </MasterTableView>
              <ClientSettings>
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnRowDblClick="UpdateEmployeeInfo" />
            </ClientSettings>
                 <FilterItemStyle CssClass="HighZindex" />
        <FilterMenu CssClass="HighZindex" EnableRoundedCorners="True">
        </FilterMenu>
        <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default">
        </HeaderContextMenu>
            </telerik:RadGrid>
        </div>

    </div>

    <%--            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>--%>




       
</asp:Content>