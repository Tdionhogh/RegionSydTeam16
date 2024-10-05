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
                            AddressType = (string)reader["AddressType"],
                            ZipCodeID = (int)reader["ZipCodeID"],
                            ZipCodeNr = (string)reader["ZipCodeNr"] // Retter til ZipCodeNr
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
                            AddressType = (string)reader["AddressType"],
                            ZipCodeID = (int)reader["ZipCodeID"],
                            ZipCodeNr = (string)reader["ZipCodeNr"] // Retter til ZipCodeNr
                        };
                    }
                }
            }

            return fromAddress;
        }

        public void Add(FromAddress fromAddress)
        {
            string query = "INSERT INTO dbo.FromAddress (StreetName, City, AddressType, ZipCodeID, ZipCodeNr) VALUES (@StreetName, @City, @AddressType, @ZipCodeID, @ZipCodeNr)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StreetName", fromAddress.StreetName);
                command.Parameters.AddWithValue("@City", fromAddress.City);
                command.Parameters.AddWithValue("@ZipCodeID", fromAddress.ZipCodeID);
                command.Parameters.AddWithValue("@AddressType", fromAddress.AddressType);
                command.Parameters.AddWithValue("@ZipCodeNr", fromAddress.ZipCodeNr); // Retter til ZipCodeNr
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(FromAddress fromAddress)
        {
            string query = "UPDATE dbo.FromAddress SET StreetName = @StreetName, City = @City, ZipCodeID = @ZipCodeID, AddressType = @AddressType, ZipCodeNr = @ZipCodeNr WHERE FromAddressID = @FromAddressID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StreetName", fromAddress.StreetName);
                command.Parameters.AddWithValue("@City", fromAddress.City);
                command.Parameters.AddWithValue("@ZipCodeID", fromAddress.ZipCodeID); // Retter til ZipCodeID
                command.Parameters.AddWithValue("@AddressType", fromAddress.AddressType);
                command.Parameters.AddWithValue("@ZipCodeNr", fromAddress.ZipCodeNr); // Retter til ZipCodeNr
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
