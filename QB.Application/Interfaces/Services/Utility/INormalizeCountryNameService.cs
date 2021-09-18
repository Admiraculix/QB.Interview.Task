using QB.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QB.Application.Interfaces.Services.Utility
{
    public interface INormalizeCountryNameService
    {
        Task<IEnumerable<CountryPopulationDto>> NormalizeUsaCountryNameAsync();
    }
}
