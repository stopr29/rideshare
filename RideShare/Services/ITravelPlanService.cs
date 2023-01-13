using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Responses;

namespace RideShare.Application
{
    public interface ITravelPlanService
    {
        Task<IResponse> SaveTravelPlan(string userId, TravelPlanDTO travelPlan);

        Task<IResponse> UpdatePublishState(string travelPlanId, TravelPlanStates newState);

        Task<IResponse> GetActive(string fromCity, string toCity);

        Task<IResponse> Join(string userId, string travelPlanId);

        Task<IResponse> GetRoutes(string fromCity, string toCity);
    }
}