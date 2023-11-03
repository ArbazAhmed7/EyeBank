using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Localities.Model;
using TransportManagementCore.Areas.Localities.Repositories;
using TransportManagementCore.Models;

namespace TransportManagementCore.Areas.Localities.Controller
{
    [Area("Localities")]
    [Route("Localities/GlassDispenseResident")]
    public class GlassDispenseResidentController : Microsoft.AspNetCore.Mvc.Controller
    {
        GlassDispenseResidentRepo repo;
        [Utilities.ViewRightsAuthorizationFilter(FormId = "LocalityGlassDispenseResident")]
        [Route("Add/{GlassDispenseResidentId}")]
        public IActionResult Add(int GlassDispenseResidentId = 0)
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["GlassDispenseResidentId"] = GlassDispenseResidentId;
            ViewData["Date"] = System.DateTime.Now.ToString("dd") + "-" + System.DateTime.Now.ToString("MMM") + "-" + System.DateTime.Now.ToString("yyyy");
            return View("~/Areas/Localities/Views/GlassDispenseResident/Index.cshtml");
        }

        [HttpGet]
        [Route("GetLastOptoById")]
        public JsonResult GetLastOptoById(GetLastOpto Model)
        {
            //CompanyModel cm = new CompanyModel();
            if (Model.ResidentAutoId > 0)
            {
                DataTable dt = new DataTable();
                repo = new GlassDispenseResidentRepo();

                List<SqlParameter> sql = SqlPara("GetGlassDispenseHistoryByResidentId");
                sql.Add(new SqlParameter("@ResidentAutoId", Model.ResidentAutoId));
                sql.Add(new SqlParameter("@GlassDispenseResidentTransDate", Model.GlassDispenseResidentTransDate));
                dt = repo.GetForModelFromDB("[Sp_GlassDispenseResident]", sql);
                DisplayGlassDispenseResidentModel model = new DisplayGlassDispenseResidentModel();
                model = repo.GetResidentLastHistory(dt);
                return Json(model);
            }
            else
            {
                return Json("");
            }
        }

        [HttpPost]
        [Route("SaveUpdate")]
        public async Task<JsonResult> SaveUpdate(GothGlassDispenseResidentModel Model)
        {

            DataTable dt = new DataTable();
            repo = new GlassDispenseResidentRepo();
            List<SqlParameter> parameters = null;
            if (Model.GlassDispenseResidentId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("[Sp_GlassDispenseResident]", parameters);
            return Json(dt.Rows[0][1].ToString());
        }

        [HttpGet]
        [Route("GetDatesofGlassDispenseResident/{ResidentAutoId}")]
        public JsonResult GetDatesofGlassDispenseResident(string ResidentAutoId)
        {
            DataTable dt = new DataTable();
            repo = new GlassDispenseResidentRepo();

            List<SqlParameter> sql = SqlPara("GetDatesofGlassDispenseResident");
            sql.Add(new SqlParameter("@ResidentAutoId", ResidentAutoId));
            dt = repo.GetForModelFromDB("[Sp_GlassDispenseResident]", sql);
            List<DropDownModel> listmodel = new List<DropDownModel>();
            listmodel = repo.DateList(dt);
            return Json(listmodel);
        }
        [HttpGet]
        [Route("GetGlassDispenseById/{GlassDispenseResidentId}")]
        public JsonResult GetGlassDispenseById(string GlassDispenseResidentId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new GlassDispenseResidentRepo();
            List<SqlParameter> sql = SqlPara("GetById");
            sql.Add(new SqlParameter("@GlassDispenseResidentId", GlassDispenseResidentId));
            dt = repo.GetForModelFromDB("[Sp_GlassDispenseResident]", sql);
            GothGlassDispenseResidentModel model = new GothGlassDispenseResidentModel();
            model = repo.GlassDispenseModel(dt);
            return Json(model);
        }


        [HttpPost]
        [Produces("application/json")]
        [Route("DeleteById/{GlassDispenseResidentId}")]
        public async Task<JsonResult> DeleteById(int GlassDispenseResidentId)
        {
            DataTable dt = new DataTable();
            repo = new GlassDispenseResidentRepo();
            List<SqlParameter> parameters = null;
            if (GlassDispenseResidentId > 0)
            {
                parameters = SqlPara("DeleteById");
                parameters.Add(new SqlParameter("@GlassDispenseResidentId", GlassDispenseResidentId));
            }
            dt = await repo.DbFunction("[Sp_GlassDispenseResident]", parameters);
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
