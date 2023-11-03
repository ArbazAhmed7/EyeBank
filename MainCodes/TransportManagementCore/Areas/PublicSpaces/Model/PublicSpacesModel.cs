using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Areas.PublicSpaces.Model
{
    public class PublicSpacesModel
    {
        public DateTime EnrollementDate { get; set; }
        public int PublicSpacesAutoId { get; set; }
        public string PublicSpacesCode { get; set; }
        public string PublicSpacesName { get; set; }
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
        public List<PublicSpacesImageModel> ImageList { get; set; }
    }
}
