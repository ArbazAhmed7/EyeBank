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

namespace TransportManagementCore.Areas.PublicSpaces.Controller
{

    [Area("PublicSpaces")]
    [Route("PublicSpaces")]
    public class PublicSpacesController : Microsoft.AspNetCore.Mvc.Controller
    {
        PublicSpacesRepo repo;
        [Utilities.ViewRightsAuthorizationFilter(FormId = "PublicSpacesEnrollment")]
        [Route("Add/{PublicSpacesAutoId}")]
        public IActionResult Add(int PublicSpacesAutoId = 0)
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["LoginId"] = HttpContext.Session.GetString("LoginId");
            ViewData["PublicSpacesAutoId"] = PublicSpacesAutoId;
            ViewData["Date"] = System.DateTime.Now.ToString("dd") + "-" + System.DateTime.Now.ToString("MMM") + "-" + System.DateTime.Now.ToString("yyyy");
            return View("~/Areas/PublicSpaces/Views/PublicSpaces/Index.cshtml");
        }
        [HttpPost]
        [Route("SaveUpdate")]
        public async Task<JsonResult> SaveUpdate(PublicSpacesModel Model)
        {
            DataTable dt = new DataTable();
            repo = new PublicSpacesRepo();
            List<SqlParameter> parameters = null;
            if (Model.PublicSpacesAutoId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("[Sp_SetupPublicSpaces]", parameters);
            if (Model.ImageList != null)
            {
                if (Model.PublicSpacesAutoId < 0 || Convert.ToInt16(dt.Rows[0][0].ToString()) > 0)
                {
                    Model.PublicSpacesAutoId = Convert.ToInt16(dt.Rows[0][0].ToString());
                    foreach (PublicSpacesImageModel DetailModel in Model.ImageList.Where(a => a.IsSaved == false && a.PublicSpacesPicture != null))
                    {
                        parameters = null;
                        parameters = SqlPara("Save");
                        if (DetailModel.CaptureDate < Model.EnrollementDate)
                            DetailModel.CaptureDate = Model.EnrollementDate;
                        parameters = repo.SetModelImage(parameters, DetailModel, Model.PublicSpacesAutoId);
                        dt = await repo.DbFunction("[sp_SetupPublicSpacesImage]", parameters);
                    }
                }
            }
            return Json(dt.Rows[0][1].ToString());
        }
        [HttpGet]
        [Route("GetById/{PublicSpacesAutoId}")]
        public async Task<JsonResult> GetById(string PublicSpacesAutoId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new PublicSpacesRepo();
            List<SqlParameter> sql = SqlPara("GetPublicSpacesByiD");
            sql.Add(new SqlParameter("@PublicSpacesAutoId", PublicSpacesAutoId));
            dt = await repo.DbFunction("[Sp_SetupPublicSpaces]", sql);
            PublicSpacesModel model = new PublicSpacesModel();
            model = repo.GetModel(dt);
            model.ViewDate = model.EnrollementDate.ToString("dd") + "-" + model.EnrollementDate.ToString("MMM") + "-" + model.EnrollementDate.ToString("yyyy");
            return Json(model);
        }
        [HttpPost]
        [Produces("application/json")]
        [Route("Delete/{PublicSpacesAutoId}")]
        public async Task<JsonResult> Delete(int PublicSpacesAutoId)
        {
            repo = new PublicSpacesRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            if (PublicSpacesAutoId > 0)
                parameters.Add(new SqlParameter("@PublicSpacesAutoId", PublicSpacesAutoId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("[Sp_SetupPublicSpaces]", parameters);
            return Json(dt.Rows[0][1].ToString());
        }
        [HttpDelete]
        [Route("DeleteByImage/{PublicSpacesImageAutoId}")]
        public async Task<JsonResult> DeleteByImage(int PublicSpacesImageAutoId)
        {
            repo = new PublicSpacesRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            parameters.Add(new SqlParameter("@PublicSpacesImageAutoId", PublicSpacesImageAutoId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("[Sp_SetupPublicSpacesImage]", parameters);
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
