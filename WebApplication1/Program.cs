using Microsoft.AspNetCore.HttpLogging;
using Weather.Interfaces;
using Weather.Services;

namespace Weather
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Dependency Injection
            builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();
            
            //CONTROLLERS
            builder.Services.AddControllers();

            //Swagger 
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Http logging
            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            var app = builder.Build();

            //APP SWAGGER
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEATHER API v1")
            );

            var logger = app.Services.GetRequiredService<ILogger<Program>>();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/error");
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpLogging();

            app.UseAuthentication();
            app.UseAuthorization();


            // Middleware: log de request/response
            app.Use(async (context, next) =>
            {
                logger.LogInformation("Request: {Method} {Path}", context.Request.Method, context.Request.Path);
                await next();
                logger.LogInformation("Response: {StatusCode}", context.Response.StatusCode);
            });

            // Middleware: tiempo de respuesta
            app.Use(async (context, next) =>
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();

                context.Response.OnStarting(() =>
                {
                    var elapsed = sw.ElapsedMilliseconds;
                    // Se escribe antes de que la respuesta empiece
                    context.Response.Headers["X-Response-Time-ms"] = elapsed.ToString("0");
                    return Task.CompletedTask;
                });

                await next();

                sw.Stop();
                logger.LogInformation("Timing: {Method} {Path} -> {StatusCode} in {Elapsed} ms",
                    context.Request.Method, context.Request.Path, context.Response.StatusCode, sw.ElapsedMilliseconds);
            });



            app.MapControllers();
            app.Run();
        }
    }
}