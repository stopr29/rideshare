using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Utils;

namespace DataAccess
{
    public class SQLDbContext : DbContext
    {
        private readonly ISettings settings;

        public SQLDbContext(ISettings settings)
        {
            this.settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(settings.DbConnectionString);
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Domain.Entities.Route> Routes { get; set; }

        public DbSet<TravelPlan> TravelPlans { get; set; }

        public DbSet<User> Users { get; set; }
    }
}