using System.Data;
using Microsoft.Data.SqlClient;

namespace Unohana.Services
{
    public class GetDBService
    {
        public DataTable SelectQuery(string tableName, string query, IConfiguration configuration)
        {
            DataTable dataTable = new();
            DataSet dataSet = new();

            string? connectionString = configuration.GetConnectionString("IsaneDataString");

            try
            {
                using (SqlConnection sqlConnection = new(connectionString))
                {
                    using (SqlDataAdapter sqlDataAdapter = new(query, connectionString))
                    {
                        sqlDataAdapter.Fill(dataSet);
                    }
                }

                dataTable = dataSet.Tables[tableName];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dataTable ?? throw new Exception("NullReferenceException");
        }
    }
}