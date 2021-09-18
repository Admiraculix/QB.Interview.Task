using System.Collections.Generic;

namespace QB.Application.Dtos
{
    public class CountryDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public ICollection<StateDto> States { get; set; }
    }
}
