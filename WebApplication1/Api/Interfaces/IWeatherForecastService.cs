using Microsoft.AspNetCore.Mvc;
using Weather.Models;

namespace Weather.Interfaces
{
    public interface IWeatherForecastService
    {
        public Task<List<WeatherForecast>?> GetAllAsync();
        public Task<WeatherForecast?> PostAsync(WeatherForecast forecast);
        public Task<WeatherForecast> PutAsync(string id, WeatherForecast forecast);
        public Task<WeatherForecast> DeleteAsync(string id);


    }
}