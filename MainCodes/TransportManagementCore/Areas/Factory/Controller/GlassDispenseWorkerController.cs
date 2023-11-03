using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Factory.Model;
using TransportManagementCore.Areas.Factory.Repositories;
using TransportManagementCore.Areas.Goths.Repositories;
using TransportManagementCore.Models;

namespace TransportManagementCore.Areas.Factory.Controller
{
    [Area("Factory")]
    [Route("Factory/GlassDispenseWorker")]
    public class GlassDispenseWorkerController : Microsoft.AspNetCore.Mvc.Controller
    {
        GlassDispenseWorkerRepo repo;
        [Route("Add/{GlassDespenseWorkerId}")]
        [Utilities.ViewRightsAuthorizationFilter(FormId = "GlassDispenseWorker")]
        public IActionResult Add(int GlassDespenseWorkerId = 0)
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["GlassDespenseWorkerId"] = GlassDespenseWorkerId;
            ViewData["Date"] = System.DateTime.Now.ToString("dd") + "-" + System.DateTime.Now.ToString("MMM") + "-" + System.DateTime.Now.ToString("yyyy");
            return View("~/Areas/Factory/Views/GlassDispenseWorker/Index.cshtml");
        }

        [HttpGet]
        [Route("GetLastOptoById")]
        public JsonResult GetLastOptoById(GetLastOpto Model)
        {
            //CompanyModel cm = new CompanyModel();
            if (Model.WorkerAutoId > 0)
            {
                DataTable dt = new DataTable();
                repo = new GlassDispenseWorkerRepo();

                List<SqlParameter> sql = SqlPara("GetGlassDispenseHistoryByWorkerId");
                sql.Add(new SqlParameter("@WorkerAutoId", Model.WorkerAutoId));
                sql.Add(new SqlParameter("@GlassDespenseWorkerTransDate", Model.GlassDespenseWorkerTransDate));
                dt = repo.GetForModelFromDB("Sp_GlassDespenseWorker", sql);
                DisplayGlassDispenseWorkerModel model = new DisplayGlassDispenseWorkerModel();
                model = repo.GetWorkerLastHistory(dt);
                return Json(model);
            }
            else {
                return Json("");
            }
        }

        [HttpPost]
        [Route("SaveUpdate")]
        public async Task<JsonResult> SaveUpdate(GlassDispenseWorkerModel Model)
        {

            DataTable dt = new DataTable();
            repo = new GlassDispenseWorkerRepo();
            List<SqlParameter> parameters = null;
            if (Model.GlassDespenseWorkerId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("[Sp_GlassDespenseWorker]", parameters);
            return Json(dt.Rows[0][1].ToString());
        }

        [HttpGet]
        [Route("GetDatesofGlassDespenseWorker/{WorkerAutoId}")]
        public JsonResult GetDatesofGlassDespenseWorker(string WorkerAutoId)
        {
            DataTable dt = new DataTable();
            repo = new GlassDispenseWorkerRepo();

            List<SqlParameter> sql = SqlPara("GetDatesofGlassDespenseWorker");
            sql.Add(new SqlParameter("@WorkerAutoId", WorkerAutoId));
            dt = repo.GetForModelFromDB("[Sp_GlassDespenseWorker]", sql);
            List<DropDownModel> listmodel = new List<DropDownModel>();
            listmodel = repo.DateList(dt);
            return Json(listmodel);
        }
        [HttpGet]
        [Route("GetGlassDespenseById/{GlassDespenseWorkerId}")]
        public JsonResult GetGlassDespenseById(string GlassDespenseWorkerId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new GlassDispenseWorkerRepo();
            List<SqlParameter> sql = SqlPara("GetById");
            sql.Add(new SqlParameter("@GlassDespenseWorkerId", GlassDespenseWorkerId));
            dt = repo.GetForModelFromDB("Sp_GlassDespenseWorker", sql);
            GlassDispenseWorkerModel model = new GlassDispenseWorkerModel();
            model = repo.GlassDispenseModel(dt);
            return Json(model);
        }


        [HttpPost]
        [Produces("application/json")]
        [Route("DeleteById/{GlassDespenseWorkerId}")]
        public async Task<JsonResult> DeleteById(int GlassDespenseWorkerId)
        {
            DataTable dt = new DataTable();
            repo = new GlassDispenseWorkerRepo();
            List<SqlParameter> parameters = null;
            if (GlassDespenseWorkerId > 0)
            {
                parameters = SqlPara("DeleteById");
                parameters.Add(new SqlParameter("@GlassDespenseWorkerId", GlassDespenseWorkerId));
            }
            dt = await repo.DbFunction("[Sp_GlassDespenseWorker]", parameters);
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
