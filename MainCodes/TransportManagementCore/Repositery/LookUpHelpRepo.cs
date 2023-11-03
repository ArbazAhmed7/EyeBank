using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Models;

namespace TransportManagementCore.Repositery
{
    public class LookUpHelpRepo
    {
        private DBHelper.DBHelper db;
        public LookUpHelpRepo()
        {
            db = new DBHelper.DBHelper();
        }
        public object DbFunction(string ProcedureName, List<SqlParameter> parameters)
        {
            DataTable dt = new DataTable();
            int[] columnsToHide = new int[] { 0 };
            try
            {
                dt = this.db.GetDataTable(ProcedureName, CommandType.StoredProcedure, parameters.ToArray());
                return Utilities.DataTables.DataTableSourceForLookup(dt, columnsToHide);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

     
        
    }
}
