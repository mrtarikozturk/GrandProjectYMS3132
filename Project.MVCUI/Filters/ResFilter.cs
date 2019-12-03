using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using Project.MVCUI.Models;
using Project.MVCUI.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Filters
{
    public class ResFilter : AuthorizeAttribute, IResultFilter
    {
        

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            bool result;
            using (HttpClient client = new HttpClient())
            {


                //http://localhost:52448//api/Home/AddLog

                client.BaseAddress = new Uri("http://localhost:52448/api/");
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
                var postTask = client.PostAsJsonAsync("Home/AddLog", log);
                HttpResponseMessage sonuc = postTask.Result;


                if (sonuc.IsSuccessStatusCode)
                {
                    result = true;

                }
                else
                {
                    result = false;

                }
            }
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            bool result;
            using (HttpClient client = new HttpClient())
            {


                //http://localhost:52448//api/Home/AddLog

                client.BaseAddress = new Uri("http://localhost:52448/api/");
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
                var postTask = client.PostAsJsonAsync("Home/AddLog", log);
                HttpResponseMessage sonuc = postTask.Result;


                if (sonuc.IsSuccessStatusCode)
                {
                    result = true;

                }
                else
                {
                    result = false;

                }
            }
        }
    }
}