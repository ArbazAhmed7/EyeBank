using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Localities.Model;

namespace TransportManagementCore.Areas.Localities.Repositories
{
    public class LocalitiesRepo
    {
        DBHelper.DBHelper db;
        public LocalitiesRepo()
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
        public List<SqlParameter> SetModel(List<SqlParameter> para, LocalitiesModel model)
        {
            para.Add(new("@LocalityAutoId", model.LocalityAutoId));
            para.Add(new("@LocalityCode", model.LocalityCode));
            para.Add(new("@LocalityName", model.LocalityName));
            para.Add(new("@Website", model.Website));
            para.Add(new("@Address1", model.Address1));
            para.Add(new("@Address2", model.Address2));
            para.Add(new("@Address3", model.Address3));
            para.Add(new("@District", model.District));
            para.Add(new("@Town", model.Town));
            para.Add(new("@City", model.City));
            para.Add(new("@NameofPerson ", model.NameofPerson));
            para.Add(new("@PersonMobile", model.PersonMobile));
            para.Add(new("@PersonRole", model.PersonRole));
            para.Add(new("@TitleAutoId", 0));
            if (model.LocalityAutoId <= 0)
                para.Add(new("@EnrollmentDate", model.EnrollementDate));
            return para;
        }

        public List<SqlParameter> SetModelImage(List<SqlParameter> para, LocalityImageModel cm, int LocalityAutoId)
        {

            if (cm.FileName != "" && cm.FileName != null)
            {
                para.Add(new("@LocalityImageAutoId", cm.LocalityImageAutoId));
                para.Add(new("@LocalityAutoId", LocalityAutoId));
                para.Add(new("@LocalityPic", cm.LocalityPicture));
                para.Add(new("@CaptureRemarks", cm.CaptureRemarks));
                para.Add(new("@FileType", cm.FileType));
                para.Add(new("@FileSize", cm.FileSize));
                para.Add(new("@CaptureDate", cm.CaptureDate ));
            }
            return para;
        }

        public LocalitiesModel GetModel(DataTable dt)
        {
            LocalitiesModel CM = new LocalitiesModel();
            List<LocalityImageModel> ListModel = new List<LocalityImageModel>();
            foreach (DataRow row in dt.Rows)
            {
                LocalityImageModel DetailModel = new LocalityImageModel();
                CM.LocalityAutoId = Convert.ToInt32(row["localityAutoId"]);
                CM.LocalityCode = row["localityCode"].ToString();
                CM.LocalityName = row["localityName"].ToString();
                CM.Address1 = row["Address1"].ToString();
                CM.Address2 = row["Address2"].ToString();
                CM.Address3 = row["Address3"].ToString();
                CM.Website = row["Website"].ToString();
                CM.District = row["District"].ToString();
                CM.Town = row["Town"].ToString();
                CM.City = row["City"].ToString();
                CM.NameofPerson = row["nameofPerson"].ToString();
                CM.PersonMobile = row["personMobile"].ToString();
                CM.PersonRole = row["personRole"].ToString();

                CM.EnrollementDate = Convert.ToDateTime(row["EnrollmentDate"].ToString());
                if (!row["DetailLocalityImageAutoId"].Equals(DBNull.Value))
                    DetailModel.LocalityAutoId = Convert.ToInt32(row["DetailLocalityImageAutoId"]);
                if (!row["LocalityImageAutoId"].Equals(DBNull.Value))
                    DetailModel.LocalityImageAutoId = Convert.ToInt32(row["LocalityImageAutoId"]);
                if (!row["FileSize"].Equals(DBNull.Value))
                    DetailModel.FileSize = Convert.ToInt32(row["FileSize"]);
                if (!row["FileType"].Equals(DBNull.Value))
                    DetailModel.FileType = Convert.ToString(row["FileType"]);
                if (!row["LocalityPic"].Equals(DBNull.Value))
                    DetailModel.LocalityPicture = Convert.ToString(row["LocalityPic"]);
                if (!row["CaptureRemarks"].Equals(DBNull.Value))
                    DetailModel.CaptureRemarks = Convert.ToString(row["CaptureRemarks"]);

                ListModel.Add(DetailModel);
            }
            CM.ImageList = ListModel;
            return CM;
        }
    }
}
