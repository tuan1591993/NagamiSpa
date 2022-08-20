using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dress_BLL.Model
{
    public class ServiceDetailsModel
    {
        public ShopModel ShopInformation { get; set; }

        public ServiceDetailModel ServiceDetails { get; set; }
        
        public List<ServiceReferenceModel> ServiceReferences { get; set; }

        public List<InformationReferenceModel> InformationReferences { get; set; }

        public List<AttachmentModel> ObjectAttachmnets { get; set; }

        public List<NavigationModel> Navigations { get; set; }

        public List<ImageHomeModel> ImageHome { get; set; }
    }
}
