using Dress_BLL.Function;
using Dress_BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageDress.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var shop = ShopService.GetShopInformation();
            return View(shop);        
        }

        public ActionResult About()
        {
            var user = AccountService.GetAllUser();
            return View(user);
        }

        public ActionResult Home()
        {
            var shop = ShopService.GetShopInformation();

            return View(shop);
        }

        public ActionResult Service(string code)
        {
            var shop = ShopService.GetServiceByCode(code);
            return View(shop);
        }

        public ActionResult News(string code)
        {
            var news = ShopService.GetNewsByCode(code);
            return View(news);
        }

        public ActionResult Search(string Key)
        {
            var search = ShopService.SearchByKey(Key);
            search.Key = Key;
            return View(search);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult AddFeedback(FeedbackModel model)
        {
            var result = ShopService.AddFeedback(model);
             
            if (result)
            {
                return Json(new { Response = "Success" });
            }
            return Json(new { Response = "Error" });
        }
    }
}