using Domain.DTOs;
using Domain.Entities;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using RideShare.Application;

namespace RideShare.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TravelPlansController : ControllerBase
    {
        private readonly ITravelPlanService travelPlanService;

        public TravelPlansController(ITravelPlanService travelPlanService)
        {
            this.travelPlanService = travelPlanService;
        }

        [HttpPost]
        [Route("Save/{userId}")]
        public async Task<IResponse> SaveTravelPlan(string userId, [FromBody] TravelPlanDTO travelPlan)
        {
            IResponse result = await travelPlanService.SaveTravelPlan(userId, travelPlan);

            return result;
        }

        [HttpPut]
        [Route("Publish/{travelPlanId}")]
        public async Task<IResponse> PublishTravelPlan(string travelPlanId)
        {
            IResponse result = await travelPlanService.UpdatePublishState(travelPlanId, Domain.Enums.TravelPlanStates.Published);

            return result;
        }

        [HttpPut]
        [Route("Unpublish/{travelPlanId}")]
        public async Task<IResponse> UnpublishTravelPlan(string travelPlanId)
        {
            IResponse result = await travelPlanService.UpdatePublishState(travelPlanId, Domain.Enums.TravelPlanStates.Unpublished);

            return result;
        }

        [HttpGet]
        [Route("GetActive")]
        public async Task<IResponse> GetActive(string fromCity, string toCity)
        {
            IResponse result = await travelPlanService.GetActive(fromCity, toCity);

            return result;
        }

        [HttpPost]
        [Route("Join/{userId}/{travelPlanId}")]
        public async Task<IResponse> Join(string userId, string travelPlanId)
        {
            IResponse result = await travelPlanService.Join(userId, travelPlanId);

            return result;
        }

        [HttpGet]
        [Route("GetRoutes")]
        public async Task<IResponse> GetRoutes(string fromCity, string toCity)
        {
            IResponse result = await travelPlanService.GetRoutes(fromCity, toCity);

            return result;
        }
    }
}