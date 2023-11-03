using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Areas.Goths.Model
{
    public class GothModel
    {
        public DateTime EnrollementDate { get; set; }
        public int GothAutoId { get; set; }
        public string GothCode { get; set; }
        public string GothName { get; set; }
        public string Website { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string NameofPerson { get; set; }
        public string PersonMobile { get; set; }
        public string PersonRole { get; set; }
        public int? TitleAutoId { get; set; }
        public string ViewDate { get; set; }
        public List<GothImageModel> ImageList { get; set; }
    }
}
