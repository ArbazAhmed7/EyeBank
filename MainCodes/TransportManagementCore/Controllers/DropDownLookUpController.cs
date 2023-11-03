using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using TransportManagementCore.Models;
using TransportManagementCore.Repositery.General;

namespace TransportManagementCore.Controllers
{
    [Route("DropDownLookUp")]
    public class DropDownLookUpController : Controller
    {
        DropDownLookUpRepo repo;
        public IActionResult Index()
        {
            return View();
        }

        [Produces("application/json")]
        [Route("Help/DropDown/{Type}")]
        public JsonResult DropDown(string Type)
        {
            repo = new DropDownLookUpRepo();
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@DropDownType", Type));
            DataTable dt = repo.DbFunction("Sp_DropDownLookUp", para);
            return Json(dt);
        }

        [Produces("application/json")]
        [Route("Help/GetLookUp/{FormId}")]
        public JsonResult GetLookUp(string FormId)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FormID", FormId));
            DataTable dt = repo.DbFunction("sp_GetLookupData", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        #region . . . .Factory . . . 

        #region . . . Factory Company . . .
        [Produces("application/json")]
        [Route("Help/GetCompanies")]
        public JsonResult GetCompanies(GetCompany model)

        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            parameters.Add(new SqlParameter("@operation", "GetCompanies"));

            DataTable dt = repo.DbFunction("Sp_SetupCompany", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        #endregion

        #region . . . Factory Workers . . . 
        [Produces("application/json")]
        [Route("Help/GetWorkersForCompany")]
        public JsonResult GetWorkersForCompany(GetWorkers model)

        {
            repo = new DropDownLookUpRepo();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            parameters.Add(new SqlParameter("@CompanyAutoId", model.AutoId));
            parameters.Add(new SqlParameter("@operation", "GetWorkers"));

            DataTable dt = repo.DbFunction("[Sp_CompanyWorker]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        #endregion

        #region . . . Factory Auto Referaction . . .

        [Produces("application/json")]
        [Route("Help/GetCompaniesForAutoRef")]
        public JsonResult GetCompaniesForAutoRef(GetCompany model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewCompaniesForAutoRef"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditCompaniesForAutoRef"));

            DataTable dt = repo.DbFunction("Sp_SetupCompany", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }


        [Produces("application/json")]
        [Route("Help/GetWorkers")]
        public JsonResult GetWorkers(GetWorkers model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "NewWorkerForRef"));
            else
                parameters.Add(new SqlParameter("@operation", "EditWorkerForRef"));

            parameters.Add(new SqlParameter("@AutoRefWorkerTransDate", Convert.ToDateTime(model.TransDate)));
            parameters.Add(new SqlParameter("@CompanyAutoId", model.AutoId));
            DataTable dt = repo.DbFunction("Sp_AutoRefTestWorker", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        #endregion

        #region . . . Factory Optometrist Worker. . .
        [Produces("application/json")]
        [Route("Help/GetCompaniesForOptometristWorker")]
        public JsonResult GetCompaniesForOptometristWorker(GetCompany model)

        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewCompaniesForOptometristWorker"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditCompaniesForOptometristWorker"));

            DataTable dt = repo.DbFunction("Sp_SetupCompany", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        [Produces("application/json")]
        [Route("Help/GetWorkersForOptometrist")]
        public JsonResult GetWorkersForOptometrist(GetWorkers Model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", Model.SearchText));
            if (Model.New)
            {
                parameters.Add(new SqlParameter("@operation", "NewWorkerForOpt"));
                parameters.Add(new SqlParameter("@OptometristWorkerTransDate", Model.TransDate));
            }
            else
                parameters.Add(new SqlParameter("@operation", "EditWorkerForOpt"));

            parameters.Add(new SqlParameter("@CompanyAutoId", Model.AutoId));
            DataTable dt = repo.DbFunction("Sp_OptometristWorker", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }


        #endregion

        #region . . . Factory Glass Despense. . .

        [Produces("application/json")]
        [Route("Help/GetCompaniesForGlassDispense")]
        public JsonResult GetCompaniesForGlassDispense(GetCompany model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewCompaniesForGlassDispense"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditCompaniesForGlassDispense"));

            DataTable dt = repo.DbFunction("Sp_SetupCompany", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }


        [Produces("application/json")]
        [Route("Help/GetWorkersForGlassDispense")]
        public JsonResult GetWorkersForGlassDispense(GetWorkers model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "NewWorkerForGlassDispense"));
            else
                parameters.Add(new SqlParameter("@operation", "EditWorkerForGlassDispense"));

            parameters.Add(new SqlParameter("@GlassDespenseWorkerTransDate", Convert.ToDateTime(model.TransDate)));
            parameters.Add(new SqlParameter("@CompanyAutoId", model.AutoId));
            DataTable dt = repo.DbFunction("Sp_GlassDespenseWorker", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        #endregion

        #region Factory Visit For Surgery Worker
        [Produces("application/json")]
        [Route("Help/GetCompaniesForVisitForSurgeryWorker")]
        public JsonResult GetCompaniesForVisitForSurgeryWorker(GetCompany model)

        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewCompaniesForVisitForSurgeryWorker"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditCompaniesForVisitForSurgeryWorker"));

            DataTable dt = repo.DbFunction("Sp_SetupCompany", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
            [Produces("application/json")]
            [Route("Help/GetWorkersForVisitForSurgeryWorker")]
            public JsonResult GetWorkersForVisitForSurgeryWorker(GetWorkers model)
            {
                repo = new DropDownLookUpRepo();
                int[] columnsToHide = new int[] { 0 };
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@SearchText", model.SearchText));
                if (model.New)
                    parameters.Add(new SqlParameter("@operation", "NewWorkerForVisitForSurgeryWorker"));
                else
                    parameters.Add(new SqlParameter("@operation", "EditWorkerForVisitForSurgeryWorker"));

                parameters.Add(new SqlParameter("@VisitDate", Convert.ToDateTime(model.TransDate)));
                parameters.Add(new SqlParameter("@CompanyAutoId", model.AutoId));
                DataTable dt = repo.DbFunction("Sp_VisitForSurgeryWorker", parameters);
                List<MainDropDown> ListModel = repo.GetList(dt);
                return Json(ListModel);
            }
        
        #endregion
        #endregion

        #region . . . Localities . . .

        #region . . . Localities . . .
        [Produces("application/json")]
        [Route("Help/GetLocalities")]
        public JsonResult GetLocalities(GetCompany model)

        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            parameters.Add(new SqlParameter("@operation", "GetLocalities"));

            DataTable dt = repo.DbFunction("[Sp_SetupLocality]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        #endregion

        #region . . . Locality Resident . . .
        [Produces("application/json")]
        [Route("Help/GetResidentsForLocality")]
        public JsonResult GetResidentsForLocality(GetWorkers model)

        {
            repo = new DropDownLookUpRepo();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            parameters.Add(new SqlParameter("@LocalityAutoId", model.AutoId));
            parameters.Add(new SqlParameter("@operation", "GetResidents"));

            DataTable dt = repo.DbFunction("[Sp_LocalityResident]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        #endregion 

        #region . . . Locality Auto Referaction  . . .
        [Produces("application/json")]
        [Route("Help/GetLocalitiesForAutoRef")]
        public JsonResult GetLocalitiesForAutoRef(GetCompany model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewLocalitiesForAutoRef"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditLocalitiesForAutoRef"));

            DataTable dt = repo.DbFunction("[Sp_SetupLocality]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        [Produces("application/json")]
        [Route("Help/GetResidentForAutoRef")]
        public JsonResult GetResidentForAutoRef(GetWorkers model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "NewResidentForRef"));
            else
                parameters.Add(new SqlParameter("@operation", "EditResidentForRef"));

            parameters.Add(new SqlParameter("@AutoRefResidentTransDate", Convert.ToDateTime(model.TransDate)));
            parameters.Add(new SqlParameter("@LocalityAutoId", model.AutoId));

            DataTable dt = repo.DbFunction("[Sp_AutoRefTestResident]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        #endregion

        #region . . . Locality Optometrist Resident. . .
        [Produces("application/json")]
        [Route("Help/GetLocalitiesForOptometristResident")]
        public JsonResult GetLocalitiesForOptometristResident(GetCompany model)

        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewLocalitiesForOptometristResident"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditLocalitiesForOptometristResident"));

            DataTable dt = repo.DbFunction("Sp_SetupLocality", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        [Produces("application/json")]
        [Route("Help/GetResidentsForOptometrist")]
        public JsonResult GetResidentsForOptometrist(GetWorkers Model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", Model.SearchText));
            if (Model.New)
            {
                parameters.Add(new SqlParameter("@operation", "NewResidentForOpt"));
                parameters.Add(new SqlParameter("@OptometristResidentTransDate", Model.TransDate));
            }
            else
                parameters.Add(new SqlParameter("@operation", "EditResidentForOpt"));

            parameters.Add(new SqlParameter("@LocalityAutoId", Model.AutoId));
            DataTable dt = repo.DbFunction("Sp_OptometristResident", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        #endregion

        #region . . . Locality Glass Despense. . .

        [Produces("application/json")]
        [Route("Help/GetLocalitiesForGlassDispense")]
        public JsonResult GetLocalitiesForGlassDispense(GetCompany model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewLocalitiesForGlassDispense"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditLocalitiesForGlassDispense"));

            DataTable dt = repo.DbFunction("[Sp_SetupLocality]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }


        [Produces("application/json")]
        [Route("Help/GetResidentsForGlassDispense")]
        public JsonResult GetResidentsForGlassDispense(GetWorkers model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "NewResidentForGlassDispense"));
            else
                parameters.Add(new SqlParameter("@operation", "EditResidentForGlassDispense"));

            parameters.Add(new SqlParameter("@GlassDispenseResidentTransDate", Convert.ToDateTime(model.TransDate)));
            parameters.Add(new SqlParameter("@LocalityAutoId", model.AutoId));
            DataTable dt = repo.DbFunction("[Sp_GlassDispenseResident]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        #endregion

        #region . . . Locality Visit For Surgery Resident . . .
        [Produces("application/json")]
        [Route("Help/GetLocalityForVisitForSurgeryResident")]
        public JsonResult GetLocalityForVisitForSurgeryResident(GetCompany model)

        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewLocalityForVisitForSurgeryResident"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditLocalityForVisitForSurgeryResident"));

            DataTable dt = repo.DbFunction("Sp_SetupLocality", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        [Produces("application/json")]
        [Route("Help/GetForVisitForSurgeryResident")]
        public JsonResult GetForVisitForSurgeryResident(GetWorkers model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "NewForVisitForSurgeryResident"));
            else
                parameters.Add(new SqlParameter("@operation", "EditForVisitForSurgeryResident"));

            parameters.Add(new SqlParameter("@VisitDate", Convert.ToDateTime(model.TransDate)));
            parameters.Add(new SqlParameter("@LocalityAutoId", model.AutoId));
            DataTable dt = repo.DbFunction("Sp_VisitForSurgeryLocalityResident", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        #endregion
        #endregion

        #region . . . Goths . . .

        #region . . . Goths . . .
        [Produces("application/json")]
        [Route("Help/GetGoths")]
        public JsonResult GetGoths(GetCompany model)

        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            parameters.Add(new SqlParameter("@operation", "GetGoths"));

            DataTable dt = repo.DbFunction("[Sp_SetupGoths]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        #endregion

        #region . . . Goths Resident . . .
        [Produces("application/json")]
        [Route("Help/GetResidentsForGoth")]
        public JsonResult GetResidentsForGoth(GetWorkers model)

        {
            repo = new DropDownLookUpRepo();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            parameters.Add(new SqlParameter("@GothAutoId", model.AutoId));
            parameters.Add(new SqlParameter("@operation", "GetResidents"));

            DataTable dt = repo.DbFunction("[Sp_GothsResident]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        #endregion

        #region . . . Goths Auto Referaction . . .

        [Produces("application/json")]
        [Route("Help/GetGothsForAutoRef")]
        public JsonResult GetGothsForAutoRef(GetCompany model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewGothsForAutoRef"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditGothsForAutoRef"));

            DataTable dt = repo.DbFunction("[Sp_SetupGoths]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        [Produces("application/json")]
        [Route("Help/GetGothResidentForAutoRef")]
        public JsonResult GetGothResidentForAutoRef(GetWorkers model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "NewResidentForRef"));
            else
                parameters.Add(new SqlParameter("@operation", "EditResidentForRef"));

            parameters.Add(new SqlParameter("@AutoRefResidentTransDate", Convert.ToDateTime(model.TransDate)));
            parameters.Add(new SqlParameter("@GothAutoId", model.AutoId));

            DataTable dt = repo.DbFunction("[Sp_GothAutoRefTestResident]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        #endregion

        #region . . . Goths Optometrist Resident . . .
        [Produces("application/json")]
        [Route("Help/GetGothsForOptometristResident")]
        public JsonResult GetGothsForOptometristResident(GetCompany model)

        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewGothsForOptometristResident"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditGothsForOptometristResident"));

            DataTable dt = repo.DbFunction("[Sp_SetupGoths]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        [Produces("application/json")]
        [Route("Help/GetGothResidentsForOptometrist")]
        public JsonResult GetGothResidentsForOptometrist(GetWorkers Model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", Model.SearchText));
            if (Model.New)
            {
                parameters.Add(new SqlParameter("@operation", "NewResidentForOpt"));
                parameters.Add(new SqlParameter("@OptometristGothResidentTransDate", Model.TransDate));
            }
            else
                parameters.Add(new SqlParameter("@operation", "EditResidentForOpt"));

            parameters.Add(new SqlParameter("@GothAutoId", Model.AutoId));
            DataTable dt = repo.DbFunction("Sp_OptometristGothResident", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        #endregion

        #region . . . Goth Glass Despense. . .

        [Produces("application/json")]
        [Route("Help/GetGothsForGlassDispense")]
        public JsonResult GetGothsForGlassDispense(GetCompany model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewGothsForGlassDispense"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditGothsForGlassDispense"));

            DataTable dt = repo.DbFunction("[Sp_SetupGoths]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }


        [Produces("application/json")]
        [Route("Help/GetGothResidentsForGlassDispense")]
        public JsonResult GetGothResidentsForGlassDispense(GetWorkers model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "NewResidentForGlassDispense"));
            else
                parameters.Add(new SqlParameter("@operation", "EditResidentForGlassDispense"));

            parameters.Add(new SqlParameter("@GlassDispenseResidentTransDate", Convert.ToDateTime(model.TransDate)));
            parameters.Add(new SqlParameter("@GothAutoId", model.AutoId));
            DataTable dt = repo.DbFunction("[Sp_GothGlassDispenseResident]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        #endregion

        #region . . . Goth Visit For Surgery  . . .
        [Produces("application/json")]
        [Route("Help/GetGothsForVisitForSurgeryResident")]
        public JsonResult GetGothsForVisitForSurgeryResident(GetCompany model)

        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewGothsForVisitForSurgeryResident"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditGothsForVisitForSurgeryResident"));

            DataTable dt = repo.DbFunction("Sp_SetupGoths", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        [Produces("application/json")]
        [Route("Help/GetResidentForVisitForSurgeryGoth")]
        public JsonResult GetResidentForVisitForSurgeryGoth(GetWorkers model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "NewResidentForVisitForSurgeryGoth"));
            else
                parameters.Add(new SqlParameter("@operation", "EditResidentForVisitForSurgeryGoth"));

            parameters.Add(new SqlParameter("@VisitDate", Convert.ToDateTime(model.TransDate)));
            parameters.Add(new SqlParameter("@GothAutoId", model.AutoId));
            DataTable dt = repo.DbFunction("[Sp_VisitForSurgeryGothResident]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        #endregion

        #endregion

        #region . . . Public Spaces . . .

        #region . . . Public Spaces . . .
        [Produces("application/json")]
        [Route("Help/GetPublicSpaces")]
        public JsonResult GetPublicSpaces(GetCompany model)

        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            parameters.Add(new SqlParameter("@operation", "GetPublicSpaces"));

            DataTable dt = repo.DbFunction("[Sp_SetupPublicSpaces]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        #endregion

        #region . . . Public Spaces Resident . . .
        [Produces("application/json")]
        [Route("Help/GetResidentsForPublicSpaces")]
        public JsonResult GetResidentsForPublicSpaces(GetWorkers model)

        {
            repo = new DropDownLookUpRepo();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            parameters.Add(new SqlParameter("@PublicSpacesAutoId", model.AutoId));
            parameters.Add(new SqlParameter("@operation", "GetResidents"));

            DataTable dt = repo.DbFunction("[Sp_PublicSpacesResident]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        #endregion

        #region . . . Public Spaces Auto Referaction  . . .
        [Produces("application/json")]
        [Route("Help/GetPublicSpacesForAutoRef")]
        public JsonResult GetPublicSpacesForAutoRef(GetCompany model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewPublicSpacesForAutoRef"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditPublicSpacesForAutoRef"));

            DataTable dt = repo.DbFunction("[Sp_SetupPublicSpaces]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        [Produces("application/json")]
        [Route("Help/GetResidentForAutoRefPublicSpaces")]
        public JsonResult GetResidentForAutoRefPublicSpaces(GetWorkers model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "NewResidentForRef"));
            else
                parameters.Add(new SqlParameter("@operation", "EditResidentForRef"));

            parameters.Add(new SqlParameter("@AutoRefResidentTransDate", Convert.ToDateTime(model.TransDate)));
            parameters.Add(new SqlParameter("@PublicSpacesAutoId", model.AutoId));

            DataTable dt = repo.DbFunction("[Sp_PublicSpacesAutoRefTestResident]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        #endregion

        #region . . . Public Spaces Optometrist Resident . . .
        [Produces("application/json")]
        [Route("Help/GetPublicSpacesForOptometristResident")]
        public JsonResult GetPublicSpacesForOptometristResident(GetCompany model)

        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewPublicSpacesForOptometristResident"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditPublicSpacesForOptometristResident"));

            DataTable dt = repo.DbFunction("[Sp_SetupPublicSpaces]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        [Produces("application/json")]
        [Route("Help/GetPublicSpacesResidentsForOptometrist")]
        public JsonResult GetPublicSpacesResidentsForOptometrist(GetWorkers Model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", Model.SearchText));
            if (Model.New)
            {
                parameters.Add(new SqlParameter("@operation", "NewResidentForOpt"));
                parameters.Add(new SqlParameter("@OptometristResidentTransDate", Model.TransDate));
            }
            else
                parameters.Add(new SqlParameter("@operation", "EditResidentForOpt"));

            parameters.Add(new SqlParameter("@PublicSpacesAutoId", Model.AutoId));
            DataTable dt = repo.DbFunction("[Sp_OptometristPublicSpacesResident]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        #endregion

        #region . . . Public Spaces Glass Despense. . .

        [Produces("application/json")]
        [Route("Help/GetPublicSpacesForGlassDispense")]
        public JsonResult GetPublicSpacesForGlassDispense(GetCompany model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewPublicSpacesForGlassDispense"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditPublicSpacesForGlassDispense"));

            DataTable dt = repo.DbFunction("[Sp_SetupPublicSpaces]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }


        [Produces("application/json")]
        [Route("Help/GetPublicSpacesResidentsForGlassDispense")]
        public JsonResult GetPublicSpacesResidentsForGlassDispense(GetWorkers model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "NewResidentForGlassDispense"));
            else
                parameters.Add(new SqlParameter("@operation", "EditResidentForGlassDispense"));

            parameters.Add(new SqlParameter("@GlassDispenseResidentTransDate", Convert.ToDateTime(model.TransDate)));
            parameters.Add(new SqlParameter("@PublicSpacesAutoId", model.AutoId));
            DataTable dt = repo.DbFunction("[Sp_PublicSpacesGlassDispenseResident]", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        #endregion

        #region . . . Public Spaces Visit For Surgery Resident . . .
        [Produces("application/json")]
        [Route("Help/GetPublicSpacesForVisitForSurgeryResident")]
        public JsonResult GetPublicSpacesForVisitForSurgeryResident(GetCompany model)

        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "GetNewPublicSpacesForVisitForSurgeryResident"));
            else
                parameters.Add(new SqlParameter("@operation", "GetEditPublicSpacesForVisitForSurgeryResident"));

            DataTable dt = repo.DbFunction("Sp_SetupPublicSpaces", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }
        [Produces("application/json")]
        [Route("Help/GetNewForVisitForSurgery")]
        public JsonResult GetNewForVisitForSurgery(GetWorkers model)
        {
            repo = new DropDownLookUpRepo();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SearchText", model.SearchText));
            if (model.New)
                parameters.Add(new SqlParameter("@operation", "NewWorkerForVisitForSurgeryWorker"));
            else
                parameters.Add(new SqlParameter("@operation", "EditWorkerForVisitForSurgeryWorker"));

            parameters.Add(new SqlParameter("@VisitDate", Convert.ToDateTime(model.TransDate)));
            parameters.Add(new SqlParameter("@PublicSpacesAutoId", model.AutoId));
            DataTable dt = repo.DbFunction("Sp_VisitForSurgeryPublicSpaces", parameters);
            List<MainDropDown> ListModel = repo.GetList(dt);
            return Json(ListModel);
        }

        #endregion
        #endregion
    }
    public class GetCompany {
        public bool New  { get; set; }
        public string SearchText { get; set; }
    }

    public class GetWorkers
    {
        public int AutoId { get; set; }
        public DateTime TransDate { get; set; }
        public bool New { get; set; }
        public string SearchText { get; set; }
    }
}
