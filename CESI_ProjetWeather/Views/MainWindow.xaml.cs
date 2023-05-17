using CESI_ProjetWeather.Services;
using CESI_ProjetWeather.ViewModels;
using System.Windows;

namespace CESI_ProjetWeather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new WeatherViewModel(new WeatherService());
        }
    }
}
