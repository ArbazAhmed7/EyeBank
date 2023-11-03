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
using TransportManagementCore.Areas.Factory.Repositories;
using TransportManagementCore.Models;

namespace TransportManagementCore.Areas.Factory.Controller
{
    [Area("Factory")]
    [Route("Factory/VisitForSurgeryWorker")]
    public class VisitForSurgeryWorkerController : Microsoft.AspNetCore.Mvc.Controller
    {
        VisitForSurgeryWorkerRepo repo;
        [Route("Add/0")]
        public IActionResult Index()
        {
            return View();
        }
          
        [HttpPost]
        [Route("SaveUpdate")]
        [Obsolete]
        public async Task<JsonResult> SaveUpdate(VisitForSurgeryWorkerModel Model)
        {
            DataTable dt = new DataTable();
            repo = new VisitForSurgeryWorkerRepo();
            List<SqlParameter> parameters = null;
            if (Model.VisitSurgeryWorkerId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("[Sp_VisitForSurgeryWorker]", parameters);
            Model.VisitSurgeryWorkerId = 0;
            if (Convert.ToInt32(dt.Rows[0][0]) > 0 && Model.VisitSurgeryWorkerId == 0 && Model.files.Count>0) {
                VisitForSurgeryWorkerDocumentsModel VD= new VisitForSurgeryWorkerDocumentsModel();
                VD.VisitSurgeryWorkerId = Convert.ToInt32(dt.Rows[0][0]);
                dt = new DataTable();
                for (int i = 0; i < Model.files.Count; i++) 
                {
                    var stream = new MemoryStream(Convert.ToInt32(Model.files[i].Length));
                    Model.files[i].CopyTo(stream);
                    VD.DocumentFile = stream.ToArray();
                    VD.DocumentDate = Model.VisitDate;
                    VD.FileType = Model.files[i].ContentType;
                    VD.WorkerAutoId = Model.WorkerAutoId;
                    VD.FileName = Model.files[i].FileName;
                    parameters = null;
                    parameters = SqlPara("Save");
                    parameters = repo.SetModelDocument(parameters, VD);
                    dt = await repo.DbFunction("Sp_VisitForSurgeryWorkerDocuments", parameters);
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
                repo = new VisitForSurgeryWorkerRepo();

                List<SqlParameter> sql = SqlPara("GetLastOptoById");
                sql.Add(new SqlParameter("@WorkerAutoId", Model.WorkerAutoId));
                sql.Add(new SqlParameter("@VisitDate", Model.VisitDate));
                dt = repo.GetForModelFromDB("Sp_VisitForSurgeryWorker", sql);
                DisplayGlassDispenseWorkerModel model = new DisplayGlassDispenseWorkerModel();
                model = repo.GetWorkerLastHistory(dt);
                return Json(model);
            }
            else
            {
                return Json("");
            }
        }

        [HttpGet]
        [Route("GetDatesofSurgeryWorker/{WorkerAutoId}")]
        public JsonResult GetDatesofSurgeryWorker(string WorkerAutoId)
        {
            DataTable dt = new DataTable();
            repo = new VisitForSurgeryWorkerRepo();
            List<SqlParameter> sql = SqlPara("GetDatesofVisitForSurgeryWorker");
            sql.Add(new SqlParameter("@WorkerAutoId", WorkerAutoId));
            dt = repo.GetForModelFromDB("[Sp_VisitForSurgeryWorker]", sql);
            List<DropDownModel> listmodel = new List<DropDownModel>();
            listmodel = repo.DateList(dt);
            return Json(listmodel);
        }

        [HttpGet]
        [Route("GetSurgeryWorkerById/{VisitSurgeryWorkerId}")]
        public JsonResult GetSurgeryWorkerById(string VisitSurgeryWorkerId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new VisitForSurgeryWorkerRepo ();
            List<SqlParameter> sql = SqlPara("GetVisitForSurgeryWorkerByID");
            sql.Add(new SqlParameter("@VisitSurgeryWorkerId", VisitSurgeryWorkerId));
            dt = repo.GetForModelFromDB("Sp_VisitForSurgeryWorker", sql);
            VisitForSurgeryWorkerModel model = new VisitForSurgeryWorkerModel();
            model = repo.GetVisitForSurgeryWorkerModel(dt);            
            model.DisplayPostDate = model.PostSurgeryVisitDate.ToString("dd") + "-" + model.PostSurgeryVisitDate.ToString("MMM") + "-" + model.PostSurgeryVisitDate.ToString("yyyy");
            return Json(model);
        }

        [HttpGet]
        [Route("GetDocumentById/{FileId}")]
        public IActionResult GetDocumentById(int FileId)
        {
            DataTable dt = new DataTable();
            repo = new VisitForSurgeryWorkerRepo();
            List<SqlParameter> sql = SqlPara("GetDocumentById");
            sql.Add(new SqlParameter("@SurgeryWorkerDocumentsId", FileId));
            VisitForSurgeryWorkerDocumentsModel model = new VisitForSurgeryWorkerDocumentsModel();
            dt = repo.GetForModelFromDB("Sp_VisitForSurgeryWorkerDocuments", sql);
            model = repo.GetDocumentsById(dt);

            MemoryStream ms = new MemoryStream(model.DocumentFile);
            Response.ContentType = model.FileType;
            //return  new FileStreamResult(ms, model.FileType); for View
            return   File(ms, model.FileType,model.FileName);
        }

        [HttpPost]
        [Route("DeleteById/{FileId}")]
        public async Task<JsonResult> DeleteById(int FileId)
        {
            repo = new VisitForSurgeryWorkerRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            parameters.Add(new SqlParameter("@SurgeryWorkerDocumentsId", FileId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("Sp_VisitForSurgeryWorkerDocuments", parameters);
            return Json(dt.Rows[0][1].ToString());
        }

        [Route("DeleteSurgeryById/{VisitSurgeryWorkerId}")]
        public async Task<JsonResult> DeleteSurgeryById(int VisitSurgeryWorkerId)
        {
            repo = new VisitForSurgeryWorkerRepo();
            List<SqlParameter> parameters = SqlPara("DeleteById");
            parameters.Add(new SqlParameter("@VisitSurgeryWorkerId", VisitSurgeryWorkerId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("Sp_VisitForSurgeryWorker", parameters);
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
