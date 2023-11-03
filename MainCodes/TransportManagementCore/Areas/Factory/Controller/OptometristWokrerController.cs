using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Goth.Repositories;
using TransportManagementCore.Areas.Setup.Model;
using TransportManagementCore.Models;
using TransportManagementCore.Models.DataTables;

namespace TransportManagementCore.Areas.Goth.Controller
{
    [Area("Factory")]
    [Route("Factory/OptometristWokrer")]
    public class OptometristWokrerController : Microsoft.AspNetCore.Mvc.Controller
    {
        OptometristWokrerRepo repo;
        [Route("List")]
        public IActionResult List()
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["LoginId"] = HttpContext.Session.GetString("LoginId");
            return View("~/Areas/Factory/Views/OptometristWokrer/vwList.cshtml");
        }
        [HttpGet]
        [Produces("application/json")]
        [Route("GetWorkerList", Name = "GetWorkerForOptometris")]
        [Route("GetWorkerList/{searchText}")]
        public async Task<object> GetWorkerList(string searchText = null)
        {
            //AutherizedFormRights rights = Utilities.General.GetFormRights(HttpContext.Session.GetString("LoginId"), Utilities.Constraints.Admission);
            int[] columnsToHide = new int[] { 0 };
            repo = new OptometristWokrerRepo();
            List<ActionButton> actionButtons = new List<ActionButton>();
            //if (rights.IsEditRights)
            //{
            actionButtons.Add(ActionButton.Edit);
            //}
            //AutherizedFormRights rightsDischarge = Utilities.General.GetFormRights(HttpContext.Session.GetString("LoginId"), Utilities.Constraints.AdmissionDischarge);
            //if (rightsDischarge.IsAddRights)
            //    actionButtons.Add(ActionButton.Discharge);

            List<SqlParameter> parameters = null;
            if (searchText != null && searchText != "")
            {
                parameters = SqlPara("Search");
                parameters.Add(new SqlParameter("@SearchText", searchText));
            }
            else
                parameters = SqlPara("GetListForWokrerOptometrist");
            return Utilities.DataTables.DataTableSource(await repo.DbFunction("[Sp_OptometristWorker]", parameters), columnsToHide, actionButtons);
        }

       

        [HttpGet]
        [Route("GetOptometristWokrerById/{OptometristWorkerId}")]
        public JsonResult GetOptometristWokrerById(string OptometristWorkerId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new OptometristWokrerRepo();
            List<SqlParameter> sql = SqlPara("GetById");
            sql.Add(new SqlParameter("@OptometristWorkerId", @OptometristWorkerId));
            dt = repo.GetForModelFromDB("[Sp_OptometristWorker]", sql);
            OptometristWokerModel model = new OptometristWokerModel();
            model = repo.GetOptometristWokerLast(dt);
            return Json(model);
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("SaveUpdate")]
        public async Task<JsonResult> SaveUpdate(OptometristWokerModel Model)
        {
            DataTable dt = new DataTable();
            repo = new OptometristWokrerRepo();
            List<SqlParameter> parameters = null;
            if (Model.OptometristWorkerId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("[Sp_OptometristWorker]", parameters);
            return Json(dt.Rows[0][1].ToString());
            
        }
        [Utilities.ViewRightsAuthorizationFilter(FormId = "OptometristWokrer")]
        [Route("Add/{WorkerAutoId}")]
        public IActionResult Add(int WorkerAutoId = 0)
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["LoginId"] = HttpContext.Session.GetString("LoginId");
            ViewData["WorkerAutoId"] = WorkerAutoId;
            ViewData["ViewDate"] = DateTime.Now.Date.ToString("dd") + " | " + DateTime.Now.Date.ToString("MMM") + " | " + DateTime.Now.Date.ToString("yyyy");
            return View("~/Areas/Factory/Views/OptometristWokrer/Index.cshtml");
        }

        [HttpGet]
        [Route("GetHistoryById/{WorkerAutoId}")]
        public async Task<object> GetHistoryById(string WorkerAutoId)
        {
            int[] columnsToHide = new int[] { 0, 1, 3, 4, 6, 8, 9, 10, 12, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25,26 };
            List<ActionButton> actionButtons = new List<ActionButton>();
            DataTable dt = new DataTable();
            repo = new OptometristWokrerRepo();
            List<SqlParameter> sql = SqlPara("GetAutoRefByWorkerId");
            sql.Add(new SqlParameter("@WorkerAutoId", WorkerAutoId));
            return Utilities.DataTables.DataTableSource(await repo.DbFunction("Sp_AutoRefTestWorker", sql), columnsToHide, actionButtons);

        }

        [HttpPost]
        [Produces("application/json")]
        [Route("DeleteById/{OptoWorkerId}")]
        public async Task<JsonResult> DeleteById(int OptoWorkerId)
        {
            DataTable dt = new DataTable();
            repo = new OptometristWokrerRepo();
            List<SqlParameter> parameters = null;
            if (OptoWorkerId > 0)
            {
                parameters = SqlPara("DeleteOptometristById");
                parameters.Add(new SqlParameter("@OptometristWorkerId", OptoWorkerId));
            }
            dt = await repo.DbFunction("[Sp_OptometristWorker]", parameters);
            return Json(dt.Rows[0][1].ToString());

        }

        [HttpGet]
        [Route("GetDatesofWorker/{WorkerAutoId}")]
        public JsonResult GetDatesofWorker(string WorkerAutoId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new OptometristWokrerRepo();

            List<SqlParameter> sql = SqlPara("GetDatesofWorker");
            sql.Add(new SqlParameter("@WorkerAutoId", WorkerAutoId));
            dt = repo.GetForModelFromDB("Sp_OptometristWorker", sql);
            List<DropDownModel> listmodel = new List<DropDownModel>();
            listmodel = repo.DateList(dt);
            return Json(listmodel);
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
