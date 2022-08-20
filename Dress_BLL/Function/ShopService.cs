using Dress.DAL.Structure;
using Dress_BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Dress_BLL.Function
{
    public class ShopService
    {
        public static LandingPageModel GetShopInformation()
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var shopInfo = ShopToModal(context.Salons.FirstOrDefault());

                var listBanner = new List<BannerModel>();
                var resultBanner = context.Banners.ToList();
                if (resultBanner != null && resultBanner.Count > 0)
                {
                    foreach (var item in resultBanner)
                    {
                        var banner = new BannerModel
                        {
                            Url = item.Url,
                            Url_Mobile = item.Url_Mobile,
                            Index = item.Index,
                        };

                        listBanner.Add(banner);
                    }
                }

                var listImageHome = new List<ImageHomeModel>();
                var resultImageHome = context.ImageHomes.ToList();
                if (resultImageHome != null && resultImageHome.Count > 0)
                {
                    foreach (var item in resultImageHome)
                    {
                        var image = new ImageHomeModel
                        {
                            Type = item.Type,
                            Url = item.Url,
                            Index = item.Index,
                            Id = item.Id
                        };

                        listImageHome.Add(image);
                    }
                }

                var listNavigation = new List<NavigationModel>();
                var resultNavigation = context.Navigations.ToList();
                if (resultNavigation != null && resultNavigation.Count > 0)
                {
                    foreach (var item in resultNavigation)
                    {
                        var banner = new NavigationModel
                        {
                            Title = item.Title,
                            Link = item.Link,
                            Parent = item.Parent,
                            Group = item.Group
                        };

                        listNavigation.Add(banner);
                    }
                }

                var listCustomer = new List<CustomerResultModel>();

                var result = context.CustomerResults.OrderBy(m => m.IndexImage).ToList();

                if (result != null && result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        var customer = new CustomerResultModel
                        {
                            CustomerName = item.CustomerName,
                            Title = item.Title,
                            ImageAfterUrl = item.ImageAfterUrl,
                            ImageBeforeUrl = item.ImageBeforeUrl,
                            ServiceName = item.ServiceName,
                            IndexImage = item.IndexImage,
                            ImageGroupUrl = item.ImageGroupUrl,
                            VideoUrl = item.VideoUrl
                        };

                        listCustomer.Add(customer);
                    }
                }

                var highlightResult = context.HighlightServices.ToList();
                var highlightService = new List<HighlightServiceModel>();

                if (highlightResult != null && highlightResult.Count > 0)
                {
                    foreach (var item in highlightResult)
                    {
                        var highlight = new HighlightServiceModel
                        {
                            Id = item.Id,
                            Description = item.Description,
                            Key = item.Key,
                            ImageUrl = item.ImageUrl,
                            Index = item.Index
                        };

                        highlightService.Add(highlight);
                    }
                }

                var processResult = context.ProcessSurgeries.ToList();
                var processSurgery = new List<ProcessSurgeryModel>();

                if (processResult != null && processResult.Count > 0)
                {
                    foreach (var item in processResult)
                    {
                        var process = new ProcessSurgeryModel
                        {
                            Id = item.Id,
                            Content = item.Content,
                            Content_Show = item.Content_Show,
                            Key = item.Key,
                            ImageUrl = item.ImageUrl,
                            Index = item.Index,
                            Title = item.Title
                        };

                        processSurgery.Add(process);
                    }
                }

                var landingPageModel = new LandingPageModel
                {
                    CustomerResults = listCustomer,
                    ShopInformation = shopInfo,
                    HighLightServices = highlightService,
                    ProcessSurgerys = processSurgery,
                    Banners = listBanner,
                    Navigations = listNavigation,
                    ImageHome = listImageHome
                };

                return landingPageModel;
            }
        }

        public static ShopModel ShopToModal(Salon shop)
        {
            var shopModal = new ShopModel();

            shopModal.Name = shop.Name;
            shopModal.Youtube_Url = shop.Youtube_Url;
            shopModal.Logo_Url = shop.Logo_Url;
            shopModal.Zalo_Url = shop.Zalo_Url;
            shopModal.Facebook_Url = shop.Facebook_Url;
            shopModal.Instagram_Url = shop.Instagram_Url;
            shopModal.TickToc_Url = shop.TickToc_Url;
            shopModal.HotLine = shop.HotLine;
            shopModal.Title = shop.Title;
            shopModal.Address = shop.Address;
            shopModal.Slogen = shop.Slogen;
            shopModal.Owner = shop.Owner;
            shopModal.Email = shop.Email;
            shopModal.WorkHour = shop.WorkHour;

            return shopModal;
        }

        public List<CustomerResultModel> GetListCustomerResult(int pageIndex)
        {
            var listCustomer = new List<CustomerResultModel>();

            using (var context = new hkv00123_BeautySalonEntities())
            {
                var result = context.CustomerResults.OrderBy(m => m.IndexImage).Skip(6 * (pageIndex - 1)).Take(6).ToList();

                if (result != null && result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        var customer = new CustomerResultModel
                        {
                            CustomerName = item.CustomerName,
                            Title = item.Title,
                            ImageAfterUrl = item.ImageAfterUrl,
                            ImageBeforeUrl = item.ImageBeforeUrl,
                            ServiceName = item.ServiceName,
                            IndexImage = item.IndexImage,
                            ImageGroupUrl = item.ImageGroupUrl,
                            VideoUrl = item.VideoUrl
                        };

                        listCustomer.Add(customer);
                    }
                }
            }

            return listCustomer;
        }

        public static bool AddFeedback(FeedbackModel data)
        {
            if (data != null)
            {
                var feedback = new Feedback
                {
                    Name = data.Name,
                    Email = data.Email,
                    Note = data.Note,
                    Phone = data.Phone,
                    CreateDate = DateTime.Now
                };

                using (var context = new hkv00123_BeautySalonEntities())
                {
                    context.Feedbacks.Add(feedback);
                    context.SaveChanges();
                }

                string fromAddress = "checksend0000@gmail.com";// Gmail Address from where you send the mail
                //string toAddress = "linhnt@apamedic.vn";
                const string fromPassword = "knxbapvievhohhpq";//Password of your gmail address
                var body = @"Họ tên: " + data.Name + "\nDịch vụ quan tâm: " + data.Note + "\nSố điện thoại: " + data.Phone;
                var subject = "Yêu cầu tư vấn khách hàng " + data.Name;

                SmtpClient smtp = new SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 20000;

                    MailMessage msg = new MailMessage("checksend0000@gmail.com", "linhnt@apamedic.vn", subject, body);
                    msg.From = new MailAddress("checksend0000@gmail.com", "Nagami Admin");
                    //Tham số lần lượt là địa chỉ người gửi, người nhận, tiêu đề và nội dung thư
                    try
                    {
                        smtp.Send(msg);
                    }
                    catch (Exception ex)
                    {
                        using (var context = new hkv00123_BeautySalonEntities())
                        {
                            var log = new GroupPost
                            {
                                Name = "Lỗi gửi mail",
                                Description = ex.Message,
                                CreatedDate = DateTime.Now
                            };

                            context.GroupPosts.Add(log);
                            context.SaveChanges();
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        public static NewsArticlesViewModel GetNewsByCode(string code)
        {
            var newArticles = new NewsArticlesViewModel();
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var shopInfo = ShopToModal(context.Salons.FirstOrDefault());
                newArticles.ShopInformation = shopInfo;

                var listNavigation = new List<NavigationModel>();
                var resultNavigation = context.Navigations.ToList();
                if (resultNavigation != null && resultNavigation.Count > 0)
                {
                    foreach (var item in resultNavigation)
                    {
                        var banner = new NavigationModel
                        {
                            Title = item.Title,
                            Link = item.Link,
                            Parent = item.Parent,
                            Group = item.Group
                        };

                        listNavigation.Add(banner);
                    }
                }

                newArticles.Navigations = listNavigation;

                var listImageHome = new List<ImageHomeModel>();
                var resultImageHome = context.ImageHomes.ToList();
                if (resultImageHome != null && resultImageHome.Count > 0)
                {
                    foreach (var item in resultImageHome)
                    {
                        var image = new ImageHomeModel
                        {
                            Type = item.Type,
                            Url = item.Url,
                            Index = item.Index,
                            Id = item.Id
                        };

                        listImageHome.Add(image);
                    }
                }

                newArticles.ImageHome = listImageHome;

                var newsArticle = context.NewsArticles.FirstOrDefault(m => m.Code == code);
                var newsArticleModel = new NewsArticlesModel
                {
                    Id = newsArticle.Id,
                    Code = newsArticle.Code,
                    Title = newsArticle.Title,
                    Content1 = newsArticle.Content1,
                    Content2 = newsArticle.Content2,
                    Content3 = newsArticle.Content3,
                    Content4 = newsArticle.Content4,
                    Image1_Url = newsArticle.Image1_Url,
                    Image2_Url = newsArticle.Image2_Url,
                    Image3_Url = newsArticle.Image3_Url,
                    Image1_Mobile_Url = newsArticle.Image1_Mobile_Url,
                    Image2_Mobile_Url = newsArticle.Image2_Mobile_Url,
                    Image3_Mobile_Url = newsArticle.Image3_Mobile_Url,
                    Image4_1_Url = newsArticle.Image4_1_Url,
                    Image4_2_Url = newsArticle.Image4_2_Url,
                    Image4_3_Url = newsArticle.Image4_3_Url,
                    Image4_4_Url = newsArticle.Image4_4_Url,
                    Image4_1_Mobile_Url = newsArticle.Image4_1_Mobile_Url,
                    Image4_2_Mobile_Url = newsArticle.Image4_2_Mobile_Url,
                    Image4_3_Mobile_Url = newsArticle.Image4_3_Mobile_Url,
                    Image4_4_Mobile_Url = newsArticle.Image4_4_Mobile_Url,
                };

                newArticles.NewsArticles = newsArticleModel;

                var informationReference = new List<NewInforReferModel>();

                var informationReferenceResult = context.NewsInfoRefers.Where(m => m.News_ID == newsArticle.Id).ToList();
                if (informationReferenceResult != null)
                {
                    foreach (var item in informationReferenceResult)
                    {
                        var temp = new NewInforReferModel
                        {
                            LogoUrl = item.LogoUrl,
                            Title = item.Title,
                            Url = item.Url,
                            Index = item.Index
                        };

                        informationReference.Add(temp);
                    }

                    newArticles.InfoRefer = informationReference;
                }

                var serviceReference = new List<NewServiceReferModel>();

                var serviceReferenceResult = context.NewsServiceRefers.Where(m => m.News_ID == newsArticle.Id).ToList();
                if (serviceReferenceResult != null)
                {
                    foreach (var item in serviceReferenceResult)
                    {
                        var temp = new NewServiceReferModel
                        {
                            LogoUrl = item.LogoUrl,
                            Title = item.Title,
                            Url = item.Url,
                            Index = item.Index
                        };

                        serviceReference.Add(temp);
                    }

                    newArticles.ServiceRefer = serviceReference;
                }
            }

            

            return newArticles;
        }

        public static ServiceDetailsModel GetServiceByCode(string Code)
        {
            var serviceDetails = new ServiceDetailsModel();

            using (var context = new hkv00123_BeautySalonEntities())
            {
                try
                {
                    var shopInfo = ShopToModal(context.Salons.FirstOrDefault());

                    var listNavigation = new List<NavigationModel>();
                    var resultNavigation = context.Navigations.ToList();
                    if (resultNavigation != null && resultNavigation.Count > 0)
                    {
                        foreach (var item in resultNavigation)
                        {
                            var banner = new NavigationModel
                            {
                                Title = item.Title,
                                Link = item.Link,
                                Parent = item.Parent,
                                Group = item.Group
                            };

                            listNavigation.Add(banner);
                        }
                    }

                    serviceDetails.Navigations = listNavigation;

                    var serviceDetail = context.ServiceDetails.FirstOrDefault(m => m.Code == Code);
                    var serviceModel = new ServiceDetailModel
                    {
                        Code = serviceDetail.Code,
                        Content1 = serviceDetail.Content1,
                        Content2 = serviceDetail.Content2,
                        Title = serviceDetail.Title,
                        Title1 = serviceDetail.Title1,
                        Title2 = serviceDetail.Title2,
                        Title3 = serviceDetail.Title3,
                        Title4 = serviceDetail.Title4,
                        Title5 = serviceDetail.Title5,
                        Title6 = serviceDetail.Title6,
                        Title7 = serviceDetail.Title7,
                    };

                    var attachment = context.Attachments.Where(m => m.Service_Id == serviceDetail.Id).ToList();

                    serviceModel.Attachment = new List<AttachmentModel>();

                    if (attachment != null)
                    {
                        foreach (var item in attachment)
                        {
                            serviceModel.Attachment.Add(new AttachmentModel
                            {
                                Type = item.Type,
                                Index_Attactment = item.Index_Attactment,
                                Name = item.Name,
                                Url = item.Url,
                                Service_Id = item.Service_Id
                            });
                        }
                    }

                    var objectAttachment = context.Attachments.Where(m => m.Service_Id == serviceDetail.Id && m.Index_Attactment == 4).OrderBy(m => m.Type).ToList();
                    var objectAttachmentList = new List<AttachmentModel>();

                    if (objectAttachment != null)
                    {
                        foreach (var item in objectAttachment)
                        {
                            objectAttachmentList.Add(new AttachmentModel
                            {
                                Type = item.Type,
                                Index_Attactment = item.Index_Attactment,
                                Name = item.Name,
                                Url = item.Url,
                                Service_Id = item.Service_Id
                            });
                        }
                    }

                    var informationReference = new List<InformationReferenceModel>();

                    var informationReferenceResult = context.InformationReferences.Where(m => m.ServiceDetails_ID == serviceDetail.Id).ToList();
                    if (informationReferenceResult != null)
                    {
                        foreach (var item in informationReferenceResult)
                        {
                            var temp = new InformationReferenceModel
                            {
                                LogoUrl = item.LogoUrl,
                                Title = item.Title,
                                Url = item.Url,
                                Index = item.Index
                            };

                            informationReference.Add(temp);
                        }
                    }

                    var serviceReference = new List<ServiceReferenceModel>();

                    var serviceReferenceResult = context.ServiceReferences.Where(m => m.ServiceDetails_ID == serviceDetail.Id).ToList();
                    if (serviceReferenceResult != null)
                    {
                        foreach (var item in serviceReferenceResult)
                        {
                            var temp = new ServiceReferenceModel
                            {
                                LogoUrl = item.LogoUrl,
                                Title = item.Title,
                                Url = item.Url,
                                Index = item.Index
                            };

                            serviceReference.Add(temp);
                        }
                    }

                    var listImageHome = new List<ImageHomeModel>();
                    var resultImageHome = context.ImageHomes.ToList();
                    if (resultImageHome != null && resultImageHome.Count > 0)
                    {
                        foreach (var item in resultImageHome)
                        {
                            var image = new ImageHomeModel
                            {
                                Type = item.Type,
                                Url = item.Url,
                                Index = item.Index,
                                Id = item.Id
                            };

                            listImageHome.Add(image);
                        }
                    }

                    serviceDetails.ShopInformation = shopInfo;
                    serviceDetails.ServiceDetails = serviceModel;
                    serviceDetails.InformationReferences = informationReference;
                    serviceDetails.ServiceReferences = serviceReference;
                    serviceDetails.ObjectAttachmnets = objectAttachmentList;
                    serviceDetails.ImageHome = listImageHome;
                }
                catch { }

            }

            return serviceDetails;
        }

        public static SearchModel SearchByKey(string key)
        {
            var search = new SearchModel();

            using (var context = new hkv00123_BeautySalonEntities())
            {
                try
                {
                    var shopInfo = ShopToModal(context.Salons.FirstOrDefault());

                    search.ShopInformation = new ShopModel();
                    search.ShopInformation = shopInfo;

                    var listNavigation = new List<NavigationModel>();
                    var resultNavigation = context.Navigations.ToList();
                    if (resultNavigation != null && resultNavigation.Count > 0)
                    {
                        foreach (var item in resultNavigation)
                        {
                            var banner = new NavigationModel
                            {
                                Title = item.Title,
                                Link = item.Link,
                                Parent = item.Parent,
                                Group = item.Group
                            };

                            listNavigation.Add(banner);
                        }
                    }

                    var listImageHome = new List<ImageHomeModel>();
                    var resultImageHome = context.ImageHomes.ToList();
                    if (resultImageHome != null && resultImageHome.Count > 0)
                    {
                        foreach (var item in resultImageHome)
                        {
                            var image = new ImageHomeModel
                            {
                                Type = item.Type,
                                Url = item.Url,
                                Index = item.Index,
                                Id = item.Id
                            };

                            listImageHome.Add(image);
                        }
                    }

                    search.ImageHome = listImageHome;

                    search.Navigations = listNavigation;

                    search.ServiceDetails = new List<ServiceDetailModel>();

                    var listServices = context.ServiceDetails.Where(m => m.Title.Contains(key) || m.SearchKey.Contains(key)).ToList();

                    if (listServices != null && listServices.Count > 0)
                    {
                        foreach (var item in listServices)
                        {
                            var attachment = context.Attachments.Where(m => m.Service_Id == item.Id).ToList();

                            var attachmentModel = new List<AttachmentModel>();

                            if (attachment != null)
                            {
                                foreach (var itemAttachment in attachment)
                                {
                                    attachmentModel.Add(new AttachmentModel
                                    {
                                        Type = itemAttachment.Type,
                                        Index_Attactment = itemAttachment.Index_Attactment,
                                        Name = itemAttachment.Name,
                                        Url = itemAttachment.Url,
                                        Service_Id = itemAttachment.Service_Id
                                    });
                                }
                            }

                            var serviceModel = new ServiceDetailModel
                            {
                                Code = item.Code,
                                Content1 = item.Content1,
                                Content2 = item.Content2,
                                Title = item.Title,
                                Title1 = item.Title1,
                                Title2 = item.Title2,
                                Title3 = item.Title3,
                                Title4 = item.Title4,
                                Title5 = item.Title5,
                                Title6 = item.Title6,
                                Title7 = item.Title7,
                                SearchKey = item.SearchKey,
                                Attachment = attachmentModel

                            };

                            search.ServiceDetails.Add(serviceModel);
                        }
                    }
                }
                catch { }
            }

            return search;
        }

        public static bool UpdateShopInformation(ShopModel shopModel)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var shop = context.Salons.FirstOrDefault(m => m.Id == shopModel.Id);

                if (shop != null)
                {
                    shop.Address = shopModel.Address;
                    shop.Facebook_Url = shopModel.Facebook_Url;
                    shop.HotLine = shopModel.HotLine;
                    shop.Instagram_Url = shopModel.Instagram_Url;
                    shop.TickToc_Url = shopModel.TickToc_Url;
                    shop.Zalo_Url = shopModel.Zalo_Url;
                    shop.WorkHour = shopModel.WorkHour;
                }

                context.Salons.Attach(shop);
                context.SaveChanges();

                return true;
            }
        }
    }
}
