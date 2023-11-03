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
using TransportManagementCore.Areas.Localities.Model;
using TransportManagementCore.Areas.Localities.Repositories;
using TransportManagementCore.Models;

namespace TransportManagementCore.Areas.Localities.Controller
{
    [Area("Locality")]
    [Route("Locality/VisitForSurgeryResident")]
    public class LocalityVisitForSurgeryController : Microsoft.AspNetCore.Mvc.Controller
    {
        VisitForSurgeryLocalityResidentRepo repo;
        [Route("Add/0")]
        public IActionResult Index()
        {
            return View("~/Areas/Localities/Views/LocalityVisitForSurgery/Index.cshtml");
        }

        [HttpPost]
        [Route("SaveUpdate")]
        [Obsolete]
        public async Task<JsonResult> SaveUpdate(VisitForSurgeryLocalityResidentModel Model)
        {
            DataTable dt = new DataTable();
            repo = new VisitForSurgeryLocalityResidentRepo();
            List<SqlParameter> parameters = null;
            if (Model.VisitSurgeryLocalityId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("[Sp_VisitForSurgeryLocalityResident]", parameters);
            Model.VisitSurgeryLocalityId = 0;
            if (Convert.ToInt32(dt.Rows[0][0]) > 0 && Model.VisitSurgeryLocalityId == 0 && Model.files.Count > 0)
            {
                VisitForSurgeryLocalityDocumentsModel VD = new VisitForSurgeryLocalityDocumentsModel();
                VD.VisitSurgeryLocalityId = Convert.ToInt32(dt.Rows[0][0]);
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
                    dt = await repo.DbFunction("Sp_VisitForSurgeryLocalityDocuments", parameters);
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
                repo = new VisitForSurgeryLocalityResidentRepo();

                List<SqlParameter> sql = SqlPara("GetLastOptoById");
                sql.Add(new SqlParameter("@ResidentAutoId", Model.WorkerAutoId));
                sql.Add(new SqlParameter("@VisitDate", Model.VisitDate));
                dt = repo.GetForModelFromDB("Sp_VisitForSurgeryLocalityResident", sql);
                DisplayGlassDispenseResidentModel model = new DisplayGlassDispenseResidentModel();
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
            repo = new VisitForSurgeryLocalityResidentRepo();
            List<SqlParameter> sql = SqlPara("GetDatesofVisitForSurgeryResident");
            sql.Add(new SqlParameter("@ResidentAutoId", ResidentAutoId));
            dt = repo.GetForModelFromDB("[Sp_VisitForSurgeryLocalityResident]", sql);
            List<DropDownModel> listmodel = new List<DropDownModel>();
            listmodel = repo.DateList(dt);
            return Json(listmodel);
        }

        [HttpGet]
        [Route("GetSurgeryWorkerById/{VisitSurgeryLocalityId}")]
        public JsonResult GetSurgeryWorkerById(string VisitSurgeryLocalityId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new VisitForSurgeryLocalityResidentRepo();
            List<SqlParameter> sql = SqlPara("GetVisitForSurgeryResidentByID");
            sql.Add(new SqlParameter("@VisitSurgeryLocalityId", VisitSurgeryLocalityId));
            dt = repo.GetForModelFromDB("Sp_VisitForSurgeryLocalityResident", sql);
            VisitForSurgeryLocalityResidentModel model = new VisitForSurgeryLocalityResidentModel();
            model = repo.GetVisitForSurgeryLocalityResidentModel(dt);
            model.DisplayPostDate = model.PostSurgeryVisitDate.ToString("dd") + "-" + model.PostSurgeryVisitDate.ToString("MMM") + "-" + model.PostSurgeryVisitDate.ToString("yyyy");
            return Json(model);
        }

        [HttpGet]
        [Route("GetDocumentById/{FileId}")]
        public IActionResult GetDocumentById(int FileId)
        {
            DataTable dt = new DataTable();
            repo = new VisitForSurgeryLocalityResidentRepo();
            List<SqlParameter> sql = SqlPara("GetDocumentById");
            sql.Add(new SqlParameter("@SurgeryLocalityDocumentsId", FileId));
            VisitForSurgeryLocalityDocumentsModel model = new VisitForSurgeryLocalityDocumentsModel();
            dt = repo.GetForModelFromDB("Sp_VisitForSurgeryLocalityDocuments", sql);
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
            repo = new VisitForSurgeryLocalityResidentRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            parameters.Add(new SqlParameter("@SurgeryLocalityDocumentsId", FileId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("Sp_VisitForSurgeryLocalityDocuments", parameters);
            return Json(dt.Rows[0][1].ToString());
        }

        [Route("DeleteSurgeryById/{VisitSurgeryLocalityId}")]
        public async Task<JsonResult> DeleteSurgeryById(int VisitSurgeryLocalityId)
        {
            repo = new VisitForSurgeryLocalityResidentRepo();
            List<SqlParameter> parameters = SqlPara("DeleteById");
            parameters.Add(new SqlParameter("@VisitSurgeryLocalityId", VisitSurgeryLocalityId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("Sp_VisitForSurgeryLocalityResident", parameters);
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
