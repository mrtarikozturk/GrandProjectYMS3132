using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using Project.MVCUI.Models;
using Project.MVCUI.Models.Enums;
using Project.MVCUI.Models.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Filters
{
    public class ResFilter : FilterAttribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            LogVM log = new LogVM();

            if (filterContext.HttpContext.Session["admin"] != null)
            {
                log.UserName = (filterContext.HttpContext.Session["admin"] as AppUser).UserName;
            }
            else if (filterContext.HttpContext.Session["member"] != null)
            {
                log.UserName = (filterContext.HttpContext.Session["member"] as AppUser).UserName;
            }
            else
            {
                log.UserName = "Anonim Kullanıcı";
            }

            log.ControllerName = filterContext.RouteData.Values["ControllerName"].ToString();
            log.ActionName = filterContext.RouteData.Values["ActionName"].ToString();
            log.Description = Keyword.Exit;
            log.Information = "View çalıştıktan sonra kaydedilmiştir.";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57177/api/");  //Burada apinin çalışacağı konuma göre adres değişecek!!!! Mevcut durumda hata verecektir.

                client.PostAsJsonAsync("HomeController/AddLog", log);
            }
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            LogVM log = new LogVM();

            if (filterContext.HttpContext.Session["admin"] != null)
            {
                log.UserName = (filterContext.HttpContext.Session["admin"] as AppUser).UserName;
            }
            else if (filterContext.HttpContext.Session["member"] != null)
            {
                log.UserName = (filterContext.HttpContext.Session["member"] as AppUser).UserName;
            }
            else
            {
                log.UserName = "Anonim Kullanıcı";
            }
            log.ControllerName = filterContext.RouteData.Values["ControllerName"].ToString();
            log.ActionName = filterContext.RouteData.Values["ActionName"].ToString();
            log.Description = Keyword.Entry;
            log.Information = "View çalışmadan önce kaydedilmiştir.";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57177/api/");        //Burada apinin çalışacağı konuma göre adres değişecek!!!! Mevcut durumda hata verecektir.

                client.PostAsJsonAsync("HomeController/AddLog", log);
            }
        }
    }
}