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

namespace RegionSyd
{
    public partial class MainWindow : Window
    {
        private readonly IRepository<Ambulance> _ambulanceRepository;

        public MainWindow()
        {
            InitializeComponent();
            _ambulanceRepository = new AmbulanceRepository(); // Opretter en instans af din repository
        }

        private void AddAmbulanceButton_Click(object sender, RoutedEventArgs e)
        {
            // Tilføj en ny ambulance
            var newAmbulance = new Ambulance
            {
                AmbulanceNumber = "A1",
                StatusID = 1,
                Capacity = 2,
                RegionID = 1,
                LastUpdated = DateTime.Now
            };

            _ambulanceRepository.Add(newAmbulance);
            MessageBox.Show("Ambulance tilføjet.");
        }

        private void GetAllAmbulancesButton_Click(object sender, RoutedEventArgs e)
        {
            // Hent alle ambulancer
            var ambulances = _ambulanceRepository.GetAll();
            foreach (var ambulance in ambulances)
            {
                Console.WriteLine($"Ambulance ID: {ambulance.AmbulanceID}, Number: {ambulance.AmbulanceNumber}, Capacity: {ambulance.Capacity}");
            }
        }

        private void UpdateAmbulanceButton_Click(object sender, RoutedEventArgs e)
        {
            // Opdater en ambulance
            var firstAmbulance = _ambulanceRepository.GetById(1);
            if (firstAmbulance != null)
            {
                firstAmbulance.AmbulanceNumber = "A2";
                firstAmbulance.Capacity = 3;
                _ambulanceRepository.Update(firstAmbulance);
                MessageBox.Show("Ambulance opdateret.");
            }
        }

        private void DeleteAmbulanceButton_Click(object sender, RoutedEventArgs e)
        {
            // Slet en ambulance
            _ambulanceRepository.Delete(1);
            MessageBox.Show("Ambulance slettet.");
        }
    }
}