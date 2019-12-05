using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
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
    [ActFilter, ResFilter, AdminAuthentication]
    public class ProductController : Controller
    {
        ProductRepository prep;

        ProductCategoryRepository pcrep;

        ProductFeatureRepository pfrep;

        FeatureRepository frep;

        CategoryRepository crep;

        public ProductController()
        {
            prep = new ProductRepository();

            pcrep = new ProductCategoryRepository();

            frep = new FeatureRepository();

            crep = new CategoryRepository();

            pfrep = new ProductFeatureRepository();
        }

        public ActionResult ListProduct()
        {
            return View(Tuple.Create(prep.GetActives(), pcrep.GetActives()));
        }

        public ActionResult AddProduct()
        {
            return View(Tuple.Create(new Product(), frep.GetActives(), crep.GetActives()));
        }

        [HttpPost]
        public ActionResult AddProduct([Bind(Prefix = "item1")]Product item, FormCollection koleksiyon)
        {
            if (item != null || koleksiyon != null)
            {
                prep.Add(item);

                var kategoriler = koleksiyon["kategoriler"];
                #region Açıklama
                /*
                         * Koleksiyon tüm girdileri taşıdığı yani product nesnesinin de girdilerini taşıdığı ve product nesnesinin zorunlu alanlar olduğu için koleksiyon null gelmiyor. Koleksiyon null gelmese de kategoriler ve özellikler null gelebilir. Çünkü bu alanlar zorunlu alanlardan değil.
                         */
                #endregion

                if (kategoriler != null)
                {
                    foreach (char kategori in kategoriler)
                    {
                        if (kategori != 44)
                        {
                            ProductCategory pc = new ProductCategory();

                            pc.CategoryID = Convert.ToInt32(kategori.ToString());
                            pc.ProductID = item.ID;
                            pcrep.Add(pc);
                        }
                    }
                }

                var ozellikler = koleksiyon["ozellikler"];

                if (ozellikler != null)
                {
                    foreach (char ozellik in ozellikler)
                    {
                        if (ozellik != 44)
                        {
                            ProductFeature pf = new ProductFeature();

                            pf.FeatureID = Convert.ToInt32(ozellik.ToString());
                            pf.ProductID = item.ID;
                            pfrep.Add(pf);
                        }
                    }
                }
                return RedirectToAction("Detail", new { id = item.ID });   //Admin ürün ekledikten sonra özeliklere değer atayabilmesi için detail sayfasına ürünün id'si ile birlikte yönlendirildi
            }
            ViewBag.Hata = "Ürün ekleme sırasında hata oluştu.";
            return View();
        }

        public ActionResult Detail(int id)
        {
            Product product = prep.GetByID(id);

            List<ProductFeature> liste = pfrep.Where(x => x.ProductID == id).ToList();

            return View(Tuple.Create(product, pcrep.GetActives(), liste));

            //Bu sayfada kullanıcı ürünün tüm özelliklerini görecek ve sadece ürün özelliklerine atama yapabilecek. Yani güncelleme sayfası gibi olmayacak. Ürün özellikleri dışındaki tüm özellikler readonly olacak, kaydet butonu ile özelliklere atanan değerler kaydedilecek ve ayrıca sayfada Güncelleme butonu olacak ve güncellemek isterse bu butona basarak güncelleme sayfasına gidebilecek. Yani sayfamız her ne kadar güncelleme sayfasına benzese de güncelleme sayfasından farklı.
        }

        [HttpPost]
        public ActionResult Detail([Bind(Prefix = "item1")]List<ProductFeature> liste)
        {
            if (liste != null)
            {
                foreach (ProductFeature item in liste)
                {
                    pfrep.Add(item);
                }
            }
            return RedirectToAction("ListProduct");
        }

        public ActionResult Update(int id)
        {
            Product product = prep.GetByID(id);

            return View(Tuple.Create(product, pfrep.GetActives(), pcrep.GetActives()));
        }

        [HttpPost]
        public ActionResult Update()
        {
            return RedirectToAction("ListProduct");
        }

        public ActionResult Delete(int id)
        {
            Product p = prep.GetByID(id);

            if (p != null)
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
