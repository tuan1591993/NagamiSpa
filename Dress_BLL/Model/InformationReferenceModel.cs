using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dress_BLL.Model
{
    public class NewInforReferModel
    {
        public int Id { get; set; }
        public int News_ID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string LogoUrl { get; set; }

        public Nullable<int> Index { get; set; }
    }
}
