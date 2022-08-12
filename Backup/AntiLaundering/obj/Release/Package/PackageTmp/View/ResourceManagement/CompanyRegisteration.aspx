<%@ Page Title="" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="CompanyRegisteration.aspx.cs" Inherits="CBEBirrManage.View.ResourceManagement.CompanyRegisteration" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/UserManagement/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="~/View/UserManagement/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 983px;
        }
        .auto-style2 {
            width: 292px;
        }
        .auto-style3 {
            width: 292px;
            height: 26px;
        }
        .auto-style4 {
            height: 26px;
        }
        .auto-style5 {
            width: 983px;
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <uc1:errormsg ID="ErrorMsg" runat="server" />
        <uc2:MessageBox ID="MessageBox" runat="server" />
    <div class="InputPanel">

        <fieldset id="Fieldset1">
            <asp:Label ID="lbltext" Text="Company Registration" runat="server"></asp:Label>
            </fieldset>

    </div>
    <div class="InputPanel" style="width: 100%; height: 450px">

        <table class="auto-style1">
            <tr>
                <td class="auto-style2">Company Name</td>
                <td class="auto-style4">
                    <telerik:RadTextBox ID="txtCompanyName" runat="server" Width="150px">
                    </telerik:RadTextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4txtCompanyName" runat="server" 
                                    ControlToValidate="txtCompanyName" ErrorMessage="Please enter a valid Company Name" ForeColor="Red" 
                                    ValidationExpression="^([a-zA-Z ]*?)([a-zA-Z]*)$"></asp:RegularExpressionValidator>
                </td>
                <td class="auto-style3">Number Of Vehicles</td>
                <td style="margin-left: 40px" class="auto-style1">
                    <telerik:RadNumericTextBox  NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0" MaxLength="10" ID="txtNumberOfVehicles" runat="server" Width="150px">
                    </telerik:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Company Type</td>
                <td class="auto-style4">
                    <telerik:RadComboBox runat="server" ID="comboComanyType" Width="150px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Please Select" Value="" />
                            <telerik:RadComboBoxItem runat="server" Text="Private Limited Company" Value="Private" />
                            <telerik:RadComboBoxItem runat="server" Text="Governmental Organization" Value="Governmental Organization" />
                            <telerik:RadComboBoxItem runat="server" Text="Governmental Developmnet Organization" Value="Governmental Developmnet Organization" />
                            <telerik:RadComboBoxItem runat="server" Text="Civil Service" Value="Civil Service" />

                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td class="auto-style3">Fax</td>
                <td class="auto-style1">
                    <telerik:RadTextBox  MaxLength="13" ID="txtFax" runat="server" LabelWidth="60px" Width="150px">
                    </telerik:RadTextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFax" ErrorMessage="pls enter the correct Fax number" ValidationExpression="^([0-9\(\)\/\+ \-]*)$" ForeColor="Red"></asp:RegularExpressionValidator>
                    
                </td>
            </tr>
            <tr>
                <td class="auto-style2">President/Director/Manager Name</td>
                <td class="auto-style6">
                    <telerik:RadTextBox ID="txtManagerName" runat="server" Width="150px">
                    </telerik:RadTextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4txtManagerName" runat="server" 
                                    ControlToValidate="txtManagerName" ErrorMessage="Please enter a valid Manager Name" ForeColor="Red" 
                                    ValidationExpression="^([a-zA-Z ]*?)\s+([a-zA-Z]*)$"></asp:RegularExpressionValidator>
                </td>
                <td class="auto-style7">Tele</td>
                <td class="auto-style1">
                    <telerik:RadTextBox ID="txtTele" Mask="###-######"  ValidationGroup="Group2" Width="150px" runat="server" MaxLength="13" >
                    </telerik:RadTextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTele" ErrorMessage="pls enter the correct phone number" ValidationExpression="^([0-9\(\)\/\+ \-]*)$" ForeColor="Red"></asp:RegularExpressionValidator>
                </td>
            </tr>

            <tr>
                <td class="auto-style3">City</td>
                <td class="auto-style4">
                    <telerik:RadTextBox ID="txtCity" runat="server" Width="150px">
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style3">Mobile</td>
                <td class="auto-style5">
                    <telerik:RadTextBox ID="txtMobile"  Mask="###-######"  ValidationGroup="Group3" Width="150px" runat="server" 
                                meta:resourcekey="txtPhoneResource1" MaxLength="13" ></telerik:RadTextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMobile" ErrorMessage="pls enter the correct Mobile number" ValidationExpression="^([0-9\(\)\/\+ \-]*)$" ForeColor="Red"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Sub City</td>
                <td class="auto-style4">
                    <telerik:RadTextBox ID="txtSubCity" runat="server" Width="150px">
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style3">Email</td>
                <td class="auto-style1">
                    <telerik:RadTextBox ID="txtEmail" runat="server" Width="150px">
                    </telerik:RadTextBox>
                     <asp:RegularExpressionValidator ID="emailValidator" runat="server" Display="Dynamic"
                            ErrorMessage="Please, enter valid e-mail address." ForeColor="Red" ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                            ControlToValidate="txtEmail">
                        </asp:RegularExpressionValidator>
                        
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Woreda</td>
                <td class="auto-style4">
                    <telerik:RadTextBox ID="txtWoreda" runat="server" Width="150px">
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style3">P.O.Box</td>
                <td class="auto-style1">
                    <telerik:RadTextBox  MaxLength="10" ID="txtPOBOX" runat="server" Width="150px"></telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="txtlogolabel" runat="server" Text='Logo' CssClass="blue"></asp:Label></td>
                <td class="auto-style4">
                    <telerik:RadUpload ID="ULogo" runat="server" AllowedFileExtensions=".jpeg,.jpg,.JPEG,.JPG,gif,GIF,.png,.tiff,.bit,.bmp,.dib" ControlObjectsVisibility="None" Width="288px"></telerik:RadUpload>
                </td>
                <td class="auto-style3">Company Description</td>
                <td class="auto-style1">
                    <telerik:RadTextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="150px">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" class="auto-style2">
                    &nbsp;</td>
                <td valign="top" class="auto-style4">
                   <asp:Label ID="txtCompanyId" runat="server" Visible="false"></asp:Label> &nbsp;</td>
                <td class="auto-style3" colspan="2">
                    <asp:Image ID="CompLogo" runat="server" Height="191px" Width="208px" />
                </td>
            </tr>
        </table>
        <div class="InputPanel" >
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <telerik:RadButton ID="btn_Register"  ButtonType="LinkButton" runat="server" Text="Register" OnClick="btn_Register_Click">
            </telerik:RadButton>
            &nbsp;&nbsp;<telerik:RadButton ID="btn_Update"  ButtonType="LinkButton" runat="server" Text="Update"  OnClick="btn_Update_Click">
            </telerik:RadButton>
            <%--<uc2:MessagePanel ID="MessagePanel1" runat="server" />--%></div>


    </div>
</asp:Content>
