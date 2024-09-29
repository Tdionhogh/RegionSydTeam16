using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using RegionSyd.Model;

namespace RegionSyd.Repositories
{
    public class TaskRepo : IRepository<Model.Task>
    {
        private readonly string _connectionString;

        public TaskRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Model.Task> GetAll()
        {
            var tasks = new List<Model.Task>();
            string query = "SELECT * FROM dbo.Task";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new Model.Task
                        {
                            TaskID = (int)reader["TaskID"],
                            RegionID = (int)reader["RegionID"],
                            AmbulanceID = (int)reader["AmbulanceID"],
                            TaskTypeID = (int)reader["TaskTypeID"],
                            PatientID = (int)reader["PatientID"],
                            PickupTime = (DateTime)reader["PickupTime"],
                            DropoffTime = (DateTime)reader["DropoffTime"],
                            TaskTime = (DateTime)reader["TaskTime"],
                            FromAdressID = (int)reader["FromAddressID"],
                            ToAddressID = (int)reader["ToAddressID"],
                            StatusID = (int)reader["StatusID"],
                            LastUpdated = (DateTime)reader["LastUpdated"]
                        });
                    }
                }
            }

            return tasks;
        }

        public Model.Task GetById(int id)
        {
            Model.Task task = null;
            string query = "SELECT * FROM dbo.Task WHERE TaskID = @TaskID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TaskID", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        task = new Model.Task
                        {
                            TaskID = (int)reader["TaskID"],
                            RegionID = (int)reader["RegionID"],
                            AmbulanceID = (int)reader["AmbulanceID"],
                            TaskTypeID = (int)reader["TaskTypeID"],
                            PatientID = (int)reader["PatientID"],
                            PickupTime = (DateTime)reader["PickupTime"],
                            DropoffTime = (DateTime)reader["DropoffTime"],
                            TaskTime = (DateTime)reader["TaskTime"],
                            FromAdressID = (int)reader["FromAddressID"],
                            ToAddressID = (int)reader["ToAddressID"],
                            StatusID = (int)reader["StatusID"],
                            LastUpdated = (DateTime)reader["LastUpdated"]
                        };
                    }
                }
            }

            return task;
        }

        public void Add(Model.Task task)
        {
            string query = "INSERT INTO dbo.Task (RegionID, AmbulanceID, TaskTypeID, PatientID, PickupTime, DropoffTime, TaskTime, FromAddressID, ToAddressID, StatusID, LastUpdated) " +
                           "VALUES (@RegionID, @AmbulanceID, @TaskTypeID, @PatientID, @PickupTime, @DropoffTime, @TaskTime, @FromAddressID, @ToAddressID, @StatusID, @LastUpdated)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RegionID", task.RegionID);
                command.Parameters.AddWithValue("@AmbulanceID", task.AmbulanceID);
                command.Parameters.AddWithValue("@TaskTypeID", task.TaskTypeID);
                command.Parameters.AddWithValue("@PatientID", task.PatientID);
                command.Parameters.AddWithValue("@PickupTime", task.PickupTime);
                command.Parameters.AddWithValue("@DropoffTime", task.DropoffTime);
                command.Parameters.AddWithValue("@TaskTime", task.TaskTime);
                command.Parameters.AddWithValue("@FromAddressID", task.FromAdressID);
                command.Parameters.AddWithValue("@ToAddressID", task.ToAddressID);
                command.Parameters.AddWithValue("@StatusID", task.StatusID);
                command.Parameters.AddWithValue("@LastUpdated", task.LastUpdated);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Model.Task task)
        {
            string query = "UPDATE dbo.Task SET RegionID = @RegionID, AmbulanceID = @AmbulanceID, TaskTypeID = @TaskTypeID, PatientID = @PatientID, " +
                           "PickupTime = @PickupTime, DropoffTime = @DropoffTime, TaskTime = @TaskTime, FromAddressID = @FromAddressID, " +
                           "ToAddressID = @ToAddressID, StatusID = @StatusID, LastUpdated = @LastUpdated WHERE TaskID = @TaskID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RegionID", task.RegionID);
                command.Parameters.AddWithValue("@AmbulanceID", task.AmbulanceID);
                command.Parameters.AddWithValue("@TaskTypeID", task.TaskTypeID);
                command.Parameters.AddWithValue("@PatientID", task.PatientID);
                command.Parameters.AddWithValue("@PickupTime", task.PickupTime);
                command.Parameters.AddWithValue("@DropoffTime", task.DropoffTime);
                command.Parameters.AddWithValue("@TaskTime", task.TaskTime);
                command.Parameters.AddWithValue("@FromAddressID", task.FromAdressID);
                command.Parameters.AddWithValue("@ToAddressID", task.ToAddressID);
                command.Parameters.AddWithValue("@StatusID", task.StatusID);
                command.Parameters.AddWithValue("@LastUpdated", task.LastUpdated);
                command.Parameters.AddWithValue("@TaskID", task.TaskID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM dbo.Task WHERE TaskID = @TaskID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TaskID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
