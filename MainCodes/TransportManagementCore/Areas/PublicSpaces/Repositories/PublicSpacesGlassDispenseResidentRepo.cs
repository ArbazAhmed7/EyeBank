using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.PublicSpaces.Model;
using TransportManagementCore.Models;

namespace TransportManagementCore.Areas.PublicSpaces.Repositories
{
    public class PublicSpacesGlassDispenseResidentRepo
    {
        DBHelper.DBHelper db;
        public PublicSpacesGlassDispenseResidentRepo()
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

        public List<SqlParameter> SetModel(List<SqlParameter> para, PublicSpacesGlassDispenseResidentModel model)
        {
            para.Add(new("@OptometristPublicSpacesResidentId", model.OptometristPublicSpacesResidentId));
            para.Add(new("@PublicSpacesGlassDispenseResidentId", model.PublicSpacesGlassDispenseResidentId));
            para.Add(new("@GlassDispenseResidentTransDate", model.GlassDispenseResidentTransDate));
            para.Add(new("@ResidentAutoId", model.ResidentAutoId));
            para.Add(new("@VisionwithGlasses_RightEye", model.VisionwithGlasses_RightEye));
            para.Add(new("@VisionwithGlasses_LeftEye", model.VisionwithGlasses_LeftEye));
            para.Add(new("@NearVA_RightEye", model.NearVA_RightEye));
            para.Add(new("@NearVA_LeftEye", model.NearVA_LeftEye));
            para.Add(new("@ResidentSatisficaion", model.ResidentSatisficaion));
            para.Add(new("@Unsatisfied", model.Unsatisfied));
            para.Add(new("@Unsatisfied_Remarks", model.Unsatisfied_Remarks));
            para.Add(new("@Unsatisfied_Reason", model.Unsatisfied_Reason));

            return para;
        }

        //public DisplayAutoRefResidentModel GetResidentLastHistory(DataTable dt)
        //{
        //    DisplayAutoRefResidentModel auto = null;
        //    if (dt.Rows.Count > 0)
        //    {
        //        auto = new DisplayAutoRefResidentModel();
        //        auto.LastVisitDate = Convert.ToString(dt.Rows[0]["Last_Visit_Date"]);
        //        auto.Right_Spherical_Points = Convert.ToString(dt.Rows[0]["Right Spherical"]);
        //        auto.Left_Spherical_Points = Convert.ToString(dt.Rows[0]["Left Spherical"]);
        //        auto.Right_Cyclinderical_Points = Convert.ToString(dt.Rows[0]["Right Cyclinderical"]);
        //        auto.Left_Cyclinderical_Points = Convert.ToString(dt.Rows[0]["Left Cyclinderical"]);
        //        auto.Right_Axix_From = Convert.ToString(dt.Rows[0]["Right Axis"]);
        //        auto.Left_Axix_From = Convert.ToString(dt.Rows[0]["Left Axis"]);
        //        auto.IPD = Convert.ToInt32(dt.Rows[0]["ipd"]);

        //    }
        //    return auto;
        //}
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
        public DisplayPublicSpacesGlassDispenseResidentModel GetResidentLastHistory(DataTable dt)
        {
            DisplayPublicSpacesGlassDispenseResidentModel auto = null;
            if (dt.Rows.Count > 0)
            {
                auto = new DisplayPublicSpacesGlassDispenseResidentModel();
                auto.Right_Spherical_Points = Convert.ToString(dt.Rows[0]["Right Spherical"]);
                auto.Left_Spherical_Points = Convert.ToString(dt.Rows[0]["Left Spherical"]);
                auto.Right_Cyclinderical_Points = Convert.ToString(dt.Rows[0]["Right Cyclinderical"]);
                auto.Left_Cyclinderical_Points = Convert.ToString(dt.Rows[0]["Left Cyclinderical"]);
                auto.Right_Axix_From = Convert.ToString(dt.Rows[0]["Right Axis"]);
                auto.Left_Axix_From = Convert.ToString(dt.Rows[0]["Left Axis"]);
                auto.IPD = Convert.ToInt32(dt.Rows[0]["ipd"]);
                auto.WearGlasses = Convert.ToBoolean(dt.Rows[0]["WearGlasses"]);
                auto.Distance = Convert.ToBoolean(dt.Rows[0]["Distance"]);
                auto.Near = Convert.ToBoolean(dt.Rows[0]["Near"]);
                auto.OptometristPublicSpacesResidentId = Convert.ToInt32(dt.Rows[0]["OptometristPublicSpacesResidentId"]);
                auto.Gender = Convert.ToString(dt.Rows[0]["Gender"]);
                auto.Age = Convert.ToInt32(dt.Rows[0]["Age"]);
            }
            return auto;
        }
        public PublicSpacesGlassDispenseResidentModel GlassDispenseModel(DataTable dt)
        {
            PublicSpacesGlassDispenseResidentModel model = new PublicSpacesGlassDispenseResidentModel();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (!row["OptometristPublicSpacesResidentId"].Equals(DBNull.Value))
                        model.OptometristPublicSpacesResidentId = Convert.ToInt32(row["OptometristPublicSpacesResidentId"].Equals(DBNull.Value));
                    model.PublicSpacesGlassDispenseResidentId = Convert.ToInt32(row["PublicSpacesGlassDispenseResidentId"]);
                    model.GlassDispenseResidentTransDate = Convert.ToDateTime(row["GlassDispenseResidentTransDate"]);
                    model.ResidentAutoId = Convert.ToInt32(row["ResidentAutoId"]);
                    model.VisionwithGlasses_RightEye = Convert.ToInt32(row["VisionwithGlasses_RightEye"]);
                    model.VisionwithGlasses_LeftEye = Convert.ToInt32(row["VisionwithGlasses_LeftEye"]);
                    model.NearVA_RightEye = Convert.ToInt32(row["NearVA_RightEye"]);
                    model.NearVA_LeftEye = Convert.ToInt32(row["NearVA_LeftEye"]);


                    model.ResidentSatisficaion = Convert.ToInt32(row["ResidentSatisficaion"]);
                    model.Unsatisfied = Convert.ToInt32(row["Unsatisfied"]);
                    model.Unsatisfied_Remarks = Convert.ToString(row["Unsatisfied_Remarks"]);
                    model.Unsatisfied_Reason = Convert.ToInt32(row["Unsatisfied_Reason"]);

                    if (!row["WearGlasses"].Equals(DBNull.Value))
                        model.WearGlasses = Convert.ToBoolean(row["WearGlasses"]);
                    if (!row["Distance"].Equals(DBNull.Value))
                        model.Distance = Convert.ToBoolean(row["Distance"]);

                    if (!row["Near"].Equals(DBNull.Value))
                        model.Near = Convert.ToBoolean(row["Near"]);


                }
            }
            return model;
        }
    }
}