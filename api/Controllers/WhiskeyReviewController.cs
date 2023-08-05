using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace api.Controllers
{
    [Route("api/v1/whiskey/reviews")] //this is the base route
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

        [HttpPut]
        public async Task<IActionResult> UpdateReview(WhiskeyReview whiskeyReview)
        {
            var success = await _database.UpdateWhiskeyReview(whiskeyReview);
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

        [HttpGet]
        [Route("user/{userId}/whiskey/{name}")]
        public async Task<IActionResult> GetUserReview(string userId, string name) 
        {
            var whiskeyReview = await _database.GetUserReview(userId, name);
            if (whiskeyReview == null)
            {
                return NotFound();
            }

            return Ok(whiskeyReview);
        }

        [HttpDelete]
        [Route("user/{userId}/whiskey/{name}")]
        public async Task<IActionResult> DeleteUserReview(string userId, string name)
        {
            var success = await _database.DeleteReview(userId, name);
            if(success) 
                return Ok();
            else 
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}