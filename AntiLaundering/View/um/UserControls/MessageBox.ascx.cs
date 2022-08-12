using System;

public partial class MessageBox : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Message = "";
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Message == null || Message == "")
        {
            divFade.Visible = false;
        }
        else
        {
            divFade.Visible = true;
        }
    }

    public string Message
    {
        get
        {
            String s = (String)ViewState["Message"];
            return ((s == null) ? String.Empty : s);
        }
        set
        {
            ViewState["Message"] = value;
            lblBilgi.InnerHtml = value;
        }
    }

    public ShowType Type
    {
        get
        {
            ShowType s = ShowType.Normal;
            if (ViewState["Type"] != null)
            {
                s = (ShowType)ViewState["Type"];
            }
            return s;
        }

        set
        {
            ViewState["Type"] = value;
            MessageType.InnerHtml = value.ToString();
        }
    }

    public enum ShowType
    { 
        Information,
        Error,
        Normal
    }
}
