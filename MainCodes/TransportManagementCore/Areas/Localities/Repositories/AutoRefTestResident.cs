using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Localities.Model;
using TransportManagementCore.Models;

namespace TransportManagementCore.Areas.Localities.Repositories
{
    public class AutoRefTestResident
    {
        DBHelper.DBHelper db;
        public AutoRefTestResident()
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
        public LocalityResidentEnrollment GetModel(DataTable dt)
        {
            LocalityResidentEnrollment CM = new LocalityResidentEnrollment();
            List<LocalityResidentEnrollmentImageModel> ListModel = new List<LocalityResidentEnrollmentImageModel>();
            foreach (DataRow row in dt.Rows)
            {
                LocalityResidentEnrollmentImageModel DetailModel = new LocalityResidentEnrollmentImageModel();
                CM.ResidentAutoId = Convert.ToInt32(row["ResidentAutoId"]);
                CM.LocalityAutoId = Convert.ToInt32(row["LocalityAutoId"]);
                CM.LocalityCode = row["LocalityCode"].ToString();
                CM.ResidentCode = row["ResidentCode"].ToString();
                CM.ResidentName = row["ResidentName"].ToString();
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
                CM.LocalityName = row["LocalityName"].ToString();
                if (!row["DetailLocalityAutoId"].Equals(DBNull.Value))
                    DetailModel.DetailLocalityAutoId = Convert.ToInt32(row["DetailLocalityAutoId"]);

                if (!row["ResidentAutoId"].Equals(DBNull.Value))
                    DetailModel.ResidentAutoId = Convert.ToInt32(row["ResidentAutoId"]);

                if (!row["ResidentImageAutoId"].Equals(DBNull.Value))
                    DetailModel.ResidentImageAutoId = Convert.ToInt32(row["ResidentImageAutoId"]);

                if (!row["FileSize"].Equals(DBNull.Value))
                    DetailModel.FileSize = Convert.ToInt32(row["FileSize"]);
                if (!row["FileType"].Equals(DBNull.Value))
                    DetailModel.FileType = Convert.ToString(row["FileType"]);

                if (!row["ResidentPic"].Equals(DBNull.Value))
                    DetailModel.ResidentPicture = Convert.ToString(row["ResidentPic"]);

                if (!row["captureRemarks"].Equals(DBNull.Value))
                    DetailModel.CaptureRemarks = Convert.ToString(row["captureRemarks"]);

                ListModel.Add(DetailModel);
            }
            CM.ImageList = ListModel;
            return CM;
        }
        public List<SqlParameter> SetModel(List<SqlParameter> para, AutoRefTestResidentModel model)
        {
            para.Add(new("@AutoRefResidentId", model.AutoRefResidentId));
            para.Add(new("@AutoRefResidentTransId", model.AutoRefResidentTransId));
            para.Add(new("@AutoRefResidentTransDate", model.AutoRefResidentTransDate));
            para.Add(new("@ResidentAutoId", model.ResidentAutoId));
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
        public List<AutoRefTestResidentModel> GetAutoRefTestModel(DataTable dt)
        {
            List<AutoRefTestResidentModel> list = new List<AutoRefTestResidentModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    AutoRefTestResidentModel at = new AutoRefTestResidentModel();

                }
            }
            return list;
        }
        public DisplayAutoRefResidentModel GetResidentLastHistory(DataTable dt)
        {
            DisplayAutoRefResidentModel auto = null;
            if (dt.Rows.Count > 0)
            {
                auto = new DisplayAutoRefResidentModel();
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

        public AutoRefTestResidentModel AutoRefModel(DataTable dt)
        {
            AutoRefTestResidentModel model = new AutoRefTestResidentModel();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    model.AutoRefResidentId = Convert.ToInt32(row["AutoRefResidentId"]);
                    model.AutoRefResidentTransId = Convert.ToString(row["AutoRefResidentId"]);
                    model.AutoRefResidentTransDate = Convert.ToDateTime(row["AutoRefResidentTransDate"]);
                    model.ResidentAutoId = Convert.ToInt32(row["ResidentAutoId"]);

                    if (!row["WearGlasses"].Equals(DBNull.Value))
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

                    if (!row["Right_Axix_To"].Equals(DBNull.Value))
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
