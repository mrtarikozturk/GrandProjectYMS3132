using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Filters
{
    public class ResFilter : AuthorizeAttribute, IResultFilter
    {
        LogRepository lrep;

        public ResFilter()
        {
            lrep = new LogRepository();
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log log = new Log();

            if (filterContext.HttpContext.Session["admin"] != null)
            {
                log.CreatedBy = (filterContext.HttpContext.Session["admin"] as AppUser).UserName;
            }
            else if (filterContext.HttpContext.Session["member"] != null)
            {
                log.CreatedBy = (filterContext.HttpContext.Session["member"] as AppUser).UserName;
            }
            else
            {
                log.CreatedBy = "Anonim Kullanıcı";
            }

            log.ControllerName = filterContext.RouteData.Values["ControllerName"].ToString();
            log.ActionName = filterContext.RouteData.Values["ActionName"].ToString();
            log.Description = Keyword.Exit;
            log.Imformation = "View çalıştıktan sonra kaydedilmiştir.";
            lrep.Add(log);
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log log = new Log();

            if (filterContext.HttpContext.Session["admin"] != null)
            {
                log.CreatedBy = (filterContext.HttpContext.Session["admin"] as AppUser).UserName;
            }
            else if (filterContext.HttpContext.Session["member"]!=null)
            {
                log.CreatedBy = (filterContext.HttpContext.Session["member"] as AppUser).UserName;
            }
            else
            {
                log.CreatedBy = "Anonim Kullanıcı";
            }
            log.ControllerName = filterContext.RouteData.Values["ControllerName"].ToString();
            log.ActionName = filterContext.RouteData.Values["ActionName"].ToString();
            log.Description = Keyword.Entry;
            log.Imformation = "View çalışmadan önce kaydedilmiştir.";
        }
    }
}