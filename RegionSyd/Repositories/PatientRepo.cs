using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model;
using System.Data.SqlClient;

namespace RegionSyd.Repositories
{
    public class PatientRepo : IRepository<Patient>
    {
        private readonly string _connectionString;

        public PatientRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Patient> GetAll()
        {
            var patients = new List<Patient>();
            string query = "SELECT * FROM dbo.Patient"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            PatientID = (int)reader["PatientID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            CprNumber = (string)reader["CprNumber"],
                            TlfNumber = (string)reader["TlfNumber"]
                        });
                    }
                }
            }

            return patients;
        }

        public Patient GetById(int id)
        {
            Patient patient = null;
            string query = "SELECT * FROM dbo.Patient WHERE PatientID = @PatientID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PatientID", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        patient = new Patient
                        {
                            PatientID = (int)reader["PatientID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            CprNumber = (string)reader["CprNumber"],
                            TlfNumber = (string)reader["TlfNumber"]
                        };
                    }
                }
            }

            return patient;
        }

        public void Add(Patient patient)
        {
            string query = "INSERT INTO dbo.Patient (FirstName, LastName, CprNumber, TlfNumber) VALUES (@FirstName, @LastName, @CprNumber, @TlfNumber)"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", patient.FirstName);
                command.Parameters.AddWithValue("@LastName", patient.LastName);
                command.Parameters.AddWithValue("@CprNumber", patient.CprNumber);
                command.Parameters.AddWithValue("@TlfNumber", patient.TlfNumber);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Patient patient)
        {
            string query = "UPDATE dbo.Patient SET FirstName = @FirstName, LastName = @LastName, CprNumber = @CprNumber, TlfNumber = @TlfNumber WHERE PatientID = @PatientID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", patient.FirstName);
                command.Parameters.AddWithValue("@LastName", patient.LastName);
                command.Parameters.AddWithValue("@CprNumber", patient.CprNumber);
                command.Parameters.AddWithValue("@TlfNumber", patient.TlfNumber);
                command.Parameters.AddWithValue("@PatientID", patient.PatientID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM dbo.Patient WHERE PatientID = @PatientID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PatientID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
