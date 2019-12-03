using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Filters
{
    public class AuthFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["oturum"] == null)
            {
                //Login sayfasına yönlendirmek
                filterContext.Result = new RedirectResult("/Home/Register");
            }
        }
    }
}