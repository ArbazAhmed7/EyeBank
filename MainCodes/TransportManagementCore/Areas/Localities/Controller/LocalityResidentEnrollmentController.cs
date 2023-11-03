﻿using Microsoft.AspNetCore.Http;
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
    [Route("Localities/LocalityResidentEnrollment")]
    public class LocalityResidentEnrollmentController : Microsoft.AspNetCore.Mvc.Controller
    {
        LocalityResidentEnrollmentRepo repo;
        [Utilities.ViewRightsAuthorizationFilter(FormId = "LocalityResidentEnrollment")]
        [Route("Add/{LocalityAutoId}")]
        public IActionResult Index(string ResidentId)
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["LoginId"] = HttpContext.Session.GetString("LoginId");
            ViewData["ResidentAutoId"] = ResidentId;
            ViewData["Date"] = System.DateTime.Now.ToString("dd") + "-" + System.DateTime.Now.ToString("MMM") + "-" + System.DateTime.Now.ToString("yyyy");
            return View("~/Areas/Localities/Views/LocalityResidentEnrollment/Index.cshtml");
        }
        [HttpPost]
        [Route("SaveUpdate")]
        public async Task<JsonResult> SaveUpdate(LocalityResidentEnrollment Model)
        {
            DataTable dt = new DataTable();
            repo = new LocalityResidentEnrollmentRepo();
            List<SqlParameter> parameters = null;
            if (Model.ResidentAutoId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("[Sp_LocalityResident]", parameters);
            if (Model.ImageList != null)
            {
                if (Model.ResidentAutoId < 0 || Convert.ToInt16(dt.Rows[0][0].ToString()) > 0)
                {
                    Model.ResidentAutoId = Convert.ToInt16(dt.Rows[0][0].ToString());
                    foreach (LocalityResidentEnrollmentImageModel DetailModel in Model.ImageList.Where(a => a.IsSaved == false && a.ResidentPicture != null))
                    {
                        DataTable dataTable = new DataTable();
                        parameters = null;
                        parameters = SqlPara("Save");
                        if (DetailModel.CaptureDate < Model.EnrollementDate)
                            DetailModel.CaptureDate = Model.EnrollementDate;
                        parameters = repo.SetModelImage(parameters, DetailModel, Model.ResidentAutoId, Model.LocalityAutoId);
                        dataTable = await repo.DbFunction("[sp_LocalityResidentImage]", parameters);
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
                repo = new LocalityResidentEnrollmentRepo();
                List<SqlParameter> sql = SqlPara("GetResidentByiD");
                sql.Add(new SqlParameter("@ResidentAutoId", ResidentAutoId));
                dt = await repo.DbFunction("[Sp_LocalityResident]", sql);
                LocalityResidentEnrollment model = new LocalityResidentEnrollment();
                model = repo.GetModel(dt);
            model.ViewDate = model.EnrollementDate.ToString("dd") + "-" + model.EnrollementDate.ToString("MMM") + "-" + model.EnrollementDate.ToString("yyyy");
            return Json(model);
            }
        [HttpPost]
        [Produces("application/json")]
        [Route("Delete/{ResidentAutoId}")]
        public async Task<JsonResult> Delete(int ResidentAutoId)
        {
            repo = new LocalityResidentEnrollmentRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            if (ResidentAutoId > 0)
                parameters.Add(new SqlParameter("@ResidentAutoId", ResidentAutoId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("[Sp_LocalityResident]", parameters);
            return Json(dt.Rows[0][1].ToString());
        }
        [HttpDelete]
        [Route("DeleteByImage/{ResidentImageAutoId}")]
        public async Task<JsonResult> DeleteByImage(int ResidentImageAutoId)
        {
            repo = new LocalityResidentEnrollmentRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            parameters.Add(new SqlParameter("@ResidentImageAutoId", ResidentImageAutoId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("[sp_LocalityResidentImage]", parameters);
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
