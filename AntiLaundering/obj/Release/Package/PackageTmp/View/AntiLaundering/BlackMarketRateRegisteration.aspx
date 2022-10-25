<%@ Page Title="" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="BlackMarketRateRegisteration.aspx.cs" Inherits="AntiLaundering.View.AntiLaundering.BlackMarketRateRegisteration" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/um/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="~/View/um/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>
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
        </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <uc1:errormsg ID="ErrorMsg" runat="server" />
        <uc2:MessageBox ID="MessageBox" runat="server" />
    <div class="InputPanel">

        <fieldset id="Fieldset1">
            <asp:Label ID="lbltext" Text="Black Market Rate Registration" runat="server"></asp:Label>
            </fieldset>

    </div>
    <div class="InputPanel" style="width: 100%; height: 450px">

        <table class="auto-style1">
            <tr>
                <td class="auto-style2">Exchange Name</td>
                <td class="auto-style4">
                               
                    <telerik:RadComboBox ID="comboExchangeName" AutoPostBack="true" runat="server" OnSelectedIndexChanged="comboExchangeName_SelectedIndexChanged">                        
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Please Select" Value="" />
                            <telerik:RadComboBoxItem runat="server" Text="US Dollar" Value="US Dollar" />
                            <telerik:RadComboBoxItem runat="server" Text="British Pound" Value="British Pound" />
                            <telerik:RadComboBoxItem runat="server" Text="Euro" Value="Euro" />
                            <telerik:RadComboBoxItem runat="server" Text="canadian dollar" Value="canadian dollar" />
                            <telerik:RadComboBoxItem runat="server" Text="UAE Dirhams" Value="UAE Dirhams" />
                            <telerik:RadComboBoxItem runat="server" Text="Saudi riyal" Value="Saudi riyal" />

                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td class="auto-style3">Exchange Code</td>
                <td style="margin-left: 40px" class="auto-style1">
                    <telerik:RadComboBox ID="comboExchangeCode" AutoPostBack="true" runat="server" OnSelectedIndexChanged="comboExchangeCode_SelectedIndexChanged">                        
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Please Select" Value="" />
                            <telerik:RadComboBoxItem runat="server" Text="USD" Value="USD" />
                            <telerik:RadComboBoxItem runat="server" Text="GBP" Value="GBP" />
                            <telerik:RadComboBoxItem runat="server" Text="Euro" Value="Euro" />
                            <telerik:RadComboBoxItem runat="server" Text="CAD" Value="CAD" />
                            <telerik:RadComboBoxItem runat="server" Text="AED" Value="AED" />
                            <telerik:RadComboBoxItem runat="server" Text="SAR" Value="SAR" />

                        </Items>
                    </telerik:RadComboBox>
                               
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Country</td>
                <td class="auto-style4">
                    <telerik:RadComboBox ID="comboCountry" AutoPostBack="true" runat="server" OnSelectedIndexChanged="comboCountry_SelectedIndexChanged">                        
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Please Select" Value="" />
                            <telerik:RadComboBoxItem runat="server" Text="USA" Value="USA" />
                            <telerik:RadComboBoxItem runat="server" Text="England" Value="England" />
                            <telerik:RadComboBoxItem runat="server" Text="Europe" Value="Europe" />
                            <telerik:RadComboBoxItem runat="server" Text="Canada" Value="Canada" />
                            <telerik:RadComboBoxItem runat="server" Text="UAE" Value="UAE" />
                            <telerik:RadComboBoxItem runat="server" Text="Suadi Arebia" Value="Suadi Arebia" />

                        </Items>
                    </telerik:RadComboBox>           
                </td>
                <td class="auto-style3">RateDate</td>
                <td style="margin-left: 40px" class="auto-style1">
                    <telerik:RadDatePicker ID="dpRateDate" Runat="server">
                    </telerik:RadDatePicker>
                               
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Black Market Rate</td>
                <td class="auto-style4">
                    <telerik:RadNumericTextBox NumberFormat-DecimalDigits="4"  ID="txtRateAmountInBirr" runat="server" MaxLength="8">
                    </telerik:RadNumericTextBox>
                               
                </td>
                <td class="auto-style3">Bank Rate</td>
                <td style="margin-left: 40px" class="auto-style1">
                    <telerik:RadNumericTextBox NumberFormat-DecimalDigits="4"  ID="txtBankRate" runat="server" MaxLength="8">
                    </telerik:RadNumericTextBox>
                               
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style6">
            <telerik:RadButton ID="btn_Register"  ButtonType="LinkButton" runat="server" Text="Register" OnClick="btn_Register_Click">
            </telerik:RadButton>
                </td>
                <td class="auto-style7"><telerik:RadButton ID="btn_Update"  ButtonType="LinkButton" runat="server" Text="Update"  OnClick="btn_Update_Click">
            </telerik:RadButton>
                </td>
                <td class="auto-style1">
                    <telerik:RadTextBox ID="txtBlackMarketRateID" runat="server"
 Visible="false"></telerik:RadTextBox>                   
                     </td>
            </tr>

            </table>
        <div class="InputPanel" >
           </div>


    </div>
</asp:Content>
