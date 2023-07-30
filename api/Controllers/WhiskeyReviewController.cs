using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace api.Controllers
{
    [Route("api/v1/whiskey/users")] //this is the base route
    [ApiController]
    public class WhiskeyReviewController : ControllerBase
    {
        private IDatabaseAdapter _database;

        public WhiskeyReviewController(IDatabaseAdapter database)
        {
            _database = database;
        }

        /// <summary>
        /// Creates a new whiskey review
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/v1/whiskey/users"
        ///     {
        ///         "userId": "testuser123",
        ///         "whiskey": "Glemorangie 10",
        ///         "notes": ["Caramel","Vanilla"],
        ///         "rating": 3,
        ///         "review": "Cracking little number... from what I can remember!"
        ///     }
        /// </remarks>
        /// <param name="userId">The user identifier that is creating the whiskey review.</param>
        /// <response code="500">The whiskey review is valid but this system cannot process it</response>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateReview(WhiskeyReview whiskeyReview)
        {
            var success = await _database.CreateWhiskeyReview(whiskeyReview);
            if(success) 
                return Ok();
            else 
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet]
        [Route("{userId}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "You haven't reviewed any whiskeys")]
        public async Task<IActionResult> GetUserReviews(string userId)
        {
            var whiskeyReviewList = await _database.GetUserReviews(userId);
            if (whiskeyReviewList.Count == 0)
            {
                return NoContent();
            }

            return Ok(whiskeyReviewList); 
        }

    //     [HttpGet]
    //     //[Authorize]
    //     [Route("{flightPlanId}")] //curly braces are variables
    //     public async Task<IActionResult> GetFlightFlightPlanById(string flightPlanId)
    //     {
    //         var flightPlan = await _database.GetFlightPlanById(flightPlanId);
    //         if (flightPlan.FlightPlanId != flightPlanId)
    //         {
    //             return StatusCode(StatusCodes.Status404NotFound);
    //         }

    //         return Ok(flightPlan);
    //     }
        
    //     // the following generates a load of swagger docs

    //     /// <summary>
    //     /// Files a new flight plan with the system
    //     /// </summary>
    //     /// <remarks>
    //     /// Sample request:
    //     /// 
    //     ///     POST /api/v1/flightplan/file
    //     ///     {
    //     ///         "aircraft_identification": "N67SVS",
    //     ///         "aircraft_type": "PA-34 Piper Seneca",
    //     ///         "airspeed": 128,
    //     ///         "altitude": 12000,
    //     ///         "flight_type": "VFR",
    //     ///         "fuel_hours": 3,
    //     ///         "fuel_minutes": 41,
    //     ///         "departure_time": "2022-07-08T00:26:45Z",
    //     ///         "estimated_arrival_time": "2022-07-08T03:49:45Z",
    //     ///         "departing_airport": "KBXA",
    //     ///         "arrival_airport": "KNZY",
    //     ///         "route": "KBXA JOH J46 DMDUP J46 KNZY",
    //     ///         "remarks": "",
    //     ///         "number_onboard": 4
    //     ///     }
    //     /// </remarks>
    //     /// <param name="flightPlan">The fight plan data to be filed.</param>
    //     /// <response code="400">There is a problem with the flight plan data received by this system</response>
    //     /// <response code="500">The flight plan is valid but this system cannot process it</response>
    //     /// <returns></returns>
    //     [HttpPost]
    //    // [Authorize]
    //     [Route("file")]
    //     public async Task<IActionResult> FileFlightPlan(FlightPlan flightPlan)
    //     {
    //         var transactionResult = await _database.FileFlightPlan(flightPlan);
    //         switch(transactionResult)
    //         {
    //             case TransactionResult.Success:
    //                 return Ok();
    //             case TransactionResult.BadRequest:
    //                 return StatusCode(StatusCodes.Status400BadRequest);
    //             default:
    //                 return StatusCode(StatusCodes.Status500InternalServerError);
    //         }
    //     }

    //     [HttpPut]
    //    // [Authorize]
    //     public async Task<IActionResult> UpdateFlightPlan(FlightPlan flightPlan)
    //     {
    //         var updateResult = await _database.UpdateFlightPlan(flightPlan.FlightPlanId, flightPlan);
    //         switch (updateResult)
    //         {
    //             case TransactionResult.Success:
    //                 return Ok();
    //             case TransactionResult.NotFound:
    //                 return StatusCode(StatusCodes.Status404NotFound);
    //             default:
    //                 return StatusCode(StatusCodes.Status500InternalServerError);
    //         }
    //     }

    //     [HttpDelete]
    //     //[Authorize]
    //     [Route("{flightPlanId}")]
    //     public async Task<IActionResult> DeleteFlightPlan(string flightPlanId)
    //     {
    //         var result = await _database.DeleteFlightPlanById(flightPlanId);
    //         if (result)
    //         {
    //             return Ok();
    //         }

    //         return StatusCode(StatusCodes.Status404NotFound);
    //     }

    //     [HttpGet]
    //     //[Authorize]
    //     [Route("airport/departure/{flightPlanId}")]
    //     public async Task<IActionResult> GetFlightPlanDepartureAirport(string flightPlanId)
    //     {
    //         var flightPlan = await _database.GetFlightPlanById(flightPlanId);
            
    //         if (flightPlan == null)
    //         {
    //             return StatusCode(StatusCodes.Status404NotFound);
    //         }

    //         return Ok(flightPlan.DepartingAirport);
    //     }

    //     [HttpGet]
    //     //[Authorize]
    //     [Route("route/{flightPlanId}")]
    //     public async Task<IActionResult> GetFlightPlanRoute(string flightPlanId)
    //     {
    //         var flightPlan = await _database.GetFlightPlanById(flightPlanId);
    //         if (flightPlan.FlightPlanId != flightPlanId)
    //         {
    //             return StatusCode(StatusCodes.Status404NotFound);
    //         }

    //         return Ok(flightPlan.Route);
    //     }

    //     [HttpGet]
    //     //[Authorize]
    //     [Route("time/enroute/{flightPlanId}")]
    //     public async Task<IActionResult> GetFlightPlanTimeEnroute(string flightPlanId)
    //     {
    //         var flightPlan = await _database.GetFlightPlanById(flightPlanId);
    //         if (flightPlan == null)
    //         {
    //             return StatusCode(StatusCodes.Status404NotFound);
    //         }
            
    //         var estimatedTimeEnroute = flightPlan.ArrivalTime - flightPlan.DepartureTime;

    //         return Ok(estimatedTimeEnroute);
    //     }
        
    }
}
