namespace Domain.DTOs
{
    public class TravelPlanDTO
    {
        public CityDTO From { get; set; }

        public CityDTO To { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int? NumberOfSeats { get; set; }

        public RouteDTO Route { get; set; }
    }
}