using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace hello_services {
    public class Global : System.Web.HttpApplication {

        void Application_Start(object sender, EventArgs e) {

            // initialize the default route configuration
            RouteTable.Routes.SetDefaultHttpConfiguration(new Microsoft.ApplicationServer.Http.WebApiConfiguration() { });
            
            // define the route to the emlpoyees service.  the path
            // is relative and can be access at http://localhost:1234/api/employees or
            // whatever your root URL is
            RouteTable.Routes.MapServiceRoute<Services.Employees>("Api/Employees");

        }

        void Application_End(object sender, EventArgs e) {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e) {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e) {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e) {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
