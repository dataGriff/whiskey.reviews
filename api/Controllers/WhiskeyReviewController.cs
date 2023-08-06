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

        // /// <summary>
        // /// Creates a new whiskey review
        // /// </summary>
        // /// <remarks>
        // /// Sample request:
        // /// 
        // ///     POST api/v1/whiskey/reviews"
        // ///     {
        // ///         "userId": "Griff",
        // ///         "whiskeyName": "Glenmorangie 10",
        // ///         "distilleryName": "Glenmorangie",
        // ///         "notes": ["Caramel","Vanilla"],
        // ///         "rating": 3,
        // ///         "review": "Cracking little number... from what I can remember!"
        // ///     }
        // /// </remarks>
        // /// <response code="500">The whiskey review is valid but this system cannot process it</response>
        // /// <returns></returns>
        // [Route("whiskeys/reviews")]
        // [HttpPost]
        // public async Task<IActionResult> CreateWhiskeyReview(WhiskeyReview whiskeyReview)
        // {
        //     var success = await _database.CreateWhiskeyReview(whiskeyReview);

        //     List<string> _names = await GetDistilleries();

        //     if (!_names.Contains(whiskeyReview.DistilleryName))
        //     {
        //         return BadRequest("Invalid value for distillery name. See /api/v1/distilleries for a list of valid values.");
        //     }

        //     if (success)
        //         return Ok();
        //     else
        //         return StatusCode(StatusCodes.Status500InternalServerError);
        // }

        // [HttpGet]
        // [Route("whiskeys/{whiskeyId}")]
        // [SwaggerResponse((int)HttpStatusCode.NoContent, "You haven't reviewed any whiskeys")]
        // public async Task<IActionResult> GetWhiskeyReviews(string whiskeyId)
        // {
        //     var whiskeyReviewList = await _database.GetWhiskeyReviews(whiskeyId);
        //     if (whiskeyReviewList.Count == 0)
        //     {
        //         return NoContent();
        //     }

        //     return Ok(whiskeyReviewList);
        // }

        // [HttpGet]
        // [Route("whiskeys/{whiskeyId}/reviews/{id}")]
        // public async Task<IActionResult> GetWhiskeyReview(string whiskeyId, string id)
        // {
        //     var whiskeyReview = await _database.GetWhiskeyReview(whiskeyId, id);
        //     if (whiskeyReview == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(whiskeyReview);
        // }

        // /// <summary>
        // /// Updates a new whiskey review
        // /// </summary>
        // /// <remarks>
        // /// Sample request:
        // /// 
        // ///     PUT api/v1/whiskey/glenmorangie-10/reviews/griff-glenmorangie10"
        // ///     {
        // ///         "userId": "Griff",
        // ///         "whiskeyName": "Glenmorangie 10",
        // ///         "distilleryName": "Glenmorangie",
        // ///         "notes": ["Caramel","Vanilla"],
        // ///         "rating": 3,
        // ///         "review": "Cracking little number... from what I can remember! Tastes like chocolate",
        // ///         "location": "Pennyroyal, Cardiff"
        // ///     }
        // /// </remarks>
        // /// <response code="500">The whiskey review is valid but this system cannot process it</response>
        // /// <returns></returns>
        // [HttpPut]
        // [Route("whiskeys/reviews")]
        // public async Task<IActionResult> UpdateWhiskeyReview(WhiskeyReview whiskeyReview)
        // {
        //     List<string> _names = await GetDistilleries();

        //     if (!_names.Contains(whiskeyReview.DistilleryName))
        //     {
        //         return BadRequest("Invalid value for distillery name. See /api/v1/distilleries for a list of valid values.");
        //     }

        //     var success = await _database.UpdateWhiskeyReview(whiskeyReview.WhiskeyID, whiskeyReview.Id, whiskeyReview);
        //     if (success)
        //         return Ok();
        //     else
        //         return StatusCode(StatusCodes.Status500InternalServerError);
        // }

        // [HttpDelete]
        // [Route("whiskeys/{whiskeyId}/reviews/{id}")]
        // public async Task<IActionResult> DeleteWhiskeyReview(string whiskeyId, string id)
        // {
        //     var success = await _database.DeleteWhiskeyReview(whiskeyId, id);
        //     if (success)
        //         return Ok();
        //     else
        //         return StatusCode(StatusCodes.Status500InternalServerError);
        // }
    }
}