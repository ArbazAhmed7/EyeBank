using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Areas.PublicSpaces.Model
{
    public class VisitForSurgeryPublicSpacesModel
    {
        public int VisitSurgeryPublicSpacesId { get; set; }
        public int OptometristPublicSpacesResidentId { get; set; }
        public int ResidentAutoId { get; set; }
        public int PublicSpacesAutoId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Hospital { get; set; }
        public string Optometrist { get; set; }
        public string Ophthalmologist { get; set; }
        public string Surgeon { get; set; }
        public string NameOfSurgery { get; set; }
        public DateTime PostSurgeryVisitDate { get; set; }
        public string DisplayPostDate { get; set; }
        public string Eye { get; set; }
        public string CommentOfSurgeonAfterSurgery { get; set; }
        public List<IFormFile> files { get; set; }
        public List<VisitForSurgeryPublicSpacesDocuments> Modelfiles { get; set; }
    }
    public class VisitForSurgeryPublicSpacesDocuments
    {
        public int SurgeryPublicSpacesDocumentsId { get; set; }
        public int VisitSurgeryPublicSpacesId { get; set; }
        public int ResidentAutoId { get; set; }
        public DateTime DocumentDate { get; set; }
        public IFormFile files { get; set; }
        public string FileName { get; set; }
        public byte[] DocumentFile { get; set; }
        public string Document { get; set; }
        public string FileType { get; set; }
    }
}
