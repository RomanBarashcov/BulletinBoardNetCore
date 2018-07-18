using AppleUsed.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AppleUsed.BLL.Services
{
    public class ProcedureService
    {
        private SqlConnection sqlConnection;
        private SqlCommand cmd;

        public ProcedureService()
        {
            DatabaseConfiguration databaseConfiguration = new DatabaseConfiguration();
            sqlConnection = new SqlConnection(databaseConfiguration.GetDataConnectionString());
            cmd = new SqlCommand();
        }

        public SqlDataReader GetResultFromStoredProcedure(string storedProcedureName)
        {
            SqlDataReader reader;

            cmd.CommandText = storedProcedureName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection;

            sqlConnection.Open();

            reader = cmd.ExecuteReader();
            // Data is accessible through the DataReader object here.

            

            return reader;

            
        }
    }
}
