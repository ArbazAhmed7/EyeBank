using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.PublicSpaces.Model;

namespace TransportManagementCore.Areas.PublicSpaces.Repositories
{
    public class PublicSpacesRepo
    {
        DBHelper.DBHelper db;
        public PublicSpacesRepo()
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
        public List<SqlParameter> SetModel(List<SqlParameter> para, PublicSpacesModel model)
        {
            para.Add(new("@PublicSpacesAutoId", model.PublicSpacesAutoId));
            para.Add(new("@PublicSpacesCode", model.PublicSpacesCode));
            para.Add(new("@PublicSpacesName", model.PublicSpacesName));
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
            if (model.PublicSpacesAutoId <= 0)
                para.Add(new("@EnrollmentDate", model.EnrollementDate));
            return para;
        }

        public List<SqlParameter> SetModelImage(List<SqlParameter> para, PublicSpacesImageModel cm, int PublicSpacesAutoId)
        {

            if (cm.FileName != "" && cm.FileName != null)
            {
                para.Add(new("@PublicSpacesImageAutoId", cm.PublicSpacesImageAutoId));
                para.Add(new("@PublicSpacesAutoId", PublicSpacesAutoId));
                para.Add(new("@PublicSpacesPic", cm.PublicSpacesPicture));
                para.Add(new("@CaptureRemarks", cm.CaptureRemarks));
                para.Add(new("@FileType", cm.FileType));
                para.Add(new("@FileSize", cm.FileSize));
                para.Add(new("@CaptureDate", cm.CaptureDate));
            }
            return para;
        }

        public PublicSpacesModel GetModel(DataTable dt)
        {
            PublicSpacesModel CM = new PublicSpacesModel();
            List<PublicSpacesImageModel> ListModel = new List<PublicSpacesImageModel>();
            foreach (DataRow row in dt.Rows)
            {
                PublicSpacesImageModel DetailModel = new PublicSpacesImageModel();
                CM.PublicSpacesAutoId = Convert.ToInt32(row["PublicSpacesAutoId"]);
                CM.PublicSpacesCode = row["PublicSpacesCode"].ToString();
                CM.PublicSpacesName = row["PublicSpacesName"].ToString();
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
                if (!row["DetailPublicSpacesImageAutoId"].Equals(DBNull.Value))
                    DetailModel.PublicSpacesAutoId = Convert.ToInt32(row["DetailPublicSpacesImageAutoId"]);
                if (!row["PublicSpacesImageAutoId"].Equals(DBNull.Value))
                    DetailModel.PublicSpacesImageAutoId = Convert.ToInt32(row["PublicSpacesImageAutoId"]);
                if (!row["FileSize"].Equals(DBNull.Value))
                    DetailModel.FileSize = Convert.ToInt32(row["FileSize"]);
                if (!row["FileType"].Equals(DBNull.Value))
                    DetailModel.FileType = Convert.ToString(row["FileType"]);
                if (!row["PublicSpacesPic"].Equals(DBNull.Value))
                    DetailModel.PublicSpacesPicture = Convert.ToString(row["PublicSpacesPic"]);
                if (!row["CaptureRemarks"].Equals(DBNull.Value))
                    DetailModel.CaptureRemarks = Convert.ToString(row["CaptureRemarks"]);

                ListModel.Add(DetailModel);
            }
            CM.ImageList = ListModel;
            return CM;
        }
    }
}
