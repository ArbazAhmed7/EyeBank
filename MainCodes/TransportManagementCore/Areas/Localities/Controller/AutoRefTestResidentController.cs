using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data; 
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Localities.Model;
using TransportManagementCore.Areas.Localities.Repositories;
using TransportManagementCore.Models;

namespace TransportManagementCore.Areas.Localities.Controller
{
    [Area("Localities")]
    [Route("Localities/AutoRefTestResident")]
    public class AutoRefTestResidentController : Microsoft.AspNetCore.Mvc.Controller
    {
        AutoRefTestResident repo;
        [Utilities.ViewRightsAuthorizationFilter(FormId = "LocalityAutoRefractionResident")]
        [Route("Add/{ResidentAutoId}")]
        public IActionResult Add(int ResidentAutoId=0)
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["ResidentAutoId"] = ResidentAutoId;
            ViewData["Date"] = System.DateTime.Now.ToString("dd") + "-" + System.DateTime.Now.ToString("MMM") + "-" + System.DateTime.Now.ToString("yyyy");
            return View("~/Areas/Localities/Views/AutoRefTestResident/Index.cshtml");
        }
        [HttpGet]
        [Route("GetById/{ResidentId}")]
        public JsonResult GetById(string ResidentId)
        
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new AutoRefTestResident();

            List<SqlParameter> sql = SqlPara("GetResidentByiD");
            sql.Add(new SqlParameter("@ResidentAutoId", ResidentId));
            dt = repo.GetForModelFromDB("[Sp_LocalityResident]", sql);
            LocalityResidentEnrollment model = new LocalityResidentEnrollment();
            model = repo.GetModel(dt);
            model.ViewDate = model.EnrollementDate.ToString("dd") + "-" + model.EnrollementDate.ToString("MMM") + "-" + model.EnrollementDate.ToString("yyyy");
            return Json(model);
        }

        [HttpPost]
        [Route("SaveUpdate")]
        public async Task<JsonResult> SaveUpdate(AutoRefTestResidentModel Model)
        {

            DataTable dt = new DataTable();
            repo = new AutoRefTestResident();
            List<SqlParameter> parameters = null;
            if (Model.AutoRefResidentId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("[Sp_AutoRefTestResident]", parameters);
            return Json(dt.Rows[0][1].ToString());
        }

        [HttpGet]
        [Route("GetDatesofResident/{ResidentAutoId}")]
        public JsonResult GetDatesofResident(string ResidentAutoId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new AutoRefTestResident();

            List<SqlParameter> sql = SqlPara("GetDatesofResident");
            sql.Add(new SqlParameter("@ResidentAutoId", ResidentAutoId));
            dt = repo.GetForModelFromDB("[Sp_AutoRefTestResident]", sql);
            List<DropDownModel> listmodel = new List<DropDownModel>();
            listmodel = repo.DateList(dt);
            return Json(listmodel);
        }

        [HttpGet]
        [Route("GetLastHistoryById/{ResidentAutoId}")]
        public JsonResult GetLastHistoryById(string ResidentAutoId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new AutoRefTestResident();

            List<SqlParameter> sql = SqlPara("GetAutoRefHistoryByResidentId");
            sql.Add(new SqlParameter("@ResidentAutoId", ResidentAutoId));
            dt = repo.GetForModelFromDB("[Sp_AutoRefTestResident]", sql);
            DisplayAutoRefResidentModel model = new DisplayAutoRefResidentModel();
            model = repo.GetResidentLastHistory(dt);
            return Json(model);
        }
        [HttpGet]
        [Route("GetAutoRefById/{AutoRefResidentId}")]
        public JsonResult GetAutoRefById(string AutoRefResidentId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new AutoRefTestResident();
            List<SqlParameter> sql = SqlPara("GetByAutoRefResidentId");
            sql.Add(new SqlParameter("@AutoRefResidentId", AutoRefResidentId));
            dt = repo.GetForModelFromDB("Sp_AutoRefTestResident", sql);
            AutoRefTestResidentModel model = new AutoRefTestResidentModel();
            model = repo.AutoRefModel(dt);
            return Json(model);
        }
        [HttpGet]
        [Route("GetAutoRefByResidentId/{ResidentId}")]
        public JsonResult GetAutoRefByResidentId(string ResidentId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new AutoRefTestResident();
            List<SqlParameter> sql = SqlPara("GetByAutoRefResidentIdForOpt");
            sql.Add(new SqlParameter("@ResidentAutoId", ResidentId));
            dt = repo.GetForModelFromDB("Sp_AutoRefTestResident", sql);
            AutoRefTestResidentModel model = new AutoRefTestResidentModel();
            model = repo.AutoRefModel(dt);
            return Json(model);
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("DeleteById/{AutoResidentId}")]
        public async Task<JsonResult> DeleteById(int AutoResidentId)
        {
            DataTable dt = new DataTable();
            repo = new AutoRefTestResident();
            List<SqlParameter> parameters = null;
            if (AutoResidentId > 0)
            {
                parameters = SqlPara("DeleteAutoRefById");
                parameters.Add(new SqlParameter("@AutoRefResidentId", AutoResidentId));
            }
            dt = await repo.DbFunction("[Sp_AutoRefTestResident]", parameters);
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
  