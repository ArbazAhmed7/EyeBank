using System;

namespace TransportManagementCore.Areas.Setup.Model
{
    public class CompanyImageModel
    {
        public int CompanyImageAutoId { get; set; }
        public int CompanyAutoId { get; set; }

        public string CompanyPicture { get; set; }
        public byte[] CompanyPic { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public DateTime CaptureDate { get; set; }
        public string CaptureRemarks { get; set; }
        public bool IsSaved { get; set; }


    }
}
