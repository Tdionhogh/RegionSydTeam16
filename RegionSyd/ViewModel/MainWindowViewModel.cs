using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks; 
using RegionSyd.Model; 
using RegionSyd.Repositories;
using Microsoft.Extensions.Configuration;

namespace RegionSyd.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly CustomTaskRepo _taskRepository; // Updated to CustomTaskRepo
        private ObservableCollection<CustomTask> _tasks; // Updated to CustomTask
        private CustomTask _selectedTask; // Updated to CustomTask

        public ObservableCollection<CustomTask> Tasks // Updated to CustomTask
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        public CustomTask SelectedTask // Updated to CustomTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        // Other collections for ComboBoxes
        public ObservableCollection<Region> Regions { get; set; }
        public ObservableCollection<Ambulance> Ambulances { get; set; }
        public ObservableCollection<TaskType> TaskTypes { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<FromAddress> FromAddresses { get; set; }
        public ObservableCollection<ToAddress> ToAddresses { get; set; }
        public ObservableCollection<Taskstatus> Statuses { get; set; }

        // Selected Items from ComboBoxes
        private Region _selectedRegion;
        public Region SelectedRegion
        {
            get => _selectedRegion;
            set
            {
                _selectedRegion = value;
                OnPropertyChanged(nameof(SelectedRegion));
            }
        }

        private Ambulance _selectedAmbulance;
        public Ambulance SelectedAmbulance
        {
            get => _selectedAmbulance;
            set
            {
                _selectedAmbulance = value;
                OnPropertyChanged(nameof(SelectedAmbulance));
            }
        }

        private TaskType _selectedTaskType;
        public TaskType SelectedTaskType
        {
            get => _selectedTaskType;
            set
            {
                _selectedTaskType = value;
                OnPropertyChanged(nameof(SelectedTaskType));
            }
        }

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

        private FromAddress _selectedFromAddress;
        public FromAddress SelectedFromAddress
        {
            get => _selectedFromAddress;
            set
            {
                _selectedFromAddress = value;
                OnPropertyChanged(nameof(SelectedFromAddress));
            }
        }

        private ToAddress _selectedToAddress;
        public ToAddress SelectedToAddress
        {
            get => _selectedToAddress;
            set
            {
                _selectedToAddress = value;
                OnPropertyChanged(nameof(SelectedToAddress));
            }
        }

        private Taskstatus _selectedStatus;
        public Taskstatus SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                _selectedStatus = value;
                OnPropertyChanged(nameof(SelectedStatus));
            }
        }

        public MainWindowViewModel(IConfiguration configuration)
        {
            _taskRepository = new CustomTaskRepo(configuration); // Initialize CustomTaskRepo with configuration
            Tasks = new ObservableCollection<CustomTask>();
            AddTaskCommand = new RelayCommand(async () => await AddTask());
            DeleteTaskCommand = new RelayCommand(async () => await DeleteTask());
            LoadData();
        }

        private async Task LoadData()
        {
            await LoadTasksAsync();
            await LoadComboBoxDataAsync();
        }

        private async Task LoadTasksAsync() // This method now returns a Task
        {
            var tasks = await _taskRepository.GetAllTasksAsync(); // Make sure this method returns IEnumerable<CustomTask>
            Tasks.Clear();
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
        }

        private async Task LoadComboBoxDataAsync()
        {
            Regions = new ObservableCollection<Region>(await _taskRepository.GetRegionsAsync());
            Ambulances = new ObservableCollection<Ambulance>(await _taskRepository.GetAmbulancesAsync());
            TaskTypes = new ObservableCollection<TaskType>(await _taskRepository.GetTaskTypesAsync());
            Patients = new ObservableCollection<Patient>(await _taskRepository.GetPatientsAsync());
            FromAddresses = new ObservableCollection<FromAddress>(await _taskRepository.GetFromAddressesAsync());
            ToAddresses = new ObservableCollection<ToAddress>(await _taskRepository.GetToAddressesAsync());
            Statuses = new ObservableCollection<Taskstatus>(await _taskRepository.GetTaskStatusesAsync());
        }

        private async Task AddTask()
        {
            if (SelectedRegion == null || SelectedAmbulance == null || SelectedTaskType == null ||
                SelectedPatient == null || SelectedFromAddress == null || SelectedToAddress == null ||
                SelectedStatus == null)
            {
                // Provide user feedback that all fields must be filled
                MessageBox.Show("Please select values for all fields.");
                return;
            }

            var newTask = new CustomTask // Updated to CustomTask
            {
                RegionID = SelectedRegion.RegionID,
                AmbulanceID = SelectedAmbulance.AmbulanceID,
                TaskTypeID = SelectedTaskType.TaskTypeID,
                PatientID = SelectedPatient.PatientID,
                FromAddressID = SelectedFromAddress.FromAddressID,
                ToAddressID = SelectedToAddress.ToAddressID,
                StatusID = SelectedStatus.StatusID,
                PickupTime = DateTime.Now, // Update as per requirements
                DropoffTime = DateTime.Now.AddHours(2), // Update as per requirements
                TaskTime = TimeSpan.FromHours(2), // Example: duration as TimeSpan
                LastUpdated = DateTime.Now
            };

            try
            {
                await _taskRepository.AddTaskAsync(newTask);
                await LoadTasksAsync(); // Refresh the task list
            }
            catch (Exception ex)
            {
                // Handle the error appropriately (log, show message, etc.)
                MessageBox.Show($"Error adding task: {ex.Message}");
            }
        }

        private async Task DeleteTask()
        {
            if (SelectedTask != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete this task?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _taskRepository.DeleteTaskAsync(SelectedTask.TaskID);
                        await LoadTasksAsync(); // Refresh the task list
                    }
                    catch (Exception ex)
                    {
                        // Handle the error appropriately (log, show message, etc.)
                        MessageBox.Show($"Error deleting task: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a task to delete.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
