using CESI_ProjetWeather.Commands;
using CESI_ProjetWeather.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CESI_ProjetWeather.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private readonly IWeatherService _weatherService;

        private string _cityName;
        private string _weatherDescription;
        private string _windSpeed;
        private string _humidity;
        private string _weatherIcon;
        private ICommand _getWeatherCommand;
        private ICommand _testCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public WeatherViewModel(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public string CityName
        {
            get => _cityName;
            set
            {
                if (_cityName != value)
                {
                    _cityName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string WeatherDescription
        {
            get => _weatherDescription;
            set
            {
                if (_weatherDescription != value)
                {
                    _weatherDescription = value;
                    OnPropertyChanged();
                }
            }
        }

        public string WindSpeed
        {
            get => _windSpeed;
            set
            {
                if (_windSpeed != value)
                {
                    _windSpeed = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Humidity
        {
            get => _humidity;
            set
            {
                if (_humidity != value)
                {
                    _humidity = value;
                    OnPropertyChanged();
                }
            }
        }

        public string WeatherIcon
        {
            get => _weatherIcon;
            set
            {
                if (_weatherIcon != value)
                {
                    _weatherIcon = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand GetWeatherCommand
        {
            get
            {
                return _getWeatherCommand ??= new RelayCommand(
                    async param =>
                    {
                        try
                        {
                            var weather = await _weatherService.GetWeatherAsync(CityName);
                            WeatherDescription = $"{weather.Temperature}°C, {weather.WeatherDescription}";
                            WindSpeed = $"{weather.WindSpeed} km/h";  
                            Humidity = $"{weather.Humidity}%";        
                            WeatherIcon = "https:" + weather.WeatherIcon;
                        }
                        catch (Exception ex)
                        {
                            // Vous pouvez remplacer cela par un logger ou tout autre mécanisme de journalisation que vous préférez.
                            System.Diagnostics.Debug.WriteLine($"An exception occurred: {ex}");
                        }
                    },
                    param => CanExecuteGetWeather());
            }
        }

        public ICommand TestCommand
        {
            get
            {
                return _testCommand ??= new RelayCommand(
                    param =>
                    {
                        System.Diagnostics.Debug.WriteLine("TestCommand was executed!");
                        return Task.CompletedTask;
                    },
                    param => true);
            }
        }

        private bool CanExecuteGetWeather()
        {
            // Pour le débogage, affichez ce message dans la console.
            // Si le message s'affiche lorsque vous cliquez sur le bouton, alors la liaison fonctionne correctement.
            System.Diagnostics.Debug.WriteLine("CanExecuteGetWeather was called!");
            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
