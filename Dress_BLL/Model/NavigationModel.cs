using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dress_BLL.Model
{
    public class NavigationModel
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; }
        public string Link { get; set; }
        public Nullable<int> Parent { get; set; }
        public string Group { get; set; }
    }
}
