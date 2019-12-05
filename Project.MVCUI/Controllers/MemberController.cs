using PagedList;
using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using Project.MVCUI.Filters;
using Project.MVCUI.Models;
using Project.MVCUI.Models.VMClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    //[ActFilter, ResFilter]
    public class MemberController : Controller
    {
        ProductRepository pRep;
        OrderRepository oRep;
        OrderDetailRepository odRep;
        CategoryRepository cRep;
        ProductCategoryRepository pcRep;
        public MemberController()
        {
            pcRep = new ProductCategoryRepository();
            pRep = new ProductRepository();
            oRep = new OrderRepository();
            odRep = new OrderDetailRepository();
            cRep = new CategoryRepository();
        }
        // GET: Member
        public ActionResult ProductList(string item, int sayfa = 1)
        {
            ViewBag.kategori = item;
            ViewBag.kategoriListesi = cRep.GetActives();
            if (item == null)
            {
                var degerler = pRep.GetActives().ToPagedList(sayfa, 10);
                return View(degerler);

            }
            else
            {
                // sorun category sını belırledıgımız ıstedıgımız urunlerı getıremıyoruz
                //productın ıcınden cekmelıyız
                return View(pRep.where(x => x.Categories.FirstOrDefault().Category.CategoryName == item).ToPagedList(sayfa, 10));

                //return View(pRep.where(x => x.Categories.FirstOrDefault().Category.CategoryName == item).ToList().ToPagedList(sayfa, 10));
            }
        }
        public ActionResult ProductDetail(int item)
        {
            if (pRep.Any(x => x.ID == item))
            {
                Product bizimurun = pRep.GetByID(item);
                using (HttpClient client = new HttpClient())
                {
                    


                    //http://localhost:61379/api/Product/GetProducts
                    //http://localhost:61379/
                    client.BaseAddress = new Uri("http://localhost:61379/api/");
                    //var postTask = client.PostAsJsonAsync("Product/GetProducts", item);
                    var getTask = client.GetAsync("Product/GetProducts");
                    var message = getTask.Result.Content.ReadAsAsync<List<ProductVM>>();

                    List<ProductVM> digerfirma = message.Result as List<ProductVM>;

                    if (digerfirma.Any(x => x.ProductName == bizimurun.ProductName))
                    {
                        ProductVM adamınurunu = digerfirma.Where(x => x.ProductName == bizimurun.ProductName).Single();
                        TempData["karsılastırma"] = "Eşleşen ürünler vardır";
                        ViewData.Add("adaminurunu", adamınurunu);

                    }
                    
                }
                return View(bizimurun);
            }
            
            TempData["karsılastırma"] = "Eşleşen ürünler yoktur";
            return RedirectToAction("ProductList");
        }

        public ActionResult SepeteAt(int id)
        {
            //if (Session["member"] == null)
            //{
            //    TempData["uyeDegil"] = "Lütfen sepete ürün eklemeden önce üye olun";
            //    Product bekleyenUrun = pRep.Find(id);
            //    Session["bekleyenUrun"] = bekleyenUrun;
            //    return RedirectToAction("Register", "Home");
            //}
            Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;
            Product eklenenUrun = pRep.GetByID(id);
            CartItem ci = new CartItem();

            ci.ID = eklenenUrun.ID;

            ci.Name = eklenenUrun.ProductName;

            ci.Price = eklenenUrun.UnitPrice;

            ci.ImagePath = eklenenUrun.ImagePath;

            c.SepeteEkle(ci);

            Session["scart"] = c;
            return RedirectToAction("ProductList");
        }
        public ActionResult CartPage()
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;

                return View(c);
            }

            else if (Session["member"] == null)
            {
                TempData["UyeDegil"] = "Lutfen önce üye olun";
                return RedirectToAction("Register", "Home");
            }

            TempData["message"] = "Sepetinizde ürün bulunmamaktadır";
            return RedirectToAction("ProductList");
        }

        public ActionResult SiparisiOnayla()
        {
            // if (Session["member"] != null)

            //  AppUser mevcutKullanici = Session["member"] as AppUser;

            //}
            // TempData["message"] = "Üye olmadan sipariş veremezsiniz";
            // return RedirectToAction("ProductList");  
            return View(Tuple.Create(new Order(), new PaymentVM()));
        }
        [HttpPost]
        public ActionResult SiparisiOnayla([Bind(Prefix = "Item1")] Order item, [Bind(Prefix = "Item2")] PaymentVM item2)
        {
            bool result = false;
            bool result2 = false;

            using (HttpClient client = new HttpClient())
            {

                //http://localhost:61183/api/Payment/ReceivePayment

                client.BaseAddress = new Uri("http://localhost:61183/api/");
                Cart sepet = Session["scart"] as Cart;
                item2.PaymentPrice = sepet.TotalPrice.Value;



                var postTask = client.PostAsJsonAsync("Payment/ReceivePayment", item2);



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

            if (result)
            {
                //AppUser kullanici = Session["member"] as AppUser;
                item.AppUserID = (Session["member"] as AppUser).ID; //Order'in kim tarafından sipariş edildigini belirlersiniz
                oRep.Add(item); //save edildiginde Order nesnesinin ID'si üretilir.

                Cart sepet = Session["scart"] as Cart;

                foreach (CartItem urun in sepet.Sepetim)
                {
                    OrderDetail od = new OrderDetail();
                    od.OrderID = item.ID;
                    od.ProductID = urun.ID;
                    od.TotalPrice = urun.SubTotal;
                    od.Amount = urun.Amount;
                    od.PaymentDate = DateTime.Now;

                    odRep.Add(od);

                }
                TempData["odeme"] = "Siparişiniz bize ulasmıstır..Tesekkür ederiz";

                using (HttpClient client = new HttpClient())
                {
                    KargoVM kargo = new KargoVM();

                    //http://localhost:57177/api/Payment/ReceivePayment

                    client.BaseAddress = new Uri("https://localhost:44333/api/");

                    kargo.Adi = (Session["member"] as AppUser).Profile.FirstName;
                    kargo.Soyadi = (Session["member"] as AppUser).Profile.LastName;
                    kargo.TCKimlikNumarası = item.TC;
                    kargo.Adres = item.Address;
                    kargo.Mail = (Session["member"] as AppUser).Email;
                    kargo.Il = item.City;
                    kargo.Ilce = item.District;
                    kargo.Mahalle = item.Town;
                    kargo.Telefon = item.Phone;




                    var postTask = client.PostAsJsonAsync("Home/KargoOlustur", kargo);



                    HttpResponseMessage sonuc = postTask.Result;


                    if (sonuc.IsSuccessStatusCode)
                    {
                        result2 = true;

                    }
                    else
                    {
                        result2 = false;

                    }




                }





            }

            else
            {
                TempData["odeme"] = "Odeme ile ilgili bir sıkıntı olustu. Lütfen banka ile iletişime geciniz";
                return RedirectToAction("ProductList");
            }

            return RedirectToAction("ProductList");
        }
        public ActionResult Delete(int id)
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;
                c.Sepettensil(id);

                if ((Session["scart"] as Cart).Sepetim.Count == 0)
                {
                    Session.Remove("scart");
                    TempData["gitti"] = "Sepetiniz bosaltılmıstır";

                    return RedirectToAction("ProductList");

                }

                return RedirectToAction("CartPage");


            }
            TempData["silinecekYok"] = "Sepetiniz bos oldugu icin bu sayfaya gitmenizde bir mantık yok";

            return RedirectToAction("ProductList");
        }
    }
}