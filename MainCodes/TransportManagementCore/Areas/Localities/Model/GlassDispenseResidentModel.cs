using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Setup.Model;

namespace TransportManagementCore.Areas.Localities.Model
{
    public class GlassDispenseResidentModel : OptometristGothResidentModel
    {
        public int GlassDispenseResidentId { get; set; }
        public DateTime GlassDispenseResidentTransDate { get; set; }
        public int VisionwithGlasses_RightEye { get; set; }
        public int VisionwithGlasses_LeftEye { get; set; }
        public int NearVA_RightEye { get; set; }
        public int NearVA_LeftEye { get; set; }
        public int ResidentSatisficaion { get; set; }
        public int Unsatisfied { get; set; }
        public string Unsatisfied_Remarks { get; set; }
        public int Unsatisfied_Reason { get; set; }
        public bool Distance { get; set; }
        public bool Near { get; set; }
        public bool WearGlasses { get; set; }

    }

    public class DisplayGlassDispenseResidentModel : DisplayAutoRefResidentModel
    {
        public int OptometristResidentId { get; set; }
        public bool Distance { get; set; }
        public bool Near { get; set; }
        public bool WearGlasses { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
    }

    public class GetLastOpto
    {
        public int ResidentAutoId { get; set; }
        public DateTime GlassDispenseResidentTransDate { get; set; }
    }
}
