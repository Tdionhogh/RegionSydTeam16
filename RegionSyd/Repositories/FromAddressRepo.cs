using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model;
using System.Data.SqlClient;

namespace RegionSyd.Repositories
{
    public class FromAddressRepo : IRepository<FromAddress>
    {
        private readonly string _connectionString;

        public FromAddressRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<FromAddress> GetAll()
        {
            var fromAddresses = new List<FromAddress>();
            string query = "SELECT * FROM dbo.FromAddress"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fromAddresses.Add(new FromAddress
                        {
                            FromAddressID = (int)reader["FromAddressID"],
                            StreetName = (string)reader["StreetName"],
                            City = (string)reader["City"],
                            ZipCode = (int)reader["ZipCode"],
                            AddressType = (string)reader["AddressType"]
                        });
                    }
                }
            }

            return fromAddresses;
        }

        public FromAddress GetById(int id)
        {
            FromAddress fromAddress = null;
            string query = "SELECT * FROM dbo.FromAddress WHERE FromAddressID = @FromAddressID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FromAddressID", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        fromAddress = new FromAddress
                        {
                            FromAddressID = (int)reader["FromAddressID"],
                            StreetName = (string)reader["StreetName"],
                            City = (string)reader["City"],
                            ZipCode = (int)reader["ZipCode"],
                            AddressType = (string)reader["AddressType"]
                        };
                    }
                }
            }

            return fromAddress;
        }

        public void Add(FromAddress fromAddress)
        {
            string query = "INSERT INTO dbo.FromAddress (StreetName, City, ZipCode, AddressType) VALUES (@StreetName, @City, @ZipCode, @AddressType)"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StreetName", fromAddress.StreetName);
                command.Parameters.AddWithValue("@City", fromAddress.City);
                command.Parameters.AddWithValue("@ZipCode", fromAddress.ZipCode);
                command.Parameters.AddWithValue("@AddressType", fromAddress.AddressType);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(FromAddress fromAddress)
        {
            string query = "UPDATE dbo.FromAddress SET StreetName = @StreetName, City = @City, ZipCode = @ZipCode, AddressType = @AddressType WHERE FromAddressID = @FromAddressID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StreetName", fromAddress.StreetName);
                command.Parameters.AddWithValue("@City", fromAddress.City);
                command.Parameters.AddWithValue("@ZipCode", fromAddress.ZipCode);
                command.Parameters.AddWithValue("@AddressType", fromAddress.AddressType);
                command.Parameters.AddWithValue("@FromAddressID", fromAddress.FromAddressID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM dbo.FromAddress WHERE FromAddressID = @FromAddressID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FromAddressID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
