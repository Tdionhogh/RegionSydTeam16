using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model;
using System.Data.SqlClient;

namespace RegionSyd.Repositories
{
    public class TaskTypeRepo : IRepository<TaskType>
    {
        private readonly string _connectionString;

        public TaskTypeRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<TaskType> GetAll()
        {
            var taskTypes = new List<TaskType>();
            string query = "SELECT * FROM dbo.TaskType"; 

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        taskTypes.Add(new TaskType
                        {
                            TaskTypeID = (int)reader["TaskTypeID"],
                            TypeOfTask = (string)reader["TypeOfTask"]
                        });
                    }
                }
            }

            return taskTypes;
        }

        public TaskType GetById(int id)
        {
            TaskType taskType = null;
            string query = "SELECT * FROM dbo.TaskType WHERE TaskTypeID = @TaskTypeID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TaskTypeID", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        taskType = new TaskType
                        {
                            TaskTypeID = (int)reader["TaskTypeID"],
                            TypeOfTask = (string)reader["TypeOfTask"]
                        };
                    }
                }
            }

            return taskType;
        }

        public void Add(TaskType taskType)
        {
            string query = "INSERT INTO dbo.TaskType (TypeOfTask) VALUES (@TypeOfTask)"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TypeOfTask", taskType.TypeOfTask);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(TaskType taskType)
        {
            string query = "UPDATE dbo.TaskType SET TypeOfTask = @TypeOfTask WHERE TaskTypeID = @TaskTypeID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TypeOfTask", taskType.TypeOfTask);
                command.Parameters.AddWithValue("@TaskTypeID", taskType.TaskTypeID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM dbo.TaskType WHERE TaskTypeID = @TaskTypeID"; // Brug dbo her

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TaskTypeID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
