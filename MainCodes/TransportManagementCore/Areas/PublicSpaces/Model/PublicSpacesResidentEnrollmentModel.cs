using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Areas.PublicSpaces.Model
{
    public class PublicSpacesResidentEnrollmentModel
    {
        public int ResidentAutoId { get; set; }
        public int PublicSpacesAutoId { get; set; }
        public string ResidentCode { get; set; }
        public string ResidentName { get; set; }
        public string RelationType { get; set; }
        public string RelationName { get; set; }
        public int? Age { get; set; }
        public int? GenderAutoId { get; set; }
        public string CNIC { get; set; }

        public bool? WearGlasses { get; set; }
        public bool? Distance { get; set; }
        public bool? Near { get; set; }
        public bool? DecreasedVision { get; set; }
        public bool? HasOccularHistory { get; set; }
        public string OccularHistoryRemarks { get; set; }
        public bool? HasMedicalHistory { get; set; }
        public string MedicalHistoryRemarks { get; set; }
        public bool? HasChiefComplain { get; set; }
        public string ChiefComplainRemarks { get; set; }
        public DateTime? ResidentTestDate { get; set; }
        public string ResidentRegNo { get; set; }
        public bool? Religion { get; set; }
        public string MobileNo { get; set; }
        public DateTime EnrollementDate { get; set; }
        public string ViewDate { get; set; }
        public string PublicSpacesName { get; set; }
        public List<PublicSpacesResidentEnrollmentImageModel> ImageList { get; set; }
        public string PublicSpacesCode { get; set; }
    }

    public class PublicSpacesResidentEnrollmentImageModel
    {
        public int DetailPublicSpacesAutoId { get; set; }
        public int ResidentImageAutoId { get; set; }
        public int ResidentAutoId { get; set; }
        public string ResidentPicture { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public DateTime CaptureDate { get; set; }
        public string CaptureRemarks { get; set; }
        public bool IsSaved { get; set; }
    }
}

