using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace PhoneSystem.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            routes.MapPageRoute("account", "Account/{action}/{*params}", "~/Account/{action}.aspx");
            routes.MapPageRoute("admin", "admin/{action}/{*params}", "~/pages/admin/{action}.aspx");
            routes.MapPageRoute("user", "user/{action}/{*params}", "~/pages/user/{action}.aspx");
        }
    }
}
