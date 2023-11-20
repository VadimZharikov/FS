using FS.DAL.Constants;
using FS.DAL.DataContext;
using FS.DAL.Entities;
using FS.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FS.DAL.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private DatabaseContext _context;

        public ReviewRepository(DatabaseContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<ReviewEntity> AddReview(ReviewEntity newReview)
        {
            await _context.Reviews.AddAsync(newReview);
            await _context.SaveChangesAsync();
            return newReview;
        }

        public async Task<ReviewEntity> GetReview(int id)
        {
            var review = await _context.Reviews
            .Include(x => x.Film)
            .FirstOrDefaultAsync(x => x.ReviewId == id);
            if (review != null)
            {
                return review;
            }
            throw new KeyNotFoundException(ExceptionConstants.ReviewByIdNotFoundMsg);
        }

        public async Task<List<ReviewEntity>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<ReviewEntity> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
                return review;
            }
            throw new KeyNotFoundException(ExceptionConstants.ReviewByIdNotFoundMsg);
        }

        public async Task<bool> ReviewExists(int id)
        {
            return await _context.Reviews.AnyAsync(e => e.ReviewId == id);
        }

        public async Task<ReviewEntity> UpdateReview(ReviewEntity review)
        {
            _context.Entry(review).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return review;
        }
    }
}
