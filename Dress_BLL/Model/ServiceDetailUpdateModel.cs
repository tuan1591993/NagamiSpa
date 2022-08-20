using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dress_BLL.Model
{
    public class ServiceDetailUpdateModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Title1 { get; set; }
        [AllowHtml]
        public string Content1 { get; set; }
        public string Title2 { get; set; }
        [AllowHtml]
        public string Content2 { get; set; }
        public string Title3 { get; set; }
        public string Title4 { get; set; }
        public string Title5 { get; set; }
        public string Title6 { get; set; }
        public string Title7 { get; set; }
        public string SearchKey { get; set; }

        public int NumberOfObjects { get; set; }

        public List<ServiceReferenceUpdateModel> ServiceReferences { get; set; }

        public List<InformationReferenceUpdateModel> InformationReferences { get; set; }

        public List<AttachmentModel> Attachment { get; set; }
    }
}
