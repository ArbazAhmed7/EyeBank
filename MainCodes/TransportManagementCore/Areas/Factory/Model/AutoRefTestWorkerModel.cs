using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Areas.Goth.Model
{
    public class AutoRefTestWorkerModel
    {
        public int AutoRefWorkerId { get; set; }
        public string AutoRefWorkerTransId { get; set; }
        public DateTime AutoRefWorkerTransDate { get; set; }
        public int WorkerAutoId { get; set; }
        public char Right_Spherical_Status { get; set; }
        public decimal Right_Spherical_Points { get; set; }
        public char Right_Cyclinderical_Status { get; set; }
        public decimal Right_Cyclinderical_Points { get; set; }
        public int Right_Axix_From { get; set; }
        public int Right_Axix_To { get; set; }
        public char Left_Spherical_Status { get; set; }
        public decimal Left_Spherical_Points { get; set; }
        public char Left_Cyclinderical_Status { get; set; }
        public decimal Left_Cyclinderical_Points { get; set; }
        public int Left_Axix_From { get; set; }
        public int Left_Axix_To { get; set; }
        public int IPD { get; set; }

        public bool WearGlasses { get; set; }
        

    }
    public class DisplayAutoRefWorkerModel {
        public string LastVisitDate { get; set; }
        public string Right_Spherical_Points { get; set; }
        public string Left_Spherical_Points { get; set; }
        public string Right_Cyclinderical_Points { get; set; }
        public string Left_Cyclinderical_Points { get; set; }
        public string Right_Axix_From { get; set; }
        public string Left_Axix_From { get; set; }
        public int IPD { get; set; }
    }
}
