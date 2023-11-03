using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Goths.Model;
using TransportManagementCore.Areas.Goths.Repositories;
using TransportManagementCore.Models;

namespace TransportManagementCore.Areas.Goths.Controller
{
    [Area("Goths")]
    [Route("Goths/GothOptometristResident")]
    public class GothOptometristResidentController : Microsoft.AspNetCore.Mvc.Controller
    {
        OptometristGothResidentRepo repo;
        [Utilities.ViewRightsAuthorizationFilter(FormId = "GothOptometristResident")]
        [Route("Add/{ResidentAutoId}")]
        public IActionResult Add(int ResidentAutoId = 0)
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["LoginId"] = HttpContext.Session.GetString("LoginId");
            ViewData["ResidentAutoId"] = ResidentAutoId;
            ViewData["ViewDate"] = DateTime.Now.Date.ToString("dd") + " | " + DateTime.Now.Date.ToString("MMM") + " | " + DateTime.Now.Date.ToString("yyyy");
            return View("~/Areas/Goths/Views/GothOptometristResident/Index.cshtml");
        }

        [HttpGet]
        [Route("GetOptometristResidentById/{OptometristResidentId}")]
        public JsonResult GetOptometristResidentById(string OptometristResidentId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new OptometristGothResidentRepo();
            List<SqlParameter> sql = SqlPara("GetById");
            sql.Add(new SqlParameter("@OptometristGothResidentId", OptometristResidentId));
            dt = repo.GetForModelFromDB("[Sp_OptometristGothResident]", sql);
            OptometristGothResidentModel model = new OptometristGothResidentModel();
            model = repo.GetOptometristResidentLast(dt);
            return Json(model);
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("SaveUpdate")]
        public async Task<JsonResult> SaveUpdate(OptometristGothResidentModel Model)
        {
            DataTable dt = new DataTable();
            repo = new OptometristGothResidentRepo();
            List<SqlParameter> parameters = null;
            if (Model.OptometristGothResidentId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("[Sp_OptometristGothResident]", parameters);
            return Json(dt.Rows[0][1].ToString());

        }
        //[Utilities.ViewRightsAuthorizationFilter(FormId = "AutoRefractionResident")]


        [HttpPost]
        [Produces("application/json")]
        [Route("DeleteById/{OptoResidentId}")]
        public async Task<JsonResult> DeleteById(int OptoResidentId)
        {
            DataTable dt = new DataTable();
            repo = new OptometristGothResidentRepo();
            List<SqlParameter> parameters = null;
            if (OptoResidentId > 0)
            {
                parameters = SqlPara("DeleteOptometristById");
                parameters.Add(new SqlParameter("@OptometristResidentId", OptoResidentId));
            }
            dt = await repo.DbFunction("[Sp_OptometristGothResident]", parameters);
            return Json(dt.Rows[0][1].ToString());

        }

        [HttpGet]
        [Route("GetDatesofResident/{ResidentAutoId}")]
        public JsonResult GetDatesofResident(string ResidentAutoId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new OptometristGothResidentRepo();

            List<SqlParameter> sql = SqlPara("GetDatesofResident");
            sql.Add(new SqlParameter("@ResidentAutoId", ResidentAutoId));
            dt = repo.GetForModelFromDB("Sp_OptometristGothResident", sql);
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
