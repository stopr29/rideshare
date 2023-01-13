using Domain.Enums;

namespace Domain.Entities
{
    public class TravelPlan
    {
        public string Id { get; set; }

        public virtual City From { get; set; }

        public string FromId { get; set; }

        public virtual City To { get; set; }

        public virtual string ToId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public TravelPlanStates State { get; set; } = TravelPlanStates.Unpublished;

        public int? NumberOfSeats { get; set; }

        public string RouteId { get; set; }

        public virtual Route Route { get; set; }

        public virtual IList<User> Users { get; set; }
    }
}