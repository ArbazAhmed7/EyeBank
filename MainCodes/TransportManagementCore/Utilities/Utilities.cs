using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using TransportManagementCore;
using TransportManagementCore.Models;
using TransportManagementCore.Models.DataTables;

namespace TransportManagementCore.Utilities
{
    public static class General
    {
        public static byte[] GetBytesFromImage(String imageFile)
        {
            MemoryStream ms = new MemoryStream();
            Image img = Image.FromFile(imageFile);
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            return ms.ToArray();
        }

        public static string GetRandomString(int size, bool singleCase = false)
        {
            Random rand = new Random();
            string seed = "abcdefghijklmnopqrstuvwyxz0123456789";
            if (!singleCase)
                seed += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = seed[rand.Next(seed.Length)];
            }
            return new string(chars);
        }
    

        //public static DataTable SendEmail(EmailModel emailModel, string FormId = "N/A")
        //{
        //    DBHelper dBHelper = new DBHelper();
        //    SqlParameter[] parameters =
        //    {
        //            new SqlParameter("@recipients", SqlDbType.VarChar) { Value = emailModel.recipients },
        //            new SqlParameter("@copy_recipients", SqlDbType.VarChar) { Value = emailModel.copy_recipients },
        //            new SqlParameter("@blind_copy_recipients", SqlDbType.VarChar) { Value = emailModel.blind_copy_recipients },
        //            new SqlParameter("@subject", SqlDbType.VarChar) { Value = emailModel.subject },
        //            new SqlParameter("@body", SqlDbType.VarChar) { Value = emailModel.body },
        //            new SqlParameter("@Attachments", SqlDbType.VarChar) { Value = emailModel.Attachments },
        //            new SqlParameter("@BranchID", SqlDbType.VarChar) { Value = Global.CurrentUser.BranchId },
        //            new SqlParameter("@FormId", SqlDbType.VarChar) { Value = FormId},
        //            new SqlParameter("@CreatedBy", SqlDbType.VarChar) { Value = Global.CurrentUser.UserId },
        //            new SqlParameter("@EntryTerminal", SqlDbType.VarChar) { Value = Global.CurrentUser.EntryTerminal },
        //            new SqlParameter("@EntryTerminalIP", SqlDbType.VarChar) { Value = Global.CurrentUser.EntryTerminalIP },
        //    };
        //    DataTable dtDonors = dBHelper.GetDataTable(commandText: "Sp_SendEmail", commandType: CommandType.StoredProcedure, parameters);
        //    return dtDonors;
        //}

        public static string SaveFileToDrive()
        {
            return "";
        }

        public  static AutherizedFormRights GetFormRights(string UserId, string FormId)
        {
            bool res = false;
            if (UserId == null)
                UserId = "";
            TransportManagementCore.DBHelper.DBHelper dBHelper = new TransportManagementCore.DBHelper.DBHelper();
            AutherizedFormRights autherizedFormRights = new AutherizedFormRights();
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", SqlDbType.VarChar) { Value = UserId },
                new SqlParameter("@FormId", SqlDbType.VarChar) { Value = FormId },
            };

            DataTable rdr = dBHelper.GetDataTable(commandText: "sp_UserRights", commandType: CommandType.StoredProcedure, parameters);

            if (rdr.Rows.Count > 0)
            {
                autherizedFormRights.CanView = Convert.ToBoolean(rdr.Rows[0][0]);
            }
            else
            {
                autherizedFormRights.CanView = false;
            }

            return autherizedFormRights;
        }

        public static void WriteLog(string msg)
        {
            DateTime dateTime = DateTime.Now;

            string fileName = string.Concat("Log_", dateTime.ToString("MM-dd-yyyy"), ".txt");
            string folderName = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\";
            string completePath = string.Concat(folderName, fileName);

            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            StreamWriter streamWriter = new StreamWriter(completePath, true);

            if (File.Exists(completePath))
            {
                msg = string.Concat(msg, " Message at ", DateTime.Now.ToString(""));
                streamWriter.WriteLine(msg);
                streamWriter.Close();
            }
        }
        public static string GetFileExtension(string contentType)
        {
            switch (contentType)
            {
                case "image/jpeg":
                    return ".jpg";
                case "application/pdf":
                    return ".pdf";
                case "application/msword":
                    return ".docx";
                // Add more cases for other file types as needed.
                default:
                    return contentType; // Default to a generic extension.
            }
        }
    }


    public static class Encryption
    {
         
        public static string GetDecrypt(string DecryptString)
        {
            string Str = "";
            string Key = "";
            object cipher = null;
            cipher = DecryptString;
            Key = "5d7er4ty75es4rt4ybv478x6x-w7@*^ds*#dc$0%";
            if ((3 * Key.Length) >= Strings.Len(cipher))
            {
                Key = Strings.Mid(Key, 1, (Strings.Len(cipher) / 3));
                for (int i = 1; i <= Key.Length; i++)
                {
                    Str = Str + Convert.ToString(Strings.Chr(Convert.ToInt32(Strings.Mid(Convert.ToString(cipher), ((i * 3) - 2), 3)) ^ Strings.Asc(Strings.Mid(Key, i, 1))));
                }
            }
            return Str;
        }

        public static string GetEncrypt(string EncryptString)
        {
            string Str = EncryptString;
            string Key = "";
            object c = null;
            string cipher = "";
            Key = "5d7er4ty75es4rt4ybv478x6x-w7@*^ds*#dc$0%";
            if (Key.Length >= Str.Length)
            {
                Key = Strings.Mid(Key, 1, Strings.Len(Str));
                for (int i = 1; i <= Key.Length; i++)
                {
                    c = Convert.ToInt32(Strings.Asc(Strings.Mid(Str, i, 1)) ^ Strings.Asc(Strings.Mid(Key, i, 1)));
                    if (Convert.ToInt32(c) < 10)
                    {
                        c = "00" + Convert.ToString(c);
                    }
                    if (Convert.ToInt32(c) > 9 && Convert.ToInt32(c) < 100)
                    {
                        c = "0" + Convert.ToString(c);
                    }
                    cipher = cipher + Convert.ToString(c);
                }
            }
            return cipher;
        }
   
    }

    public static class DataTables
    {

        private class DTO
        {
            public List<object> columns { get; set; }

            public List<object[]> data { get; set; }

            public int[] columnsToHide { get; set; }

            public List<ActionButton> actionButtons { get; set; }


        }

        public static object DataTableSourceForLookup(string query, int[] columnsToHide)
        {

            using (var reader = DBHelper.DBHelper.GetReader(query))
            {

                List<object[]> data = new List<object[]>();
                while (reader.Read())
                {
                    object[] row = new object[reader.FieldCount + 1];
                    row[0] = "select";
                    for (int i = 1; i < reader.FieldCount + 1; i++)
                    {
                        if (reader.IsDBNull(i - 1))
                            row[i] = "";
                        else
                            row[i] = reader.GetValue(i - 1);
                    }
                    data.Add(row);
                }

                List<object> columns = new List<object>();
                columns.Add(new { title = "Select" });
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columns.Add(new { title = reader.GetName(i) });
                }

                DTO dto = new DTO
                {
                    columns = columns,
                    data = data
                };

                if (columnsToHide != null)
                {
                    for (int i = 0; i < columnsToHide.Length; i++)
                    {
                        columnsToHide[i] = columnsToHide[i] + 1;
                    }
                    dto.columnsToHide = columnsToHide;
                }

                return dto;

            }

        }

        public static object DataTableSourceForLookup(DataTable dt, int[] columnsToHide)
        {
            using (var reader = dt.CreateDataReader())
            {

                List<object[]> data = new List<object[]>();
                while (reader.Read())
                {
                    object[] row = new object[reader.FieldCount + 1];
                    row[0] = "select";
                    for (int i = 1; i < reader.FieldCount + 1; i++)
                    {
                        if (reader.IsDBNull(i - 1))
                            row[i] = "";
                        else
                            row[i] = reader.GetValue(i - 1);
                    }
                    data.Add(row);
                }

                List<object> columns = new List<object>();
                columns.Add(new { title = "Select" });
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columns.Add(new { title = reader.GetName(i) });
                }

                DTO dto = new DTO
                {
                    columns = columns,
                    data = data
                };

                if (columnsToHide != null)
                {
                    for (int i = 0; i < columnsToHide.Length; i++)
                    {
                        columnsToHide[i] = columnsToHide[i] + 1;
                    }
                    dto.columnsToHide = columnsToHide;
                }

                return dto;

            }

        }

        private static object GetData(string query, int[] columnsToHide, List<ActionButton> actionButtons)
        {

            using (var reader = DBHelper.DBHelper.GetReader(query))
            {

                List<object[]> data = new List<object[]>();
                while (reader.Read())
                {
                    object[] row = new object[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.IsDBNull(i))
                            row[i] = "";
                        else
                            row[i] = reader.GetValue(i);
                    }
                    data.Add(row);
                }

                List<object> columns = new List<object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columns.Add(new { title = reader.GetName(i) });
                }


                DTO dto = new DTO
                {
                    columns = columns,
                    data = data
                };

                if (columnsToHide != null)
                    dto.columnsToHide = columnsToHide;

                if (actionButtons != null)
                    dto.actionButtons = actionButtons;

                return dto;

            }

        }

        public static object DataTableSource(string query)
        {
            return GetData(query, null, null);
        }

        public static object DataTableSource(string query, int[] columnsToHide)
        {
            return GetData(query, columnsToHide, null);
        }

        public static object DataTableSource(string query, int[] columnsToHide, List<ActionButton> actionButtons)
        {
            return GetData(query, columnsToHide, actionButtons);
        }

        public static object DataTableSource(string query, string searchKeyword, string[] columnsToSearch, int[] columnsToHide, List<ActionButton> actionButtons)
        {

            if (string.IsNullOrEmpty(query))
                throw new Exception("Source query can not be null.");

            if (string.IsNullOrEmpty(searchKeyword))
                return GetData(query, columnsToHide, actionButtons);

            if (columnsToSearch == null || columnsToSearch.Length == 0)
                throw new Exception("Please provide atleast 1 column name to perform search in.");


            string stringToInject = "";
            for (int i = 0; i < columnsToSearch.Length; i++)
            {
                stringToInject += $"lower({columnsToSearch[i]}) like lower('%{searchKeyword}%')";
                if (i < columnsToSearch.Length - 1)
                {
                    stringToInject += " or ";
                }
            }

            int where = query.LastIndexOf("where", StringComparison.OrdinalIgnoreCase);
            int orderBy = query.LastIndexOf("order by", StringComparison.OrdinalIgnoreCase);
            int groupBy = query.LastIndexOf("group by", StringComparison.OrdinalIgnoreCase);
            int having = query.LastIndexOf("having", StringComparison.OrdinalIgnoreCase);

            int nearestClause;
            var numbers = new[] { orderBy, groupBy, having };
            Array.Sort(numbers);
            nearestClause = numbers[2];
            foreach (int number in numbers)
                if (number != -1 && number < nearestClause)
                    nearestClause = number;

            query = nearestClause == -1 ? (where == -1 ? $"{query} where {stringToInject}" : $"{query} and ({stringToInject})") : (where == -1 ? query.Insert(nearestClause - 1, $" where {stringToInject} ") : query.Insert(nearestClause - 1, $" and ({stringToInject}) "));

            return GetData(query, columnsToHide, actionButtons);

        }

        public static object DataTableSource(DataTable dt, int[] columnsToHide, List<ActionButton> actionButtons, bool RevertColumnsToHide = false)
        {
            using (var reader = dt.CreateDataReader())
            {

                List<object[]> data = new List<object[]>();
                while (reader.Read())
                {
                    object[] row = new object[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.IsDBNull(i))
                            row[i] = "";
                        else
                            row[i] = reader.GetValue(i);
                    }
                    data.Add(row);
                }

                int[] columnIndices = new int[reader.FieldCount];
                List<object> columns = new List<object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnIndices[i] = i;
                    columns.Add(new { title = reader.GetName(i) });
                }

                DTO dto = new DTO
                {
                    columns = columns,
                    data = data
                };

                if (columnsToHide != null && columnsToHide.Length > 0)
                {
                    if (RevertColumnsToHide)
                        columnsToHide = columnIndices.Except(columnsToHide).ToArray();

                    dto.columnsToHide = columnsToHide;

                }

                if (actionButtons != null && actionButtons.Count > 0)
                    dto.actionButtons = actionButtons;



                return dto;

            }

        }


    }

    public static class DropDowns
    {

        public class Extra
        {

            public Extra(string key, string value)
            {
                this.key = key;
                this.value = value;
            }

            public string key { get; set; }
            public string value { get; set; }

        }

        public class DropDownOption
        {
            public string Id { get; set; }
            public string Value { get; set; }
            public List<Extra> extras { get; set; }
        }


        public static object GetData(string option)
        {
            DataTable dt = new DataTable();
            DBHelper.DBHelper db = new DBHelper.DBHelper();

            SqlParameter[] param =
            {
                new SqlParameter("@Option", SqlDbType.VarChar) { Value = option } ,
                new SqlParameter("@UserID", SqlDbType.Int) { Value = Global.CurrentUser.UserId } ,
                new SqlParameter("@BranchID", SqlDbType.Int) { Value = Global.CurrentUser.BranchId }
            };
            dt = db.GetDataTable("sp_Dropdown", CommandType.StoredProcedure, param);
            return GetData(dt);
        }


        public static object GetData(DataTable dt)
        {
            List<DropDownOption> dropDownOptions = new List<DropDownOption>();
            using (var reader = dt.CreateDataReader())
            {
                while (reader.Read())
                {
                    DropDownOption dropDownOption = new DropDownOption();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (i == 0)
                        {
                            dropDownOption.Id = reader.GetValue(i).ToString();
                        }
                        else if (i == 1)
                        {
                            dropDownOption.Value = reader.GetValue(i).ToString();
                        }
                        else
                        {
                            if (dropDownOption.extras == null)
                                dropDownOption.extras = new List<Extra>();
                            dropDownOption.extras.Add(new Extra(reader.GetName(i).ToString(), reader.GetValue(i).ToString()));
                        }
                    }
                    dropDownOptions.Add(dropDownOption);
                }
            }
            return dropDownOptions;
        }

    }

    public static class CompanyCodeFormat
    {
        public static bool CheckAutoCodeFormatEnable(string FormID, int CompanyID)
        {
            bool r = false;
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            sb.AppendFormat("SELECT ParameterID FROM tbl_CompanyParameter WHERE ParameterName='Auto Genrated Code' AND ParameterTitle='{0}' AND CompanyID={1}", FormID, CompanyID);
            dt = DBHelper.DBHelper.ExecuteQueryReturnDT(sb.ToString());
            if (dt.Rows.Count > 0)
            {
                if (Int16.Parse(dt.Rows[0]["ParameterID"].ToString()) == 1)
                {
                    r = true;
                }
                else
                {
                    r = false;
                }
            }
            else
            {
                r = false;
            }
            return r;
        }

        private static int GetMaxID(string tableName, string ColumnsName, int CompanyID)
        {
            int MaxID = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SELECT ISNULL(MAX({0}) + 1, 1) AS MaxID FROM {1} WHERE CompanyID={2} ", ColumnsName, tableName, CompanyID);
            MaxID = int.Parse(DBHelper.DBHelper.ExecuteQueryReturnDT(sb.ToString()).Rows[0]["MaxID"].ToString());
            return MaxID;
        }

        public static string GenerateCode(string TableName, string ColumnsName, string FormID, int CompanyID)
        {
            StringBuilder sb = new StringBuilder();
            if (CheckAutoCodeFormatEnable(FormID, CompanyID) != true)
            {
                return "0";
            }
            else
            {
                string Code = String.Empty;
                int MaxID = GetMaxID(TableName, ColumnsName, CompanyID);
                sb.AppendFormat(" SELECT (CAST(Prefix AS varchar)+CAST(Seperator AS varchar)+CAST(REPLICATE(Sufix, SufixLen-LEN({0})) AS varchar(12))+ CAST({1} AS NVARCHAR(10))) AS Code  FROM tbl_CompanyCodeformat WHERE FormID ='{2}' AND CompanyID ={3} ", MaxID.ToString().Length, MaxID, FormID, CompanyID);
                Code = DBHelper.DBHelper.ExecuteQueryReturnDT(sb.ToString()).Rows[0]["Code"].ToString();
                if (string.IsNullOrEmpty(Code))
                {
                    return "0";
                }
                else
                {
                    return Code;
                }
            }
        }

        public static string GenerateCode(string TableName, string ColumnName, bool doIncrement, int? BranchID = null)
        {
            string flag = String.Empty;
            string code = String.Empty;
            int branchid;
            if (BranchID == null || BranchID == 0)
                branchid = Global.CurrentUser.BranchId;
            else
                branchid = Convert.ToInt32(BranchID);
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@BranchID", SqlDbType.Int) { Value = branchid },
                new SqlParameter("@TableName", SqlDbType.NVarChar, 100) { Value = TableName } ,
                new SqlParameter("@ColumnName", SqlDbType.NVarChar, 100) { Value = ColumnName },
                new SqlParameter("@flag", SqlDbType.NVarChar, 100) { Value = flag }
            };

            var param = sqlParameters;
            using (SqlConnection connection = new SqlConnection(DBHelper.DBHelper.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GetCode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (var p in param)
                    {
                        command.Parameters.Add(p);
                    }

                    command.Parameters.Add("@TransactionID", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    code = command.Parameters["@TransactionID"].Value.ToString();
                }
            }
            return code;
        }
        public static string GenerateCodeWithFlag(string TableName, string ColumnName, bool doIncrement, int? BranchID = null, string Flag = null)
        {
            string flag = String.Empty;
            string code = String.Empty;
            int branchid;
            if (BranchID == null || BranchID == 0)
                branchid = Global.CurrentUser.BranchId;
            else
                branchid = Convert.ToInt32(BranchID);
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@BranchID", SqlDbType.Int) { Value = branchid },
                new SqlParameter("@TableName", SqlDbType.NVarChar, 100) { Value = TableName } ,
                new SqlParameter("@ColumnName", SqlDbType.NVarChar, 100) { Value = ColumnName },
                new SqlParameter("@flag", SqlDbType.NVarChar, 100) { Value = Flag }
            };

            var param = sqlParameters;
            using (SqlConnection connection = new SqlConnection(DBHelper.DBHelper.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GetCode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (var p in param)
                    {
                        command.Parameters.Add(p);
                    }

                    command.Parameters.Add("@TransactionID", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    code = command.Parameters["@TransactionID"].Value.ToString();
                }
            }
            return code;
        }
    }



    // Convert List to Datatable 
    // User = Utilities.ConvertListToDataTable._convertListToDataTable<SecondaryItemDetailModal>(obj.SecodaryItem);
    public static class ConvertListToDataTable
    {
        public static DataTable _convertListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            try
            {
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Defining type of data column gives proper data table 
                    var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name, type);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //put a breakpoint here and check datatable
            return dataTable;
        }
    }


}

namespace HMIS.General.Common
{
    public static class _Funcations
    {
        public static List<SqlParameter> _GetCommonParameters(string task)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@task", task),
                new SqlParameter("@BranchID", Global.CurrentUser.BranchId),
                new SqlParameter("@UserType",Global.CurrentUser.UserType ),
                new SqlParameter("@UserId", Global.CurrentUser.UserId ),
                new SqlParameter("@EntryTerminal", Global.CurrentUser.EntryTerminal),
                new SqlParameter("@EntryTerminalIP", Global.CurrentUser.EntryTerminalIP)
            };
            return sqlParameters;
        }



    }
}

namespace HMIS.Common.Session
{
    public static class _Session
    {
        public static void SetSessionObjectAsJson(this Microsoft.AspNetCore.Http.ISession session, string key, object value)
        {
            //List<WtrStudent> _sList = new List<WtrStudent>();
            //SessionHelper.SetSessionObjectAsJson(HttpContext.Session, "userObject", _sList);
            //List<WtrStudent> _sessionList = SessionHelper.GetObjectFromJson<List<WtrStudent>>(HttpContext.Session, "userObject");
            //CurrentUserLogIn u =_Session.GetSessionObjectFromJson<CurrentUserLogIn>(this.Context.Session, "logUser");
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetSessionObjectFromJson<T>(this Microsoft.AspNetCore.Http.ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}