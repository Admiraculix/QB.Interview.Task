using Microsoft.Extensions.DependencyInjection;
using QB.Application.Interfaces.InfrastuctureServices;

namespace QB.External.Rest.Service.Extensions
{
    public static class DependencyRegistrationExtensions
    {
        public static IServiceCollection RegisterExternalRestServiceDepenencies(this IServiceCollection services)
        {
            services.AddTransient<IStatExternalService, ConcreteStatExternalService>();

            return services;
        }
    }
}
