using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Factory.Model;
using TransportManagementCore.Models;

namespace TransportManagementCore.Areas.Factory.Repositories
{
    public class VisitForSurgeryWorkerRepo
    {
        DBHelper.DBHelper db;
        public VisitForSurgeryWorkerRepo()
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

        
        public VisitForSurgeryWorkerModel GetVisitForSurgeryWorkerModel(DataTable dt)
        {
            VisitForSurgeryWorkerModel model = new VisitForSurgeryWorkerModel();
            List<VisitForSurgeryWorkerDocumentsModel> listDocument = new List<VisitForSurgeryWorkerDocumentsModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    model.VisitSurgeryWorkerId = Convert.ToInt32(row["VisitSurgeryWorkerId"]);
                    model.OptometristWorkerId = Convert.ToInt32(row["OptometristWorkerId"]);
                    model.WorkerAutoId = Convert.ToInt32(row["WorkerAutoId"]);
                    model.CompanyAutoId = Convert.ToInt32(row["CompanyAutoId"]);
                    model.VisitDate = Convert.ToDateTime(row["VisitDate"]);
                    model.Hospital = Convert.ToString(row["Hospital"]);
                    model.Optometrist = Convert.ToString(row["Optometrist"]);
                    model.Ophthalmologist = Convert.ToString(row["Ophthalmologist"]);
                    model.Surgeon = Convert.ToString(row["Surgeon"]);


                    model.NameOfSurgery = Convert.ToString(row["NameOfSurgery"]);
                    model.PostSurgeryVisitDate = Convert.ToDateTime(row["PostSurgeryVisitDate"]);
                    model.Eye = Convert.ToString(row["Eye"]);
                    model.CommentOfSurgeonAfterSurgery = Convert.ToString(row["CommentOfSurgeonAfterSurgery"]);

                    VisitForSurgeryWorkerDocumentsModel vm = new VisitForSurgeryWorkerDocumentsModel();
                    if (!row["SurgeryWorkerDocumentsId"].Equals(DBNull.Value))
                        vm.SurgeryWorkerDocumentsId = Convert.ToInt32(row["SurgeryWorkerDocumentsId"]);
                    //if (!row["DocumentFile"].Equals(DBNull.Value))
                    //    vm.DocumentFile = (byte[])(row["DocumentFile"]);

                    if (!row["DocumentDate"].Equals(DBNull.Value))
                        vm.DocumentDate = Convert.ToDateTime(row["DocumentDate"]);

                    if (!row["FileName"].Equals(DBNull.Value))
                        vm.FileName = Convert.ToString(row["FileName"]);

                    if (!row["FileType"].Equals(DBNull.Value))
                        vm.FileType = Convert.ToString(row["FileType"]);
                    vm.VisitSurgeryWorkerId = model.VisitSurgeryWorkerId;
                    vm.WorkerAutoId = model.WorkerAutoId;
                    listDocument.Add(vm);
                    

                }
                model.Modelfiles=listDocument;
            }
            return model;
        }

        public VisitForSurgeryWorkerDocumentsModel  GetDocumentsById(DataTable dt)
        {
            VisitForSurgeryWorkerDocumentsModel model = new VisitForSurgeryWorkerDocumentsModel();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (!row["DocumentFile"].Equals(DBNull.Value))
                        model .DocumentFile= (byte[])(row["DocumentFile"]);

                    if (!row["FileType"].Equals(DBNull.Value))
                        model.FileType = (string)(row["FileType"]);

                    if (!row["FileName"].Equals(DBNull.Value))
                        model.FileName = (string)(row["FileName"]);
                }
            }
            return model;
        }
        public List<SqlParameter> SetModel(List<SqlParameter> para, VisitForSurgeryWorkerModel model)
        {
            para.Add(new("@VisitSurgeryWorkerId", model.VisitSurgeryWorkerId));
            para.Add(new("@OptometristWorkerId", model.OptometristWorkerId));
            para.Add(new("@WorkerAutoId", model.WorkerAutoId));
            para.Add(new("@CompanyAutoId", model.CompanyAutoId));
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
        public List<SqlParameter> SetModelDocument(List<SqlParameter> para, VisitForSurgeryWorkerDocumentsModel model)
        {
            para.Add(new("@VisitSurgeryWorkerId", model.VisitSurgeryWorkerId));
            para.Add(new("@SurgeryWorkerDocumentsId", model.SurgeryWorkerDocumentsId));
            para.Add(new("@WorkerAutoId", model.WorkerAutoId));
            para.Add(new("@DocumentDate", model.DocumentDate));
            para.Add(new("@FileName", model.FileName));
            para.Add(new("@DocumentFile", model.DocumentFile));
            para.Add(new("@FileType", model.FileType));
            return para;
        }
           
        public DisplayGlassDispenseWorkerModel GetWorkerLastHistory(DataTable dt)
        {
            DisplayGlassDispenseWorkerModel auto = null;
            if (dt.Rows.Count > 0)
            {
                auto = new DisplayGlassDispenseWorkerModel();  
                auto.WearGlasses = Convert.ToBoolean(dt.Rows[0]["WearGlasses"]);
                auto.Distance = Convert.ToBoolean(dt.Rows[0]["Distance"]);
                auto.Near = Convert.ToBoolean(dt.Rows[0]["Near"]);
                auto.OptometristWorkerId = Convert.ToInt32(dt.Rows[0]["OptometristWorkerId"]);
                auto.Gender = Convert.ToString(dt.Rows[0]["Gender"]);
                auto.Age = Convert.ToInt32(dt.Rows[0]["Age"]);
                auto.MobileNo = Convert.ToString(dt.Rows[0]["MobileNo"]);
            }
            return auto;
        }
    }
}

