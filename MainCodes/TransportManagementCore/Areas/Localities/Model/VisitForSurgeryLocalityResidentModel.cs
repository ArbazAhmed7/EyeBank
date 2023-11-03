using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Areas.Localities.Model
{
    public class VisitForSurgeryLocalityResidentModel
    {
        public int VisitSurgeryLocalityId { get; set; }
        public int OptometristResidentId { get; set; } 
        public int ResidentAutoId { get; set; }
        public int LocalityAutoId  { get; set; }
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
        public List<VisitForSurgeryLocalityDocumentsModel> Modelfiles { get; set; }
    }
    public class VisitForSurgeryLocalityDocumentsModel {
        public int SurgeryLocalityDocumentsId { get; set; }
        public int VisitSurgeryLocalityId { get; set; }
        public int ResidentAutoId { get; set; }
        public DateTime DocumentDate { get; set; }
        public IFormFile files { get; set; }
        public string FileName { get; set; }
        public byte[] DocumentFile { get; set; }
        public string Document { get; set; }
        public string FileType { get; set; }
    }
}
