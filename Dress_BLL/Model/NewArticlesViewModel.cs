using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dress_BLL.Model
{
    public class NewsArticlesViewModel
    {
        public ShopModel ShopInformation { get; set; }

        public NewsArticlesModel NewsArticles { get; set; }

        public List<NavigationModel> Navigations { get; set; }

        public List<ImageHomeModel> ImageHome { get; set; }

        public List<NewInforReferModel> InfoRefer { get; set; }

        public List<NewServiceReferModel> ServiceRefer { get; set; }
    }
}
