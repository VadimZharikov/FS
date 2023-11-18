using FS.DAL.Entities;

namespace FS.DAL.Interfaces
{
    public interface IReviewRepository
    {
        Task<ReviewEntity> AddReview(ReviewEntity review);
        Task<List<ReviewEntity>> GetReviews();
        Task<ReviewEntity> GetReview(int id);
        Task<ReviewEntity> UpdateReview(ReviewEntity review);
        Task<ReviewEntity> DeleteReview(int id);
        Task<bool> ReviewExists(int id);
    }
}
