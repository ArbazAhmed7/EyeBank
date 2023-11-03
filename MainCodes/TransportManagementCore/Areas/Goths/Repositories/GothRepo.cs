using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Goths.Model;

namespace TransportManagementCore.Areas.Goths.Repositories
{
    public class GothRepo
    {
        DBHelper.DBHelper db;
        public GothRepo()
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
        public List<SqlParameter> SetModel(List<SqlParameter> para, GothModel model)
        {
            para.Add(new("@GothAutoId", model.GothAutoId));
            para.Add(new("@GothCode", model.GothCode));
            para.Add(new("@GothName", model.GothName));
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
            if (model.GothAutoId <= 0)
                para.Add(new("@EnrollmentDate", model.EnrollementDate));
            return para;
        }

        public List<SqlParameter> SetModelImage(List<SqlParameter> para, GothImageModel cm, int GothAutoId)
        {

            if (cm.FileName != "" && cm.FileName != null)
            {
                para.Add(new("@GothImageAutoId", cm.GothImageAutoId));
                para.Add(new("@GothAutoId", GothAutoId));
                para.Add(new("@GothPic", cm.GothPicture));
                para.Add(new("@CaptureRemarks", cm.CaptureRemarks));
                para.Add(new("@FileType", cm.FileType));
                para.Add(new("@FileSize", cm.FileSize));
                para.Add(new("@CaptureDate", cm.CaptureDate));
            }
            return para;
        }

        public GothModel GetModel(DataTable dt)
        {
            GothModel CM = new GothModel();
            List<GothImageModel> ListModel = new List<GothImageModel>();
            foreach (DataRow row in dt.Rows)
            {
                GothImageModel DetailModel = new GothImageModel();
                CM.GothAutoId = Convert.ToInt32(row["GothAutoId"]);
                CM.GothCode = row["GothCode"].ToString();
                CM.GothName = row["GothName"].ToString();
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
                if (!row["DetailGothImageAutoId"].Equals(DBNull.Value))
                    DetailModel.GothAutoId = Convert.ToInt32(row["DetailGothImageAutoId"]);
                if (!row["GothImageAutoId"].Equals(DBNull.Value))
                    DetailModel.GothImageAutoId = Convert.ToInt32(row["GothImageAutoId"]);
                if (!row["FileSize"].Equals(DBNull.Value))
                    DetailModel.FileSize = Convert.ToInt32(row["FileSize"]);
                if (!row["FileType"].Equals(DBNull.Value))
                    DetailModel.FileType = Convert.ToString(row["FileType"]);
                if (!row["GothPic"].Equals(DBNull.Value))
                    DetailModel.GothPicture = Convert.ToString(row["GothPic"]);
                if (!row["CaptureRemarks"].Equals(DBNull.Value))
                    DetailModel.CaptureRemarks = Convert.ToString(row["CaptureRemarks"]);

                ListModel.Add(DetailModel);
            }
            CM.ImageList = ListModel;
            return CM;
        }
    }
}
