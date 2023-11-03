using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Areas.PublicSpaces.Model
{
    public class PublicSpacesAutoRefTestResidentModel
    {
        public int AutoRefResidentId { get; set; }
        public string AutoRefResidentTransId { get; set; }
        public DateTime? AutoRefResidentTransDate { get; set; }
        public int? ResidentAutoId { get; set; }
        public char Right_Spherical_Status { get; set; }
        public decimal? Right_Spherical_Points { get; set; }
        public char Right_Cyclinderical_Status { get; set; }
        public decimal? Right_Cyclinderical_Points { get; set; }
        public int? Right_Axix_From { get; set; }
        public int? Right_Axix_To { get; set; }
        public char Left_Spherical_Status { get; set; }
        public decimal? Left_Spherical_Points { get; set; }
        public char Left_Cyclinderical_Status { get; set; }
        public decimal? Left_Cyclinderical_Points { get; set; }
        public int? Left_Axix_From { get; set; }
        public int? Left_Axix_To { get; set; }
        public string UserId { get; set; }
        public int? IPD { get; set; }

        public bool WearGlasses { get; set; }


    }
    public class DisplayAutoRefResidentModel
    {
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
