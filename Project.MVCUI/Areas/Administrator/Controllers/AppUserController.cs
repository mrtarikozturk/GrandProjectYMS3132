using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.COMMON.CommonTools;
using Project.MODEL.Entities;
using Project.MODEL.Enums;
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

        public AppUserController()
        {
            arep = new AppUserRepository();

            adrep = new AppUserDetailRepository();
        }

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
            if (!ModelState.IsValid)
            {
                return View();
            }   //Kullanıcı tarayıcının JS kapatıp giriş yapmak isteyebilir. 

            if (item != null && item2 != null)
            {
                if (arep.Any(x => x.UserName != item.UserName && DantexCrypt.DeCrypt(x.Password) != item.Password && x.Email != item.Email))
                {
                    item.Role = UserRole.Admin;
                    arep.Add(item);
                    item2.ID = item.ID;
                    adrep.Add(item2);
                    MailSender.Send(item.Email, body: $"{"http://localhost:60442/Home/RegisterOnay/"}{item.ActivationCode}", subject: "Doğrulama Kodu");
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
            AppUser a = arep.FirstOrDefault(x => x.ID == id && x.Status != DataStatus.Deleted);
            AppUserDetail ad = adrep.FirstOrDefault(x => x.ID == id && x.Status != DataStatus.Deleted);

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
            if (!ModelState.IsValid)
            {
                return View();
            }

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

            if (a != null)
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