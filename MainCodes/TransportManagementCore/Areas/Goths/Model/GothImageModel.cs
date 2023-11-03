using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Areas.Goths.Model
{
    public class GothImageModel
    {
        public int GothImageAutoId { get; set; }
        public int GothAutoId { get; set; }

        public string GothPicture { get; set; }
        public byte[] GothPic { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public DateTime CaptureDate { get; set; }
        public string CaptureRemarks { get; set; }
        public bool IsSaved { get; set; }
    }
}
