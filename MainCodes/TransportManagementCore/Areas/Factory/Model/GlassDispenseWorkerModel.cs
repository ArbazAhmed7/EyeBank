using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Goth.Model;
using TransportManagementCore.Areas.Setup.Model;

namespace TransportManagementCore.Areas.Factory.Model
{
    public class GlassDispenseWorkerModel : OptometristWokerModel 
    {
        public int GlassDespenseWorkerId { get; set; }
        public DateTime GlassDespenseWorkerTransDate { get; set; }
        public int VisionwithGlasses_RightEye { get; set; }
        public int VisionwithGlasses_LeftEye { get; set; }
        public int NearVA_RightEye { get; set; }
        public int NearVA_LeftEye { get; set; }
        public int WorkerSatisficaion { get; set; }
        public int Unsatisfied { get; set; } 
        public string Unsatisfied_Remarks { get; set; }
        public int Unsatisfied_Reason { get; set; }
        public bool Distance { get; set; }
        public bool Near { get; set; }
        public bool WearGlasses { get; set; }

    }

    public class DisplayGlassDispenseWorkerModel: DisplayAutoRefWorkerModel {
        public int OptometristWorkerId { get; set; }
        public bool Distance { get; set; }
        public bool Near { get; set; }
        public bool WearGlasses { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
    }

    public class GetLastOpto {
        public int WorkerAutoId { get; set; }
        public DateTime GlassDespenseWorkerTransDate { get; set; }
    }
}
