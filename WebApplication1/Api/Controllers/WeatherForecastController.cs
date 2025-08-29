using Microsoft.AspNetCore.Mvc;
using Weather.Interfaces;
using Weather.Services;
using Weather.Models;
using Weather.Base;
using Weather.Const;
using Weather.Models.Dtos;

namespace Weather.Controllers
{

    [ApiController]

    [Route("api/v1/wf")]

    public class WeatherForecastController : ControllerBase

    {
        private readonly IWeatherForecastService _weatherService;

        public WeatherForecastController(IWeatherForecastService weatherService) {

            _weatherService = weatherService;


        }

        private static readonly string[] Summaries = new[]

        {

            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"

        };

        // Method implementations go here

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {


            var result = await _weatherService.GetAllAsync();

            if (result is null) return Ok(new BaseResponse<List<WeatherForecast>> { Data = null, Error = ResponseErrors.elementNotFound });

            return Ok(new BaseResponse<List<WeatherForecast>> { Data = result });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] WeatherForecastDto forecastDto)
        {
            if (!ModelState.IsValid) return BadRequest(new BaseResponse<List<WeatherForecast>> { Data = null, Error = ResponseErrors.InvalidModel });


            WeatherForecast forecast = new WeatherForecast()
            {
                Id = Guid.NewGuid().ToString(),
                Date = forecastDto.Date,
                TemperatureC = forecastDto.TemperatureC,
                Summary = forecastDto.Summary

            };

            var result = await _weatherService.PostAsync(forecast);

            if(result == null) return StatusCode(500, new BaseResponse<List<WeatherForecast>> { Data = null, Error =  ResponseErrors.ErrorWritingDB});

            return Ok(new BaseResponse<WeatherForecast?> { Data = result });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutAsync(string id, [FromBody] WeatherForecastDto forecast)
        {

            if (!ModelState.IsValid) return BadRequest(new BaseResponse<List<WeatherForecast>> { Data = null, Error = ResponseErrors.InvalidModel });


            WeatherForecast Newforecast = new WeatherForecast()
            {
                Date = forecast.Date,
                TemperatureC = forecast.TemperatureC,
                Summary = forecast.Summary

            };

            var updatedForecast = await _weatherService.PutAsync(id, Newforecast);

            if (updatedForecast == null)
                return NotFound(new BaseResponse<WeatherForecast?> { Data = null, Error = ResponseErrors.elementNotFound });

            return Ok(new BaseResponse<WeatherForecast> { Data = updatedForecast});
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _weatherService.DeleteAsync(id);

            if (deleted is null)
                return NotFound(new BaseResponse<string> { Data = null, Error = ResponseErrors.elementNotFound });

            return NoContent();
        }



    }


}

