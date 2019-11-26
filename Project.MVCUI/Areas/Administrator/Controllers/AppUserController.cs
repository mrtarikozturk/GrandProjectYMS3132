using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Administrator.Controllers
{
    public class AppUserController : Controller //Tamamlandı
    {
        #region Açıklama
        /*
         * Burada kullanıcı ekleme, güncelleme, silme ve listeleme işlemleri tanımlandı. Başka bir admin daha tanımlancak ise buradan tanımlancak. Doğası gereği admin içerden biri yani admin tarafından yetkilendirilir.
         * 
         */
        #endregion

        AppUserRepository arep;

        AppUserDetailRepository adrep;

        public ActionResult List()
        {
            return View(arep.GetActives());
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add([Bind(Prefix = "item1")]AppUser item, [Bind(Prefix = "item2")]AppUserDetail item2)
        {
            if (item != null && item2 != null)
            {
                if (arep.Any(x => x.UserName != item.UserName && x.Password != item.Password && x.Email != item.Email))
                {
                    arep.Add(item);
                    item2.ID = item.ID;
                    adrep.Add(item2);
                    return View("List");
                }
                ViewBag.ZatenVar = "Böyle bir kullanıcı zaten var.";
                return View();
            }
            ViewBag.Hata = "Kullanıcı oluşturulurken hata oluştu.";
            return View();
        }

        public ActionResult Update(int id)
        {
            #region Açıklama
            //GetById burada veritabanındaki tüm kayıtlardan aktif-pasif demeden getirmektedir. Dolayısıyla silinen veriler de gelebilir bu durumda. Buraya bir daha bakacağız!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!GetById sorgusu sadece aktiflerde sordu yapacak şekilde düzeltildi. 
            #endregion

            AppUser a = arep.GetByID(id);
            AppUserDetail ad = adrep.GetByID(id);

            if (a != null) //Veri başka bir admin tarafından silinmiş olabilir. Bu durumda null gelme olasılığı bulunmakatadır.
            {
                return View(Tuple.Create(a, ad));
            }
            ViewBag.Bulunamadı = "Veri güncellenmek istenirken hata ile karşılaşıldı.";
            return View();
        }

        [HttpPost]
        public ActionResult Update([Bind(Prefix = "item1")]AppUser item, [Bind(Prefix = "item2")]AppUserDetail item2)
        {
            if (item != null && item2 != null)
            {
                arep.Update(item);
                adrep.Update(item2);
                return View("List");
            }
            ViewBag.Hata = "Hata oluştu";
            return View();
        }

        public ActionResult Delete(int id)
        {
            AppUser a = arep.GetByID(id);

            #region Açıklama
            //if (a != null)
            //{
            //    arep.Delete(a);
            //}
            //else
            //{
            //    ViewBag.Hata = "Hata Oluştu";       //Böyle bir kullanımda ViewBag ile gönderilmek istenen veri yakalanamaz. Çünkü böyle bir durumda ikinci bir action çalıştırılmak istenmektedir ve ViewBag yapısı ile ikinci bir action metoda veri gönderilmez. Oluşturulduğu action metodun yaşam döngüsü bittikten sonra kendisi de yok olur. Bu nedenle burada kllanılması gereken yapı tam anlamıyla TempData'dır. TempData ile bir sonraki çalışacak action metodun yaşam döngüsüne veri aktarılabilir.

            //}
            //return RedirectToAction("List"); 
            #endregion

            if (a!=null)
            {
                arep.Delete(a);
            }
            else
            {
                TempData["Hata"] = "Hata Oluştu";
            }
            return RedirectToAction("List");
        }
    }
}