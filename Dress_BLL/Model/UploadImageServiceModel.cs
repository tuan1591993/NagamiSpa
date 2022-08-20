using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dress_BLL.Model
{
    public class UploadImageServiceModel
    {
        public int ServiceId { get; set; }
        public string Type { get; set; }
        public string URL { get; set; }
        public int IndexAttachment { get; set; }
    }
}
