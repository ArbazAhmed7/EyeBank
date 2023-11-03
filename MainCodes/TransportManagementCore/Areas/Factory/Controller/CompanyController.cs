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
using TransportManagementCore.Models.DataTables;
using TransportManagementCore.Utilities;

namespace TransportManagementCore.Areas.Setup.Controller
{
    [Area("Factory")]
    [Route("Factory/Company")]
    public class CompanyController : Microsoft.AspNetCore.Mvc.Controller
    {

        CompanyRepo repo;
        [Utilities.ViewRightsAuthorizationFilter(FormId = "CompanyEnrollment")]
        [Route("Add/{CompanyAutoId}")]
        public IActionResult Add(int CompanyAutoId = 0)
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["LoginId"] = HttpContext.Session.GetString("LoginId");
            ViewData["CompanyAutoId"] = CompanyAutoId;
            ViewData["Date"] = System.DateTime.Now.ToString("dd")+"-"+ System.DateTime.Now.ToString("MMM")+"-"+ System.DateTime.Now.ToString("yyyy");
            return View("~/Areas/Factory/Views/Company/Index.cshtml");
        }
        [Route("List")]
        public IActionResult List()
        {
            string userid = HttpContext.Session.GetString("LoginId");
            ViewData["LoginId"] = HttpContext.Session.GetString("LoginId");
            return View("~/Areas/Factory/Views/Company/vwList.cshtml");
        }
        #region FOR list
        [HttpGet]
        [Produces("application/json")]
        [Route("GetCompanyList", Name = "GetCompany")]
        [Route("GetCompanyList/{searchText}")]
        public async Task<object> GetCompanyList(string searchText = null)
        {
            //AutherizedFormRights rights = Utilities.General.GetFormRights(HttpContext.Session.GetString("LoginId"), Utilities.Constraints.Admission);
            int[] columnsToHide = new int[] { 0 };
            repo = new CompanyRepo();
            List<ActionButton> actionButtons = new List<ActionButton>();
            //if (rights.IsEditRights)
            //{
            actionButtons.Add(ActionButton.Edit);
            actionButtons.Add(ActionButton.Delete);
            //}
            //AutherizedFormRights rightsDischarge = Utilities.General.GetFormRights(HttpContext.Session.GetString("LoginId"), Utilities.Constraints.AdmissionDischarge);
            //if (rightsDischarge.IsAddRights)
            //    actionButtons.Add(ActionButton.Discharge);

            //Global.CurrentUser.BranchId = Convert.ToInt32(HttpContext.Session.GetString("BranchId"));
            List<SqlParameter> parameters = null;
            if (searchText != null && searchText != "")
            {
                parameters = SqlPara("Search");
                parameters.Add(new SqlParameter("@SearchText", searchText));
            }
            else
                parameters = SqlPara("GetAllCompany");
            return Utilities.DataTables.DataTableSource(await repo.DbFunction("Sp_SetupCompany", parameters), columnsToHide, actionButtons);
        }
        #endregion

        [HttpGet]
        [Route("GetById/{CompanyAutoId}")]
        public async Task<JsonResult> GetById(string CompanyAutoId)
        {
            //CompanyModel cm = new CompanyModel();
            DataTable dt = new DataTable();
            repo = new CompanyRepo();
            List<SqlParameter> sql = SqlPara("GetCompanyById");
            sql.Add(new SqlParameter("@CompanyAutoId", CompanyAutoId));
            dt = await repo.DbFunction("Sp_SetupCompany", sql);
            CompanyModel model = new CompanyModel();
            model = repo.GetModel(dt);
            model.ViewDate = model.EnrollementDate.ToString("dd") + "-" + model.EnrollementDate.ToString("MMM") + "-" + model.EnrollementDate.ToString("yyyy");
            return Json(model);
        }

        [HttpPost]
        [Route("SaveUpdate")]
        public async Task<JsonResult> SaveUpdate(CompanyModel Model)
        {
            DataTable dt = new DataTable();
            repo = new CompanyRepo();
            List<SqlParameter> parameters = null;
            if (Model.CompanyAutoId > 0)
                parameters = SqlPara("Update");
            else
                parameters = SqlPara("Save");

            parameters = repo.SetModel(parameters, Model);
            dt = await repo.DbFunction("Sp_SetupCompany", parameters);
            if (Model.ImageList != null)
            {
                if (Model.CompanyAutoId < 0 || Convert.ToInt16(dt.Rows[0][0].ToString()) > 0)
                {
                    Model.CompanyAutoId = Convert.ToInt16(dt.Rows[0][0].ToString());
                    foreach (CompanyImageModel DetailModel in Model.ImageList.Where(a => a.IsSaved == false && a.CompanyPicture != null))
                    {
                        parameters = null;
                        parameters = SqlPara("Save");
                        parameters = repo.SetModelImage(parameters, DetailModel, Model.CompanyAutoId);
                        dt = await repo.DbFunction("sp_SetupCompanyImage", parameters);
                    }
                }
            }
            return Json(dt.Rows[0][1].ToString());
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("Delete/{CompanyAutoId}")]
        public async Task<JsonResult> Delete(int CompanyAutoId)
        {
            repo = new CompanyRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            if(CompanyAutoId > 0)
                parameters.Add(new SqlParameter("@CompanyAutoId", CompanyAutoId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("Sp_SetupCompany", parameters);
            return Json(dt.Rows[0][1].ToString());
        }
        [HttpPost]
        [Route("DeleteByImage/{CompanyImageAutoId}")]
        public async Task<JsonResult> DeleteByImage(int CompanyImageAutoId)
        {
            repo = new CompanyRepo();
            List<SqlParameter> parameters = SqlPara("Delete");
            parameters.Add(new SqlParameter("@CompanyImageAutoId", CompanyImageAutoId));
            DataTable dt = new DataTable();
            dt = await repo.DbFunction("sp_SetupCompanyImage", parameters);
            return Json(dt.Rows[0][1].ToString());
        }

        public JsonResult GetImageByte(string Image)
        {
            byte[] ImageByte = General.GetBytesFromImage(Image);
            return Json(ImageByte);
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
