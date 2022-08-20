using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dress_BLL.Model
{
    public class ImageHomeModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }

        public string Title { get; set; }
        public Nullable<int> Index { get; set; }
    }
}
