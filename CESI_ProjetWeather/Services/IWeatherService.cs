using CESI_ProjetWeather.Models;
using System.Threading.Tasks;

namespace CESI_ProjetWeather.Services
{
    public interface IWeatherService
    {
        Task<Weather> GetWeatherAsync(string cityName);
    }

}
