using System;
using System.Collections.Generic;
using TransportManagementCore.Areas.Goth.Model;

namespace TransportManagementCore.Areas.Setup.Model
{
    public class CompanyWorkerEnrollmentModel
    {
        public DateTime EnrollementDate { get; set; }
        public int WorkerAutoId { get; set; }
        public string CompanyCode { get; set; }
        public int CompanyAutoId { get; set; }
        public string WorkerCode { get; set; }
        public string WorkerName { get; set; }
        public string CompanyName { get; set; }
        public string RelationType { get; set; }
        public string RelationName { get; set; }
        public int Age { get; set; }
        public int GenderAutoId { get; set; }
        public string CNIC { get; set; }
        public bool WearGlasses { get; set; }
        public bool Distance { get; set; }
        public bool Near { get; set; }
        public bool DecreasedVision { get; set; }
        public bool Religion { get; set; }
        public string MobileNo { get; set; }
        public string ViewDate { get; set; }
        public List<CompanyWorkerEnrollmentImageModel> ImageList { get; set; }
        public List<AutoRefTestWorkerModel> AutoRefTestWorkerList { get; set; }
    }
    public class CompanyWorkerEnrollmentImageModel
    {
        public int DetailCompanyAutoId { get; set; }
        public int WorkerImageAutoId { get; set; }
        public int WorkerAutoId { get; set; }

        public string WorkerPicture { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public DateTime CaptureDate { get; set; }
        public string CaptureRemarks { get; set; }
        public bool IsSaved { get; set; }


    }
}
