using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

namespace api.Controllers
{
    [Route("api/v1")] //this is the base route
    [ApiController]
    public class WhiskeyReviewController : ControllerBase
    {
        private IDatabaseAdapter _database;

        private readonly IMemoryCache _cache;

        public WhiskeyReviewController(IDatabaseAdapter database, IMemoryCache cache)
        {
            _database = database;
             _cache = cache;
        }

         /// <summary>
        /// Gets list of distilleries
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v1/distilleries
        /// </remarks>
        /// <response code="200">Successfully returned distillers</response>
        /// <returns></returns>
        [Route("distilleries")]
        [HttpGet]
        public async Task<List<Distillery>> GetDistilleries()
        {
            const string cacheKey = "distilleries";
            if (!_cache.TryGetValue(cacheKey, out List<Distillery> _distilleries))
            {
                Console.WriteLine("Retrieving data from storage...");
                string jsonString = await System.IO.File.ReadAllTextAsync("Datasource\\distilleries.json");
                _distilleries = JsonSerializer.Deserialize<List<Distillery>>(jsonString);

                _cache.Set(cacheKey, _distilleries, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(365)
                });
            }
            else
            {
                Console.WriteLine("Retrieving data from cache...");
            }
            return _distilleries;
        }

        /// <summary>
        /// Creates a new whiskey review
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/v1/whiskey/reviews"
        ///     {
        ///         "userId": "griff",
        ///         "whiskeyName": "Glenmorangie 10",
        ///         "distilleryName": "Glenmorangie",
        ///         "notes": ["Caramel","Vanilla"],
        ///         "rating": 3,
        ///         "review": "Cracking little number..."
        ///     }
        /// </remarks>
        /// <response code="500">The whiskey review is valid but this system cannot process it</response>
        /// <returns></returns>
        [Route("whiskeys/reviews")]
        [HttpPost]
        public async Task<IActionResult> CreateWhiskeyReview(WhiskeyReview whiskeyReview)
        {
            
            List<Distillery> _distilleries = await GetDistilleries();
            List<string> _names = _distilleries.Select(x => x.Name).ToList();

            if (!_names.Contains(whiskeyReview.DistilleryName))
            {
                return BadRequest("Invalid value for distillery name. See /api/v1/distilleries for a list of valid values.");
            }

            var success = await _database.CreateWhiskeyReview(whiskeyReview);

            if (success)
                return Ok();
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }


        /// <summary>
        /// Gets all of a users whiskey reviews
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v1/users/griff/whiskeys
        /// </remarks>
        /// <response code="200">The users whiskey reviews have been successfully returned</response>
        /// <returns></returns>
        [HttpGet]
        [Route("users/{userId}/whiskeys")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "You haven't reviewed any whiskeys")]
        public async Task<IActionResult> GetWhiskeyReviews(string userId)
        {
            var whiskeyReviewList = await _database.GetWhiskeyReviews(userId);
            if (whiskeyReviewList.Count == 0)
            {
                return NoContent();
            }

            return Ok(whiskeyReviewList);
        }

        /// <summary>
        /// Gets a whiskey review
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v1/users/griff/whiskeys/glenmorangie10-griff
        /// </remarks>
        /// <response code="200">The whiskey review has been successfully returned</response>
        /// <returns></returns>
        [HttpGet]
        [Route("users/{userId}/whiskeys/{id}")]
        public async Task<IActionResult> GetWhiskeyReview(string userId, string id)
        {
            var whiskeyReview = await _database.GetWhiskeyReview(userId, id);
            if (whiskeyReview == null)
            {
                return NotFound();
            }

            return Ok(whiskeyReview);
        }

        /// <summary>
        /// Updates whiskey review
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/v1/whiskey/reviews"
        ///     {
        ///         "userId": "griff",
        ///         "whiskeyName": "Glenmorangie 10",
        ///         "distilleryName": "Glenmorangie",
        ///         "notes": ["Caramel","Honey"],
        ///         "rating": 3,
        ///         "review": "Cracking little number... from what I can remember!"
        ///     }
        /// </remarks>
        /// <response code="500">The whiskey review is valid but this system cannot process it</response>
        /// <returns></returns>
        [HttpPut]
        [Route("whiskeys/reviews")]
        public async Task<IActionResult> UpdateWhiskeyReview(WhiskeyReview whiskeyReview)
        {
            List<Distillery> _distilleries = await GetDistilleries();
            List<string> _names = _distilleries.Select(x => x.Name).ToList();

            if (!_names.Contains(whiskeyReview.DistilleryName))
            {
                return BadRequest("Invalid value for distillery name. See /api/v1/distilleries for a list of valid values.");
            }

            var success = await _database.UpdateWhiskeyReview(whiskeyReview.WhiskeyID, whiskeyReview.Id, whiskeyReview);
            if (success)
                return Ok();
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Deletes whiskey review
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/v1/users/griff/whiskeys/glenmorangie10-griff
        /// </remarks>
        /// <response code="200">The whiskey review has been successfully deleted</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("users/{userId}/whiskeys/{id}")]
        public async Task<IActionResult> DeleteWhiskeyReview(string userId, string id)
        {
            var success = await _database.DeleteWhiskeyReview(userId, id);
            if (success)
                return Ok();
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}