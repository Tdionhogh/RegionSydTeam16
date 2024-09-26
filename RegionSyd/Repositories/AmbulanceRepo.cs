using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using RegionSyd.Model;

namespace RegionSyd.Repositories
{
    public class AmbulanceRepository : IRepository<Ambulance>
    {
        private readonly string _connectionString;

        public AmbulanceRepository()
        {
            _connectionString = App.ConnectionString; // Hent forbindelsesstrengen fra App
        }

        public IEnumerable<Ambulance> GetAll()
        {
            var ambulances = new List<Ambulance>();
            string query = "SELECT * FROM Ambulance"; // Sørg for at dette er navnet på din tabel

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ambulances.Add(new Ambulance
                        {
                            AmbulanceID = (int)reader["AmbulanceID"],
                            AmbulanceNumber = (string)reader["AmbulanceNumber"],
                            StatusID = (int)reader["StatusID"],
                            Capacity = (int)reader["Capacity"],
                            RegionID = (int)reader["RegionID"],
                            LastUpdated = (DateTime)reader["LastUpdated"]
                        });
                    }
                }
            }

            return ambulances;
        }

        public Ambulance GetById(int id)
        {
            Ambulance ambulance = null;
            string query = "SELECT * FROM Ambulance WHERE AmbulanceID = @AmbulanceID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AmbulanceID", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ambulance = new Ambulance
                        {
                            AmbulanceID = (int)reader["AmbulanceID"],
                            AmbulanceNumber = (string)reader["AmbulanceNumber"],
                            StatusID = (int)reader["StatusID"],
                            Capacity = (int)reader["Capacity"],
                            RegionID = (int)reader["RegionID"],
                            LastUpdated = (DateTime)reader["LastUpdated"]
                        };
                    }
                }
            }

            return ambulance;
        }

        public void Add(Ambulance ambulance)
        {
            string query = "INSERT INTO Ambulance (AmbulanceNumber, StatusID, Capacity, RegionID, LastUpdated) VALUES (@AmbulanceNumber, @StatusID, @Capacity, @RegionID, @LastUpdated)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AmbulanceNumber", ambulance.AmbulanceNumber);
                command.Parameters.AddWithValue("@StatusID", ambulance.StatusID);
                command.Parameters.AddWithValue("@Capacity", ambulance.Capacity);
                command.Parameters.AddWithValue("@RegionID", ambulance.RegionID);
                command.Parameters.AddWithValue("@LastUpdated", ambulance.LastUpdated);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Ambulance ambulance)
        {
            string query = "UPDATE Ambulance SET AmbulanceNumber = @AmbulanceNumber, StatusID = @StatusID, Capacity = @Capacity, RegionID = @RegionID, LastUpdated = @LastUpdated WHERE AmbulanceID = @AmbulanceID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AmbulanceNumber", ambulance.AmbulanceNumber);
                command.Parameters.AddWithValue("@StatusID", ambulance.StatusID);
                command.Parameters.AddWithValue("@Capacity", ambulance.Capacity);
                command.Parameters.AddWithValue("@RegionID", ambulance.RegionID);
                command.Parameters.AddWithValue("@LastUpdated", ambulance.LastUpdated);
                command.Parameters.AddWithValue("@AmbulanceID", ambulance.AmbulanceID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Ambulance WHERE AmbulanceID = @AmbulanceID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AmbulanceID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
