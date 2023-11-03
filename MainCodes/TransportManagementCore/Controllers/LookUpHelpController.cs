using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Repositery;

namespace TransportManagementCore.Controllers
{
    [Route("LookupHelp")]
    public class LookUpHelpController : Controller
    {
        LookUpHelpRepo repo;
        public LookUpHelpController()
        {
            repo = new LookUpHelpRepo();
        }
        [Produces("application/json")]
        [Route("Help/GetAllCompany")]
        public object GetAllCompany()
        {
            DataTable dt = new DataTable();
            int[] columnsToHide = new int[] { 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@operation", "GetAllCompany"));
            return repo.DbFunction("Sp_SetupCompany", parameters);
        }

     
    }
}
