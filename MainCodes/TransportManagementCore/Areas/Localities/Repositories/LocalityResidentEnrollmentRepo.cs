using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Localities.Model;

namespace TransportManagementCore.Areas.Localities.Repositories
{
    public class LocalityResidentEnrollmentRepo
    {
        DBHelper.DBHelper db;
        public LocalityResidentEnrollmentRepo()
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

        public List<SqlParameter> SetModelImage(List<SqlParameter> para, LocalityResidentEnrollmentImageModel cm, int ResidentId, int LocalityId)
        {

            if (cm.FileName != "" && cm.FileName != null)
            {
                //cm.LocalityPic = Convert.FromBase64String(cm.LocalityPicture);
                para.Add(new("@ResidentImageAutoId", cm.ResidentImageAutoId));
                para.Add(new("@ResidentAutoId", ResidentId));
                para.Add(new("@LocalityAutoId", LocalityId));
                para.Add(new("@ResidentPic", cm.ResidentPicture));
                para.Add(new("@CaptureRemarks", cm.CaptureRemarks));
                para.Add(new("@FileType", cm.FileType));
                para.Add(new("@FileSize", cm.FileSize));
            }
            return para;
        }
        public List<SqlParameter> SetModel(List<SqlParameter> para, LocalityResidentEnrollment model)
        {
            para.Add(new("@ResidentAutoId", model.ResidentAutoId));
            para.Add(new("@ResidentName", model.ResidentName));
            para.Add(new("@ResidentCode", model.ResidentCode));
            para.Add(new("@LocalityAutoId", model.LocalityAutoId));
            para.Add(new("@LocalityCode", model.LocalityCode));
            para.Add(new("@RelationType", model.RelationType));
            para.Add(new("@RelationName", model.RelationName));
            para.Add(new("@Age", model.Age));
            para.Add(new("@CNIC", model.CNIC));
            para.Add(new("@WearGlasses", model.WearGlasses));
            para.Add(new("@Distance", model.Distance));
            para.Add(new("@Near", model.Near));
            para.Add(new("@DecreasedVision", model.DecreasedVision));
            para.Add(new("@Religion", model.Religion));
            para.Add(new("@GenderAutoId", model.GenderAutoId));
            para.Add(new("@MobileNo", model.MobileNo));
            if (model.ResidentAutoId <= 0)
                para.Add(new("@EnrollmentDate", model.EnrollementDate));
            return para;
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
    }
}
