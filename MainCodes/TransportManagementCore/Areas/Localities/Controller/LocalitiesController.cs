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

namespace TransportManagementCore.Areas.Localities.Controller
{
    [Area("Localities")]
    [Route("Localities")]
    public class LocalitiesController : Microsoft.AspNetCore.Mvc.Controller
    {
        LocalitiesRepo repo;
        [Utilities.ViewRightsAuthorizationFilter(FormId = "LocalityEnrollment")]
        [Route("Add/{LocalityAutoId}")]
        public IActionResult Add(int LocalityAutoId = 0)
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["LoginId"] = HttpContext.Session.GetString("LoginId");
            ViewData["LocalityAutoId"] = LocalityAutoId;
            ViewData["Date"] = System.DateTime.Now.ToString("dd") + "-" + System.DateTime.Now.ToString("MMM") + "-" + System.DateTime.Now.ToString("yyyy");
            return View("~/Areas/Localities/Views/Localities/Index.cshtml");
        }
        [HttpPost]
        [Route("SaveUpdate")]
        public async Task<JsonResult> SaveUpdate(LocalitiesModel Model)
        {
            DataTable dt = new DataTable();
            repo = new LocalitiesRepo();
            List<SqlParameter> parameters = null;
            if (Model.LocalityAutoId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("[Sp_SetupLocality]", parameters);
            if (Model.ImageList != null)
            {
                if (Model.LocalityAutoId < 0 || Convert.ToInt16(dt.Rows[0][0].ToString()) > 0)
                {
                    Model.LocalityAutoId = Convert.ToInt16(dt.Rows[0][0].ToString());
                    foreach (LocalityImageModel DetailModel in Model.ImageList.Where(a => a.IsSaved == false && a.LocalityPicture != null))
                    {
                        parameters = null;
                        parameters = SqlPara("Save");
                        if (DetailModel.CaptureDate < Model.EnrollementDate)
                            DetailModel.CaptureDate = Model.EnrollementDate;
                        parameters = repo.SetModelImage(parameters, DetailModel, Model.LocalityAutoId);
                        dt = await repo.DbFunction("[sp_SetupLocalityImage]", parameters);
                    }
                }
            }
            return Json(dt.Rows[0][1].ToString());
        }
        [HttpGet]
        [Route("GetById/{LocalityAutoId}")]
        public async Task<JsonResult> GetById(string LocalityAutoId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new LocalitiesRepo();
            List<SqlParameter> sql = SqlPara("GetLocalityByiD");
            sql.Add(new SqlParameter("@LocalityAutoId", LocalityAutoId));
            dt = await repo.DbFunction("[Sp_SetupLocality]", sql);
            LocalitiesModel model = new LocalitiesModel();
            model = repo.GetModel(dt);
            model.ViewDate = model.EnrollementDate.ToString("dd") + "-" + model.EnrollementDate.ToString("MMM") + "-" + model.EnrollementDate.ToString("yyyy");
            return Json(model);
        }
        [HttpPost]
        [Produces("application/json")]
        [Route("Delete/{LocalityAutoId}")]
        public async Task<JsonResult> Delete(int LocalityAutoId)
        {
            repo = new LocalitiesRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            if (LocalityAutoId > 0)
                parameters.Add(new SqlParameter("@LocalityAutoId", LocalityAutoId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("[Sp_SetupLocality]", parameters);
            return Json(dt.Rows[0][1].ToString());
        }
        [HttpDelete]
        [Route("DeleteByImage/{LocalityImageAutoId}")]
        public async Task<JsonResult> DeleteByImage(int LocalityImageAutoId)
        {
            repo = new LocalitiesRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            parameters.Add(new SqlParameter("@LocalityImageAutoId", LocalityImageAutoId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("[sp_SetupLocalityImage]", parameters);
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
