using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Setup.Model;
using TransportManagementCore.Areas.Setup.Repositories;
using TransportManagementCore.Models;
using TransportManagementCore.Models.DataTables;

namespace TransportManagementCore.Areas.Setup.Controller
{
    [Area("Factory")]
    [Route("Factory/CompanyWorkerEnrollment")]
    public class CompanyWorkerEnrollmentController : Microsoft.AspNetCore.Mvc.Controller
    {
        CompanyWorkerEnrollmentRepo repo;
        [Utilities.ViewRightsAuthorizationFilter(FormId = "WorkerEnrollment")]
        [Route("Add/{WorkerAutoId}")]
        public IActionResult Add(int WorkerAutoId = 0)
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["LoginId"] = HttpContext.Session.GetString("LoginId");
            ViewData["WorkerAutoId"] = WorkerAutoId;
            ViewData["Date"] = System.DateTime.Now.ToString("dd") + "-" + System.DateTime.Now.ToString("MMM") + "-" + System.DateTime.Now.ToString("yyyy");
            return View("~/Areas/Factory/Views/CompanyWorkerEnrollment/Index.cshtml");
        }
        [Route("List")]
        public IActionResult List()
        {
            return View("~/Areas/Factory/Views/CompanyWorkerEnrollment/vwList.cshtml");
        }
        #region comment Code
        //[HttpGet]
        //[Produces("application/json")]
        //[Route("GetWorkerList", Name = "GetWorker")]
        //[Route("GetWorkerList/{searchText}")]
        //public async Task<object> GetWorkerList(string searchText = null)
        //{
        //    //AutherizedFormRights rights = Utilities.General.GetFormRights(HttpContext.Session.GetString("LoginId"), Utilities.Constraints.Admission);
        //    int[] columnsToHide = new int[] { 0 };
        //    repo = new CompanyWorkerEnrollmentRepo();
        //    List<ActionButton> actionButtons = new List<ActionButton>();
        //    //if (rights.IsEditRights)
        //    //{
        //    actionButtons.Add(ActionButton.Edit);
        //    actionButtons.Add(ActionButton.Delete);
        //    //}
        //    //AutherizedFormRights rightsDischarge = Utilities.General.GetFormRights(HttpContext.Session.GetString("LoginId"), Utilities.Constraints.AdmissionDischarge);
        //    //if (rightsDischarge.IsAddRights)
        //    //    actionButtons.Add(ActionButton.Discharge);

        //    //Global.CurrentUser.BranchId = Convert.ToInt32(HttpContext.Session.GetString("BranchId"));
        //    List<SqlParameter> parameters = null;
        //    if (searchText != null && searchText != "")
        //    {
        //        parameters = SqlPara("Search");
        //        parameters.Add(new SqlParameter("@SearchText", searchText));
        //    }
        //    else
        //        parameters = SqlPara("GetAllWorker");
        //    return Utilities.DataTables.DataTableSource(await repo.DbFunction("[Sp_CompanyWorker]", parameters), columnsToHide, actionButtons);
        //}
        #endregion
        [HttpGet]
        [Route("GetById/{WorkerAutoId}")]
        public async Task<JsonResult> GetById(string WorkerAutoId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new CompanyWorkerEnrollmentRepo();
            List<SqlParameter> sql = SqlPara("GetWorkerByiD");
            sql.Add(new SqlParameter("@WorkerAutoId", WorkerAutoId));
            dt = await repo.DbFunction("Sp_CompanyWorker", sql);
            CompanyWorkerEnrollmentModel model = new CompanyWorkerEnrollmentModel();
            model = repo.GetModel(dt);
            model.ViewDate = model.EnrollementDate.ToString("dd") + "-" + model.EnrollementDate.ToString("MMM") + "-" + model.EnrollementDate.ToString("yyyy");
            return Json(model);
        }
        [HttpPost]
        [Route("SaveUpdate")]
        public async Task<JsonResult> SaveUpdate(CompanyWorkerEnrollmentModel Model)
        {
            DataTable dt = new DataTable();
            repo = new CompanyWorkerEnrollmentRepo();
            List<SqlParameter> parameters = null;
            if (Model.WorkerAutoId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("Sp_CompanyWorker", parameters);
            if (Model.ImageList != null)
            {
                if (Model.WorkerAutoId < 0 || Convert.ToInt16(dt.Rows[0][0].ToString()) > 0)
                {
                    Model.WorkerAutoId = Convert.ToInt16(dt.Rows[0][0].ToString());
                    foreach (CompanyWorkerEnrollmentImageModel DetailModel in Model.ImageList.Where(a => a.IsSaved == false && a.WorkerPicture != null))
                    {
                        DataTable dataTable = new DataTable();
                        parameters = null;
                        parameters = SqlPara("Save");
                        Model.CompanyAutoId = Convert.ToInt32(Model.CompanyCode);
                        parameters = repo.SetModelImage(parameters, DetailModel, Model.WorkerAutoId, Model.CompanyAutoId);
                        dataTable = await repo.DbFunction("sp_SetupCompanyWorkerImage", parameters);
                        if (!(Convert.ToInt32(dataTable.Rows[0][0].ToString()) > 0 && dataTable.Rows[0][1].ToString().ToLower().Contains("successfully"))) {
                            dt.Rows[0][1] = "Failed to Saved Image";
                        }
                    }
                }
            }
            return Json(dt.Rows[0][1].ToString());
        }
        [HttpPost]
        [Produces("application/json")]
        [Route("DeleteById/{WorkerAutoId}")]
        public async Task<JsonResult> DeleteById(int WorkerAutoId)
        {
            DataTable dt = new DataTable();
            repo = new CompanyWorkerEnrollmentRepo();
            List<SqlParameter> parameters = null;
            if (WorkerAutoId > 0)
            {
                parameters = SqlPara("DeleteWorkerById");
                parameters.Add(new SqlParameter("@WorkerAutoId", WorkerAutoId));
            }
            dt = await repo.DbFunction("[Sp_CompanyWorker]", parameters);
            return Json(dt.Rows[0][1].ToString());

        }

        [HttpDelete]
        [Route("Delete/{WorkerAutoId}")]
        public async Task<JsonResult> Delete(string WorkerAutoId)
        {
            repo = new CompanyWorkerEnrollmentRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            parameters.Add(new SqlParameter("@WorkerAutoId", WorkerAutoId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("Sp_CompanyWorker", parameters);
            return Json(dt.Rows[0][1].ToString());
        }

        [HttpPost]
        [Route("DeleteByImage/{WorkerImageAutoId}")]
        public async Task<JsonResult> DeleteByImage(int WorkerImageAutoId)
        {
            repo = new CompanyWorkerEnrollmentRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            parameters.Add(new SqlParameter("@WorkerImageAutoId", WorkerImageAutoId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("[sp_SetupCompanyWorkerImage]", parameters);
            return Json(dt.Rows[0][1].ToString());
        }

        [HttpGet]
        [Route("GetCode/{CompanyCode}")]
        public async Task<JsonResult> GetCode(string CompanyCode)
        {
            repo = new CompanyWorkerEnrollmentRepo();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@operation", "Code"));
            parameters.Add(new SqlParameter("@CodeType", "CompanyWorker"));
            parameters.Add(new SqlParameter("@CodeLength", 4));
            parameters.Add(new SqlParameter("@PreFix", "04"));
            parameters.Add(new SqlParameter("@CompanyCode", CompanyCode));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("[Sp_GetCode]", parameters);
            return Json(dt.Rows[0][0].ToString());
        }

        [Route("DropDown/{Type}")]
        public async Task<List<DropDownModel>> DropDown(string Type)
        {
            repo = new CompanyWorkerEnrollmentRepo();
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@DropDownType", Type));
            DataTable dt = await repo.DbFunction("Sp_DropDownLookUp", para);
            List<DropDownModel> list = repo.List(dt);
            return list;
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
