using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using RegionSyd.Model;
using Microsoft.Extensions.Configuration;

namespace RegionSyd.Repositories
{
    public class CustomTaskRepo
    {
        private readonly string _connectionString;

        public CustomTaskRepo(IConfiguration configuration) // Use IConfiguration instead of string
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection"); // Read from appsettings.json
        }

        // Get all tasks asynchronously
        public async Task<List<CustomTask>> GetAllTasksAsync()
        {
            var tasks = new List<CustomTask>();
            string query = @"
                SELECT 
                    t.TaskID,
                    r.RegionName,
                    a.AmbulanceNumber,
                    tt.TypeOfTask,
                    p.PatientName,
                    ts.StatusName,
                    t.PickupTime,
                    t.DropoffTime,
                    DATEDIFF(MINUTE, t.PickupTime, t.DropoffTime) AS TaskDuration,
                    CONCAT(fa.StreetName, ', ', fa.City) AS FromAddress,
                    CONCAT(ta.StreetName, ', ', ta.City) AS ToAddress,
                    t.LastUpdated
                FROM 
                    dbo.CustomTask t
                JOIN 
                    dbo.Region r ON t.RegionID = r.RegionID
                JOIN 
                    dbo.Ambulance a ON t.AmbulanceID = a.AmbulanceID
                JOIN 
                    dbo.TaskType tt ON t.TaskTypeID = tt.TaskTypeID
                JOIN
                    dbo.Patient p ON t.PatientID = p.PatientID
                JOIN
                    dbo.Taskstatus ts ON t.StatusID = ts.StatusID
                JOIN 
                    dbo.FromAddress fa ON t.FromAddressID = fa.FromAddressID
                JOIN 
                    dbo.ToAddress ta ON t.ToAddressID = ta.ToAddressID;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        tasks.Add(new CustomTask
                        {
                            TaskID = (int)reader["TaskID"],
                            RegionName = reader["RegionName"] as string ?? string.Empty,
                            AmbulanceNumber = reader["AmbulanceNumber"] as string ?? string.Empty,
                            TypeOfTask = reader["TypeOfTask"] as string ?? string.Empty,
                            PatientName = reader["PatientName"] as string ?? string.Empty,
                            StatusName = reader["StatusName"] as string ?? string.Empty,
                            PickupTime = (DateTime)reader["PickupTime"],
                            DropoffTime = (DateTime)reader["DropoffTime"],
                            TaskTime = TimeSpan.FromMinutes((int)reader["TaskDuration"]),
                            FromAddress = reader["FromAddress"] as string ?? string.Empty,
                            ToAddress = reader["ToAddress"] as string ?? string.Empty,
                            LastUpdated = (DateTime)reader["LastUpdated"]
                        });
                    }
                }
            }

            return tasks;
        }

        // Add a new task asynchronously
        public async Task<CustomTask> AddTaskAsync(CustomTask newTask)
        {
            string query = @"
                INSERT INTO dbo.CustomTask 
                (RegionID, AmbulanceID, TaskTypeID, PatientID, FromAddressID, ToAddressID, StatusID, PickupTime, DropoffTime, LastUpdated) 
                VALUES 
                (@RegionID, @AmbulanceID, @TaskTypeID, @PatientID, @FromAddressID, @ToAddressID, @StatusID, @PickupTime, @DropoffTime, @LastUpdated)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@RegionID", SqlDbType.Int).Value = newTask.RegionID;
                command.Parameters.Add("@AmbulanceID", SqlDbType.Int).Value = newTask.AmbulanceID;
                command.Parameters.Add("@TaskTypeID", SqlDbType.Int).Value = newTask.TaskTypeID;
                command.Parameters.Add("@PatientID", SqlDbType.Int).Value = newTask.PatientID;
                command.Parameters.Add("@FromAddressID", SqlDbType.Int).Value = newTask.FromAddressID;
                command.Parameters.Add("@ToAddressID", SqlDbType.Int).Value = newTask.ToAddressID;
                command.Parameters.Add("@StatusID", SqlDbType.Int).Value = newTask.StatusID;
                command.Parameters.Add("@PickupTime", SqlDbType.DateTime).Value = newTask.PickupTime;
                command.Parameters.Add("@DropoffTime", SqlDbType.DateTime).Value = newTask.DropoffTime;
                command.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = DateTime.Now;

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            return newTask;
        }

        // Delete a task asynchronously
        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            string query = "DELETE FROM dbo.CustomTask WHERE TaskID = @TaskID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@TaskID", SqlDbType.Int).Value = taskId;

                await connection.OpenAsync();
                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0; // Return true if the delete was successful
            }
        }

        // Get all ambulances asynchronously
        internal async Task<IEnumerable<Ambulance>> GetAmbulancesAsync()
        {
            var ambulances = new List<Ambulance>();
            string query = "SELECT AmbulanceID, AmbulanceNumber FROM dbo.Ambulance";

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
                            AmbulanceNumber = reader["AmbulanceNumber"] as string ?? string.Empty
                        });
                    }
                }
            }

            return ambulances;
        }

        // Get all regions asynchronously
        internal async Task<IEnumerable<Region>> GetRegionsAsync()
        {
            var regions = new List<Region>();
            string query = "SELECT RegionID, RegionName FROM dbo.Region";

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
                            RegionName = reader["RegionName"] as string ?? string.Empty
                        });
                    }
                }
            }

            return regions;
        }

        // Get all task types asynchronously
        internal async Task<IEnumerable<TaskType>> GetTaskTypesAsync()
        {
            var taskTypes = new List<TaskType>();
            string query = "SELECT TaskTypeID, TypeOfTask FROM dbo.TaskType";

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
                            TypeOfTask = reader["TypeOfTask"] as string ?? string.Empty
                        });
                    }
                }
            }

            return taskTypes;
        }

        // Get all patients asynchronously
        internal async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            var patients = new List<Patient>();
            string query = "SELECT PatientID, PatientName FROM dbo.Patient"; // Adjust based on your Patient table

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
                            PatientName = reader["PatientName"] as string ?? string.Empty // Adjust based on your model
                        });
                    }
                }
            }

            return patients;
        }

        // Get all from addresses asynchronously
        internal async Task<IEnumerable<FromAddress>> GetFromAddressesAsync()
        {
            var fromAddresses = new List<FromAddress>();
            string query = "SELECT FromAddressID, StreetName, City FROM dbo.FromAddress"; // Adjust as needed

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
                            StreetName = reader["StreetName"] as string ?? string.Empty,
                            City = reader["City"] as string ?? string.Empty
                        });
                    }
                }
            }

            return fromAddresses;
        }

        // Get all to addresses asynchronously
        internal async Task<IEnumerable<ToAddress>> GetToAddressesAsync()
        {
            var toAddresses = new List<ToAddress>();
            string query = "SELECT ToAddressID, StreetName, City FROM dbo.ToAddress"; // Adjust as needed

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
                            StreetName = reader["StreetName"] as string ?? string.Empty,
                            City = reader["City"] as string ?? string.Empty
                        });
                    }
                }
            }

            return toAddresses;
        }

        // Get all task statuses asynchronously
        internal async Task<IEnumerable<Taskstatus>> GetTaskStatusesAsync()
        {
            var taskStatuses = new List<Taskstatus>();
            string query = "SELECT StatusID, StatusName FROM dbo.TaskStatus"; // Adjust as needed

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
                            StatusName = reader["StatusName"] as string ?? string.Empty
                        });
                    }
                }
            }

            return taskStatuses;
        }
    }
}
