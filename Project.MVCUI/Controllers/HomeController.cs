using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{

    // Test amaçlı oluşturulmuştur....
    public class HomeController : Controller
    {
        ProductRepository pRep;
        public HomeController()
        {
            pRep = new ProductRepository();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View(pRep.GetAll());
        }
    }
}