using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dress_BLL.Model
{
    public class NewsArticlesModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Content1 { get; set; }
        public string Image1_Url { get; set; }
        [AllowHtml]
        public string Content2 { get; set; }
        public string Image2_Url { get; set; }
        [AllowHtml]
        public string Content3 { get; set; }
        public string Image3_Url { get; set; }
        [AllowHtml]
        public string Content4 { get; set; }
        public string Image4_1_Url { get; set; }
        public string Image4_2_Url { get; set; }
        public string Image4_3_Url { get; set; }
        public string Image4_4_Url { get; set; }
        public string Image1_Mobile_Url { get; set; }
        public string Image2_Mobile_Url { get; set; }
        public string Image3_Mobile_Url { get; set; }
        public string Image4_1_Mobile_Url { get; set; }
        public string Image4_2_Mobile_Url { get; set; }
        public string Image4_3_Mobile_Url { get; set; }
        public string Image4_4_Mobile_Url { get; set; }

        public List<NewServiceReferModel> ServiceReferences { get; set; }

        public List<NewInforReferModel> InformationReferences { get; set; }
    }
}
