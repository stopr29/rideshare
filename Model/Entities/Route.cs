namespace Domain.Entities
{
    public class Route
    {
        public string Id { get; set; }

        public virtual IList<City> Cities { get; set; }
    }
}