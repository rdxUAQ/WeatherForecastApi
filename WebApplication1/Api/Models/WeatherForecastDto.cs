using System.ComponentModel.DataAnnotations;

namespace Weather.Models.Dtos
{
    public class WeatherForecastDto{
         
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int TemperatureC { get; set; }


        [Required]
        public string? Summary { get; set; }

    }
}