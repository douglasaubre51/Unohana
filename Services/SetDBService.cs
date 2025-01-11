using Microsoft.Data.SqlClient;

namespace Unohana.Services
{
    public class SetDBService
    {
        public void InsertQuery(string query, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("IsaneDataString");

            try
            {
                using (SqlConnection sqlConnection = new(connectionString))
                {
                    using (SqlCommand sqlcommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlcommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}