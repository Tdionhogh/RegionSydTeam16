using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RegionSyd.Model;
using RegionSyd.Repositories;
using System.Collections.Generic;


namespace RegionSyd.View
{
    public partial class MainWindow : Window
    {
        private TaskRepo _taskRepository;
        private List<Model.Task> _tasks;

        public MainWindow()
        {
            InitializeComponent();
            _taskRepository = new TaskRepo(App.ConnectionString);
            LoadData();
        }

        private async void LoadData()
        {
            await LoadDataAsync(); // Sørg for, at LoadDataAsync kaldes korrekt
        }

        private async Task<List<Model.Task>> LoadDataAsync() // Returtypen er List<Model.Task>
        {
            try
            {
                _tasks = await _taskRepository.GetAllTasksAsync();
                TasksDataGrid.ItemsSource = _tasks;

                RegionComboBox.ItemsSource = await _taskRepository.GetRegionsAsync();
                AmbulanceComboBox.ItemsSource = await _taskRepository.GetAmbulancesAsync();
                TaskTypeComboBox.ItemsSource = await _taskRepository.GetTaskTypesAsync();
                PatientComboBox.ItemsSource = await _taskRepository.GetPatientsAsync();
                FromAddressComboBox.ItemsSource = await _taskRepository.GetFromAddressesAsync();
                ToAddressComboBox.ItemsSource = await _taskRepository.GetToAddressesAsync();
                StatusComboBox.ItemsSource = await _taskRepository.GetTaskStatusesAsync();

                return _tasks; // Returner listen af opgaver
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl ved indlæsning af data: {ex.Message}");
                return null; // Returner null i tilfælde af fejl
            }
        }

        private async void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegionComboBox.SelectedItem == null ||
                AmbulanceComboBox.SelectedItem == null ||
                TaskTypeComboBox.SelectedItem == null ||
                PatientComboBox.SelectedItem == null ||
                FromAddressComboBox.SelectedItem == null ||
                ToAddressComboBox.SelectedItem == null ||
                StatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Vær venlig at vælge værdier for alle felter.");
                return;
            }

            try
            {
                var newTask = new Model.Task
                {
                    RegionID = (RegionComboBox.SelectedItem as Region)?.RegionID ?? 0,
                    AmbulanceID = (AmbulanceComboBox.SelectedItem as Ambulance)?.AmbulanceID ?? 0,
                    TaskTypeID = (TaskTypeComboBox.SelectedItem as TaskType)?.TaskTypeID ?? 0,
                    PatientID = (PatientComboBox.SelectedItem as Patient)?.PatientID ?? 0,
                    FromAddressID = (FromAddressComboBox.SelectedItem as FromAddress)?.FromAddressID ?? 0,
                    ToAddressID = (ToAddressComboBox.SelectedItem as ToAddress)?.ToAddressID ?? 0,
                    StatusID = (StatusComboBox.SelectedItem as Taskstatus)?.StatusID ?? 0,
                    PickupTime = DateTime.Parse("2024-01-01 10:00"),
                    DropoffTime = DateTime.Parse("2024-01-01 12:00"),
                    TaskTime = DateTime.Parse("2024-01-01 11:00"),
                    LastUpdated = DateTime.Now
                };

                await _taskRepository.AddTaskAsync(newTask);
                await LoadDataAsync();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Database-fejl: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl ved tilføjelsen af opgaven: {ex.Message}");
            }
        }

        private async void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksDataGrid.SelectedItem is Model.Task selectedTask)
            {
                var result = MessageBox.Show("Er du sikker på, at du vil slette denne opgave?", "Bekræft sletning", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    await _taskRepository.DeleteTaskAsync(selectedTask.TaskID);
                    await LoadDataAsync();
                }
            }
            else
            {
                MessageBox.Show("Vælg en opgave at slette.");
            }
        }
    }
}