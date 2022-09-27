using Dress.DAL.Structure;
using Dress_BLL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Globalization;
using System.Data.Entity.Core.Objects;

namespace Dress_BLL.Function
{
    public class AccountService
    {
        public static List<UserModel> GetAllUser()
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                return ToListModal(context.Users.ToList());
            }
        }

        public static ShopModel GetShopInformation()
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var ShopEntity = context.Salons.FirstOrDefault();
                return new ShopModel
                {
                    Address = ShopEntity.Address,
                    Email = ShopEntity.Email,
                    Youtube_Url = ShopEntity.Youtube_Url,
                    Facebook_Url = ShopEntity.Facebook_Url,
                    Zalo_Url = ShopEntity.Zalo_Url,
                    Instagram_Url = ShopEntity.Instagram_Url,
                    TickToc_Url = ShopEntity.TickToc_Url,
                    HotLine = ShopEntity.HotLine,
                    WorkHour = ShopEntity.WorkHour,
                    Slogen = ShopEntity.Slogen,
                    Name = ShopEntity.Name,
                    Title = ShopEntity.Title,
                    Owner = ShopEntity.Owner,
                    Logo_Url = ShopEntity.Logo_Url
                };
            }
        }

        public static UserModel GetUserById(int id)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var userDetails = context.Users.FirstOrDefault(m => m.Id == id);

                return new UserModel
                {
                    Id = id,
                    FullName = userDetails.FullName,
                    UserName = userDetails.UserName,
                    Address = userDetails.Address,
                    Phone = userDetails.Phone,
                    Password = userDetails.Password,
                    Email = userDetails.Email,
                    Role = userDetails.Role
                };
            }
        }

        public static bool AddOrUpdateUser(UserModel userModel)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                if (userModel.Id != 0)
                {
                    var user = context.Users.FirstOrDefault(m => m.Id == userModel.Id);
                    user.UserName = userModel.UserName;
                    user.Password = userModel.Password;
                    user.Role = userModel.Role;
                    user.FullName = userModel.FullName;
                    user.Email = userModel.Email;
                    user.Phone = userModel.Phone;

                    context.Users.AddOrUpdate(user);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    var userAdd = new User();
                    userAdd.Role = userModel.Role;
                    userAdd.FullName = userModel.FullName;
                    userAdd.UserName = userModel.UserName;
                    userAdd.Password = userModel.Password;
                    userAdd.Email = userModel.Email;
                    userAdd.Phone = userModel.Phone;

                    context.Users.Add(userAdd);
                    context.SaveChanges();
                    return true;
                }
            }
        }

        public static bool DeleteUserById(int id)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var userDetails = context.Users.FirstOrDefault(m => m.Id == id);
                context.Users.Remove(userDetails);
                context.SaveChanges();
                return true;
            }
        }

        public static UserModel Login(string username, string password)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                return ToModal(context.Users.SingleOrDefault(m => m.UserName == username && m.Password == password));
            }
        }

        public static List<UserModel> ToListModal(List<User> user)
        {
            var listusermodal = new List<UserModel>();

            foreach (var item in user)
            {
                var usermodal = new UserModel();
                usermodal.Id = item.Id;
                usermodal.Image = item.Avatar;
                usermodal.Phone = item.Phone;
                usermodal.Role = item.Role;
                usermodal.Phone = item.Phone;
                usermodal.UserName = item.UserName;
                usermodal.DateofBirth = item.BirthDay;
                usermodal.FullName = item.FullName;
                usermodal.Gender = item.Gender;

                listusermodal.Add(usermodal);
            }

            return listusermodal;
        }

        public static UserModel ToModal(User user)
        {
            if (user != null)
            {
                var usermodal = new UserModel();
                usermodal.Id = user.Id;
                usermodal.Image = user.Avatar;
                usermodal.Phone = user.Phone;
                usermodal.Role = user.Role;
                usermodal.Phone = user.Phone;
                usermodal.UserName = user.UserName;
                usermodal.DateofBirth = user.BirthDay;
                usermodal.FullName = user.FullName;
                usermodal.Gender = user.Gender;

                return usermodal;
            }

            return null;
        }

        public static List<FeedbackModel> GetAllFeedback()
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var dt = DateTime.Now.AddDays(-30);
                var feedBack = context.Feedbacks.Where(m => m.CreateDate >= dt).OrderByDescending(m => m.CreateDate).ToList();
                var result = new List<FeedbackModel>();
                foreach (var item in feedBack)
                {
                    result.Add(new FeedbackModel
                    {
                        Email = item.Email,
                        Name = item.Name,
                        Note = item.Note,
                        Phone = item.Phone,
                        CreateDate = item.CreateDate
                    });
                }

                return result;
            }
        }

        public static List<NewsArticlesModel> GetAllNewsArticles()
        {
            var listNewsArticles = new List<NewsArticlesModel>();
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var newsArticlesEtt = context.NewsArticles.ToList();
                foreach (var item in newsArticlesEtt)
                {
                    var model = new NewsArticlesModel
                    {
                        Code = item.Code,
                        Title = item.Title,
                    };

                    listNewsArticles.Add(model);
                }
            }

            return listNewsArticles;
        }

        public static List<ServiceDetailsModel> GetAllServices()
        {
            var listServiceDetails = new List<ServiceDetailsModel>();

            using (var context = new hkv00123_BeautySalonEntities())
            {
                var serviceDetailsEtt = context.ServiceDetails.ToList();
                foreach (var serviceDetail in serviceDetailsEtt)
                {
                    var serviceDetails = new ServiceDetailsModel();
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
                        SearchKey = serviceDetail.SearchKey
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

                    var objectAttachment = context.Attachments.Where(m => m.Service_Id == serviceDetail.Id && m.Index_Attactment == 4).ToList();
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
                                ServiceDetails_ID = serviceDetail.Id,
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
                                ServiceDetails_ID = serviceDetail.Id,
                                Title = item.Title,
                                Url = item.Url,
                                Index = item.Index

                            };

                            serviceReference.Add(temp);
                        }
                    }

                    serviceDetails.ServiceDetails = serviceModel;
                    serviceDetails.InformationReferences = informationReference;
                    serviceDetails.ServiceReferences = serviceReference;
                    serviceDetails.ObjectAttachmnets = objectAttachmentList;

                    listServiceDetails.Add(serviceDetails);
                }
            }

            return listServiceDetails;
        }

        public static NewsArticlesModel GetNewsByCode(string code)
        {
            var serviceDetails = new NewsArticlesModel();

            using (var context = new hkv00123_BeautySalonEntities())
            {
                var newsArticle = context.NewsArticles.FirstOrDefault(m => m.Code == code);

                var serviceModel = new NewsArticlesModel
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

                serviceDetails = serviceModel;

                var informationReference = new List<NewInforReferModel>();

                var informationReferenceResult = context.NewsInfoRefers.Where(m => m.News_ID == newsArticle.Id).ToList();
                if (informationReferenceResult != null)
                {
                    foreach (var item in informationReferenceResult)
                    {
                        var temp = new NewInforReferModel
                        {
                            Id = item.Id,
                            LogoUrl = item.LogoUrl,
                            Title = item.Title,
                            Url = item.Url,
                            News_ID = newsArticle.Id,
                            Index = item.Index
                        };

                        informationReference.Add(temp);
                    }

                    serviceDetails.InformationReferences = informationReference;
                }

                var serviceReference = new List<NewServiceReferModel>();

                var serviceReferenceResult = context.NewsServiceRefers.Where(m => m.News_ID == newsArticle.Id).ToList();
                if (serviceReferenceResult != null)
                {
                    foreach (var item in serviceReferenceResult)
                    {
                        var temp = new NewServiceReferModel
                        {
                            Id = item.Id,
                            LogoUrl = item.LogoUrl,
                            Title = item.Title,
                            Url = item.Url,
                            News_ID = newsArticle.Id,
                            Index = item.Index
                        };

                        serviceReference.Add(temp);
                    }

                    serviceDetails.ServiceReferences = serviceReference;
                }
            }

            return serviceDetails;
        }

        public static ServiceDetailsModel GetServiceByCode(string code)
        {
            var serviceDetails = new ServiceDetailsModel();

            using (var context = new hkv00123_BeautySalonEntities())
            {
                var serviceDetail = context.ServiceDetails.FirstOrDefault(m => m.Code == code);

                var serviceModel = new ServiceDetailModel
                {
                    Id = serviceDetail.Id,
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
                    SearchKey = serviceDetail.SearchKey,
                    NumberOfObjects = serviceDetail.NumberOfObjects.Value
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

                var objectAttachment = context.Attachments.Where(m => m.Service_Id == serviceDetail.Id && m.Index_Attactment == 4).ToList();
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
                            Id = item.Id,
                            LogoUrl = item.LogoUrl,
                            Title = item.Title,
                            Url = item.Url,
                            ServiceDetails_ID = serviceDetail.Id,
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
                            Id = item.Id,
                            LogoUrl = item.LogoUrl,
                            Title = item.Title,
                            Url = item.Url,
                            ServiceDetails_ID = serviceDetail.Id,
                            Index = item.Index
                        };

                        serviceReference.Add(temp);
                    }
                }

                serviceDetails.ServiceDetails = serviceModel;
                serviceDetails.InformationReferences = informationReference;
                serviceDetails.ServiceReferences = serviceReference;
                serviceDetails.ObjectAttachmnets = objectAttachmentList;
            }

            return serviceDetails;
        }

        public static bool AddOrUpdateNews(NewsArticlesModel model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                try
                {
                    var serviceEntity = context.NewsArticles.FirstOrDefault(m => m.Id == model.Id);

                    if (serviceEntity != null)
                    {
                        serviceEntity.Code = model.Code;
                        serviceEntity.Title = model.Title;
                        serviceEntity.Content1 = model.Content1;
                        serviceEntity.Content2 = model.Content2;
                        serviceEntity.Content3 = model.Content3;
                        serviceEntity.Content4 = model.Content4;

                        context.NewsArticles.AddOrUpdate(serviceEntity);
                        context.SaveChanges();

                        foreach (var item in model.ServiceReferences)
                        {
                            var serviceReferencesEntity = context.NewsServiceRefers.FirstOrDefault(m => m.Id == item.Id);
                            serviceReferencesEntity.Title = item.Title;
                            serviceReferencesEntity.Url = item.Url;
                            serviceReferencesEntity.Index = item.Index;

                            context.NewsServiceRefers.Attach(serviceReferencesEntity);
                            context.Entry(serviceReferencesEntity).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                        }

                        foreach (var item in model.InformationReferences)
                        {
                            var informationReferencesEntity = context.NewsInfoRefers.FirstOrDefault(m => m.Id == item.Id);
                            informationReferencesEntity.Title = item.Title;
                            informationReferencesEntity.Url = item.Url;
                            informationReferencesEntity.Index = item.Index;

                            context.NewsInfoRefers.Attach(informationReferencesEntity);
                            context.Entry(informationReferencesEntity).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        var serviceEntityInsert = new NewsArticle
                        {
                            Code = model.Code,
                            Title = model.Title,
                            Content1 = model.Content1,
                            Content2 = model.Content2,
                            Content3 = model.Content3,
                            Content4 = model.Content4,
                        };

                        context.NewsArticles.Add(serviceEntityInsert);
                        context.SaveChanges();

                        foreach (var item in model.ServiceReferences)
                        {
                            var ServiceReferences = new NewsServiceRefer
                            {
                                News_ID = serviceEntityInsert.Id,
                                Title = item.Title,
                                Url = item.Url,
                                LogoUrl = item.LogoUrl,
                                Index = item.Index
                            };

                            context.NewsServiceRefers.Add(ServiceReferences);
                        }

                        foreach (var item in model.InformationReferences)
                        {
                            var informationReferences = new NewsInfoRefer
                            {
                                News_ID = serviceEntityInsert.Id,
                                Title = item.Title,
                                Url = item.Url,
                                LogoUrl = item.LogoUrl,
                                Index = item.Index
                            };

                            context.NewsInfoRefers.Add(informationReferences);
                        }

                        context.SaveChanges();
                    }

                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public static bool AddOrUpdateServices(ServiceDetailUpdateModel service)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                try
                {
                    var serviceEntity = context.ServiceDetails.FirstOrDefault(m => m.Id == service.Id);

                    if (serviceEntity != null)
                    {
                        serviceEntity.Code = service.Code;
                        serviceEntity.Content1 = service.Content1;
                        serviceEntity.Content2 = service.Content2;
                        serviceEntity.Title = service.Title;
                        serviceEntity.Title1 = service.Title1;
                        serviceEntity.Title2 = service.Title2;
                        serviceEntity.Title3 = service.Title3;
                        serviceEntity.Title4 = service.Title4;
                        serviceEntity.Title5 = service.Title5;
                        serviceEntity.Title6 = service.Title6;
                        serviceEntity.Title7 = service.Title7;
                        serviceEntity.SearchKey = service.SearchKey;
                        serviceEntity.NumberOfObjects = service.NumberOfObjects;

                        context.ServiceDetails.AddOrUpdate(serviceEntity);
                        context.SaveChanges();

                        foreach (var item in service.ServiceReferences)
                        {
                            var serviceReferencesEntity = context.ServiceReferences.FirstOrDefault(m => m.Id == item.Id);
                            serviceReferencesEntity.Title = item.Title;
                            serviceReferencesEntity.Url = item.Url;
                            serviceReferencesEntity.Index = item.Index;

                            context.ServiceReferences.Attach(serviceReferencesEntity);
                            context.Entry(serviceReferencesEntity).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                        }

                        foreach (var item in service.InformationReferences)
                        {
                            var informationReferencesEntity = context.InformationReferences.FirstOrDefault(m => m.Id == item.Id);
                            informationReferencesEntity.Title = item.Title;
                            informationReferencesEntity.Url = item.Url;
                            informationReferencesEntity.Index = item.Index;

                            context.InformationReferences.Attach(informationReferencesEntity);
                            context.Entry(informationReferencesEntity).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                        }

                        return true;
                    }
                    else
                    {
                        var serviceEntityInsert = new ServiceDetail
                        {
                            Code = service.Code,
                            Content1 = service.Content1,
                            Content2 = service.Content2,
                            Title = service.Title,
                            Title1 = service.Title1,
                            Title2 = service.Title2,
                            Title3 = service.Title3,
                            Title4 = service.Title4,
                            Title5 = service.Title5,
                            Title6 = service.Title6,
                            Title7 = service.Title7,
                            SearchKey = service.SearchKey,
                            NumberOfObjects = service.NumberOfObjects
                        };

                        context.ServiceDetails.Add(serviceEntityInsert);
                        context.SaveChanges();

                        foreach (var item in service.ServiceReferences)
                        {
                            var ServiceReferences = new ServiceReference
                            {
                                ServiceDetails_ID = serviceEntityInsert.Id,
                                Title = item.Title,
                                Url = item.Url,
                                LogoUrl = item.LogoUrl,
                                Index = item.Index
                            };

                            context.ServiceReferences.Add(ServiceReferences);
                        }

                        foreach (var item in service.InformationReferences)
                        {
                            var informationReferences = new InformationReference
                            {
                                ServiceDetails_ID = serviceEntityInsert.Id,
                                Title = item.Title,
                                Url = item.Url,
                                LogoUrl = item.LogoUrl,
                                Index = item.Index
                            };

                            context.InformationReferences.Add(informationReferences);
                        }

                        context.SaveChanges();
                    }

                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public static bool UpdateShopInformation(ShopModel shop)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var shopInfomation = context.Salons.FirstOrDefault(m => m.Id == shop.Id);
                if (shopInfomation != null)
                {
                    shopInfomation.Address = shop.Address;
                    shopInfomation.Zalo_Url = shop.Zalo_Url;
                    shopInfomation.Youtube_Url = shop.Youtube_Url;
                    shopInfomation.Facebook_Url = shop.Facebook_Url;
                    shopInfomation.Instagram_Url = shop.Instagram_Url;
                    shopInfomation.TickToc_Url = shop.TickToc_Url;
                    shopInfomation.HotLine = shop.HotLine;
                    shopInfomation.Title = shop.Title;
                    shopInfomation.WorkHour = shop.WorkHour;
                    shopInfomation.Slogen = shop.Slogen;
                    shopInfomation.Logo_Url = shop.Logo_Url;
                    shopInfomation.Email = shop.Email;
                    shopInfomation.Owner = shop.Owner;

                    context.Salons.AddOrUpdate(shopInfomation);
                    context.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public static bool UpdateHighlightService(HighlightServiceModel model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var update = context.HighlightServices.FirstOrDefault(m => m.Id == model.Id);
                if (update != null)
                {
                    update.ImageUrl = model.ImageUrl;
                    update.Description = model.Description;

                    context.HighlightServices.AddOrUpdate(update);
                    context.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public static bool UpdateImageHome(ImageHomeModel model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var update = context.ImageHomes.FirstOrDefault(m => m.Id == model.Id);
                if (update != null)
                {
                    update.Url = model.Url;

                    context.ImageHomes.AddOrUpdate(update);
                    context.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public static List<ImageHomeModel> GetListImageHome()
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var imageHome = context.ImageHomes.ToList();
                var imageHomeList = new List<ImageHomeModel>();

                foreach (var item in imageHome)
                {
                    var model = new ImageHomeModel
                    {
                        Id = item.Id,
                        Index = item.Index,
                        Type = item.Type,
                        Title = item.Title,
                        Url = item.Url,
                    };

                    imageHomeList.Add(model);
                }

                return imageHomeList;
            }
        }

        public static List<HighlightServiceModel> GetHighlightService()
        {
            var highlightService = new List<HighlightServiceModel>();

            using (var context = new hkv00123_BeautySalonEntities())
            {
                var highlightResult = context.HighlightServices.ToList();

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
            }

            return highlightService;
        }

        public static List<NavigationModel> GetAllNavigation()
        {
            var navigationList = new List<NavigationModel>();

            using (var context = new hkv00123_BeautySalonEntities())
            {
                var navigationResult = context.Navigations.ToList();

                if (navigationResult != null && navigationResult.Count > 0)
                {
                    foreach (var item in navigationResult)
                    {
                        var navigationModel = new NavigationModel
                        {
                            Id = item.Id,
                            Title = item.Title,
                            Link = item.Link,
                            Group = item.Group,
                            Parent = item.Parent
                        };

                        navigationList.Add(navigationModel);
                    }
                }
            }

            return navigationList;
        }

        public static bool AddOrUpdateNavigation(NavigationModel model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var update = context.Navigations.FirstOrDefault(m => m.Id == model.Id);
                if (update != null)
                {
                    update.Title = model.Title;
                    update.Link = model.Link;

                    context.Navigations.AddOrUpdate(update);
                    context.SaveChanges();

                    return true;
                }
                else
                {
                    var navigationAdd = new Navigation();
                    navigationAdd.Title = model.Title;
                    navigationAdd.Link = model.Link;
                    navigationAdd.Group = model.Group;

                    context.Navigations.Add(navigationAdd);
                    context.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public static NavigationModel GetNavigationById(int id)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var navigationDetails = context.Navigations.FirstOrDefault(m => m.Id == id);

                return new NavigationModel
                {
                    Id = id,
                    Link = navigationDetails.Link,
                    Group = navigationDetails.Group,
                    Title = navigationDetails.Title,
                    Parent = navigationDetails.Parent
                };
            }
        }

        public static bool DeleteNavigationById(int id)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var navigationDetails = context.Navigations.FirstOrDefault(m => m.Id == id);
                context.Navigations.Remove(navigationDetails);
                context.SaveChanges();
                return true;
            }
        }

        public static bool DeleteServiceById(int id)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var service = context.ServiceDetails.FirstOrDefault(m => m.Id == id);
                context.ServiceDetails.Remove(service);
                context.SaveChanges();
                return true;
            }
        }

        public static bool DeleteNewsById(int id)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var service = context.NewsArticles.FirstOrDefault(m => m.Id == id);
                context.NewsArticles.Remove(service);
                context.SaveChanges();
                return true;
            }
        }

        public static List<BannerModel> GetBanners()
        {
            var bannerList = new List<BannerModel>();

            using (var context = new hkv00123_BeautySalonEntities())
            {
                var bannerResult = context.Banners.ToList();

                if (bannerResult != null && bannerResult.Count > 0)
                {
                    foreach (var item in bannerResult)
                    {
                        var banner = new BannerModel
                        {
                            Id = item.Id,
                            Url = item.Url,
                            Url_Mobile = item.Url_Mobile,
                            Index = item.Index
                        };

                        bannerList.Add(banner);
                    }
                }
            }

            return bannerList;
        }

        public static bool UpdateBanner(BannerModel model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var update = context.Banners.FirstOrDefault(m => m.Id == model.Id);
                if (update != null)
                {
                    if (!string.IsNullOrEmpty(model.Url))
                    {
                        update.Url = model.Url;
                    }

                    if (!string.IsNullOrEmpty(model.Url_Mobile))
                    {
                        update.Url_Mobile = model.Url_Mobile;
                    }

                    context.Banners.AddOrUpdate(update);
                    context.SaveChanges();

                    return true;
                }
            }

            return false;
        }


        public static List<ProcessSurgeryModel> GetProcessSurgery()
        {
            var processSurgeries = new List<ProcessSurgeryModel>();

            using (var context = new hkv00123_BeautySalonEntities())
            {
                var processSurgeryResult = context.ProcessSurgeries.ToList();

                if (processSurgeryResult != null && processSurgeryResult.Count > 0)
                {
                    foreach (var item in processSurgeryResult)
                    {
                        var highlight = new ProcessSurgeryModel
                        {
                            Id = item.Id,
                            Key = item.Key,
                            Title = item.Title,
                            Content = item.Content,
                            Content_Show = item.Content_Show,
                            ImageUrl = item.ImageUrl,
                            Index = item.Index
                        };

                        processSurgeries.Add(highlight);
                    }
                }
            }

            return processSurgeries;
        }

        public static bool UpdateProcessSurgery(List<ProcessSurgeryModel> model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                foreach (var item in model)
                {
                    var update = context.ProcessSurgeries.FirstOrDefault(m => m.Id == item.Id);
                    if (update != null)
                    {
                        update.Title = item.Title;
                        update.Content = item.Content;
                        update.Content_Show = item.Content_Show;

                        context.ProcessSurgeries.AddOrUpdate(update);
                        context.SaveChanges();
                    }
                }

                return true;
            }

            return false;
        }

        public static List<CustomerResultModel> GetCustomerResults()
        {
            var customerResultList = new List<CustomerResultModel>();

            using (var context = new hkv00123_BeautySalonEntities())
            {
                var customerResultResult = context.CustomerResults.ToList();

                if (customerResultResult != null && customerResultResult.Count > 0)
                {
                    foreach (var item in customerResultResult)
                    {
                        var customerResult = new CustomerResultModel
                        {
                            Id = item.Id,
                            Title = item.Title,
                            VideoUrl = item.VideoUrl,
                            CustomerName = item.CustomerName,
                            ImageGroupUrl = item.ImageGroupUrl,
                            ImageBeforeUrl = item.ImageBeforeUrl,
                            ImageAfterUrl = item.ImageAfterUrl
                        };

                        customerResultList.Add(customerResult);
                    }
                }
            }

            return customerResultList;
        }

        public static bool UpdateCustomerResults(CustomerResultModel model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var update = context.CustomerResults.FirstOrDefault(m => m.Id == model.Id);
                if (update != null)
                {
                    update.VideoUrl = model.VideoUrl;
                    update.ImageGroupUrl = model.ImageGroupUrl;
                    update.ImageBeforeUrl = model.ImageBeforeUrl;
                    update.ImageAfterUrl = model.ImageAfterUrl;

                    context.CustomerResults.AddOrUpdate(update);
                    context.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public static bool UpdateListCustomerResults(List<LinkCustomerViewModel> model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                foreach (var item in model)
                {
                    var update = context.CustomerResults.FirstOrDefault(m => m.Id == item.Id);

                    update.VideoUrl = item.VideoUrl;

                    context.CustomerResults.AddOrUpdate(update);
                    context.SaveChanges();
                }

                return true;
            }

            return false;
        }

        public static bool UploadPhoto(UploadImageServiceModel model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var attachment = context.Attachments.FirstOrDefault(m => m.Service_Id == model.ServiceId && m.Index_Attactment == model.IndexAttachment && m.Type == model.Type);
                if (attachment != null)
                {
                    attachment.Url = model.URL;
                    context.Attachments.AddOrUpdate(attachment);
                    context.SaveChanges();
                }
                else
                {
                    var attachmentInsert = new Attachment();
                    attachmentInsert.Index_Attactment = model.IndexAttachment;
                    attachmentInsert.Service_Id = model.ServiceId;
                    attachmentInsert.Type = model.Type;
                    attachmentInsert.Url = model.URL;

                    context.Attachments.Add(attachmentInsert);
                    context.SaveChanges();
                }

                return true;
            }
        }

        public static bool DeleteObject(UploadImageServiceModel model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var attachment = context.Attachments.FirstOrDefault(m => m.Service_Id == model.ServiceId && m.Index_Attactment == model.IndexAttachment && m.Type == model.Type);
                if (attachment != null)
                {
                    context.Attachments.Remove(attachment);
                    context.SaveChanges();
                }

                return true;
            }
        }

        public static bool UploadServiceReference(UploadImageServiceModel model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                if (model.Type == "Service")
                {
                    var attachment = context.ServiceReferences.FirstOrDefault(m => m.ServiceDetails_ID == model.ServiceId && m.Index == model.IndexAttachment);

                    if (attachment != null)
                    {
                        attachment.LogoUrl = model.URL;
                        context.ServiceReferences.AddOrUpdate(attachment);
                        context.SaveChanges();
                    }
                }
                else
                {
                    var attachment = context.InformationReferences.FirstOrDefault(m => m.ServiceDetails_ID == model.ServiceId && m.Index == model.IndexAttachment);

                    if (attachment != null)
                    {
                        attachment.LogoUrl = model.URL;
                        context.InformationReferences.AddOrUpdate(attachment);
                        context.SaveChanges();
                    }
                }

                return true;
            }
        }

        public static bool UploadNewsReference(UploadImageServiceModel model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                if (model.Type == "Service")
                {
                    var attachment = context.NewsServiceRefers.FirstOrDefault(m => m.News_ID == model.ServiceId && m.Index == model.IndexAttachment);

                    if (attachment != null)
                    {
                        attachment.LogoUrl = model.URL;
                        context.NewsServiceRefers.AddOrUpdate(attachment);
                        context.SaveChanges();
                    }
                }
                else
                {
                    var attachment = context.NewsInfoRefers.FirstOrDefault(m => m.News_ID == model.ServiceId && m.Index == model.IndexAttachment);

                    if (attachment != null)
                    {
                        attachment.LogoUrl = model.URL;
                        context.NewsInfoRefers.AddOrUpdate(attachment);
                        context.SaveChanges();
                    }
                }

                return true;
            }
        }

        public static bool UploadNews(UploadImageServiceModel model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var news = context.NewsArticles.FirstOrDefault(m => m.Id == model.ServiceId);
                if (news != null)
                {
                    switch (model.Type)
                    {
                        case "Banner1":
                            news.Image1_Url = model.URL;
                            break;
                        case "Banner2":
                            news.Image2_Url = model.URL;
                            break;
                        case "Banner3":
                            news.Image3_Url = model.URL;
                            break;
                        case "Banner1_Mobile":
                            news.Image1_Mobile_Url = model.URL;
                            break;
                        case "Banner2_Mobile":
                            news.Image2_Mobile_Url = model.URL;
                            break;
                        case "Banner3_Mobile":
                            news.Image3_Mobile_Url = model.URL;
                            break;
                        case "Footer1":
                            news.Image4_1_Url = model.URL;
                            break;
                        case "Footer2":
                            news.Image4_2_Url = model.URL;
                            break;
                        case "Footer3":
                            news.Image4_3_Url = model.URL;
                            break;
                        case "Footer4":
                            news.Image4_4_Url = model.URL;
                            break;
                    }

                    context.NewsArticles.AddOrUpdate(news);
                    context.SaveChanges();
                }
                
                return true;
            }
        }

        public static bool UploadPhotoProcess(UploadImageHomeModel model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var attachment = context.ProcessSurgeries.FirstOrDefault(m => m.Id == model.Id);
                if (attachment != null)
                {
                    attachment.ImageUrl = model.Url;
                    context.ProcessSurgeries.AddOrUpdate(attachment);
                    context.SaveChanges();
                }

                return true;
            }
        }

        public static bool UploadHighlightService(UploadImageHomeModel model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var attachment = context.HighlightServices.FirstOrDefault(m => m.Id == model.Id);
                if (attachment != null)
                {
                    attachment.ImageUrl = model.Url;
                    context.HighlightServices.AddOrUpdate(attachment);
                    context.SaveChanges();
                }

                return true;
            }
        }

        public static bool UploadImageCustomerResults(CustomerResultModel model)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var customer = context.CustomerResults.FirstOrDefault(m => m.Id == model.Id);
                if (customer != null)
                {
                    if (!string.IsNullOrEmpty(model.ImageGroupUrl))
                    {
                        customer.ImageGroupUrl = model.ImageGroupUrl;
                    }
                    if (!string.IsNullOrEmpty(model.ImageAfterUrl))
                    {
                        customer.ImageAfterUrl = model.ImageAfterUrl;
                    }
                    if (!string.IsNullOrEmpty(model.ImageBeforeUrl))
                    {
                        customer.ImageBeforeUrl = model.ImageBeforeUrl;
                    }
                    if (!string.IsNullOrEmpty(model.CustomerName))
                    {
                        customer.CustomerName = model.CustomerName;
                    }

                    context.CustomerResults.AddOrUpdate(customer);
                    context.SaveChanges();
                }

                return true;
            }
        }

        public static List<FeedbackModel> FilterFeedback(FeedbackFilterModel filter)
        {
            using (var context = new hkv00123_BeautySalonEntities())
            {
                var feedbackList = new List<FeedbackModel>();
                var feedbackResult = new List<Feedback>();
                var dt = DateTime.Now.AddDays(-7);

                if (!string.IsNullOrEmpty(filter.Phone))
                {
                    feedbackResult = context.Feedbacks.Where(m => m.Phone.Contains(filter.Phone)).OrderByDescending(m => m.CreateDate).ToList();
                }
                else
                {
                    if (!string.IsNullOrEmpty(filter.ServiceKey))
                    {
                        feedbackResult = context.Feedbacks.Where(m => m.Note.Contains(filter.ServiceKey)).OrderByDescending(m => m.CreateDate).ToList();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(filter.Date))
                        {
                            var filterDate = DateTime.ParseExact(filter.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            feedbackResult = context.Feedbacks.Where(m => EntityFunctions.TruncateTime(m.CreateDate) == filterDate.Date).OrderByDescending(m => m.CreateDate).ToList();
                        }
                        else
                        {
                            feedbackResult = context.Feedbacks.Where(m => m.CreateDate > dt).OrderByDescending(m => m.CreateDate).ToList();
                        }
                    }
                }

                if (feedbackResult != null)
                {
                    foreach (var item in feedbackResult)
                    {
                        var feedbackModel = new FeedbackModel
                        {
                            Email = item.Email,
                            Name = item.Name,
                            Note = item.Note,
                            Phone = item.Phone,
                            CreateDate = item.CreateDate
                        };

                        feedbackList.Add(feedbackModel);
                    }
                }

                return feedbackList;
            }
        }
    }
}
