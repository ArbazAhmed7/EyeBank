using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Setup.Model;
using TransportManagementCore.Models;

namespace TransportManagementCore.Areas.Setup.Repositories
{
    public class CompanyWorkerEnrollmentRepo
    {
        DBHelper.DBHelper db;
        public CompanyWorkerEnrollmentRepo()
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
                dt.Rows[0][0] = 0;
                dt.Rows[0][1] = "Error :" + ex.Message.ToString();
            }
            return dt;
        }

     
            public List<DropDownModel> List(DataTable dt)
        {
            List<DropDownModel> Lst = new List<DropDownModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DropDownModel dm = new DropDownModel();
                    dm.Code = row["Code"].ToString();
                    dm.Text = row["Text"].ToString();
                    dm.Type = row["Type"].ToString();
                    Lst.Add(dm);
                }
            }
            return Lst;
        }

        public List<SqlParameter> SetModelImage(List<SqlParameter> para, CompanyWorkerEnrollmentImageModel cm, int WorkerId, int CompanyId)
        {

            if (cm.FileName != "" && cm.FileName != null)
            {
                //cm.CompanyPic = Convert.FromBase64String(cm.CompanyPicture);
                para.Add(new("@WorkerImageAutoId", cm.WorkerImageAutoId));
                para.Add(new("@WorkerAutoId", WorkerId));
                para.Add(new("@CompanyAutoId", CompanyId));
                para.Add(new("@WorkerPic", cm.WorkerPicture));
                para.Add(new("@CaptureRemarks", cm.CaptureRemarks));
                para.Add(new("@FileType", cm.FileType));
                para.Add(new("@FileSize", cm.FileSize));
            }
            return para;
        }
        public List<SqlParameter> SetModel(List<SqlParameter> para, CompanyWorkerEnrollmentModel model)
        {
            para.Add(new("@WorkerAutoId", model.WorkerAutoId));
            para.Add(new("@WorkerName", model.WorkerName));
            para.Add(new("@WorkerCode", model.WorkerCode));
            para.Add(new("@CompanyAutoId", model.CompanyAutoId));
            para.Add(new("@CompanyCode", model.CompanyCode));
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
            if (model.WorkerAutoId <=0 )
                para.Add(new("@EnrollmentDate", model.EnrollementDate));
            return para;
        }
        public CompanyWorkerEnrollmentModel GetModel(DataTable dt)
        {
            CompanyWorkerEnrollmentModel CM = new CompanyWorkerEnrollmentModel();
            List<CompanyWorkerEnrollmentImageModel> ListModel = new List<CompanyWorkerEnrollmentImageModel>();
            foreach (DataRow row in dt.Rows)
            {
                CompanyWorkerEnrollmentImageModel DetailModel = new CompanyWorkerEnrollmentImageModel();
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
                CM.CompanyName = row["CompanyName"].ToString();
                if (!row["DetailCompanyAutoId"].Equals(DBNull.Value))
                    DetailModel.DetailCompanyAutoId = Convert.ToInt32(row["DetailCompanyAutoId"]);

                if (!row["WorkerAutoId"].Equals(DBNull.Value))
                    DetailModel.WorkerAutoId = Convert.ToInt32(row["WorkerAutoId"]);

                if (!row["WorkerImageAutoId"].Equals(DBNull.Value))
                    DetailModel.WorkerImageAutoId = Convert.ToInt32(row["WorkerImageAutoId"]);

                if (!row["FileSize"].Equals(DBNull.Value))
                    DetailModel.FileSize = Convert.ToInt32(row["FileSize"]);
                if (!row["FileType"].Equals(DBNull.Value))
                    DetailModel.FileType = Convert.ToString(row["FileType"]);

                if (!row["WorkerPic"].Equals(DBNull.Value))
                    DetailModel.WorkerPicture = Convert.ToString(row["WorkerPic"]);

                if (!row["captureRemarks"].Equals(DBNull.Value))
                    DetailModel.CaptureRemarks = Convert.ToString(row["captureRemarks"]);

                ListModel.Add(DetailModel);
            }
            CM.ImageList = ListModel;
            return CM;
        }


    }
}
