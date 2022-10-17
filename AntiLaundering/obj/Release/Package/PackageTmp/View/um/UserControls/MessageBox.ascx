<%@ Control Language="C#" AutoEventWireup="true" Inherits="MessageBox" CodeBehind="MessageBox.ascx.cs" %>
<script type="text/javascript">
  $(document).ready(
      function()
      {
          var Sc = document.getElementById("State");
          if (Sc)
          {
              var divB = $(<%=lblBilgi.ClientID %>);
              var div = $(<%=divFade.ClientID%>);
              var mType = $(<%= lbltype.ClientID %>).attributes("innerText");
              //var mType = $document.getElementById((<%=lbltype.ClientID %>)).attributes("innerText").toString();
              if (divB.attr("innerText").length > 0)
              {
                  var Basla = 0;
                  var Color1 = '#9EF487';
                  var Color2 = '#026D01';

                  if (mType == "Error")
                  {
                      Color1 = '#FFB9B0';
                      Color2 = '#FF0000';
                      Basla = 4;
                      alert("Error");
                  }
                  else if (mType == "Information")
                  {
                      Color1 = '#C1EFFD';
                      Color2 = '#71A3B3';
                      Basla = 4;
                  }
                  div.attr("style", "border:1px solid " + Color2 + ";background-color:" + Color1 + ";color:" + Color2 + ";");
              }
          }
      });
</script>
<div id="divFade" runat="server" class="divProcess" 
    style="background-color: #CCEAFF;
    border: 1px solid #099CFF; font-family: 'times New Roman', Times, serif; color: #550000;
    padding-left: 5px; text-transform: capitalize; height: 30px; padding-top: 10px; padding-bottom: 5px; margin-right: 40px; margin-bottom:10px;">
    <span runat="server" id="MessageType" style="display: none;"></span>
    <asp:Label ID="lbltype" runat="server" Visible="false"></asp:Label>
    <span runat="server" id="lblBilgi" class="lblBilgi" /><span id="State" />
</div>
