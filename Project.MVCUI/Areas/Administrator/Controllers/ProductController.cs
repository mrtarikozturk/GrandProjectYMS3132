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
    public class ProductController : Controller
    {
        ProductRepository prep;

        ProductCategoryRepository pcrep;

        FeatureRepository frep;

        CategoryRepository crep;

        public ProductController()
        {
            prep = new ProductRepository();

            pcrep = new ProductCategoryRepository();

            frep = new FeatureRepository();

            crep = new CategoryRepository();
        }

        public ActionResult ListProduct()
        {
            return View(Tuple.Create(prep.GetActives(), pcrep.GetActives(), crep.GetActives()));
        }

        public ActionResult AddProduct()
        {
            return View(Tuple.Create(new Product(), frep.GetActives(), crep.GetActives()));
        }

        [HttpPost]
        public ActionResult AddProduct([Bind(Prefix ="item1")]Product item, FormCollection koleksiyon)
        {

            if (item!=null && koleksiyon!=null)
            {
                prep.Add(item);

            }
            return RedirectToAction("ListProduct");
        }

        public ActionResult Detail(int id)
        {
            Product product = prep.GetByID(id);

            return View(product);       //Eksik olabilir. Üzerinde çalışacam.
        }

        [HttpPost]
        public ActionResult Detail()
        {
            //Üzerinde çalışılacak
            return RedirectToAction("ListProduct");
        }

        public ActionResult Update(int id)
        {
            Product product = prep.GetByID(id);

            return View(Tuple.Create(product, frep.GetActives(), crep.GetActives()));
        }

        [HttpPost]
        public ActionResult Update()
        {
            return RedirectToAction("ListProduct");
        }

        public ActionResult Delete(int id)
        {
            Product p = prep.GetByID(id);

            if (p!=null)
            {
                prep.Delete(p);
            }
            else
            {
                TempData["Hata"] = "Hata Oluştu";
            }
            return RedirectToAction("ListProduct");
        }
    }
}
