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
using System.Web.UI.WebControls;

namespace Project.MVCUI.Filters
{
    public class ActFilter : FilterAttribute, IActionFilter
    {



        public void OnActionExecuted(ActionExecutedContext filterContext)
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
                    log.UserName = "Anonim Kullanici";
                }

                log.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                log.ActionName = filterContext.ActionDescriptor.ActionName;
                log.Description = Keyword.Exit;
                log.Information = "Metot çalıştıktan sonra kaydedildi.";
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

        public void OnActionExecuting(ActionExecutingContext filterContext)
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
                    log.UserName = "Anonim Kullanici";
                }

                log.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                log.ActionName = filterContext.ActionDescriptor.ActionName;
                log.Description = Keyword.Entry;
                log.Information = "Metot çalışmadan önce kaydedildi.";
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
