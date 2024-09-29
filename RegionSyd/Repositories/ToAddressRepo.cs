﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model;
using System.Data.SqlClient;

namespace RegionSyd.Repositories
{
    public class ToAddressRepo : IRepository<ToAddress>
    {
        private readonly string _connectionString;

        public ToAddressRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ToAddress> GetAll()
        {
            var toAddresses = new List<ToAddress>();
            string query = "SELECT * FROM dbo.ToAddress"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        toAddresses.Add(new ToAddress
                        {
                            ToAddressID = (int)reader["ToAddressID"],
                            StreetName = (string)reader["StreetName"],
                            City = (string)reader["City"],
                            ZipCode = (int)reader["ZipCode"]
                        });
                    }
                }
            }

            return toAddresses;
        }

        public ToAddress GetById(int id)
        {
            ToAddress toAddress = null;
            string query = "SELECT * FROM dbo.ToAddress WHERE ToAddressID = @ToAddressID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ToAddressID", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        toAddress = new ToAddress
                        {
                            ToAddressID = (int)reader["ToAddressID"],
                            StreetName = (string)reader["StreetName"],
                            City = (string)reader["City"],
                            ZipCode = (int)reader["ZipCode"]
                        };
                    }
                }
            }

            return toAddress;
        }

        public void Add(ToAddress toAddress)
        {
            string query = "INSERT INTO dbo.ToAddress (StreetName, City, ZipCode) VALUES (@StreetName, @City, @ZipCode)"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StreetName", toAddress.StreetName);
                command.Parameters.AddWithValue("@City", toAddress.City);
                command.Parameters.AddWithValue("@ZipCode", toAddress.ZipCode);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(ToAddress toAddress)
        {
            string query = "UPDATE dbo.ToAddress SET StreetName = @StreetName, City = @City, ZipCode = @ZipCode WHERE ToAddressID = @ToAddressID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StreetName", toAddress.StreetName);
                command.Parameters.AddWithValue("@City", toAddress.City);
                command.Parameters.AddWithValue("@ZipCode", toAddress.ZipCode);
                command.Parameters.AddWithValue("@ToAddressID", toAddress.ToAddressID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM dbo.ToAddress WHERE ToAddressID = @ToAddressID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ToAddressID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
