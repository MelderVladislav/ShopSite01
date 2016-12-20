using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopSite.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult HomePage()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Categories()
        {
            u0263406_shopBaseEntities context = new u0263406_shopBaseEntities();
            var categoriesList = context.CATEGORY.ToList();
            return View(categoriesList);
        }
        public ActionResult Products(int CategoryId)
        {
            u0263406_shopBaseEntities context = new u0263406_shopBaseEntities();
            var productsList = context.PRODUCT.Where(c => c.CATEGORY_ID == CategoryId).ToList();
            return View(productsList);
        }
    }
}