using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.PublicSpaces.Model;

namespace TransportManagementCore.Areas.Goths.Model
{
    public class VisitForSurgeryGothResidentModel
    {
        public int VisitSurgeryGothResidentId { get; set; }
        public int OptometristGothResidentId { get; set; }
        public int ResidentAutoId { get; set; }
        public int GothAutoId { get; set; }
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
        public List<VisitForSurgeryGothResidentDocuments> Modelfiles { get; set; }
    }
    public class VisitForSurgeryGothResidentDocuments
    {
        public int SurgeryGothResidentDocumentsId { get; set; }
        public int VisitSurgeryGothResidentId { get; set; }
        public int ResidentAutoId { get; set; }
        public DateTime DocumentDate { get; set; }
        public IFormFile files { get; set; }
        public string FileName { get; set; }
        public byte[] DocumentFile { get; set; }
        public string Document { get; set; }
        public string FileType { get; set; }
    }
}
