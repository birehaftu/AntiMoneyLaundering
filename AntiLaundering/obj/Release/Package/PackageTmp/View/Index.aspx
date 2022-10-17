<%@ Page Language="C#" MasterPageFile="~/View/HomePage.Master"
    AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="AntiLaundering.View.Index" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/View/um/UserControls/errormsg.ascx" TagName="errormsg" TagPrefix="uc1" %>
<%@ Register Src="~/View/um/UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="images/fav.ico" rel="icon" style="" />
  <%--  <link href="~/View/Styles/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
    <link href="~/View/Styles/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <script src="~/View/js/jquery-1.7.1.js" type="text/javascript"></script>
    <script src="~/View/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>--%>
    
    <link rel="stylesheet" type="text/css" href="../src/css/mapstyle.css" />
    
    <script type="text/javascript">
        $(document).ready(function () {
            $('#accordion ul > li ul').click(function (e) {
                e.stopPropagation();
            }).filter(':not(:first)').hide();
            $('#accordion ul > li').click(function () {
                var selfClick = $(this).find('ul:first').is(':visible');
                if (!selfClick) {
                    $(this).parent().find('> li ul:visible').slideToggle();
                }
                $(this).find('ul:first').stop(true, true).slideToggle();
            });

        });
        
    </script>
    <script type="text/javascript">
    </script>
    <style type="text/css">
        
.rtSelected .rtIn
{
    background-color: #fcc32f !important;
    background-image: none !important;
    color: white !important;
}

        .RadTreeView .rtUL .rtLI .rtChecked,
.RadTreeView .rtUL .rtLI .rtUnchecked,
.RadTreeView .rtUL .rtLI .rtIn {
            cursor: pointer;
        }
        .style1 {
        }

        #frame {
            width: 984px;
            height: 719px;
        }

        #accordion {
            text-align: left;
            padding-left: 0px;
        }

        .acc {
            list-style: none;
            margin-top: 0px;
            margin-left: 0px;
            padding-left: 0px;
        }

        #accordion li {
            cursor: pointer;
            background: url(accordion_bg.png) repeat-x;
            font-weight: bold;
            color: #015287;
            border: 1px solid #b2b2b2;
            margin-bottom: 2px;
            list-style-image: none;
            list-style-position: outside;
            list-style-type: none;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            padding: 4px 8px;
        }

            #accordion li.active {
                color: #D15600;
            }

            #accordion li ul {
                padding: 0;
                margin: 10px 0 0 0;
            }

            #accordion li.active li {
                text-indent: 0;
            }

            #accordion li li {
                font-weight: normal;
                background: none;
                border: 0;
            }

        .style2 {
            width: 171px;
        }

        .back {
            background: url(accordion_bg.png) repeat-x;
        }

        #done {
            width: 53px;
        }

        a {
            text-decoration: none;
        }

        .wi {
            width: 100%;
        }
    .auto-style2 {
        height: 203px;
    }
    </style>

    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="border:none;background-color:#800080;height:100%;border-bottom-width:0px">
        <table width="100%" border="0" class="auto-style2">
            <tr>
                <td valign="top" style="border:none; margin:0px 0px 0px 0px">
                   
                    <telerik:RadSplitter ID="radSplitter" runat="server" BorderSize="0" Height="549px" Width="100%"
                         Orientation="Vertical">
                        <telerik:RadPane ID="RadPane1" runat="server" BorderWidth="0" Width="19%" BackColor="#800080">
                            
                            <telerik:RadTreeView ID="RadTreeView1" runat="server" Height="99%" Width="100%" RenderMode="Native" ShowLineImages="false"
                                EnableDragAndDrop="false" OnNodeClick="RadTreeView1_NodeClick" Style="background-color:#800080;color:white;border: 0px ;" Font-Size="Medium">
                            </telerik:RadTreeView>
                        </telerik:RadPane>
                        <telerik:RadSplitBar ID="RadSplitbar1" runat="server" BackColor="InActiveCaption" CollapseMode="Forward">
                        </telerik:RadSplitBar> 
                        <telerik:RadPane ID="contentPane" BackColor="#ffffff"  Height="79%" runat="server" ContentUrl="">
                            
                       
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                </td>
            </tr>
        </table>
    </div>
    
</asp:Content>
