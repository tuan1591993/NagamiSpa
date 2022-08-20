using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dress_BLL.Model
{
    public class HighlightServiceModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Nullable<int> Index { get; set; }
    }
}
