using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Factory.Model;
using TransportManagementCore.Areas.Goths.Model;
using TransportManagementCore.Areas.Localities.Model;
using TransportManagementCore.Models;

namespace TransportManagementCore.Areas.Goths.Repositories
{
    public class VisitForSurgeryGothResidentRepo
    {
        DBHelper.DBHelper db;
        public VisitForSurgeryGothResidentRepo()
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


        public VisitForSurgeryGothResidentModel GetVisitForSurgeryGothResidentModel(DataTable dt)
        {
            VisitForSurgeryGothResidentModel model = new VisitForSurgeryGothResidentModel();
            List<VisitForSurgeryGothResidentDocuments> listDocument = new List<VisitForSurgeryGothResidentDocuments>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    model.VisitSurgeryGothResidentId = Convert.ToInt32(row["VisitSurgeryGothResidentId"]);
                    model.OptometristGothResidentId = Convert.ToInt32(row["OptometristGothResidentId"]);
                    model.ResidentAutoId = Convert.ToInt32(row["ResidentAutoId"]);
                    model.GothAutoId = Convert.ToInt32(row["GothAutoId"]);
                    model.VisitDate = Convert.ToDateTime(row["VisitDate"]);
                    model.Hospital = Convert.ToString(row["Hospital"]);
                    model.Optometrist = Convert.ToString(row["Optometrist"]);
                    model.Ophthalmologist = Convert.ToString(row["Ophthalmologist"]);
                    model.Surgeon = Convert.ToString(row["Surgeon"]);


                    model.NameOfSurgery = Convert.ToString(row["NameOfSurgery"]);
                    model.PostSurgeryVisitDate = Convert.ToDateTime(row["PostSurgeryVisitDate"]);
                    model.Eye = Convert.ToString(row["Eye"]);
                    model.CommentOfSurgeonAfterSurgery = Convert.ToString(row["CommentOfSurgeonAfterSurgery"]);

                    VisitForSurgeryGothResidentDocuments vm = new VisitForSurgeryGothResidentDocuments();
                    if (!row["SurgeryGothResidentDocumentsId"].Equals(DBNull.Value))
                        vm.SurgeryGothResidentDocumentsId = Convert.ToInt32(row["SurgeryGothResidentDocumentsId"]);
                    //if (!row["DocumentFile"].Equals(DBNull.Value))
                    //    vm.DocumentFile = (byte[])(row["DocumentFile"]);

                    if (!row["DocumentDate"].Equals(DBNull.Value))
                        vm.DocumentDate = Convert.ToDateTime(row["DocumentDate"]);

                    if (!row["FileName"].Equals(DBNull.Value))
                        vm.FileName = Convert.ToString(row["FileName"]);

                    if (!row["FileType"].Equals(DBNull.Value))
                        vm.FileType = Convert.ToString(row["FileType"]);
                    vm.VisitSurgeryGothResidentId = model.VisitSurgeryGothResidentId;
                    vm.ResidentAutoId = model.ResidentAutoId;
                    listDocument.Add(vm);


                }
                model.Modelfiles = listDocument;
            }
            return model;
        }

        public VisitForSurgeryGothResidentDocuments GetDocumentsById(DataTable dt)
        {
            VisitForSurgeryGothResidentDocuments model = new VisitForSurgeryGothResidentDocuments();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (!row["DocumentFile"].Equals(DBNull.Value))
                        model.DocumentFile = (byte[])(row["DocumentFile"]);

                    if (!row["FileType"].Equals(DBNull.Value))
                        model.FileType = (string)(row["FileType"]);

                    if (!row["FileName"].Equals(DBNull.Value))
                        model.FileName = (string)(row["FileName"]);
                }
            }
            return model;
        }
        public List<SqlParameter> SetModel(List<SqlParameter> para, VisitForSurgeryGothResidentModel model)
        {
            para.Add(new("@VisitSurgeryGothResidentId", model.VisitSurgeryGothResidentId));
            para.Add(new("@OptometristGothResidentId", model.OptometristGothResidentId));
            para.Add(new("@ResidentAutoId", model.ResidentAutoId));
            para.Add(new("@GothAutoId", model.GothAutoId));
            para.Add(new("@VisitDate", model.VisitDate));
            para.Add(new("@Hospital", model.Hospital));
            para.Add(new("@Optometrist", model.Optometrist));
            para.Add(new("@Ophthalmologist", model.Ophthalmologist));
            para.Add(new("@Surgeon", model.Surgeon));
            para.Add(new("@NameOfSurgery", model.NameOfSurgery));
            para.Add(new("@PostSurgeryVisitDate", model.PostSurgeryVisitDate));
            para.Add(new("@Eye", model.Eye));
            para.Add(new("@CommentOfSurgeonAfterSurgery", model.CommentOfSurgeonAfterSurgery));
            return para;
        }
        public List<SqlParameter> SetModelDocument(List<SqlParameter> para, VisitForSurgeryGothResidentDocuments model)
        {
            para.Add(new("@VisitSurgeryGothResidentId", model.VisitSurgeryGothResidentId));
            para.Add(new("@SurgeryGothResidentDocumentsId", model.SurgeryGothResidentDocumentsId));
            para.Add(new("@ResidentAutoId", model.ResidentAutoId));
            para.Add(new("@DocumentDate", model.DocumentDate));
            para.Add(new("@FileName", model.FileName));
            para.Add(new("@DocumentFile", model.DocumentFile));
            para.Add(new("@FileType", model.FileType));
            return para;
        }

        public DisplayGothGlassDispenseResidentModel GetWorkerLastHistory(DataTable dt)
        {
            DisplayGothGlassDispenseResidentModel auto = null;
            if (dt.Rows.Count > 0)
            {
                auto = new DisplayGothGlassDispenseResidentModel();
                auto.WearGlasses = Convert.ToBoolean(dt.Rows[0]["WearGlasses"]);
                auto.Distance = Convert.ToBoolean(dt.Rows[0]["Distance"]);
                auto.Near = Convert.ToBoolean(dt.Rows[0]["Near"]);
                auto.OptometristGothResidentId = Convert.ToInt32(dt.Rows[0]["OptometristGothResidentId"]);
                auto.Gender = Convert.ToString(dt.Rows[0]["Gender"]);
                auto.Age = Convert.ToInt32(dt.Rows[0]["Age"]);
                auto.MobileNo = Convert.ToString(dt.Rows[0]["MobileNo"]);
            }
            return auto;
        }
    }
}
