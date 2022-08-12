using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.Configuration;
using System.Data.SqlClient;
namespace AntiLaundering.View
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //AntiLaundering.Control.VehicleTracking.NotificationsHub();
            // Register the default hubs route:~/ signalr / hubs
            //RouteTable.Routes.MapHubs();
            //SqlDependency.Start(ConfigurationManager.ConnectionStrings["FMSReport"].ConnectionString);
        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            //SqlDependency.Stop(ConfigurationManager.ConnectionStrings["FMSReport"].ConnectionString);
        }

    }

}