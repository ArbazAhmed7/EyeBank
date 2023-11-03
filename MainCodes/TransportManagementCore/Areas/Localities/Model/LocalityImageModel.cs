using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Areas.Localities.Model
{
    public class LocalityImageModel
    {
        public int LocalityImageAutoId { get; set; }
        public int LocalityAutoId { get; set; }

        public string LocalityPicture { get; set; }
        public byte[] LocalityPic { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public DateTime CaptureDate { get; set; }
        public string CaptureRemarks { get; set; }
        public bool IsSaved { get; set; }

    }
}
