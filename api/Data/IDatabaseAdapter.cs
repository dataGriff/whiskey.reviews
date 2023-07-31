using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Data
{
    public interface IDatabaseAdapter
    {

        Task<bool> CreateWhiskeyReview(WhiskeyReview whiskeyReview);

        Task<List<WhiskeyReview>> GetUserReviews(string userId);

        Task<WhiskeyReview> GetUserReview(string userId, string whiskey);

        Task<bool> UpdateWhiskeyReview(WhiskeyReview whiskeyReview);

        Task<bool> DeleteReview(string userId, string whiskey) ;
    }
}
