using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QB.Application.Interfaces.InfrastuctureServices
{
    public interface IStatExternalService
    {
        List<Tuple<string, int>> GetCountryPopulations();
        Task<List<Tuple<string, int>>> GetCountryPopulationsAsync();
    }
}
