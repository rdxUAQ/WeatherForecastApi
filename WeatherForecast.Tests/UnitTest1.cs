using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Weather.Controllers;
using Weather.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Weather.Models;
using Weather.Base;

namespace WeatherForecastT.Tests;

public class UnitTest1 
{
    [Fact]
    public async Task Get_All_Weather_Reports()
    {
        //arrange
        List<WeatherForecast> fakeListElements = new List<WeatherForecast>
        {
            new WeatherForecast { Date = DateTime.Now, TemperatureC = 20, Summary = "Sunny" },
            new WeatherForecast { Date = DateTime.Now.AddDays(1), TemperatureC = 22, Summary = "Cloudy" },
            new WeatherForecast { Date = DateTime.Now.AddDays(2), TemperatureC = 18, Summary = "Rainy" },
            new WeatherForecast { Date = DateTime.Now.AddDays(3), TemperatureC = 25, Summary = "Hot" },
            new WeatherForecast { Date = DateTime.Now.AddDays(4), TemperatureC = 15, Summary = "Windy" }
        };
        var fakeRegisters = A.CollectionOfFake<WeatherForecast>(5).AsEnumerable();
        var IFakeService = A.Fake<IWeatherForecastService>();

        A.CallTo(() => IFakeService.GetAllAsync()).Returns(Task.FromResult<List<WeatherForecast>?>(fakeListElements));
        var controller = new WeatherForecastController(IFakeService);
        //act

        var actionResult = await controller.GetAllRegisters();
        //asert

        var okResult = actionResult as OkObjectResult;
        var baseResponse = okResult?.Value as BaseResponse<List<WeatherForecast>>;
        var returnRegisters = baseResponse?.Data;
        Assert.NotNull(returnRegisters);
        Assert.Equal(fakeListElements, returnRegisters);
    }
}
