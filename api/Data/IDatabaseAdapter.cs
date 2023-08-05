using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Data
{
    public interface IDatabaseAdapter
    {
        Task<bool> CreateWhiskeyReview(WhiskeyReview whiskeyReview);

        Task<List<WhiskeyReview>> GetWhiskeyReviews(string whiskeyId);

        Task<WhiskeyReview> GetWhiskeyReview(string whiskeyId, string id);

        Task<bool> UpdateWhiskeyReview(string whiskeyId, string id, WhiskeyReview whiskeyReview);

        Task<bool> DeleteWhiskeyReview(string whiskeyId, string id);
    }
}
