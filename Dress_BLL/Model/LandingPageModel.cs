using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dress_BLL.Model
{
    public class LandingPageModel
    {
        public ShopModel ShopInformation { get; set; }

        public List<NavigationModel> Navigations  { get; set; }

        public List<BannerModel> Banners { get; set; }

        public List<CustomerResultModel> CustomerResults { get; set; }

        public List<HighlightServiceModel> HighLightServices { get; set; }

        public List<ProcessSurgeryModel> ProcessSurgerys { get; set; }

        public List<ImageHomeModel> ImageHome { get; set; }
    }
}
