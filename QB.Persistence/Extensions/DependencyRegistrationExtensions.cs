using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QB.Application.Interfaces.InfrastuctureServices;
using QB.Persistence.Sqlite.Extensions;

namespace QB.Persistence.Extensions
{
    public static class DependencyRegistrationExtensions
    {
        public static IServiceCollection RegisterPersistenceDepenencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterSqlitePersistance(configuration);
            services.AddTransient<IUnitOfWork, ApplicationUnitOfWork>();

            return services;
        }
    }
}
