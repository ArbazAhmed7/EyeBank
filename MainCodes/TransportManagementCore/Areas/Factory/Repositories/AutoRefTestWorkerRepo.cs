using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Goth.Model;
using TransportManagementCore.Areas.Setup.Model;
using TransportManagementCore.Models;

namespace TransportManagementCore.Areas.Goth.Repositories
{
    public class AutoRefTestWorkerRepo
    {
        DBHelper.DBHelper db;
        public AutoRefTestWorkerRepo()
        {
            db = new DBHelper.DBHelper();
        }
        public async Task<DataTable> DbFunction(string ProcedureName, List<SqlParameter> parameters)
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
        public DataTable GetForModelFromDB(string ProcedureName, List<SqlParameter> parameters)
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
        public CompanyWorkerEnrollmentModel GetModel(DataTable dt)
        {
            CompanyWorkerEnrollmentModel CM = new CompanyWorkerEnrollmentModel();
            foreach (DataRow row in dt.Rows)
            {
                CM.WorkerAutoId = Convert.ToInt32(row["WorkerAutoId"]);
                CM.CompanyAutoId = Convert.ToInt32(row["CompanyAutoId"]);
                CM.CompanyCode = row["companyCode"].ToString();
                CM.WorkerCode = row["WorkerCode"].ToString();
                CM.WorkerName = row["WorkerName"].ToString();
                CM.RelationType = row["RelationType"].ToString();
                CM.RelationName = row["RelationName"].ToString();
                CM.Age = Convert.ToInt32(row["Age"].ToString());
                CM.GenderAutoId = Convert.ToInt32(row["GenderAutoId"].ToString());
                CM.CNIC = row["CNIC"].ToString();
                CM.DecreasedVision = Convert.ToBoolean(row["DecreasedVision"].ToString());
                CM.Near = Convert.ToBoolean(row["Near"].ToString());
                CM.Distance = Convert.ToBoolean(row["Distance"].ToString());
                CM.WearGlasses = Convert.ToBoolean(row["WearGlasses"].ToString());
                CM.Religion = Convert.ToBoolean(row["Religion"].ToString());
                CM.MobileNo = row["MobileNo"].ToString();
                CM.EnrollementDate = Convert.ToDateTime(row["EnrollmentDate"].ToString());

                
            }
            return CM;
        }
        public List<SqlParameter> SetModel(List<SqlParameter> para, AutoRefTestWorkerModel model) {
            para.Add(new("@AutoRefWorkerId", model.AutoRefWorkerId));
            para.Add(new("@AutoRefWorkerTransId", model.AutoRefWorkerTransId));
            para.Add(new("@AutoRefWorkerTransDate", model.AutoRefWorkerTransDate));
            para.Add(new("@WorkerAutoId", model.WorkerAutoId));
            para.Add(new("@Right_Spherical_Status", model.Right_Spherical_Status));
            para.Add(new("@Right_Spherical_Points", model.Right_Spherical_Points));
            para.Add(new("@Right_Cyclinderical_Status", model.Right_Cyclinderical_Status));
            para.Add(new("@Right_Cyclinderical_Points", model.Right_Cyclinderical_Points));
            para.Add(new("@Right_Axix_From", model.Right_Axix_From));
            para.Add(new("@Left_Spherical_Status", model.Left_Spherical_Status));
            para.Add(new("@Left_Cyclinderical_Status", model.Left_Cyclinderical_Status));
            para.Add(new("@Left_Spherical_Points", model.Left_Spherical_Points));
            para.Add(new("@Left_Cyclinderical_Points", model.Left_Cyclinderical_Points));
            para.Add(new("@Left_Axix_From", model.Left_Axix_From));
            para.Add(new("@IPD", model.IPD));
            return para;
        }
        public List<AutoRefTestWorkerModel> GetAutoRefTestModel(DataTable dt) {
            List<AutoRefTestWorkerModel> list = new List<AutoRefTestWorkerModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    AutoRefTestWorkerModel at = new AutoRefTestWorkerModel();

                }
            }
            return list;
        }
        public DisplayAutoRefWorkerModel GetWorkerLastHistory(DataTable dt) {
            DisplayAutoRefWorkerModel auto = null;
            if (dt.Rows.Count > 0) {
                auto = new DisplayAutoRefWorkerModel();
                auto.LastVisitDate = Convert.ToString(dt.Rows[0]["Last_Visit_Date"]);
                auto.Right_Spherical_Points = Convert.ToString(dt.Rows[0]["Right Spherical"]);
                auto.Left_Spherical_Points = Convert.ToString(dt.Rows[0]["Left Spherical"]);
                auto.Right_Cyclinderical_Points = Convert.ToString(dt.Rows[0]["Right Cyclinderical"]);
                auto.Left_Cyclinderical_Points = Convert.ToString(dt.Rows[0]["Left Cyclinderical"]);
                auto.Right_Axix_From = Convert.ToString(dt.Rows[0]["Right Axis"]);
                auto.Left_Axix_From = Convert.ToString(dt.Rows[0]["Left Axis"]);
                auto.IPD = Convert.ToInt32(dt.Rows[0]["ipd"]);
                
            }
            return auto;
        }
        public List<DropDownModel> DateList(DataTable dt)
        {
            List<DropDownModel> list = new List<DropDownModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DropDownModel dm = new DropDownModel();
                    dm.Code = Convert.ToString(row["Id"]);
                    dm.Text = Convert.ToString(row["Text"]);
                    list.Add(dm);
                }
            }
            return list;
        }

        public AutoRefTestWorkerModel AutoRefModel(DataTable dt)
        {
            AutoRefTestWorkerModel model = new AutoRefTestWorkerModel();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    model.AutoRefWorkerId= Convert.ToInt32(row["AutoRefWorkerId"]); 
                    model.AutoRefWorkerTransId= Convert.ToString(row["AutoRefWorkerId"]);
                    model.AutoRefWorkerTransDate= Convert.ToDateTime(row["AutoRefWorkerTransDate"]);
                    model.WorkerAutoId = Convert.ToInt32(row["WorkerAutoId"]);

                    if(!row["WearGlasses"].Equals(DBNull.Value))
                        model.WearGlasses = Convert.ToBoolean(row["WearGlasses"]);
                    if (!row["Right_Spherical_Status"].Equals(DBNull.Value))
                        model.Right_Spherical_Status = Convert.ToChar(row["Right_Spherical_Status"]);

                    if (!row["Right_Spherical_Points"].Equals(DBNull.Value))
                        model.Right_Spherical_Points = Convert.ToDecimal(row["Right_Spherical_Points"]);

                    if (!row["Right_Cyclinderical_Status"].Equals(DBNull.Value))
                        model.Right_Cyclinderical_Status = Convert.ToChar(row["Right_Cyclinderical_Status"]);

                    if (!row["Right_Cyclinderical_Points"].Equals(DBNull.Value))
                        model.Right_Cyclinderical_Points = Convert.ToDecimal(row["Right_Cyclinderical_Points"]);

                    if (!row["Right_Axix_From"].Equals(DBNull.Value))
                        model.Right_Axix_From = Convert.ToInt32(row["Right_Axix_From"]);

                    if(!row["Right_Axix_To"].Equals(DBNull.Value))
                        model.Right_Axix_To = Convert.ToInt32(row["Right_Axix_To"]);

                    if (!row["Left_Spherical_Status"].Equals(DBNull.Value))
                        model.Left_Spherical_Status = Convert.ToChar(row["Left_Spherical_Status"]);

                    if (!row["Left_Spherical_Points"].Equals(DBNull.Value))
                        model.Left_Spherical_Points = Convert.ToDecimal(row["Left_Spherical_Points"]);

                    if (!row["Left_Cyclinderical_Status"].Equals(DBNull.Value))
                        model.Left_Cyclinderical_Status = Convert.ToChar(row["Left_Cyclinderical_Status"]);

                    if (!row["Left_Cyclinderical_Points"].Equals(DBNull.Value))
                        model.Left_Cyclinderical_Points = Convert.ToDecimal(row["Left_Cyclinderical_Points"]);

                    if (!row["Left_Axix_From"].Equals(DBNull.Value))
                        model.Left_Axix_From = Convert.ToInt32(row["Left_Axix_From"]);

                    if (!row["Left_Axix_To"].Equals(DBNull.Value))
                        model.Left_Axix_To = Convert.ToInt32(row["Left_Axix_To"]);

                    if (!row["IPD"].Equals(DBNull.Value))
                        model.IPD = Convert.ToInt32(row["IPD"]);
                    

                }
            }
            return model;
        }

       
    }
}
