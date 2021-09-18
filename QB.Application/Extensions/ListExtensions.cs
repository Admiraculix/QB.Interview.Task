using QB.Application.Dtos;
using QB.Domain.Models;
using System.Collections.Generic;

namespace QB.Application.Extensions
{
    public static class ListExtensions
    {
        public static List<StatePopulationDto> AddCountryIds(this List<StatePopulationDto> statePopulationDtoList, List<State> stateEntityList)
        {
            foreach (var state in stateEntityList)
            {
                foreach (var dto in statePopulationDtoList)
                {
                    if (state.StateId == dto.StateId)
                    {
                        dto.CountryId = state.CountryId;
                    }
                }
            }

            return statePopulationDtoList;
        }
    }
}
