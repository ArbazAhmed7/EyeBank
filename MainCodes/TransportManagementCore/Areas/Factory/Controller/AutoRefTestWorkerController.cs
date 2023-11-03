using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Goth.Model;
using TransportManagementCore.Areas.Goth.Repositories;
using TransportManagementCore.Areas.Setup.Model;
using TransportManagementCore.Controllers;
using TransportManagementCore.Models;
using TransportManagementCore.Models.DataTables;
using Microsoft.Extensions.Configuration; 
namespace TransportManagementCore.Areas.Goth.Controller
{
    [Area("Factory")]
    [Route("Factory/AutoRefTestWorker")]
    public class AutoRefTestWorkerController : Microsoft.AspNetCore.Mvc.Controller
    {
        AutoRefTestWorkerRepo repo;

        private readonly IConfiguration Configuration;
        public AutoRefTestWorkerController(IConfiguration config)
        {
            this.Configuration = config;

        }
        [Route("List")]
        public IActionResult List()
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["LoginId"] = HttpContext.Session.GetString("LoginId");
            return View("~/Areas/Factory/Views/AutoRefTestWorker/vwList.cshtml");
        }
        [Utilities.ViewRightsAuthorizationFilter(FormId = "AutoRefractionWorker")]
        [Route("Add/{WorkerAutoId}")]
        public IActionResult Add(int WorkerAutoId = 0)
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["WorkerAutoId"] = WorkerAutoId;
            ViewData["Date"] = System.DateTime.Now.ToString("dd") + "-" + System.DateTime.Now.ToString("MMM") + "-" + System.DateTime.Now.ToString("yyyy");
            return View("~/Areas/Factory/Views/AutoRefTestWorker/Index.cshtml");
        }
        [HttpGet]
        [Produces("application/json")]
        [Route("GetWorkerList", Name = "GetWorkerForAutoRef")]
        [Route("GetWorkerList/{searchText}")]
        public async Task<object> GetWorkerList(string searchText = null)
        {
            //AutherizedFormRights rights = Utilities.General.GetFormRights(HttpContext.Session.GetString("LoginId"), Utilities.Constraints.Admission);
            int[] columnsToHide = new int[] { 0 };
             repo = new  AutoRefTestWorkerRepo();
            List<ActionButton> actionButtons = new List<ActionButton>();
            //if (rights.IsEditRights)
            //{
            actionButtons.Add(ActionButton.Edit);
            //}
            AutherizedFormRights formRights = Utilities.General.GetFormRights(HttpContext.Session.GetString("LoginId"), "AutoRefractionWorker");
            //if (rightsDischarge.IsAddRights)
            //    actionButtons.Add(ActionButton.Discharge);

            //Global.CurrentUser.BranchId = Convert.ToInt32(HttpContext.Session.GetString("BranchId"));
            List <SqlParameter> parameters = null;
            if (searchText != null && searchText != "")
            {
                parameters = SqlPara("Search");
                parameters.Add(new SqlParameter("@SearchText", searchText));
            }
            else
                parameters = SqlPara("GetAllWorker");
            return Utilities.DataTables.DataTableSource(await repo.DbFunction("[Sp_CompanyWorker]", parameters), columnsToHide, actionButtons);
        }
        [HttpGet]
        [Route("GetById/{WorkerAutoId}")]
        public JsonResult GetById(string WorkerAutoId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new AutoRefTestWorkerRepo();

            List<SqlParameter> sql = SqlPara("GetWorkerByiD");
            sql.Add(new SqlParameter("@WorkerAutoId", WorkerAutoId));
            dt =  repo.GetForModelFromDB("Sp_CompanyWorker", sql);
            CompanyWorkerEnrollmentModel model = new CompanyWorkerEnrollmentModel();
            model = repo.GetModel(dt);
            model.ViewDate = model.EnrollementDate.ToString("dd") + "-" + model.EnrollementDate.ToString("MMM") + "-" + model.EnrollementDate.ToString("yyyy");
            return Json(model);
        }
        [HttpGet]
        [Route("GetLastHistoryById/{WorkerAutoId}")]
        public JsonResult GetLastHistoryById(string WorkerAutoId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new AutoRefTestWorkerRepo();

            List<SqlParameter> sql = SqlPara("GetAutoRefHistoryByWorkerId");
            sql.Add(new SqlParameter("@WorkerAutoId", WorkerAutoId));
            dt = repo.GetForModelFromDB("Sp_AutoRefTestWorker", sql);
            DisplayAutoRefWorkerModel model = new DisplayAutoRefWorkerModel();
            model = repo.GetWorkerLastHistory(dt);
            return Json(model);
        }

        
        [HttpGet]
        [Route("GetDatesofWorker/{WorkerAutoId}")]
        public JsonResult GetDatesofWorker(string WorkerAutoId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new AutoRefTestWorkerRepo();

            List<SqlParameter> sql = SqlPara("GetDatesofWorker");
            sql.Add(new SqlParameter("@WorkerAutoId", WorkerAutoId));
            dt = repo.GetForModelFromDB("Sp_AutoRefTestWorker", sql);
            List<DropDownModel> listmodel = new List<DropDownModel>();
            listmodel = repo.DateList(dt);
            return Json(listmodel);
        }


        [HttpGet]
        [Route("GetAutoRefById/{AutoRefWorkerId}")]
        public JsonResult GetAutoRefById(string AutoRefWorkerId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new AutoRefTestWorkerRepo();
            List<SqlParameter> sql = SqlPara("GetByAutoRefWorkerId");
            sql.Add(new SqlParameter("@AutoRefWorkerId", AutoRefWorkerId));
            dt = repo.GetForModelFromDB("Sp_AutoRefTestWorker", sql);
            AutoRefTestWorkerModel  model = new AutoRefTestWorkerModel();
            model = repo.AutoRefModel(dt);
            return Json(model);
        }

        [HttpGet]
        [Route("GetAutoRefByWorkerId/{WorkerId}")]
        public JsonResult GetAutoRefByWorkerId(string WorkerId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new AutoRefTestWorkerRepo();
            List<SqlParameter> sql = SqlPara("GetByAutoRefWorkerIdForOpt");
            sql.Add(new SqlParameter("@WorkerAutoId", WorkerId));
            dt = repo.GetForModelFromDB("Sp_AutoRefTestWorker", sql);
            AutoRefTestWorkerModel model = new AutoRefTestWorkerModel();
            model = repo.AutoRefModel(dt);
            return Json(model);
        }

        [HttpPost]
        [Route("SaveUpdate")]
        public async Task<JsonResult> SaveUpdate(AutoRefTestWorkerModel Model)
        {

            DataTable dt = new DataTable();
            repo = new AutoRefTestWorkerRepo();
            List<SqlParameter> parameters = null;
            if (Model.AutoRefWorkerId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("Sp_AutoRefTestWorker", parameters);
            return Json(dt.Rows[0][1].ToString());
        }
        [HttpPost]
        [Produces("application/json")]
        [Route("DeleteById/{AutoWorkerId}")]
        public async Task<JsonResult> DeleteById(int AutoWorkerId)
        {
            DataTable dt = new DataTable();
            repo = new AutoRefTestWorkerRepo();
            List<SqlParameter> parameters = null;
            if (AutoWorkerId > 0)
            {
                parameters = SqlPara("DeleteAutoRefById");
                parameters.Add(new SqlParameter("@AutoRefWorkerId", AutoWorkerId));
            }
            dt = await repo.DbFunction("[Sp_AutoRefTestWorker]", parameters);
            return Json(dt.Rows[0][1].ToString());

        }

        [HttpGet]
        [Route("RedirectToLogin")]
        public IActionResult RedirectToLogin()
        {
            if (Global.MainApplication == null) {
                var MainApplication = this.Configuration.GetSection("MainApplicationURL").GetSection("URL").Value;
                HttpContext.Session.SetString("MainApplication", MainApplication);
                Global.MainApplication = MainApplication;
            }
                // Redirect to an external URL
                return Redirect(Global.MainApplication);
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
