using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Antiforgery;

namespace Weather.Models
{
    public class WeatherForecast

    {


        [Required]
        public string? Id { get;  set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int TemperatureC { get; set; }


        [Required]
        public string? Summary { get; set; }

    


    }
}