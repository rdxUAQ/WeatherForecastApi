using Newtonsoft.Json;
using Weather.Interfaces;
using Weather.Models;

namespace Weather.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private static readonly string currentDir = AppContext.BaseDirectory;
        private static readonly string forecastsPath = Path.Combine(currentDir, "Api", "Data", "Forecast.json");
        private readonly string json = File.ReadAllText(forecastsPath);

        public async Task<List<WeatherForecast>?> GetAllAsync()
        {


            await Task.Delay(2000);
            
            try
            {

                var results = JsonConvert.DeserializeObject<List<WeatherForecast>>(json);

                return results;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }

            

            return null;
            
            
        }
        public async Task<WeatherForecast?> PostAsync(WeatherForecast forecast)
        {
            try
            {

                var results = JsonConvert.DeserializeObject<List<WeatherForecast>>(json) ?? new List<WeatherForecast>();

                results.Add(forecast);

                string newJson = JsonConvert.SerializeObject(results, Formatting.Indented);

                await File.WriteAllTextAsync(forecastsPath, newJson);

                return forecast;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);

                return null;

            }

        }
        
        public async Task<WeatherForecast> PutAsync(string id, WeatherForecast forecast)
        {
            Console.WriteLine($"PutAsync called with id: {id}");

            await Task.Delay(1000);

            try
            {
            var results = JsonConvert.DeserializeObject<List<WeatherForecast>>(json) ?? new List<WeatherForecast>();
            Console.WriteLine("Deserialized forecasts from JSON.");

            var oldforecast = results.FirstOrDefault(f => f.Id == id);

            if (forecast == null || oldforecast == null)
            {
                foreach(var f in results) Console.WriteLine(f.Id);
                Console.WriteLine("Forecast or oldforecast is null. Update failed.");
                return null;
            }

            // Update the properties with the new values from weatherForecast
            oldforecast.Date = forecast.Date;
            oldforecast.TemperatureC = forecast.TemperatureC;
            oldforecast.Summary = forecast.Summary;
            Console.WriteLine($"Updated forecast with id: {id}");

            string newJson = JsonConvert.SerializeObject(results, Formatting.Indented);
            await File.WriteAllTextAsync(forecastsPath, newJson);
            Console.WriteLine("Wrote updated forecasts to JSON file.");

            return forecast;
            }
            catch (Exception ex)
            {
            Console.WriteLine($"Exception in PutAsync: {ex}");
            return null;
            }
        }
        public async Task<WeatherForecast> DeleteAsync(string id)
        {
            try
            {
                var results = JsonConvert.DeserializeObject<List<WeatherForecast>>(json) ?? new List<WeatherForecast>();

                var forecastToRemove = results.FirstOrDefault(f => f.Id == id.ToString());
                if (forecastToRemove == null)
                {
                    Console.WriteLine($"No forecast found with id: {id}");
                    return null;
                }

                results.Remove(forecastToRemove);

                string newJson = JsonConvert.SerializeObject(results, Formatting.Indented);
                await File.WriteAllTextAsync(forecastsPath, newJson);

                return forecastToRemove;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in DeleteAsync: {ex}");
                return null;
            }
        }

        

        

        
    }
}