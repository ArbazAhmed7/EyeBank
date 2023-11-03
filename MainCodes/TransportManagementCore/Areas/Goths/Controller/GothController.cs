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

namespace TransportManagementCore.Areas.Goths.Controller
{ 
    [Area("Goths")]
    [Route("Goths")]
    public class GothController : Microsoft.AspNetCore.Mvc.Controller
    {
        GothRepo repo;
        [Utilities.ViewRightsAuthorizationFilter(FormId = "GothEnrollment")]
        [Route("Add/{GothAutoId}")]
        public IActionResult Add(int GothAutoId = 0)
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["LoginId"] = HttpContext.Session.GetString("LoginId");
            ViewData["GothAutoId"] = GothAutoId;
            ViewData["Date"] = System.DateTime.Now.ToString("dd") + "-" + System.DateTime.Now.ToString("MMM") + "-" + System.DateTime.Now.ToString("yyyy");
            return View("~/Areas/Goths/Views/Goth/Index.cshtml");
        }
        [HttpPost]
        [Route("SaveUpdate")]
        public async Task<JsonResult> SaveUpdate(GothModel Model)
        {
            DataTable dt = new DataTable();
            repo = new GothRepo();
            List<SqlParameter> parameters = null;
            if (Model.GothAutoId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("[Sp_SetupGoths]", parameters);
            if (Model.ImageList != null)
            {
                if (Model.GothAutoId < 0 || Convert.ToInt16(dt.Rows[0][0].ToString()) > 0)
                {
                    Model.GothAutoId = Convert.ToInt16(dt.Rows[0][0].ToString());
                    foreach (GothImageModel DetailModel in Model.ImageList.Where(a => a.IsSaved == false && a.GothPicture != null))
                    {
                        parameters = null;
                        parameters = SqlPara("Save");
                        if (DetailModel.CaptureDate < Model.EnrollementDate)
                            DetailModel.CaptureDate = Model.EnrollementDate;
                        parameters = repo.SetModelImage(parameters, DetailModel, Model.GothAutoId);
                        dt = await repo.DbFunction("[sp_SetupGothsImage]", parameters);
                    }
                }
            }
            return Json(dt.Rows[0][1].ToString());
        }
        [HttpGet]
        [Route("GetById/{GothAutoId}")]
        public async Task<JsonResult> GetById(string GothAutoId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new GothRepo();
            List<SqlParameter> sql = SqlPara("GetGothByiD");
            sql.Add(new SqlParameter("@GothAutoId", GothAutoId));
            dt = await repo.DbFunction("[Sp_SetupGoths]", sql);
            GothModel model = new GothModel();
            model = repo.GetModel(dt);
            model.ViewDate = model.EnrollementDate.ToString("dd") + "-" + model.EnrollementDate.ToString("MMM") + "-" + model.EnrollementDate.ToString("yyyy");
            return Json(model);
        }
        [HttpPost]
        [Produces("application/json")]
        [Route("Delete/{GothAutoId}")]
        public async Task<JsonResult> Delete(int GothAutoId)
        {
            repo = new GothRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            if (GothAutoId > 0)
                parameters.Add(new SqlParameter("@GothAutoId", GothAutoId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("[Sp_SetupGoths]", parameters);
            return Json(dt.Rows[0][1].ToString());
        }
        [HttpPost]
        [Route("DeleteByImage/{GothImageAutoId}")]
        public async Task<JsonResult> DeleteByImage(int GothImageAutoId)
        {
            repo = new GothRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            parameters.Add(new SqlParameter("@GothImageAutoId", GothImageAutoId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("[Sp_SetupGothsImage]", parameters);
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
