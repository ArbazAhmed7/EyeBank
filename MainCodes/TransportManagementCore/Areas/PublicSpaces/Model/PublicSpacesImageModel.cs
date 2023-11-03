using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Areas.PublicSpaces.Model
{
    public class PublicSpacesImageModel
    {
        public int PublicSpacesImageAutoId { get; set; }
        public int PublicSpacesAutoId { get; set; }

        public string PublicSpacesPicture { get; set; }
        public byte[] PublicSpacesPic { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public DateTime CaptureDate { get; set; }
        public string CaptureRemarks { get; set; }
        public bool IsSaved { get; set; }
    }
}
