using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;
using System.Web.UI;

namespace CRMTree_Web.App_Start
{
    class SalesRouteHandler : IRouteHandler
    {
        public SalesRouteHandler(string virtualPath)
        {
            this.VirtualPath = virtualPath;
        }
        public string VirtualPath { get; private set; }
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            var page = BuildManager.CreateInstanceFromVirtualPath(VirtualPath, typeof(Page)) as IHttpHandler;
            return page;
        }
    }
}
