using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model;
using System.Data.SqlClient;

namespace RegionSyd.Repositories
{
    public class RegionRepo : IRepository<Region>
    {
        private readonly string _connectionString;

        public RegionRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Region> GetAll()
        {
            var regions = new List<Region>();
            string query = "SELECT * FROM dbo.Region"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        regions.Add(new Region
                        {
                            RegionID = (int)reader["RegionID"],
                            RegionName = (string)reader["RegionName"]
                        });
                    }
                }
            }

            return regions;
        }

        public Region GetById(int id)
        {
            Region region = null;
            string query = "SELECT * FROM dbo.Region WHERE RegionID = @RegionID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RegionID", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        region = new Region
                        {
                            RegionID = (int)reader["RegionID"],
                            RegionName = (string)reader["RegionName"] 
                        };
                    }
                }
            }

            return region;
        }

        public void Add(Region region)
        {
            string query = "INSERT INTO dbo.Region (RegionName) VALUES (@RegionName)"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RegionName", region.RegionName); 
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Region region)
        {
            string query = "UPDATE dbo.Region SET RegionName = @RegionName WHERE RegionID = @RegionID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RegionName", region.RegionName); 
                command.Parameters.AddWithValue("@RegionID", region.RegionID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM dbo.Region WHERE RegionID = @RegionID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RegionID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
