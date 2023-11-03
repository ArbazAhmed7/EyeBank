using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.PublicSpaces.Model;
using TransportManagementCore.Areas.PublicSpaces.Repositories;
using TransportManagementCore.Models;

namespace TransportManagementCore.Areas.PublicSpaces.Controller
{
    [Area("PublicSpaces")]
    [Route("PublicSpaces/GlassDispenseResident")]
    public class PublicSpacesGlassDispenseResidentController : Microsoft.AspNetCore.Mvc.Controller
    {
        PublicSpacesGlassDispenseResidentRepo repo;
        [Utilities.ViewRightsAuthorizationFilter(FormId = "PublicSpacesGlassDispenseResident")]
        [Route("Add/{PublicSpacesGlassDispenseResidentId}")]
        public IActionResult Add(int PublicSpacesGlassDispenseResidentId = 0)
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["PublicSpacesGlassDispenseResidentId"] = PublicSpacesGlassDispenseResidentId;
            ViewData["Date"] = System.DateTime.Now.ToString("dd") + "-" + System.DateTime.Now.ToString("MMM") + "-" + System.DateTime.Now.ToString("yyyy");
            return View("~/Areas/PublicSpaces/Views/PublicSpacesGlassDispenseResident/Index.cshtml");
        }

        [HttpGet]
        [Route("GetLastOptoById")]
        public JsonResult GetLastOptoById(GetLastOpto Model)
        {
            //CompanyModel cm = new CompanyModel();
            if (Model.ResidentAutoId > 0)
            {
                DataTable dt = new DataTable();
                repo = new PublicSpacesGlassDispenseResidentRepo();

                List<SqlParameter> sql = SqlPara("GetGlassDispenseHistoryByResidentId");
                sql.Add(new SqlParameter("@ResidentAutoId", Model.ResidentAutoId));
                sql.Add(new SqlParameter("@GlassDispenseResidentTransDate", Model.GlassDispenseResidentTransDate));
                dt = repo.GetForModelFromDB("[Sp_PublicSpacesGlassDispenseResident]", sql);
                DisplayPublicSpacesGlassDispenseResidentModel model = new DisplayPublicSpacesGlassDispenseResidentModel();
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
        public async Task<JsonResult> SaveUpdate(PublicSpacesGlassDispenseResidentModel Model)
        {

            DataTable dt = new DataTable();
            repo = new PublicSpacesGlassDispenseResidentRepo();
            List<SqlParameter> parameters = null;
            if (Model.PublicSpacesGlassDispenseResidentId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("[Sp_PublicSpacesGlassDispenseResident]", parameters);
            return Json(dt.Rows[0][1].ToString());
        }

        [HttpGet]
        [Route("GetDatesofGlassDispenseResident/{ResidentAutoId}")]
        public JsonResult GetDatesofGlassDispenseResident(string ResidentAutoId)
        {
            DataTable dt = new DataTable();
            repo = new PublicSpacesGlassDispenseResidentRepo();

            List<SqlParameter> sql = SqlPara("GetDatesofGlassDispenseResident");
            sql.Add(new SqlParameter("@ResidentAutoId", ResidentAutoId));
            dt = repo.GetForModelFromDB("[Sp_PublicSpacesGlassDispenseResident]", sql);
            List<DropDownModel> listmodel = new List<DropDownModel>();
            listmodel = repo.DateList(dt);
            return Json(listmodel);
        }
        [HttpGet]
        [Route("GetGlassDispenseById/{PublicSpacesGlassDispenseResidentId}")]
        public JsonResult GetGlassDispenseById(string PublicSpacesGlassDispenseResidentId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new PublicSpacesGlassDispenseResidentRepo();
            List<SqlParameter> sql = SqlPara("GetById");
            sql.Add(new SqlParameter("@PublicSpacesGlassDispenseResidentId", PublicSpacesGlassDispenseResidentId));
            dt = repo.GetForModelFromDB("[Sp_PublicSpacesGlassDispenseResident]", sql);
            PublicSpacesGlassDispenseResidentModel model = new PublicSpacesGlassDispenseResidentModel();
            model = repo.GlassDispenseModel(dt);
            return Json(model);
        }


        [HttpPost]
        [Produces("application/json")]
        [Route("DeleteById/{PublicSpacesGlassDispenseResidentId}")]
        public async Task<JsonResult> DeleteById(int PublicSpacesGlassDispenseResidentId)
        {
            DataTable dt = new DataTable();
            repo = new PublicSpacesGlassDispenseResidentRepo();
            List<SqlParameter> parameters = null;
            if (PublicSpacesGlassDispenseResidentId > 0)
            {
                parameters = SqlPara("DeleteById");
                parameters.Add(new SqlParameter("@PublicSpacesGlassDispenseResidentId", PublicSpacesGlassDispenseResidentId));
            }
            dt = await repo.DbFunction("[Sp_PublicSpacesGlassDispenseResident]", parameters);
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
