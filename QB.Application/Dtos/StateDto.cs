using System.Collections.Generic;

namespace QB.Application.Dtos
{
    public class StateDto
    {
        public int StateId { get; set; }
        public string StateName { get; set; }

        public int CountryId { get; set; }
        public CountryDto Country { get; set; }
        public ICollection<CityDto> Cities { get; set; }
    }
}
