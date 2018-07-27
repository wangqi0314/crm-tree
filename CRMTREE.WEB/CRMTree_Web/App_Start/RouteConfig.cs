using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;
using CRMTree_Web.App_Start;

namespace CRMTree_Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);
            routes.MapPageRoute("WQ", "2009", "~/WebForm1.aspx");

            routes.Add("SalesRoute",
                new Route("SalesReport/{locale}/{year}/{*queryvalues}"
                    , new SalesRouteHandler("~/WebForm1.aspx"))
                    {
                        Constraints = new RouteValueDictionary { { "locale", "[a-z]{2}-[a-z]{2}" }, { "year", @"\d{4}" } },
                        Defaults = new RouteValueDictionary { { "locale", "en-US" }, { "year", DateTime.Now.Year.ToString() } }
                    });
            routes.Add("ExpensesRoute",
                new Route("ExpensesReport/{locale}/{year}/{*queryvalues}"
                 , new ExpensesRouteHandler())
            {
                Constraints = new RouteValueDictionary { { "locale", "[a-z]{2}-[a-z]{2}" }, { "year", @"\d{4}" } },
                Defaults = new RouteValueDictionary { { "locale", "en-US" }, { "year", DateTime.Now.Year.ToString() } }
            });
        }
    }
}
