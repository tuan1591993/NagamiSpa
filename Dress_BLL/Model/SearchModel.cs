using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dress_BLL.Model
{
    public class SearchModel
    {
        public ShopModel ShopInformation { get; set; }

        public List<NavigationModel> Navigations { get; set; }

        public List<ImageHomeModel> ImageHome { get; set; }

        public List<ServiceDetailModel> ServiceDetails { get; set; }

        public string Key { get; set; }
    }
}
