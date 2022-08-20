using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dress_BLL.Model
{
    public class BannerModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Url_Mobile { get; set; }
        public Nullable<int> Index { get; set; }
    }
}
