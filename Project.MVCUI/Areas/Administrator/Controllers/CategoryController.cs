using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using Project.MVCUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Administrator.Controllers
{
    [ActFilter, ResFilter]
    public class CategoryController : Controller
    {
        CategoryRepository crep;

        public CategoryController()
        {
            crep = new CategoryRepository();
        }

        public ActionResult ListCategory()
        {
            return View(crep.GetActives());
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category item)
        {
            if (item != null)
            {
                if (crep.Any(x => x.CategoryName == item.CategoryName && x.Description == item.Description && x.Status != MODEL.Enums.DataStatus.Deleted))
                {
                    ViewBag.Mevcut = "Eklenmek istenen kategori zaten mevcut.";
                    return View();
                }
                crep.Add(item);
                return RedirectToAction("ListCategory");
            }
            ViewBag.Hata = "Hata Oluştu.";
            return View();
        }

        public ActionResult UpdateCategory(int id)
        {
            Category category = crep.GetByID(id);
            if (category != null)
            {
                return View(category);
            }
            ViewBag.Hata = "Hata Oluştu";
            return View();
        }

        public ActionResult UpdateCategory(Category item)
        {
            if (item != null)
            {
                crep.Update(item);
                return RedirectToAction("ListCategory");
            }
            return View();
        }

        public ActionResult DeleteCategory(int id)
        {
            Category c = crep.GetByID(id);
            if (c != null)
            {
                crep.Delete(c);
            }
            else
            {
                TempData["Hata"] = "Hata Oluştu";       //ViewBag yapısı burada kullanılamaz. Çünkü ben başka bir action çalıştırmak istiyorum. Bu nedenle bir sonraki action metoda verimin ulaşmasını istiyorsam TempData kullanmalıyım.
            }
            return RedirectToAction("ListCategory");
        }
    }
}