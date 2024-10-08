using System;
using System.Windows;
using System.Windows.Controls;
using RegionSyd.Model;
using RegionSyd.Repositories;
using Microsoft.Extensions.Configuration;

namespace RegionSyd.View
{
    public partial class MainWindow : Window
    {
        private readonly CustomTaskRepo _customTaskRepo;

        public MainWindow()
        {
            InitializeComponent();

            // Build configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) // Set the base path
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // Load configuration
                .Build();

            // Pass the IConfiguration to CustomTaskRepo
            _customTaskRepo = new CustomTaskRepo(configuration); // Now using IConfiguration

            LoadComboBoxes();
            LoadTasks();
        }

        private async void LoadComboBoxes()
        {
            // Load regions
            var regions = await _customTaskRepo.GetRegionsAsync(); // Assuming you implement this method
            RegionComboBox.ItemsSource = regions; // Ensure Region has a property for display
            RegionComboBox.DisplayMemberPath = "RegionName"; // Use appropriate property for display
            RegionComboBox.SelectedValuePath = "RegionID"; // Use appropriate property for value

            // Load ambulances
            var ambulances = await _customTaskRepo.GetAmbulancesAsync(); // Assuming you implement this method
            AmbulanceComboBox.ItemsSource = ambulances;
            AmbulanceComboBox.DisplayMemberPath = "AmbulanceNumber";
            AmbulanceComboBox.SelectedValuePath = "AmbulanceID";

            // Load task types
            var taskTypes = await _customTaskRepo.GetTaskTypesAsync(); // Assuming you implement this method
            TaskTypeComboBox.ItemsSource = taskTypes;
            TaskTypeComboBox.DisplayMemberPath = "TypeOfTask";
            TaskTypeComboBox.SelectedValuePath = "TaskTypeID";

            // Load patients
            var patients = await _customTaskRepo.GetPatientsAsync(); // Assuming you implement this method
            PatientComboBox.ItemsSource = patients;
            PatientComboBox.DisplayMemberPath = "PatientName"; // Use appropriate property
            PatientComboBox.SelectedValuePath = "PatientID";

            // Load from addresses
            var fromAddresses = await _customTaskRepo.GetFromAddressesAsync(); // Assuming you implement this method
            FromAddressComboBox.ItemsSource = fromAddresses;
            FromAddressComboBox.DisplayMemberPath = "StreetName"; // Use appropriate property
            FromAddressComboBox.SelectedValuePath = "FromAddressID";

            // Load to addresses
            var toAddresses = await _customTaskRepo.GetToAddressesAsync(); // Assuming you implement this method
            ToAddressComboBox.ItemsSource = toAddresses;
            ToAddressComboBox.DisplayMemberPath = "StreetName"; // Use appropriate property
            ToAddressComboBox.SelectedValuePath = "ToAddressID";

            // Load statuses
            var statuses = await _customTaskRepo.GetTaskStatusesAsync(); // Assuming you implement this method
            StatusComboBox.ItemsSource = statuses;
            StatusComboBox.DisplayMemberPath = "StatusName"; // Update to appropriate property for display
            StatusComboBox.SelectedValuePath = "StatusID"; // Use appropriate property for value
        }

        private async void LoadTasks()
        {
            var tasks = await _customTaskRepo.GetAllTasksAsync();
            TasksDataGrid.ItemsSource = tasks;
        }

        private async void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if any of the ComboBoxes have no selected value
            if (RegionComboBox.SelectedValue == null ||
                AmbulanceComboBox.SelectedValue == null ||
                TaskTypeComboBox.SelectedValue == null ||
                PatientComboBox.SelectedValue == null ||
                FromAddressComboBox.SelectedValue == null ||
                ToAddressComboBox.SelectedValue == null ||
                StatusComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select values for all fields.");
                return; // Exit the method early if any fields are unselected
            }

            // Create the new task with safe casting
            var newTask = new CustomTask
            {
                RegionID = (int)RegionComboBox.SelectedValue,
                AmbulanceID = (int)AmbulanceComboBox.SelectedValue,
                TaskTypeID = (int)TaskTypeComboBox.SelectedValue,
                PatientID = (int)PatientComboBox.SelectedValue,
                FromAddressID = (int)FromAddressComboBox.SelectedValue,
                ToAddressID = (int)ToAddressComboBox.SelectedValue,
                StatusID = (int)StatusComboBox.SelectedValue,
                PickupTime = DateTime.Now, // Update according to your requirement
                DropoffTime = DateTime.Now.AddHours(1), // Update according to your requirement
                LastUpdated = DateTime.UtcNow
            };

            try
            {
                await _customTaskRepo.AddTaskAsync(newTask);
                LoadTasks(); // Refresh the list
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding task: {ex.Message}");
            }
        }

        private async void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksDataGrid.SelectedItem is CustomTask selectedTask)
            {
                bool success = await _customTaskRepo.DeleteTaskAsync(selectedTask.TaskID);
                if (success)
                {
                    LoadTasks(); // Refresh the list after deletion
                }
                else
                {
                    MessageBox.Show("Failed to delete the task.");
                }
            }
            else
            {
                MessageBox.Show("Please select a task to delete.");
            }
        }
    }
}