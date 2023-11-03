using System;
using System.Collections.Generic;

namespace TransportManagementCore.Areas.Setup.Model
{
    public class CompanyModel
    {
        public DateTime EnrollementDate { get; set; }
        public int CompanyAutoId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string Website { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public int WorkForce { get; set; }
        public string OwnerName { get; set; }
        public string OwnerMobile { get; set; }
        public string OwnerEmail { get; set; }
        public string AdminHeadName { get; set; }
        public string AdminHeadMobile { get; set; }
        public string AdminHeadEmail { get; set; }
        public string HRHeadName { get; set; }
        public string HRHeadMobile { get; set; }
        public string HRHeadEmail { get; set; }
        public string ViewDate { get; set; }
        public List<CompanyImageModel> ImageList { get; set; }

    }
}
