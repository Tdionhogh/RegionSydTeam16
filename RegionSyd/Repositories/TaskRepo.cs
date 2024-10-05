using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using RegionSyd.Model;

namespace RegionSyd.Repositories
{
    public class TaskRepo
    {
        private readonly string _connectionString;

        public TaskRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Hent alle opgaver
        public async Task<List<Model.Task>> GetAllTasksAsync()
        {
            var tasks = new List<Model.Task>();
            string query = "SELECT * FROM dbo.Task"; // Opdater dette med dit faktiske tabelnavn

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
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

        // Hent alle regioner
        public async Task<List<Region>> GetRegionsAsync()
        {
            var regions = new List<Region>();
            string query = "SELECT * FROM dbo.Region"; // Opdater dette med dit faktiske tabelnavn

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        regions.Add(new Region
                        {
                            RegionID = (int)reader["RegionID"],
                            RegionName = (string)reader["RegionName"] // Antag at der er et felt "Name"
                        });
                    }
                }
            }

            return regions;
        }

        // Hent alle ambulancer
        public async Task<List<Ambulance>> GetAmbulancesAsync()
        {
            var ambulances = new List<Ambulance>();
            string query = "SELECT * FROM dbo.Ambulance"; // Opdater dette med dit faktiske tabelnavn

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ambulances.Add(new Ambulance
                        {
                            AmbulanceID = (int)reader["AmbulanceID"],
                            AmbulanceNumber = (string)reader["AmbulanceNumber"] // Antag at der er et felt "Name"
                        });
                    }
                }
            }

            return ambulances;
        }

        // Hent alle tasktyper
        public async Task<List<TaskType>> GetTaskTypesAsync()
        {
            var taskTypes = new List<TaskType>();
            string query = "SELECT * FROM dbo.TaskType"; // Opdater dette med dit faktiske tabelnavn

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        taskTypes.Add(new TaskType
                        {
                            TaskTypeID = (int)reader["TaskTypeID"],
                            TypeOfTask = (string)reader["Name"] // Antag at der er et felt "Name"
                        });
                    }
                }
            }

            return taskTypes;
        }

        // Hent alle patienter
        public async Task<List<Patient>> GetPatientsAsync()
        {
            var patients = new List<Patient>();
            string query = "SELECT PatientID, FirstName, LastName FROM dbo.Patient"; // Antag at der er kolonner for FirstName og LastName

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        patients.Add(new Patient
                        {
                            PatientID = (int)reader["PatientID"],
                            FirstName = (string)reader["Name"], 
                            LastName = (string)reader["LastName"]
                        });
                    }
                }
            }

            return patients;
        }

        // Hent adresser fra og til
        public async Task<List<FromAddress>> GetFromAddressesAsync()
        {
            var fromAddresses = new List<FromAddress>();
            string query = "SELECT * FROM dbo.FromAddress"; // Opdater dette med dit faktiske tabelnavn

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        fromAddresses.Add(new FromAddress
                        {
                            FromAddressID = (int)reader["FromAddressID"],
                            StreetName = (string)reader["StreetName"] 
                        });
                    }
                }
            }

            return fromAddresses;
        }

        public async Task<List<ToAddress>> GetToAddressesAsync()
        {
            var toAddresses = new List<ToAddress>();
            string query = "SELECT * FROM dbo.ToAddress"; // Opdater dette med dit faktiske tabelnavn

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        toAddresses.Add(new ToAddress
                        {
                            ToAddressID = (int)reader["ToAddressID"],
                            StreetName = (string)reader["StreetName"] // Antag at der er et felt "Address"
                        });
                    }
                }
            }

            return toAddresses;
        }

        public async Task<List<Taskstatus>> GetTaskStatusesAsync()
        {
            var taskStatuses = new List<Taskstatus>();
            string query = "SELECT * FROM dbo.Taskstatus"; // Opdater dette med dit faktiske tabelnavn

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        taskStatuses.Add(new Taskstatus
                        {
                            StatusID = (int)reader["StatusID"],
                            StatusName = (string)reader["StatusName"] // Antag at der er et felt "StatusName"
                        });
                    }
                }
            }

            return taskStatuses;
        }

        // Tilføj ny opgave
        public async Task<Model.Task> AddTaskAsync(Model.Task newTask)
        {
            string query = "INSERT INTO dbo.Task (RegionID, AmbulanceID, TaskTypeID, PatientID, FromAddressID, ToAddressID, StatusID, PickupTime, DropoffTime, TaskTime, LastUpdated) VALUES (@RegionID, @AmbulanceID, @TaskTypeID, @PatientID, @FromAddressID, @ToAddressID, @StatusID, @PickupTime, @DropoffTime, @TaskTime, @LastUpdated)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RegionID", newTask.RegionID);
                command.Parameters.AddWithValue("@AmbulanceID", newTask.AmbulanceID);
                command.Parameters.AddWithValue("@TaskTypeID", newTask.TaskTypeID);
                command.Parameters.AddWithValue("@PatientID", newTask.PatientID);
                command.Parameters.AddWithValue("@FromAddressID", newTask.FromAdressID);
                command.Parameters.AddWithValue("@ToAddressID", newTask.ToAddressID);
                command.Parameters.AddWithValue("@StatusID", newTask.StatusID);
                command.Parameters.AddWithValue("@PickupTime", newTask.PickupTime);
                command.Parameters.AddWithValue("@DropoffTime", newTask.DropoffTime);
                command.Parameters.AddWithValue("@TaskTime", newTask.TaskTime);
                command.Parameters.AddWithValue("@LastUpdated", newTask.LastUpdated);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            return newTask;
        }

        public async Task<Model.Task> DeleteTaskAsync(int taskId)
        {
            string query = "DELETE FROM dbo.Task WHERE TaskID = @TaskID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TaskID", taskId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            return null;
        }
    }
}
