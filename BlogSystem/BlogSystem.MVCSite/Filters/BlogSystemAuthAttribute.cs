using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogSystem.MVCSite.Filters
{ 
    public class BlogSystemAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //base.OnAuthorization(filterContext);
            //当cookie有值，session为空时，将cookie数据同步到session。方面后面直接从session中取值
            if (filterContext.HttpContext.Session["loginName"] == null&& 
                filterContext.HttpContext.Request.Cookies["loginName"] != null)
            {
                filterContext.HttpContext.Session["loginName"] = filterContext.HttpContext.Request.Cookies["loginName"].Value;
                filterContext.HttpContext.Session["loginId"] = filterContext.HttpContext.Request.Cookies["loginId"].Value;
            }
            if (filterContext.HttpContext.Session["loginName"] == null &&
                filterContext.HttpContext.Request.Cookies["loginName"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary() {
                    {"Controller","Home"},
                    {"Action","Login"}
                });
            }
        }
    }
}