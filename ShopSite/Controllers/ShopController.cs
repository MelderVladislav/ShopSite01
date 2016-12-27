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
       
        public ActionResult Basket()
        {
            HttpCookie cookieOrder = Request.Cookies["order"];
            HttpCookie cookie1;
            if (cookieOrder == null)
            {
                
                cookie1 = new HttpCookie("order","");
                cookie1.Path = "/Shop";
                Response.Cookies.Add(cookie1); 
            }

            string MyCookie = Request.Cookies["order"].Value;

            string[] masProducts = MyCookie.Split('&');
            u0263406_shopBaseEntities context = new u0263406_shopBaseEntities();
            int tempId;
            List<PRODUCT> purchaseList = new List<PRODUCT>();
            PRODUCT tempProduct;
            bool tempConvert;
            string check = "";
            if (masProducts.Length != 0)
                foreach (string str in masProducts)
                {

                    tempConvert = Int32.TryParse(str.ToString(), out tempId);
                    if (tempConvert)
                    {
                        tempProduct = context.PRODUCT.Where(c => c.PRODUCT_ID == tempId).ToList().FirstOrDefault();
                        if (tempProduct != null)
                        {
                            check += str;
                            purchaseList.Add(tempProduct);
                        }
                    }

                }
            ViewBag.Check = check;
            ViewBag.MyCookie = MyCookie;
            return View(purchaseList);
        }
        public ActionResult Order()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OrderResult(Models.OrderModel MyModel)
        {
            
            if (MyModel == null) return new EmptyResult();
            string resultString = "<p>Запрос на сервер Косого переулка был получен " + DateTime.Now + "</p>"+"<p>ФИО заказчика: "+MyModel.FullName+"</p>" 
                +"<p>Адрес почтового ящика: "+MyModel.OrderMail+"</p>"+"<p>Список покупок:</p>";
            string MyCookie = Request.Cookies["order"].Value;

            string[] masProducts = MyCookie.Split('&');
            u0263406_shopBaseEntities context = new u0263406_shopBaseEntities();
            int tempId;
           List<string> purchaseList = new List<string>();
            List<PRODUCT> purchaseListProduct = new List<PRODUCT>();
            PRODUCT tempProduct;
            bool tempConvert;    
            if (masProducts.Length != 0)
                foreach (string str in masProducts)
                {

                    tempConvert = Int32.TryParse(str.ToString(), out tempId);
                    if (tempConvert)
                    {
                        tempProduct = context.PRODUCT.Where(c => c.PRODUCT_ID == tempId).ToList().FirstOrDefault();
                        if (tempProduct != null)
                        {

                            resultString += "<p>"+tempProduct.PRODUCT_NAME+ "</p>";
                            purchaseListProduct.Add(tempProduct);
                        }
                    }

                }
            decimal sum=0;
            foreach(PRODUCT pr in purchaseListProduct)
            {
                sum += pr.PRODUCT_PRICE;
            }
            string sumString = Convert.ToString(sum);
            resultString += "Общая сумма: " + sumString+" галеонов";
            MvcHtmlString mvcString = new MvcHtmlString(resultString);
            
            ViewBag.ResultString = mvcString;
            return View();
        }
    }
}