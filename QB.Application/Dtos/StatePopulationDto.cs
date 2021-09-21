namespace QB.Application.Dtos
{
    public class StatePopulationDto
    {
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public int? Population { get; set; }
        public int CountryId { get; set; }
    }
}
