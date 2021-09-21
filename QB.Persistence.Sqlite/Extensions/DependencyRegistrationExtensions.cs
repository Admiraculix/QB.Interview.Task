using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QB.Application.Interfaces.Repositories;
using QB.Persistence.Sqlite.Repositories;
using System.IO;
using System.Runtime.InteropServices;

namespace QB.Persistence.Sqlite.Extensions
{
    public static class DependencyRegistrationExtensions
    {
        private const string TemplateName = "{FullPath}";
        private const string DatabaseFolderName = "Db";
        private const string ExecuteAssemblyName = "\\QB.API";
        private const string LinuxDbFullPath = "../src/QB.Persistence.Sqlite/Db/citystatecountry.db";

        public static IServiceCollection RegisterSqlitePersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqliteDbContext>(options =>
                options.UseSqlite(BuildFullPathForConnectionStringgSqliteDb(configuration)));

            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();

            return services;
        }

        private static string BuildFullPathForConnectionStringgSqliteDb(IConfiguration configuration)
        {
            var isLinuxOs = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            if (isLinuxOs)
            {
                return Directory.Exists("../src") ? $"Data Source={LinuxDbFullPath}" : $"Data Source=Db/citystatecountry.db";
            }

            var assemblyName = typeof(DependencyRegistrationExtensions).Assembly.GetName().Name;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), assemblyName, DatabaseFolderName)
                           .Replace(ExecuteAssemblyName, string.Empty);
            var connectionString = configuration.GetConnectionString("ApplicationConnection")
                                   .Replace(TemplateName, $"{filePath}{Path.DirectorySeparatorChar}");

            return connectionString;
        }
    }
}
