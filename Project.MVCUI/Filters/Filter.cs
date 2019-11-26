using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Filters
{
    public class Filter: ActionFilterAttribute
    {
        #region Açıklama
        //ActFilter ve ResFilter sınıflarını içinde barındıran tek bir sınıf oluşturuldu. Böyle bir şeyin varolduğunu öğrendikten sonra denemekti. Silinebilir de kalabiilir de. Buarada daha sonra metotlardaki kod tekrarını önlemeye yönelik bir metoto oluşturulacak.
        #endregion

        LogRepository lrep;

        public Filter()
        {
            lrep = new LogRepository();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
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
                log.CreatedBy = "Anonim Kullanici";
            }

            log.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            log.ActionName = filterContext.ActionDescriptor.ActionName;
            log.Description = Keyword.Exit;
            log.Imformation = "Metot çalıştıktan sonra kaydedildi.";
            lrep.Add(log);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
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
                log.CreatedBy = "Anonim Kullanici";
            }

            log.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            log.ActionName = filterContext.ActionDescriptor.ActionName;
            log.Description = Keyword.Entry;
            log.Imformation = "Metot çalışmadan önce kaydedildi.";
            lrep.Add(log);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
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

        public override void OnResultExecuting(ResultExecutingContext filterContext)
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
            log.Description = Keyword.Entry;
            log.Imformation = "View çalışmadan önce kaydedilmiştir.";
        }

    }
}