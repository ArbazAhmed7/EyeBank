using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Sockets;

namespace TransportManagementCore.DBHelper
{
    public class DBHelper : IDisposable
    {
        void IDisposable.Dispose()
        {

        }
        public DBHelper()
        {

        }
        public static string ConnectionString { get; set; }

        public static DataTable ExecuteQueryReturnDT(string sqlQuery)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 3600;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter ada = new SqlDataAdapter(sqlQuery, conn);
                ada.Fill(dt);
                cmd.Parameters.Clear();
                return dt;
            }
        }

        public static string ENTTerminalName()
        {
            try
            {
                // Get the local computer host name.
                return Dns.GetHostName() == "" ? "::0" : Dns.GetHostName();

            }
            catch (SocketException e)
            {
                return $"SocketException caught!!! Source {e.Source} and message {e.Message}";
            }
            catch (Exception e)
            {
                return $"Exception caught!!! Source {e.Source} and message {e.Message}";
            }
        }

        public void CloseConnection(SqlConnection connection)
        {
            connection.Close();
        }

        public SqlParameter CreateParameter(string name, object value, DbType dbType)
        {
            return CreateParameter(name, 0, value, dbType, ParameterDirection.Input);
        }

        public SqlParameter CreateParameter(string name, int size, object value, DbType dbType)
        {
            return CreateParameter(name, size, value, dbType, ParameterDirection.Input);
        }

        public SqlParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection direction)
        {
            return new SqlParameter
            {
                DbType = dbType,
                ParameterName = name,
                Size = size,
                Direction = direction,
                Value = value
            };
        }


        //anc
        public DataTable GetDataTable(string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    string query = " Exec " + commandText;
                    command.CommandType = commandType;
                    command.CommandTimeout = 360000;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            query = query + " " + parameter.ParameterName + " = '" + parameter.Value + "', ";
                            command.Parameters.Add(parameter);
                        }
                    }

                    var dataset = new DataSet();
                    var dataAdaper = new SqlDataAdapter(command);
                    dataAdaper.Fill(dataset);

                    return dataset.Tables[0];
                }
            }
        }

        public DataTable GetSingleDataTable(string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    command.CommandTimeout = 360000;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    var dataTable = new DataTable();
                    var dataAdaper = new SqlDataAdapter(command);
                    dataAdaper.Fill(dataTable);

                    return dataTable;
                }
            }
        }

        public DataTable GetDataTable(string commandText, CommandType commandType, List<SqlParameter> parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    DataTable dataset = new DataTable();
                    SqlDataAdapter dataAdaper = new SqlDataAdapter(command);
                    dataAdaper.Fill(dataset);

                    return dataset;
                }
            }
        }


        public DataTable GetDataTable(string commandText, CommandType commandType, SqlParameter[] parameters, out string Result)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = commandType;
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }


                        var dataset = new DataSet();
                        var dataAdaper = new SqlDataAdapter(command);
                        dataAdaper.Fill(dataset);
                        if (dataset.Tables[0].Rows.Count > 0)
                        {
                            Result = dataset.Tables[0].Rows[0][0].ToString();
                        }
                        else
                        {
                            Result = "";
                        }
                        return dataset.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Result = "Error" + ex.Message;
                return null;
            }

        }

        public DataTable GetDataTable(string commandText, CommandType commandType, List<SqlParameter> parameters, out string Result)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = commandType;
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }

                        var dataset = new DataSet();
                        var dataAdaper = new SqlDataAdapter(command);
                        dataAdaper.Fill(dataset);
                        if (dataset.Tables[0].Rows.Count > 0)
                        {
                            Result = dataset.Tables[0].Rows[0][0].ToString();
                        }
                        else
                        {
                            Result = "";
                        }
                        return dataset.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Result = "Error" + ex.Message;
                return null;
            }

        }

        public DataSet GetDataSet(string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    string query = " Exec " + commandText;
                    command.CommandType = commandType;
                    command.CommandTimeout = 360000;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            query = query + " " + parameter.ParameterName + " = '" + parameter.Value + "', ";
                            command.Parameters.Add(parameter);
                        }
                    }

                    var dataset = new DataSet();
                    var dataAdaper = new SqlDataAdapter(command);
                    dataAdaper.Fill(dataset);

                    return dataset;
                }
            }
        }

        public DataSet GetDataSet(string commandText, CommandType commandType, SqlParameter[] parameters, out string Result)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = commandType;
                        command.CommandTimeout = 360000;
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }

                        var dataset = new DataSet();
                        var dataAdaper = new SqlDataAdapter(command);
                        dataAdaper.Fill(dataset);
                        Result = dataset.Tables[0].Rows[0][0].ToString();
                        return dataset;
                    }
                }
            }
            catch (Exception ex)
            {
                Result = "Error" + ex.Message;
                return null;
            }

        }

        public void ExecuteNonQuery(string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }
        }


        public DataTable BulkInsert(string commandText, CommandType commandType, string ParameterName, DataTable dataTable)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    command.Parameters.AddWithValue(ParameterName, dataTable);
                    var dataset = new DataSet();
                    var dataAdaper = new SqlDataAdapter(command);
                    dataAdaper.Fill(dataset);
                    return dataset.Tables[0];
                }
            }

        }

        public IDataReader GetDataReader(string commandText, CommandType commandType, SqlParameter[] parameters, out SqlConnection connection)
        {
            IDataReader reader = null;
            connection = new SqlConnection(ConnectionString);
            connection.Open();

            var command = new SqlCommand(commandText, connection);
            command.CommandType = commandType;
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            reader = command.ExecuteReader();

            return reader;
        }

        public IDataReader GetDataReader(string commandText)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            var command = new SqlCommand(commandText, connection);
            return command.ExecuteReader();
        }

        public IDataReader GetDataReader(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            IDataReader reader = null;
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            var command = new SqlCommand(commandText, connection);
            command.CommandType = commandType;
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            reader = command.ExecuteReader();

            return reader;
        }

        public void Delete(string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Insert(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public int Insert(string commandText, CommandType commandType, SqlParameter[] parameters, out int lastId)
        {
            lastId = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    object newId = command.ExecuteScalar();
                    lastId = Convert.ToInt32(newId);
                }
            }

            return lastId;
        }

        public long Insert(string commandText, CommandType commandType, SqlParameter[] parameters, out long lastId)
        {
            lastId = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    object newId = command.ExecuteScalar();
                    lastId = Convert.ToInt64(newId);
                }
            }

            return lastId;
        }

        public void InsertWithTransaction(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            SqlTransaction transactionScope = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                transactionScope = connection.BeginTransaction();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    try
                    {
                        command.ExecuteNonQuery();
                        transactionScope.Commit();
                    }
                    catch (Exception)
                    {
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public void InsertWithTransaction(string commandText, CommandType commandType, IsolationLevel isolationLevel, SqlParameter[] parameters)
        {
            SqlTransaction transactionScope = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                transactionScope = connection.BeginTransaction(isolationLevel);

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    try
                    {
                        command.ExecuteNonQuery();
                        transactionScope.Commit();
                    }
                    catch (Exception)
                    {
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public void Update(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateWithTransaction(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            SqlTransaction transactionScope = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                transactionScope = connection.BeginTransaction();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    try
                    {
                        command.ExecuteNonQuery();
                        transactionScope.Commit();
                    }
                    catch (Exception)
                    {
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public void UpdateWithTransaction(string commandText, CommandType commandType, IsolationLevel isolationLevel, SqlParameter[] parameters)
        {
            SqlTransaction transactionScope = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                transactionScope = connection.BeginTransaction(isolationLevel);

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    try
                    {
                        command.ExecuteNonQuery();
                        transactionScope.Commit();
                    }
                    catch (Exception)
                    {
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public object GetScalarValue(string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    return command.ExecuteScalar();
                }
            }
        }

        public static IDataReader GetReader(string commandText)
        {
            return GetReader(commandText, CommandType.Text, null);
        }

        public static IDataReader GetReader(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(commandText, connection);
            command.CommandType = commandType;
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            return command.ExecuteReader();
        }

        public static DataTable GetDataTableStatic(string commandText, CommandType commandType, List<SqlParameter> parameters = null)
        {

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    var dataset = new DataSet();
                    var dataAdaper = new SqlDataAdapter(command);
                    dataAdaper.Fill(dataset);

                    return dataset.Tables[0];
                }
            }
        }

        public static DataTable GetDataTableStatic(string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    var dataset = new DataSet();
                    var dataAdaper = new SqlDataAdapter(command);
                    dataAdaper.Fill(dataset);

                    return dataset.Tables[0];
                }
            }
        }

        public static DataTable GetDataTableStatic(string commandText, CommandType commandType, List<SqlParameter> parameters, out string Result)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = commandType;
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }

                        var dataset = new DataSet();
                        var dataAdaper = new SqlDataAdapter(command);
                        dataAdaper.Fill(dataset);
                        Result = dataset.Tables[0].Rows[0][0].ToString();
                        return dataset.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Result = "Error" + ex.Message;
                return null;
            }
        }

        public static DataTable GetDataTableStatic(string commandText, CommandType commandType, SqlParameter[] parameters, out string Result)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = commandType;
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }

                        var dataset = new DataSet();
                        var dataAdaper = new SqlDataAdapter(command);
                        dataAdaper.Fill(dataset);
                        Result = dataset.Tables[0].Rows[0][0].ToString();
                        return dataset.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Result = "Error" + ex.Message;
                return null;
            }

        }

        public static string GetDataTableStatic(string commandText, CommandType commandType, string outputParameterName, List<SqlParameter> parameters = null)
        {
            outputParameterName = "@" + outputParameterName;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.Parameters.Add(outputParameterName, SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    return command.Parameters[outputParameterName].Value.ToString();
                }
            }
        }




    }
}
