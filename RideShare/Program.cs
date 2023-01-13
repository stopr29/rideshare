using AutoMapper;
using DataAccess;
using Domain;
using RideShare.Application;
using RideShare.Services;
using Utils;

namespace RideShare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ISettings settings = new Settings();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSingleton(settings);

            builder.Services.AddDbContext<SQLDbContext>();

            MapperConfiguration configuration = new(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            IMapper mapper = configuration.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddScoped<ITravelPlanService, TravelPlanService>();
            builder.Services.AddScoped<IUsersService, UsersService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}