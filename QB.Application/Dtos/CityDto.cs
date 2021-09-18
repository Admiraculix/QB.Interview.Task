namespace QB.Application.Dtos
{
    public class CityDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int? Population { get; set; }

        public int StateId { get; set; }
        public StateDto State { get; set; }
    }
}
