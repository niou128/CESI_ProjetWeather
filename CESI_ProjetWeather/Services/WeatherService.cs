using CESI_ProjetWeather.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CESI_ProjetWeather.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Weather> GetWeatherAsync(string cityName)
        {
            string apiKey = "a23d97464a974bd9860154322231405";
            string requestUrl = $"https://api.weatherapi.com/v1/current.json?key={apiKey}&q={cityName}";
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.GetAsync(requestUrl);
            }
            catch (HttpRequestException e)
            {
                throw new Exception($"Error fetching weather data: {e.Message}");
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error fetching weather data: {response.StatusCode}");
            }

            string content;

            try
            {
                content = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Error reading weather data: {e.Message}");
            }

            dynamic jsonResponse;

            try
            {
                jsonResponse = JsonConvert.DeserializeObject(content);
            }
            catch (JsonException e)
            {
                throw new Exception($"Error parsing weather data: {e.Message}");
            }

            if (jsonResponse == null || jsonResponse.current == null || jsonResponse.location == null)
            {
                throw new Exception("Error parsing weather data: Missing expected elements");
            }

            return new Weather
            {
                CityName = jsonResponse.location.name,
                Temperature = jsonResponse.current.temp_c,
                Humidity = jsonResponse.current.humidity,
                WindSpeed = jsonResponse.current.wind_kph,
                WeatherDescription = jsonResponse.current.condition.text,
                WeatherIcon = jsonResponse.current.condition.icon
            };
        }
    }



}
