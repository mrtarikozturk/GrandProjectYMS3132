using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.COMMON.CommonTools;
using Project.MODEL.Entities;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Administrator.Controllers
{
    //[ActFilter, ResFilter, AdminAuthentication]
    public class HomeController : Controller
    {
        #region Açıklama
        /*
        * Admin'ler için giriş sayfası ayrı olacak. Admin, member ile aynı sayfadan giriş yapmamalı. Admin'nin girişleri bu Controller'da kontrol edilecek. Ayrıca kişi admin olarak tanımlanması için mevcut edamin tarafından yetki verilmesi gerektiği yani memmber gibi bir üyelik işlemi olmayacağı için burada sadece admin girişleri kontrol altına alındı. Admin ekleme, silme, ve güncelleme işlemleri için AppUser Controller'ı oluşturuldu.
        * Aynı şekilde member da buradan giriş yapmamalı, yapamamlı. Bu nedenle burada sadece kullanıcını sadece admin olup olmadığı kontrol edildi ve admin değilse girişe izin verilmedi.
        * 
        */
        #endregion

        AppUserRepository arep;

        public HomeController()
        {
            arep = new AppUserRepository();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AppUser item)
        {
            if (arep.Any(x => x.UserName == item.UserName && DantexCrypt.DeCrypt(x.Password) == item.Password && x.Role == MODEL.Enums.UserRole.Admin))
            {
                Session["admin"] = arep.FirstOrDefault(x => x.UserName == item.UserName && x.Password == item.Password && x.Role == MODEL.Enums.UserRole.Admin);

                return RedirectToAction("ListProduct", "Product");
            }
            ViewBag.Hata = "Hatalı Giriş Yaptınız.";
            return View();
        }

        public ActionResult LogOut()
        {
            if (Session["admin"] != null)
            {
                Session.Remove("admin");
            }
            return RedirectToAction("Login");
        }
    }
}