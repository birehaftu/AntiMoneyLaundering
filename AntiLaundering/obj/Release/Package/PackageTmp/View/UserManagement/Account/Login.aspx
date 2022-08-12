<%@ Page Title="Log In" Language="C#" MasterPageFile="~/View/Front.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="CBEBirrManage.View.Account.Login" %>

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
    <style type="text/css">
        .auto-style1 {
            width: 465px;
            height: 184px;
        }
        .username{
background-image:url('../../images/Human_LogIn.jpg');
background-position: left;
	background-repeat: no-repeat;
}
          .password{
background-image:url('../../images/LockKey2.jpg');
background-position: left;
	background-repeat: no-repeat;
}
        .auto-style2 {
            width: 458px;
            height: 55px;
        }
        .auto-style3 {
            left: 0px;
            top: 0px;
            height: 365px;
        }
        .auto-style4 {
            width: 446px;
        }
        </style>
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="col-md-6" style="padding-top:60px;border-bottom:0px">

        <div id="wrapper"  style="align-content:center" >
            <div id="login" style="align-content:center" class="auto-style3" >
                &nbsp;
                 <div class="auto-style1" style="margin-top:-100px;text-align:center">
                <asp:Image ID="Image2" runat="server"  ImageUrl="" Width="205px" Height="185px" />
                     <br />
                     

                </div>
                <asp:Login ID="LoginUser" runat="server" EnableViewState="true"
                    RenderOuterTable="true" DestinationPageUrl="../../redirect/redirect.aspx" OnLoginError="LoginUser_LoginError" Width="375px">
                    <LayoutTemplate>
                        <span style="color: red" class="failureNotification">
                            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                            <%--<asp:Literal ID="Literal1" runat="server"></asp:Literal>--%>
                        </span>
                        <asp:Panel ID="pnlDefaultButton"  style="align-content:center"  runat="server" DefaultButton="LoginButton">

                            <%--<asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" ValidationGroup="LoginUserValidationGroup" />--%>
                            <h1 style="text-align:center;color:#800080;font-size:xx-large" >
                         CBEBirr Midleware System <br />
                           SIGN IN
                     </h1>
                            <p>
                                <asp:TextBox ID="username" runat="server" placeholder="Enter Usename" CssClass="username" Width="199px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                    ErrorMessage="*" ForeColor="Red" ValidationGroup="LoginUserValidationGroup" ToolTip="User Name is required."></asp:RequiredFieldValidator>
                                <%--<input id="username" name="username" required="required" type="text" placeholder="myusername or mymail@mail.com" />--%>
                            </p>
                            <p>
                                <asp:TextBox ID="Password" runat="server" CssClass="password" placeholder="Enter Password" TextMode="Password" Width="197px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ForeColor="Red" ControlToValidate="Password"
                                    CssClass="failureNotification" ErrorMessage="*"
                                    ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator>
                                <%--<input id="password" name="password" required="required" type="password" placeholder="eg. X8df!90EO" />--%>
                            </p>
                            <p>
                                <%--<asp:CheckBox ID="RememberMe" runat="server" />--%>
                                <telerik:RadCheckBox ID="RememberMe" AutoPostBack="false" runat="server"></telerik:RadCheckBox>
                                <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Remember Me</asp:Label>

                            </p>
                            </fieldset>
                            <p class="auto-style4">
                            <asp:Button ID="LoginButton" BackColor="#800080" Font-Size="Medium" Font-Bold="true" runat="server" ForeColor="White" CommandName="Login" Text="Log In      "
                                ValidationGroup="LoginUserValidationGroup" OnClick="LoginButton_Click" Width="204px" Height="20px" />
                                </p>
                       </asp:Panel>
                           
                        <%--<a href="../../HomePage.aspx">  <asp:Button ID="Button1" runat="server"  Text="Other Link" /></a>--%>
                    </LayoutTemplate>
                 
                </asp:Login>
               

            </div>
        </div>

    </div>

   
</asp:Content>
