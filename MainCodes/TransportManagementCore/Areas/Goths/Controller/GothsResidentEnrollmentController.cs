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
    [Route("Goths/GothsResidentEnrollment")]
    public class GothsResidentEnrollmentController : Microsoft.AspNetCore.Mvc.Controller
    {
        GothsResidentEnrollmentRepo repo;
        [Utilities.ViewRightsAuthorizationFilter(FormId = "GothResidentEnrollment")]
        [Route("Add/{GothAutoId}")]
        public IActionResult Index(string ResidentId)
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["LoginId"] = HttpContext.Session.GetString("LoginId");
            ViewData["ResidentAutoId"] = ResidentId;
            ViewData["Date"] = System.DateTime.Now.ToString("dd") + "-" + System.DateTime.Now.ToString("MMM") + "-" + System.DateTime.Now.ToString("yyyy");
            return View("~/Areas/Goths/Views/GothsResidentEnrollment/Index.cshtml");
        }
        [HttpPost]
        [Route("SaveUpdate")]
        public async Task<JsonResult> SaveUpdate(GothResidentEnrollmentModel Model)
        {
            DataTable dt = new DataTable();
            repo = new GothsResidentEnrollmentRepo();
            List<SqlParameter> parameters = null;
            if (Model.ResidentAutoId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("[Sp_GothsResident]", parameters);
            if (Model.ImageList != null)
            {
                if (Model.ResidentAutoId < 0 || Convert.ToInt16(dt.Rows[0][0].ToString()) > 0)
                {
                    Model.ResidentAutoId = Convert.ToInt16(dt.Rows[0][0].ToString());
                    foreach (GothResidentEnrollmentImageModel DetailModel in Model.ImageList.Where(a => a.IsSaved == false && a.ResidentPicture != null))
                    {
                        DataTable dataTable = new DataTable();
                        parameters = null;
                        parameters = SqlPara("Save");
                        if (DetailModel.CaptureDate < Model.EnrollementDate)
                            DetailModel.CaptureDate = Model.EnrollementDate;
                        parameters = repo.SetModelImage(parameters, DetailModel, Model.ResidentAutoId, Model.GothAutoId);
                        dataTable = await repo.DbFunction("[Sp_GothsResidentImage]", parameters);
                        if (!(Convert.ToInt32(dataTable.Rows[0][0].ToString()) > 0 && dataTable.Rows[0][1].ToString().ToLower().Contains("successfully")))
                        {
                            dt.Rows[0][1] = "Failed to Saved Image";
                        }
                      
                    }
                }
            }
            return Json(dt.Rows[0][1].ToString());
        }
        [HttpGet]
        [Route("GetById/{ResidentAutoId}")]
        public async Task<JsonResult> GetById(string ResidentAutoId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new GothsResidentEnrollmentRepo();
            List<SqlParameter> sql = SqlPara("GetResidentByiD");
            sql.Add(new SqlParameter("@ResidentAutoId", ResidentAutoId));
            dt = await repo.DbFunction("[Sp_GothsResident]", sql);
            GothResidentEnrollmentModel model = new GothResidentEnrollmentModel();
            model = repo.GetModel(dt);
            model.ViewDate = model.EnrollementDate.ToString("dd") + "-" + model.EnrollementDate.ToString("MMM") + "-" + model.EnrollementDate.ToString("yyyy");
            return Json(model);
        }
        [HttpPost]
        [Produces("application/json")]
        [Route("Delete/{ResidentAutoId}")]
        public async Task<JsonResult> Delete(int ResidentAutoId)
        {
            repo = new GothsResidentEnrollmentRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            if (ResidentAutoId > 0)
                parameters.Add(new SqlParameter("@ResidentAutoId", ResidentAutoId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("[Sp_GothsResident]", parameters);
            return Json(dt.Rows[0][1].ToString());
        }
        [HttpDelete]
        [Route("DeleteByImage/{ResidentImageAutoId}")]
        public async Task<JsonResult> DeleteByImage(int ResidentImageAutoId)
        {
            repo = new GothsResidentEnrollmentRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            parameters.Add(new SqlParameter("@ResidentImageAutoId", ResidentImageAutoId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("[Sp_GothsResidentImage]", parameters);
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
