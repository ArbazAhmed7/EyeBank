using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Setup.Model;

namespace TransportManagementCore.Areas.Setup.Repositories
{
    public class CompanyRepo
    {
        DBHelper.DBHelper db;
        public CompanyRepo()
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
                dt.Rows[0][0] = -1;
                dt.Rows[0][1] = "Error :" + ex.Message.ToString();
            }
            return dt;
        }
        public List<SqlParameter> SetModel(List<SqlParameter> para, CompanyModel model)
        {
            para.Add(new("@CompanyAutoId", model.CompanyAutoId));
            para.Add(new("@CompanyCode", model.CompanyCode));
            para.Add(new("@CompanyName", model.CompanyName));
            para.Add(new("@Website", model.Website));
            para.Add(new("@Address1", model.Address1));
            para.Add(new("@Address2", model.Address2));
            para.Add(new("@Address3", model.Address3));
            para.Add(new("@District", model.District));
            para.Add(new("@Town", model.Town));
            para.Add(new("@City", model.City));
            para.Add(new("@WorkForce", model.WorkForce));
            para.Add(new("@OwnerName ", model.OwnerName));
            para.Add(new("@OwnerMobile", model.OwnerMobile));
            para.Add(new("@OwnerEmail", model.OwnerEmail));
            para.Add(new("@AdminHeadName", model.AdminHeadName));
            para.Add(new("@AdminHeadMobile", model.AdminHeadMobile));
            para.Add(new("@AdminHeadEmail", model.AdminHeadEmail)); 
            para.Add(new("@HRHeadName", model.HRHeadName));
            para.Add(new("@HRHeadMobile", model.HRHeadMobile));
            para.Add(new("@HRHeadEmail", model.HRHeadEmail)); 
            para.Add(new("@TitleAutoId", 0));
            if(model.CompanyAutoId<=0)
                para.Add(new("@EnrollmentDate", model.EnrollementDate));
            return para;
        }

        public List<SqlParameter> SetModelImage(List<SqlParameter> para, CompanyImageModel cm, int CompanyId)
        {

            if (cm.FileName != "" && cm.FileName != null)
            {
                para.Add(new("@CompanyImageAutoId", cm.CompanyImageAutoId));
                para.Add(new("@CompanyAutoId", CompanyId));
                para.Add(new("@CompanyPic", cm.CompanyPicture));
                para.Add(new("@CaptureRemarks", cm.CaptureRemarks));
                para.Add(new("@FileType", cm.FileType));
                para.Add(new("@FileSize", cm.FileSize));
            }
            return para;
        }

        public CompanyModel GetModel(DataTable dt)
        {
            CompanyModel CM = new CompanyModel();
            List<CompanyImageModel> ListModel = new List<CompanyImageModel>();
            foreach (DataRow row in dt.Rows)
            {
                CompanyImageModel DetailModel = new CompanyImageModel();
                CM.CompanyAutoId = Convert.ToInt32(row["companyAutoId"]);
                CM.CompanyCode = row["CompanyCode"].ToString();
                CM.CompanyName = row["CompanyName"].ToString();
                CM.Address1 = row["Address1"].ToString();
                CM.Address2 = row["Address2"].ToString();
                CM.Address3 = row["Address3"].ToString();
                CM.Website= row["Website"].ToString();
                CM.District = row["District"].ToString();
                CM.Town = row["Town"].ToString();
                CM.City = row["City"].ToString();
                CM.WorkForce = Convert.ToInt32(row["WorkForce"].ToString());
                CM.OwnerName = row["OwnerName"].ToString();
                CM.OwnerMobile = row["OwnerMobile"].ToString();
                CM.OwnerEmail = row["OwnerEmail"].ToString();
                CM.AdminHeadName = row["AdminHeadName"].ToString();
                CM.AdminHeadMobile = row["AdminHeadMobile"].ToString();
                CM.AdminHeadEmail = row["AdminHeadEmail"].ToString();
                CM.HRHeadName = row["HRHeadName"].ToString();
                CM.HRHeadEmail = row["HRHeadEmail"].ToString();
                CM.HRHeadMobile = row["HRHeadMobile"].ToString();

                CM.EnrollementDate = Convert.ToDateTime(row["EnrollmentDate"].ToString());
                if (!row["DetailCompanyImageAutoId"].Equals(DBNull.Value))
                    DetailModel.CompanyAutoId = Convert.ToInt32(row["DetailCompanyImageAutoId"]);
                if (!row["CompanyImageAutoId"].Equals(DBNull.Value))
                    DetailModel.CompanyImageAutoId = Convert.ToInt32(row["CompanyImageAutoId"]);
                if (!row["FileSize"].Equals(DBNull.Value))
                    DetailModel.FileSize = Convert.ToInt32(row["FileSize"]);
                if (!row["FileType"].Equals(DBNull.Value))
                    DetailModel.FileType = Convert.ToString(row["FileType"]);
                if (!row["CompanyPic"].Equals(DBNull.Value))
                    DetailModel.CompanyPicture = Convert.ToString(row["CompanyPic"]);
                if (!row["CaptureRemarks"].Equals(DBNull.Value))
                    DetailModel.CaptureRemarks = Convert.ToString(row["CaptureRemarks"]);

                ListModel.Add(DetailModel);
            }
            CM.ImageList = ListModel;
            return CM;
        }


    }
}
