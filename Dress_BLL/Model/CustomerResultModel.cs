using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dress_BLL.Model
{
    public class CustomerResultModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string CustomerName { get; set; }
        public string ImageGroupUrl { get; set; }
        public string ServiceName { get; set; }
        public string ImageBeforeUrl { get; set; }
        public string ImageAfterUrl { get; set; }
        public Nullable<int> IndexImage { get; set; }
        public string Type { get; set; }
    }
}
