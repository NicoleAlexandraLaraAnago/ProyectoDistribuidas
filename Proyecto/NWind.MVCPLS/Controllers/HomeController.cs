using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NWindProxyService;
using Entities;

namespace NWind.MVCPLS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(int id)
        {
            // Obtener los productos de la categoría
            var Proxy = new Proxy();
            var Products = Proxy.FilterProductsByCategoryID(id);
            return View("ProductList", Products);
        }
    }
}