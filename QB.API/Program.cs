using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace QB.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = CreateSerilogLogger();

            try
            {
                Log.Information($"Starting up {typeof(Program).Assembly.GetName().Name}");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Application {typeof(Program).Assembly.GetName().Name} start-up failed");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var env = hostingContext.HostingEnvironment;

                        config
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: false)
                            .AddEnvironmentVariables();
                    });
                });

        private static ILogger CreateSerilogLogger()
        {
            var configuration = new ConfigurationBuilder()
                               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                               .AddEnvironmentVariables()
                               .Build();

            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.File(Path.Combine("Logs", "qb_task_log.txt"), rollingInterval: RollingInterval.Day) ;

            return loggerConfiguration.CreateLogger();
        }
    }
}
