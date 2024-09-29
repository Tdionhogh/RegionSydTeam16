using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model;
using System.Data.SqlClient;

namespace RegionSyd.Repositories
{
    public class ZipCodeRepo : IRepository<ZipCode>
    {
        private readonly string _connectionString;

        public ZipCodeRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ZipCode> GetAll()
        {
            var zipCodes = new List<ZipCode>();
            string query = "SELECT * FROM dbo.ZipCode";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        zipCodes.Add(new ZipCode
                        {
                            ZipCodeID = (int)reader["ZipCodeID"],
                            RegionID = (int)reader["RegionID"]
                        });
                    }
                }
            }

            return zipCodes;
        }

        public ZipCode GetById(int id)
        {
            ZipCode zipCode = null;
            string query = "SELECT * FROM dbo.ZipCode WHERE ZipCodeID = @ZipCodeID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ZipCodeID", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        zipCode = new ZipCode
                        {
                            ZipCodeID = (int)reader["ZipCodeID"],
                            RegionID = (int)reader["RegionID"]
                        };
                    }
                }
            }

            return zipCode;
        }

        public void Add(ZipCode zipCode)
        {
            string query = "INSERT INTO dbo.ZipCode (RegionID) VALUES (@RegionID)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RegionID", zipCode.RegionID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(ZipCode zipCode)
        {
            string query = "UPDATE dbo.ZipCode SET RegionID = @RegionID WHERE ZipCodeID = @ZipCodeID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RegionID", zipCode.RegionID);
                command.Parameters.AddWithValue("@ZipCodeID", zipCode.ZipCodeID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM dbo.ZipCode WHERE ZipCodeID = @ZipCodeID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ZipCodeID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
