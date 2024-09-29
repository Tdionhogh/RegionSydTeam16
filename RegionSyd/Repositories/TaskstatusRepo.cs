using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model;
using System.Data.SqlClient;

namespace RegionSyd.Repositories
{
    public class TaskstatusRepo : IRepository<Taskstatus>
    {
        private readonly string _connectionString;

        public TaskstatusRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Taskstatus> GetAll()
        {
            var taskStatuses = new List<Taskstatus>();
            string query = "SELECT * FROM dbo.Taskstatus"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        taskStatuses.Add(new Taskstatus
                        {
                            StatusID = (int)reader["StatusID"],
                            StatusName = (string)reader["StatusName"]
                        });
                    }
                }
            }

            return taskStatuses;
        }

        public Taskstatus GetById(int id)
        {
            Taskstatus taskStatus = null;
            string query = "SELECT * FROM dbo.Taskstatus WHERE StatusID = @StatusID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StatusID", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        taskStatus = new Taskstatus
                        {
                            StatusID = (int)reader["StatusID"],
                            StatusName = (string)reader["StatusName"]
                        };
                    }
                }
            }

            return taskStatus;
        }

        public void Add(Taskstatus taskStatus)
        {
            string query = "INSERT INTO dbo.Taskstatus (StatusName) VALUES (@StatusName)"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StatusName", taskStatus.StatusName);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Taskstatus taskStatus)
        {
            string query = "UPDATE dbo.Taskstatus SET StatusName = @StatusName WHERE StatusID = @StatusID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StatusName", taskStatus.StatusName);
                command.Parameters.AddWithValue("@StatusID", taskStatus.StatusID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM dbo.Taskstatus WHERE StatusID = @StatusID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StatusID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
