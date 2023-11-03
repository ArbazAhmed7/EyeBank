using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using TransportManagementCore.Models;

namespace TransportManagementCore.Repositery.General
{
    public class DropDownLookUpRepo
    {
        DBHelper.DBHelper db;
        public DropDownLookUpRepo()
        {
            db = new DBHelper.DBHelper();
        }
        public DataTable DbFunction(string ProcedureName, List<SqlParameter> parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = this.db.GetDataTable(ProcedureName, CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                dt.Rows[0][0] = "Error :" + ex.Message.ToString();
            }
            return dt;
        }
        public List<MainDropDown> GetList(DataTable dt)
        {
            List<MainDropDown> list = new List<MainDropDown>();
            foreach (DataRow row in dt.Rows)
            {
                MainDropDown m = new MainDropDown();
                m.Id = Convert.ToInt32(row["Id"]);
                m.Code = Convert.ToString(row["Code"]);
                m.Text = Convert.ToString(row["Text"]);
                list.Add(m);
            }
            return list;
        }
    }
}
