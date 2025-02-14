using System;
using System.Web;
using System.Web.Routing;

namespace Backend
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Register routes for CRUD operations
            RouteTable.Routes.MapPageRoute(
                "Default",
                "",
                "~/Default.aspx"
            );
            
            RouteTable.Routes.MapPageRoute(
                "Create",
                "create",
                "~/Create.aspx"
            );
            
            RouteTable.Routes.MapPageRoute(
                "Edit",
                "edit/{id}",
                "~/Edit.aspx"
            );
            
            RouteTable.Routes.MapPageRoute(
                "Details",
                "details/{id}",
                "~/Details.aspx"
            );
        }
    }
}
