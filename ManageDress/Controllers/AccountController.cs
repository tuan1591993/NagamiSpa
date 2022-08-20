using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dress_BLL.Function;
using Dress_BLL.Model;

namespace ManageDress.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Admin()
        {
            ViewBag.Name = Session["FullName"];
            return View();
        }

        public ActionResult ManageUser()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                var user = AccountService.GetAllUser();
                return View(user);
            }

            return RedirectToAction("Login");
        }

        public ActionResult ManageImageHome()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                var user = AccountService.GetListImageHome();
                return View(user);
            }

            return RedirectToAction("Login");
        }

        public ActionResult ManageFeedback(FeedbackFilterModel filter)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                var feedback = AccountService.FilterFeedback(filter);

                foreach (var item in feedback)
                {
                    item.DateView = item.CreateDate.Value.ToString("dd/MM/yyyy hh:mm");
                }
                return View(feedback);
            }

            return RedirectToAction("Login");
        }

        public ActionResult ManageService()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                var services = AccountService.GetAllServices();
                return View(services);
            }

            return RedirectToAction("Login");
        }

        public ActionResult ManageNews()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                var services = AccountService.GetAllNewsArticles();
                return View(services);
            }

            return RedirectToAction("Login");
        }

        public ActionResult ManageShop()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                var shops = AccountService.GetShopInformation();
                return View(shops);
            }

            return RedirectToAction("Login");
        }

        public ActionResult ManageCustomerView()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                var customer = AccountService.GetCustomerResults();
                return View(customer);
            }

            return RedirectToAction("Login");
        }

        public ActionResult ManageProcess()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                var shops = AccountService.GetProcessSurgery();
                return View(shops);
            }

            return RedirectToAction("Login");
        }

        public ActionResult ManageBanner()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                var banner = AccountService.GetBanners();
                return View(banner);
            }

            return RedirectToAction("Login");
        }

        public ActionResult HighLightService()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                var service = AccountService.GetHighlightService();
                return View(service);
            }

            return RedirectToAction("Login");
        }

        public ActionResult ManageNavigation()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                var service = AccountService.GetAllNavigation();
                return View(service);
            }

            return RedirectToAction("Login");
        }

        public ActionResult EditService(string serviceCode)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                var services = AccountService.GetServiceByCode(serviceCode);
                return View(services);
            }

            return RedirectToAction("Login");
        }

        public ActionResult EditNews(string code)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                var news = AccountService.GetNewsByCode(code);
                return View(news);
            }

            return RedirectToAction("Login");
        }


        public ActionResult AddService()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return View();
            }

            return RedirectToAction("Login");
        }

        public ActionResult AddNews()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return View();
            }

            return RedirectToAction("Login");
        }

        public ActionResult AddNavigation()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return View();
            }

            return RedirectToAction("Login");
        }

        public ActionResult EditNavigation(int id)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                var services = AccountService.GetNavigationById(id);
                return View(services);
            }

            return RedirectToAction("Login");
        }

        public ActionResult EditUser(int id)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                var services = AccountService.GetUserById(id);
                return View(services);
            }

            return RedirectToAction("Login");
        }

        public ActionResult AddUser()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return View();
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            var userModel = AccountService.Login(userName, password);
            if (userModel != null)
            {
                Session["FullName"] = userModel.FullName;
                Session["UserName"] = userModel.UserName;
                Session["Role"] = userModel.Role;
                Session["idUser"] = userModel.Id;
                return RedirectToAction("ManageShop");
            }
            else
            {
                TempData["Error"] = "Tài khoản hoặc mật khẩu không đúng";
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult FilterFeedback(FeedbackFilterModel model)
        {
            var result = AccountService.FilterFeedback(model);

            if (result != null && result.Count() > 0)
            {
                foreach (var item in result)
                {
                    item.DateView = item.CreateDate.Value.ToString("dd/MM/yyyy hh:mm");
                }
                return Json(new { Response = result });
            }
            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult UpdateShop(ShopModel model)
        {
            var result = AccountService.UpdateShopInformation(model);

            if (result)
            {
                return Json(new { Response = "Success" });
            }
            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult UpdateService(ServiceDetailUpdateModel model)
        {
            var result = AccountService.AddOrUpdateServices(model);

            if (result)
            {
                return Json(new { Response = "Success" });
            }
            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult UpdateNews(NewsArticlesModel model)
        {
            var result = AccountService.AddOrUpdateNews(model);

            if (result)
            {
                return Json(new { Response = "Success" });
            }
            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult DeleteService(ServiceDetailUpdateModel model)
        {
            var result = AccountService.DeleteServiceById(model.Id);

            if (result)
            {
                return Json(new { Response = "Success" });
            }
            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult DeleteNews(NewsArticlesModel model)
        {
            var result = AccountService.DeleteNewsById(model.Id);

            if (result)
            {
                return Json(new { Response = "Success" });
            }
            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult UpdateNavigation(NavigationModel model)
        {
            var result = AccountService.AddOrUpdateNavigation(model);

            if (result)
            {
                return Json(new { Response = "Success" });
            }
            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult DeleteNavigation(NavigationModel model)
        {
            var result = AccountService.DeleteNavigationById(model.Id);

            if (result)
            {
                return Json(new { Response = "Success" });
            }
            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult UpdateLinkCustomerView(List<LinkCustomerViewModel> model)
        {
            var result = AccountService.UpdateListCustomerResults(model);

            if (result)
            {
                return Json(new { Response = "Success" });
            }
            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult UpdateProcessSurgery(List<ProcessSurgeryModel> model)
        {
            var result = AccountService.UpdateProcessSurgery(model);

            if (result)
            {
                return Json(new { Response = "Success" });
            }
            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult AddOrUpdateUser(UserModel model)
        {
            var result = AccountService.AddOrUpdateUser(model);

            if (result)
            {
                return Json(new { Response = "Success" });
            }
            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult DeleteUser(UserModel model)
        {
            var result = AccountService.DeleteUserById(model.Id);

            if (result)
            {
                return Json(new { Response = "Success" });
            }
            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            //Kiểm tra file upload có tồn tại không
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var fileImg = Request.Files["HelpSectionImages"];
                string path = string.Empty;
                var serviceId = Request.Form["Code"];
                var type = Request.Form["Type"];
                var index = Request.Form["Index_Attachment"];


                // xử lý của bạn
                // Bạn có thể trả về 1 json đường dẫn ảnh

                if (fileImg != null)
                {
                    path = Path.Combine(Server.MapPath("~/Images/Services"), Path.GetFileName(fileImg.FileName));
                    fileImg.SaveAs(path);

                    var model = new UploadImageServiceModel();
                    model.ServiceId = Convert.ToInt16(serviceId);
                    model.Type = type;
                    model.IndexAttachment = Convert.ToInt16(index);
                    var imagePath = "../Images/Services/" + fileImg.FileName;
                    model.URL = imagePath;

                    var result = AccountService.UploadPhoto(model);

                    if (result)
                    {
                        return Json(new { Response = "Success", ImagePath = imagePath });
                    }
                }
            }

            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult DeleteObject()
        {
            var serviceId = Request.Form["Code"];
            var type = Request.Form["Type"];
            var index = Request.Form["Index_Attachment"];



            var model = new UploadImageServiceModel();
            model.ServiceId = Convert.ToInt16(serviceId);
            model.Type = type;
            model.IndexAttachment = Convert.ToInt16(index);

            var result = AccountService.DeleteObject(model);

            if (result)
            {
                return Json(new { Response = "Success" });
            }

            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult UploadNews()
        {
            //Kiểm tra file upload có tồn tại không
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var fileImg = Request.Files["HelpSectionImages"];
                string path = string.Empty;
                var newsId = Request.Form["Code"];
                var type = Request.Form["Type"];

                // xử lý của bạn
                // Bạn có thể trả về 1 json đường dẫn ảnh

                if (fileImg != null)
                {
                    path = Path.Combine(Server.MapPath("~/Images/Information"), Path.GetFileName(fileImg.FileName));
                    fileImg.SaveAs(path);

                    var model = new UploadImageServiceModel();
                    model.ServiceId = Convert.ToInt16(newsId);
                    model.Type = type;
                    var imagePath = "../Images/Information/" + fileImg.FileName;
                    model.URL = imagePath;

                    var result = AccountService.UploadNews(model);

                    if (result)
                    {
                        return Json(new { Response = "Success", ImagePath = imagePath });
                    }
                }
            }

            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult UploadServiceReference()
        {
            //Kiểm tra file upload có tồn tại không
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var fileImg = Request.Files["HelpSectionImages"];
                string path = string.Empty;
                var serviceId = Request.Form["Service_Id"];
                var type = Request.Form["Type"];
                var index = Request.Form["Index"];


                // xử lý của bạn
                // Bạn có thể trả về 1 json đường dẫn ảnh

                if (fileImg != null)
                {
                    path = Path.Combine(Server.MapPath("~/Images/Services"), Path.GetFileName(fileImg.FileName));
                    fileImg.SaveAs(path);

                    var model = new UploadImageServiceModel();
                    model.ServiceId = Convert.ToInt16(serviceId);
                    model.Type = type;
                    model.IndexAttachment = Convert.ToInt16(index);
                    var imagePath = "../Images/Services/" + fileImg.FileName;
                    model.URL = imagePath;

                    var result = AccountService.UploadServiceReference(model);

                    if (result)
                    {
                        return Json(new { Response = "Success", ImagePath = imagePath });
                    }
                }
            }

            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult UploadNewsReference()
        {
            //Kiểm tra file upload có tồn tại không
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var fileImg = Request.Files["HelpSectionImages"];
                string path = string.Empty;
                var serviceId = Request.Form["Service_Id"];
                var type = Request.Form["Type"];
                var index = Request.Form["Index"];


                // xử lý của bạn
                // Bạn có thể trả về 1 json đường dẫn ảnh

                if (fileImg != null)
                {
                    path = Path.Combine(Server.MapPath("~/Images/Information"), Path.GetFileName(fileImg.FileName));
                    fileImg.SaveAs(path);

                    var model = new UploadImageServiceModel();
                    model.ServiceId = Convert.ToInt16(serviceId);
                    model.Type = type;
                    model.IndexAttachment = Convert.ToInt16(index);
                    var imagePath = "../Images/Information/" + fileImg.FileName;
                    model.URL = imagePath;

                    var result = AccountService.UploadNewsReference(model);

                    if (result)
                    {
                        return Json(new { Response = "Success", ImagePath = imagePath });
                    }
                }
            }

            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult UploadImageProcess()
        {
            //Kiểm tra file upload có tồn tại không
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var fileImg = Request.Files["HelpSectionImages"];
                string path = string.Empty;
                var Id = Request.Form["Id"];

                // xử lý của bạn
                // Bạn có thể trả về 1 json đường dẫn ảnh

                if (fileImg != null)
                {
                    path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(fileImg.FileName));
                    fileImg.SaveAs(path);

                    var imagePath = "../Images/" + fileImg.FileName;

                    var model = new UploadImageHomeModel();
                    model.Id = Convert.ToInt16(Id);
                    model.Url = imagePath;

                    var result = AccountService.UploadPhotoProcess(model);

                    if (result)
                    {
                        return Json(new { Response = "Success", ImagePath = imagePath });
                    }
                }
            }

            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult UploadHighlightService()
        {
            //Kiểm tra file upload có tồn tại không
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var fileImg = Request.Files["HelpSectionImages"];
                string path = string.Empty;
                var Id = Request.Form["Id"];

                // xử lý của bạn
                // Bạn có thể trả về 1 json đường dẫn ảnh

                if (fileImg != null)
                {
                    path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(fileImg.FileName));
                    fileImg.SaveAs(path);

                    var imagePath = "../Images/" + fileImg.FileName;

                    var model = new UploadImageHomeModel();
                    model.Id = Convert.ToInt16(Id);
                    model.Url = imagePath;

                    var result = AccountService.UploadHighlightService(model);

                    if (result)
                    {
                        return Json(new { Response = "Success", ImagePath = imagePath });
                    }
                }
            }

            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult UploadImageBanner()
        {
            //Kiểm tra file upload có tồn tại không
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var fileImg = Request.Files["HelpSectionImages"];
                string path = string.Empty;
                var Id = Request.Form["Id"];
                var type = Request.Form["Type"];

                // xử lý của bạn
                // Bạn có thể trả về 1 json đường dẫn ảnh

                if (fileImg != null)
                {
                    path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(fileImg.FileName));
                    fileImg.SaveAs(path);

                    var imagePath = "../Images/" + fileImg.FileName;

                    var model = new BannerModel();
                    model.Id = Convert.ToInt16(Id);
                    if (type == "Web")
                    {
                        model.Url = imagePath;
                    }
                    else
                    {
                        model.Url_Mobile = imagePath;
                    }

                    var result = AccountService.UpdateBanner(model);

                    if (result)
                    {
                        return Json(new { Response = "Success", ImagePath = imagePath });
                    }
                }
            }

            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult UploadImageCustomer()
        {
            //Kiểm tra file upload có tồn tại không
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var fileImg = Request.Files["HelpSectionImages"];
                string path = string.Empty;
                var Id = Request.Form["Id"];
                var type = Request.Form["Type"];

                // xử lý của bạn
                // Bạn có thể trả về 1 json đường dẫn ảnh

                if (fileImg != null)
                {
                    path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(fileImg.FileName));
                    fileImg.SaveAs(path);

                    var imagePath = "../Images/" + fileImg.FileName;

                    var model = new CustomerResultModel();
                    model.Id = Convert.ToInt16(Id);
                    if (type == "AnhKhachHang")
                    {
                        model.ImageGroupUrl = imagePath;
                    }
                    else
                    {
                        if (type == "AnhYoutube")
                        {
                            model.ImageAfterUrl = imagePath;
                        }
                        else
                        {
                            if (type == "AnhTruocSau")
                            {
                                model.ImageBeforeUrl = imagePath;
                            }
                            else
                            {
                                if (type == "AnhTruocSauMobile")
                                {
                                    model.CustomerName = imagePath;
                                }
                            }
                        }
                    }

                    var result = AccountService.UploadImageCustomerResults(model);

                    if (result)
                    {
                        return Json(new { Response = "Success", ImagePath = imagePath });
                    }
                }
            }

            return Json(new { Response = "Error" });
        }

        [HttpPost]
        public ActionResult UploadImageHome()
        {
            //Kiểm tra file upload có tồn tại không
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var fileImg = Request.Files["HelpSectionImages"];
                string path = string.Empty;
                var Id = Request.Form["Id"];

                // xử lý của bạn
                // Bạn có thể trả về 1 json đường dẫn ảnh

                if (fileImg != null)
                {
                    path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(fileImg.FileName));
                    fileImg.SaveAs(path);

                    var imagePath = "../Images/" + fileImg.FileName;

                    var model = new ImageHomeModel();
                    model.Id = Convert.ToInt16(Id);
                    model.Url = imagePath;

                    var result = AccountService.UpdateImageHome(model);

                    if (result)
                    {
                        return Json(new { Response = "Success", ImagePath = imagePath });
                    }
                }
            }

            return Json(new { Response = "Error" });
        }
    }
}