using Microsoft.Extensions.DependencyInjection;
using QB.Application.Interfaces.Services.Business;
using QB.Application.Interfaces.Services.Utility;
using QB.Application.Services.Business;
using QB.Application.Services.Utility;

namespace QB.Application.Extensions
{
    public static class DependencyRegistrationExtensions
    {
        public static IServiceCollection RegisterApplicationServiceDepenencies(this IServiceCollection services)
        {
            services.AddTransient<ICityBusinessService, CityBusinessService>();
            services.AddTransient<IStateBusinessService, StateBusinessService>();
            services.AddTransient<ICountryBusinessService, CountryBusinessService>();
            services.AddTransient<INormalizeCountryNameService, NormalizeCountryNameService>();

            return services;
        }
    }
}
