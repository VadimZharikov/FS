using FS.BLL.Entities;

namespace FS.BLL.Interfaces
{
    public interface IReviewService
    {
        public Task<bool> AddReview(Review review);
        public Task<List<Review>> GetReviews();
        public Task<Review> GetReview(int id);
        public Task<bool> PutReview(int id, Review review);
        public Task<bool> DeleteReview(int id);
    }
}
