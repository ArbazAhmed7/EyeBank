using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Areas.Factory.Model
{
    public class VisitForSurgeryWorkerModel
    {
        public int VisitSurgeryWorkerId { get; set; }
        public int OptometristWorkerId { get; set; }
  
        public int WorkerAutoId { get; set; }
        public int CompanyAutoId { get; set; }
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
        public List<VisitForSurgeryWorkerDocumentsModel> Modelfiles { get; set; }
        
    }

    public class VisitForSurgeryWorkerDocumentsModel {
        public int SurgeryWorkerDocumentsId { get; set; }
        public int VisitSurgeryWorkerId { get; set; }
        public int WorkerAutoId { get; set; }
        public DateTime DocumentDate { get; set; }
        public IFormFile files { get; set; }
        public string FileName { get; set; }
        public byte[] DocumentFile { get; set; }
        public string Document { get; set; }
        public string FileType { get; set; }
    }
    public class GetLastOptoForSurgery
    {
        public int WorkerAutoId { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
