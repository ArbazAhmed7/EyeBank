using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Factory.Model;
using TransportManagementCore.Areas.Goths.Model;
using TransportManagementCore.Areas.Goths.Repositories;
using TransportManagementCore.Areas.Localities.Model;
using TransportManagementCore.Models;

namespace TransportManagementCore.Areas.Goths.Controller
{ 
    [Area("Goths")]
    [Route("Goths/VisitForSurgeryResident")]
    public class GothVisitForSurgeryController : Microsoft.AspNetCore.Mvc.Controller
    {
        VisitForSurgeryGothResidentRepo repo;
        [Route("Add/0")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("SaveUpdate")]
        [Obsolete]
        public async Task<JsonResult> SaveUpdate(VisitForSurgeryGothResidentModel Model)
        {
            DataTable dt = new DataTable();
            repo = new VisitForSurgeryGothResidentRepo();
            List<SqlParameter> parameters = null;
            if (Model.VisitSurgeryGothResidentId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("[Sp_VisitForSurgeryGothResident]", parameters);
            Model.VisitSurgeryGothResidentId = 0;
            if (Convert.ToInt32(dt.Rows[0][0]) > 0 && Model.VisitSurgeryGothResidentId == 0 && Model.files.Count > 0)
            {
                VisitForSurgeryGothResidentDocuments VD = new VisitForSurgeryGothResidentDocuments();
                VD.VisitSurgeryGothResidentId = Convert.ToInt32(dt.Rows[0][0]);
                dt = new DataTable();
                for (int i = 0; i < Model.files.Count; i++)
                {
                    var stream = new MemoryStream(Convert.ToInt32(Model.files[i].Length));
                    Model.files[i].CopyTo(stream);
                    VD.DocumentFile = stream.ToArray();
                    VD.DocumentDate = Model.VisitDate;
                    VD.FileType = Model.files[i].ContentType;
                    VD.ResidentAutoId = Model.ResidentAutoId;
                    VD.FileName = Model.files[i].FileName;
                    parameters = null;
                    parameters = SqlPara("Save");
                    parameters = repo.SetModelDocument(parameters, VD);
                    dt = await repo.DbFunction("Sp_VisitForSurgeryGothResidentDocuments", parameters);
                }
            }
            return Json(dt.Rows[0][1].ToString());

        }

        [HttpGet]
        [Route("GetLastOptoForSurgery")]
        public JsonResult GetLastOptoForSurgery(GetLastOptoForSurgery Model)
        {
            //CompanyModel cm = new CompanyModel();
            if (Model.WorkerAutoId > 0)
            {
                DataTable dt = new DataTable();
                repo = new VisitForSurgeryGothResidentRepo();

                List<SqlParameter> sql = SqlPara("GetLastOptoById");
                sql.Add(new SqlParameter("@ResidentAutoId", Model.WorkerAutoId));
                sql.Add(new SqlParameter("@VisitDate", Model.VisitDate));
                dt = repo.GetForModelFromDB("Sp_VisitForSurgeryGothResident", sql);
                DisplayGothGlassDispenseResidentModel model = new DisplayGothGlassDispenseResidentModel();
                model = repo.GetWorkerLastHistory(dt);
                return Json(model);
            }
            else
            {
                return Json("");
            }
        }

        [HttpGet]
        [Route("GetDatesofSurgeryWorker/{ResidentAutoId}")]
        public JsonResult GetDatesofSurgeryWorker(string ResidentAutoId)
        {
            DataTable dt = new DataTable();
            repo = new VisitForSurgeryGothResidentRepo();
            List<SqlParameter> sql = SqlPara("GetDatesofVisitForSurgeryResident");
            sql.Add(new SqlParameter("@ResidentAutoId", ResidentAutoId));
            dt = repo.GetForModelFromDB("[Sp_VisitForSurgeryGothResident]", sql);
            List<DropDownModel> listmodel = new List<DropDownModel>();
            listmodel = repo.DateList(dt);
            return Json(listmodel);
        }

        [HttpGet]
        [Route("GetSurgeryWorkerById/{VisitSurgeryGothResidentId}")]
        public JsonResult GetSurgeryWorkerById(string VisitSurgeryGothResidentId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new VisitForSurgeryGothResidentRepo();
            List<SqlParameter> sql = SqlPara("GetVisitForSurgeryResidentByID");
            sql.Add(new SqlParameter("@VisitSurgeryGothResidentId", VisitSurgeryGothResidentId));
            dt = repo.GetForModelFromDB("Sp_VisitForSurgeryGothResident", sql);
            VisitForSurgeryGothResidentModel model = new VisitForSurgeryGothResidentModel();
            model = repo.GetVisitForSurgeryGothResidentModel(dt);
            model.DisplayPostDate = model.PostSurgeryVisitDate.ToString("dd") + "-" + model.PostSurgeryVisitDate.ToString("MMM") + "-" + model.PostSurgeryVisitDate.ToString("yyyy");
            return Json(model);
        }

        [HttpGet]
        [Route("GetDocumentById/{FileId}")]
        public IActionResult GetDocumentById(int FileId)
        {
            DataTable dt = new DataTable();
            repo = new VisitForSurgeryGothResidentRepo();
            List<SqlParameter> sql = SqlPara("GetDocumentById");
            sql.Add(new SqlParameter("@SurgeryGothResidentDocumentsId", FileId));
            VisitForSurgeryGothResidentDocuments model = new VisitForSurgeryGothResidentDocuments();
            dt = repo.GetForModelFromDB("Sp_VisitForSurgeryGothResidentDocuments", sql);
            model = repo.GetDocumentsById(dt);

            MemoryStream ms = new MemoryStream(model.DocumentFile);
            Response.ContentType = model.FileType;
            //return  new FileStreamResult(ms, model.FileType); for View
            return File(ms, model.FileType, model.FileName);
        }

        [HttpPost]
        [Route("DeleteById/{FileId}")]
        public async Task<JsonResult> DeleteById(int FileId)
        {
            repo = new VisitForSurgeryGothResidentRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            parameters.Add(new SqlParameter("@SurgeryGothResidentDocumentsId", FileId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("Sp_VisitForSurgeryGothResidentDocuments", parameters);
            return Json(dt.Rows[0][1].ToString());
        }

        [Route("DeleteSurgeryById/{VisitSurgeryGothResidentId}")]
        public async Task<JsonResult> DeleteSurgeryById(int VisitSurgeryGothResidentId)
        {
            repo = new VisitForSurgeryGothResidentRepo();
            List<SqlParameter> parameters = SqlPara("DeleteById");
            parameters.Add(new SqlParameter("@VisitSurgeryGothResidentId", VisitSurgeryGothResidentId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("Sp_VisitForSurgeryGothResident", parameters);
            return Json(dt.Rows[0][1].ToString());
        }
        public List<SqlParameter> SqlPara(string Operation)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter> {
            new SqlParameter("@Operation", Operation),
            new SqlParameter("@EntryTerminal", DBHelper.DBHelper.ENTTerminalName()),
            new SqlParameter("@UserEmpName", HttpContext.Session.GetString("LoginId")),
            new SqlParameter("@EntryTerminalIP","")
            };
            return sqlParameters;
        }
    }
}
