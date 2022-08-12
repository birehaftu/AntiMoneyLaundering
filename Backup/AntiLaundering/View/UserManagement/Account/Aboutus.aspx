<%@ Page Title="Log In" Language="C#" MasterPageFile="~/View/Front.master" AutoEventWireup="true"
    CodeBehind="Aboutus.aspx.cs" Inherits="HohomaERP.View.Account.Aboutus" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <%--<script type="text/javascript">
        function validation() {
            var a = document.getElementById("UserName").value;            
            if (a == "") {
                document.getElementById("FailureText").innerHTML="Please Enter Your Name";
                document.form.name.focus();
                return false;
            }
            if (!isNaN(a)) {
                document.getElementById("FailureText").innerHTML="Please Enter Only Characters";
                //document.form.name.select();
                return false;
            }
            if ((a.length < 5) || (a.length > 15)) {
                document.getElementById("FailureText").innerHTML="Your Character must be 5 to 15 Character";
                //document.form.name.select();
                return false;
            }
        }
</script>--%>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
                    <telerik:RadScriptManager ID="RadScriptManager2" runat="server">
                    </telerik:RadScriptManager>
    <asp:Login ID="LoginUser" runat="server" EnableViewState="true" 
        RenderOuterTable="true" DestinationPageUrl="../../redirect/redirect.aspx" Height="365px" Width="914px" OnLoginError="LoginUser_LoginError">
        <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                ValidationGroup="LoginUserValidationGroup" />
            <div style="position: relative; margin-top: 100px; width: 931px; height: 322px; background-color: #ffffff; top: -65px; left: 0px; margin-left: auto; margin-right: auto;">
                <div style="position: relative; float: right; width: 426px; height: 316px; background-image: url('images/BusPic.jpg'); top: 0px; left: 0px;">

                    <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/View/images/NewFont.jpg" Width="300px" Height="200" BackColor="White" BorderColor="White" />--%>

                    <asp:Image ID="Image2" runat="server" ImageUrl="~/View/images/Master_Logo.jpg" Width="200px" Height="200" />
                </div>
                <div class="accountInfo">
                    <fieldset class="login" style="border-color: #122732; color: #000000;">
                        <legend style="border-color: #000000">Account Information</legend>
                        <p>
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                            <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                            <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <%--<asp:CheckBox ID="RememberMe" runat="server" />--%>
                            <telerik:RadCheckBox ID="RememberMe" runat="server"></telerik:RadCheckBox>
                            <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Remember Me</asp:Label>

                        </p>
                    </fieldset>
                    <p class="submitButton">
                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In"
                            ValidationGroup="LoginUserValidationGroup" OnClick="LoginButton_Click" />
                    </p>

                </div>

            </div>
        </LayoutTemplate>
    </asp:Login>
</asp:Content>
