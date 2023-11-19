using AutoMapper;
using FS.BLL.Entities;
using FS.BLL.Interfaces;
using FS.BLL.Utilities;
using FS.DAL.Entities;
using FS.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace FS.BLL.Services
{
    public class ReviewService : IReviewService
    {
        private IReviewRepository _reviewRepo;
        private IFilmRepository _filmRepo;
        private IMapper _mapper;
        private ILogger<ReviewService> _logger;

        public ReviewService(IReviewRepository reviewRepo, IFilmRepository filmRepo, IMapper mapper, ILogger<ReviewService> logger)
        {
            this._reviewRepo = reviewRepo;
            this._filmRepo = filmRepo;
            this._mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddReview(Review review)
        {
            var result = await this._reviewRepo.AddReview(_mapper.Map<Review, ReviewEntity>(review));
            if (result.ReviewId > 0) 
            {
                if (!await _filmRepo.TryUpdateFilmStars(result.FilmId))
                {
                    _logger.LogError($"Couldn't update Film star rating \"{result.Film.Title}\"");
                }
                _logger.LogInformation($"Review {result.ReviewId} - {result.Title} was added");
                return true;
            }
            return false;
        }

        public async Task<List<Review>> GetReviews()
        {
            List<Review> reviews = _mapper.Map<List<ReviewEntity>, List<Review>>(await _reviewRepo.GetReviews());
            return reviews;
        }

        public async Task<Review> GetReview(int id)
        {
            Review newReview = _mapper.Map<ReviewEntity, Review>(await _reviewRepo.GetReview(id));
            return newReview;
        }

        public async Task<bool> PutReview(int id, Review review)
        {
            if (id != review.ReviewId)
            {
                return false;
            }

            var result = await this._reviewRepo.UpdateReview(_mapper.Map<Review, ReviewEntity>(review));
            if (result.ReviewId > 0)
            {
                if (!(await _filmRepo.TryUpdateFilmStars(result.FilmId)))
                {
                    _logger.LogError($"Couldn't update Film star rating \"{result.Film.Title}\"");
                }
                _logger.LogInformation($"Review {result.ReviewId} - {result.Title} was updated");
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteReview(int id)
        {
            var result = await _reviewRepo.DeleteReview(id);
            if (result.ReviewId > 0)
            {
                if (!(await _filmRepo.TryUpdateFilmStars(result.FilmId)))
                {
                    _logger.LogError($"Couldn't update Film star rating \"{result.Film.Title}\"");
                }
                _logger.LogInformation($"Review {result.ReviewId} - {result.Title} was deleted");
                return true;
            }
            return false;
        }
    }
}
