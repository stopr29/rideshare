using AutoMapper;
using DataAccess;
using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RideShare.Services;

namespace RideShare.Application
{
    public class TravelPlanService : ITravelPlanService
    {
        private readonly SQLDbContext dbContext;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;
        private readonly ILogger<ITravelPlanService> logger;

        public TravelPlanService(IUsersService usersService, IMapper mapper, ILogger<ITravelPlanService> logger, SQLDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.usersService = usersService;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IResponse> SaveTravelPlan(string userId, TravelPlanDTO travelPlanDTO)
        {
            try
            {
                TravelPlan travelPlan = mapper.Map<TravelPlan>(travelPlanDTO);

                User user = await usersService.GetUserById(userId);
                travelPlan.Users = new List<User> { user };

                dbContext.Add(travelPlan);
                await dbContext.SaveChangesAsync();

                return new OkResponse();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new FailResponse("Something went wrong at saving the travel plan.");
            }
        }

        public async Task<IResponse> UpdatePublishState(string travelPlanId, TravelPlanStates newState)
        {
            try
            {
                TravelPlan travelPlan = await dbContext.TravelPlans.FirstOrDefaultAsync(tp => tp.Id == travelPlanId);

                if (travelPlan == null)
                {
                    return new FailResponse("Travel plan not found.");
                }

                travelPlan.State = newState;

                dbContext.Update(travelPlan);
                dbContext.SaveChanges();

                return new OkResponse();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new FailResponse("Something went wrong at updating the travel plan state.");
            }
        }

        public async Task<IResponse> GetActive(string fromCity, string toCity)
        {
            try
            {
                IList<TravelPlan> travelPlans = await dbContext.TravelPlans.Where(tp => tp.Date > DateTime.UtcNow
                                            && tp.From.Name == fromCity
                                            && tp.To.Name == toCity).ToListAsync();

                if (travelPlans.Count == 0)
                {
                    return new FailResponse($"There are no active travel plans that go from {fromCity} to {toCity}.");
                }

                return new EntityResponse<IList<TravelPlan>>(travelPlans);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new FailResponse("Something went wrong at retrieving active travel plans.");
            }
        }

        public async Task<IResponse> Join(string userId, string travelPlanId)
        {
            try
            {
                TravelPlan travelPlan = await dbContext.TravelPlans.FirstOrDefaultAsync(tp => tp.Id == travelPlanId);

                if (travelPlan == null)
                {
                    return new FailResponse("Travel plan does not exist.");
                }

                if (travelPlan.NumberOfSeats == 0)
                {
                    return new FailResponse("There are not left seats.");
                }

                if (travelPlan.Users.FirstOrDefault(u => u.Id == userId) != null)
                {
                    return new FailResponse("User already joined.");
                }

                User user = await usersService.GetUserById(userId);

                travelPlan.Users.Add(user);
                travelPlan.NumberOfSeats--;

                dbContext.Update(travelPlan);
                dbContext.SaveChanges();

                return new OkResponse();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new FailResponse("Something went wrong while joining the travel plan.");
            }
        }

        public async Task<IResponse> GetRoutes(string fromCity, string toCity)
        {
            try
            {
                IList<TravelPlan> travelPlans = await dbContext.TravelPlans.Where(tp =>
                        tp.State == TravelPlanStates.Published).ToListAsync();

                IList<TravelPlan> travelPlansResult = new List<TravelPlan>();

                foreach (TravelPlan travelPlan in travelPlans)
                {
                    if (IsSubRoute(fromCity, toCity, travelPlan.Route))
                    {
                        travelPlansResult.Add(travelPlan);
                    }
                }

                if (travelPlansResult.Count == 0)
                {
                    return new FailResponse($"There are no active travel plans that go through {fromCity} and {toCity}.");
                }

                return new EntityResponse<IList<TravelPlan>>(travelPlansResult);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new FailResponse("Something went wrong while getting the travel plans.");
            }
        }

        private bool IsSubRoute(string fromCity, string toCity, Domain.Entities.Route route)
        {
            int indexFromCity = -1;
            int indexToCity = -1;

            for (int i = 0; i < route.Cities.Count; i++)
            {
                if (route.Cities[i].Name == fromCity)
                {
                    indexFromCity = i;
                }

                if (route.Cities[i].Name == toCity)
                {
                    indexToCity = i;
                    break;
                }
            }

            return indexFromCity != -1 && indexToCity != -1 && indexToCity > indexFromCity;
        }
    }
}