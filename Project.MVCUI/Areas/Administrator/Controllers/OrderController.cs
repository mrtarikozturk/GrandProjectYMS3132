using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Administrator.Controllers
{
    public class OrderController : Controller
    {
        #region Açıklama
        /*
         * Burası geliştirilecek, henüz karar verilmedi nasıl bir konsept olacağına. Admin sipariş sayfasında hangi verileri görecek bir araştır.
         * Kullanıcı siparişi verdikten sonra tüm siparişler admin paneline düşer ve adminin onayladığı siparişler hazırlanır ve gönderilir. Yani kullanıcının her sipariş vermesi ile olay bitmiyor. Dolayısıyla sipariş tablosunda onay property'sinin de açılması gerekir. Admin onay vermesi halinde kargonun gönderilmesi aksi halde kullanıcıya bilgi verilmesi gerekir. Buranın detaylarına gireceğiz bu nedenle sonraya bırakıldı.
         * 
         * 
         * 
         */
        #endregion

        OrderRepository orep;

        OrderDetailRepository odrep;

        public OrderController()
        {
            orep = new OrderRepository();

            odrep = new OrderDetailRepository();
        }
        
        public ActionResult List()
        {
            return View(orep.GetActives());
        }
    }
}
