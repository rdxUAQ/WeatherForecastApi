using Weather.Interfaces;
using Weather.Services;

namespace Weather
{

    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder();

            builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseAuthorization();
            app.MapControllers();
            app.Run();



        }
    }
}