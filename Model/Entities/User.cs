namespace Domain.Entities
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual IList<TravelPlan> TravelPlans { get; set; }
    }
}