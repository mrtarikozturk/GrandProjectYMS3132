using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Filters
{
    public class ActFilter : FilterAttribute, IActionFilter
    {
        LogRepository lrep;

        public ActFilter()
        {
            lrep = new LogRepository();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log log = new Log();

            if (filterContext.HttpContext.Session["admin"]!=null)
            {
                log.CreatedBy = (filterContext.HttpContext.Session["admin"] as AppUser).UserName;
            }
            else if(filterContext.HttpContext.Session["member"] != null)
            {
                log.CreatedBy = (filterContext.HttpContext.Session["member"] as AppUser).UserName;
            }
            else
            {
                log.CreatedBy = "Anonim Kullanici";
            }

            log.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            log.ActionName = filterContext.ActionDescriptor.ActionName;
            log.Description = Keyword.Exit;
            log.Imformation = "Metot çalıştıktan sonra kaydedildi.";
            lrep.Add(log);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log log = new Log();

            if (filterContext.HttpContext.Session["admin"]!=null)
            {
                log.CreatedBy = (filterContext.HttpContext.Session["admin"] as AppUser).UserName;
            }
            else if (filterContext.HttpContext.Session["member"]!=null)
            {
                log.CreatedBy = (filterContext.HttpContext.Session["member"] as AppUser).UserName;
            }
            else
            {
                log.CreatedBy = "Anonim Kullanici";
            }

            log.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            log.ActionName = filterContext.ActionDescriptor.ActionName;
            log.Description = Keyword.Entry;
            log.Imformation = "Metot çalışmadan önce kaydedildi.";
            lrep.Add(log);
        }
    }
}
