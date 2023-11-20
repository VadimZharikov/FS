using FS.BLL.Entities;
using FS.BLL.Services;
using FS.DAL.Entities;
using FS.DAL.Interfaces;

namespace FS.BLL.UnitTests.Services.Reviews
{
    public class ReviewServiceTests
    {
        private readonly Mock<IReviewRepository> _reviewRepoMock = new Mock<IReviewRepository>();
        private readonly Mock<IFilmRepository> _filmRepoMock = new Mock<IFilmRepository>();
        private readonly Mock<ILogger<ReviewService>> _loggerMock = new Mock<ILogger<ReviewService>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly ReviewService _service;

        public ReviewServiceTests()
        {
            _service = new ReviewService(_reviewRepoMock.Object, _filmRepoMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetReview_ValidId_ReturnsReviewById()
        {
            var reviewId = new Random().Next();
            var filmId = new Random().Next();
            var reviewName = "Ok";
            var stars = 5;
            var filmTest = new Film { FilmId = filmId };
            var filmTestEntity = new FilmEntity { FilmId = filmId };
            var reviewTestEntity = new ReviewEntity { 
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTestEntity,
                FilmId = filmId 
            };
            var reviewTest = new Review { 
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTest,
                FilmId = filmId
            };

            _reviewRepoMock.Setup(x => x.GetReview(reviewId)).ReturnsAsync(reviewTestEntity);
            _mapperMock.Setup(x => x.Map<ReviewEntity, Review>(reviewTestEntity))
                .Returns(reviewTest);

            var review = await _service.GetReview(reviewId);

            _reviewRepoMock.Verify(x => x.GetReview(reviewId));
            reviewId.ShouldBe(review.ReviewId);
            reviewName.ShouldBe(review.Title);
            stars.ShouldBe(review.Stars);
        }
        [Fact]
        public async Task GetReviews_ReturnsReviewsList()
        {
            _reviewRepoMock.Setup(x => x.GetReviews()).ReturnsAsync(It.IsAny<List<ReviewEntity>>());
            _mapperMock.Setup(x => x.Map<List<ReviewEntity>, List<Review>>(It.IsAny<List<ReviewEntity>>()))
                .Returns(new List<Review>());

            var reviews = await _service.GetReviews();

            _reviewRepoMock.Verify(x => x.GetReviews());
            reviews.ShouldBeOfType<List<Review>>();
        }
        [Fact]
        public async Task AddReview_ValidReview_ReturnsTrue()
        {
            var reviewId = new Random().Next();
            var filmId = new Random().Next();
            var reviewName = "Ok";
            var stars = 5;
            var filmTest = new Film { FilmId = filmId };
            var filmTestEntity = new FilmEntity { FilmId = filmId };
            var reviewTestEntity = new ReviewEntity
            {
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTestEntity,
                FilmId = filmId
            };
            var reviewTest = new Review
            {
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTest,
                FilmId = filmId
            };
            filmTestEntity.Reviews = new List<ReviewEntity>() { reviewTestEntity};

            _reviewRepoMock.Setup(x => x.AddReview(reviewTestEntity)).ReturnsAsync(reviewTestEntity);
            _filmRepoMock.Setup(x => x.UpdateFilm(filmTestEntity)).ReturnsAsync(filmTestEntity);
            _filmRepoMock.Setup(x => x.GetFilm(filmId)).ReturnsAsync(filmTestEntity);
            _mapperMock.Setup(x => x.Map<Review, ReviewEntity>(reviewTest))
                .Returns(reviewTestEntity);

            var review = await _service.AddReview(reviewTest);

            _reviewRepoMock.Verify(x => x.AddReview(reviewTestEntity));
            review.ShouldBeTrue();
        }
        [Fact]
        public async Task AddReview_InValidReview_ReturnsFalse()
        {
            var reviewId = 0;
            var filmId = new Random().Next();
            var reviewName = "Ok";
            var stars = 5;
            var filmTest = new Film { FilmId = filmId };
            var filmTestEntity = new FilmEntity { FilmId = filmId };
            var reviewTestEntity = new ReviewEntity
            {
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTestEntity,
                FilmId = filmId
            };
            var reviewTest = new Review
            {
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTest,
                FilmId = filmId
            };
            filmTestEntity.Reviews = new List<ReviewEntity>() { reviewTestEntity };

            _reviewRepoMock.Setup(x => x.AddReview(reviewTestEntity)).ReturnsAsync(reviewTestEntity);
            _filmRepoMock.Setup(x => x.UpdateFilm(filmTestEntity)).ReturnsAsync(filmTestEntity);
            _filmRepoMock.Setup(x => x.GetFilm(filmId)).ReturnsAsync(filmTestEntity);
            _mapperMock.Setup(x => x.Map<Review, ReviewEntity>(reviewTest))
                .Returns(reviewTestEntity);

            var review = await _service.AddReview(reviewTest);

            _reviewRepoMock.Verify(x => x.AddReview(reviewTestEntity));
            review.ShouldBeFalse();
        }
        [Fact]
        public async Task PutReview_ValidReview_ReturnsTrue()
        {
            var reviewId = new Random().Next();
            var filmId = new Random().Next();
            var reviewName = "Ok";
            var stars = 5;
            var filmTest = new Film { FilmId = filmId };
            var filmTestEntity = new FilmEntity { FilmId = filmId };
            var reviewTestEntity = new ReviewEntity
            {
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTestEntity,
                FilmId = filmId
            };
            var reviewTest = new Review
            {
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTest,
                FilmId = filmId
            };
            filmTestEntity.Reviews = new List<ReviewEntity>() { reviewTestEntity };

            _reviewRepoMock.Setup(x => x.UpdateReview(reviewTestEntity)).ReturnsAsync(reviewTestEntity);
            _filmRepoMock.Setup(x => x.UpdateFilm(filmTestEntity)).ReturnsAsync(filmTestEntity);
            _filmRepoMock.Setup(x => x.GetFilm(filmId)).ReturnsAsync(filmTestEntity);
            _mapperMock.Setup(x => x.Map<Review, ReviewEntity>(reviewTest))
                .Returns(reviewTestEntity);

            var review = await _service.PutReview(reviewId, reviewTest);

            _reviewRepoMock.Verify(x => x.UpdateReview(reviewTestEntity));
            review.ShouldBeTrue();
        }
        [Fact]
        public async Task PutReview_InValidReview_ReturnsFalse()
        {
            var reviewId = 0;
            var filmId = new Random().Next();
            var reviewName = "";
            var stars = -1;
            var filmTest = new Film { FilmId = filmId };
            var filmTestEntity = new FilmEntity { FilmId = filmId };
            var reviewTestEntity = new ReviewEntity
            {
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTestEntity,
                FilmId = filmId
            };
            var reviewTest = new Review
            {
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTest,
                FilmId = filmId
            };
            filmTestEntity.Reviews = new List<ReviewEntity>() { reviewTestEntity };

            _reviewRepoMock.Setup(x => x.UpdateReview(reviewTestEntity)).ReturnsAsync(reviewTestEntity);
            _filmRepoMock.Setup(x => x.UpdateFilm(filmTestEntity)).ReturnsAsync(filmTestEntity);
            _filmRepoMock.Setup(x => x.GetFilm(filmId)).ReturnsAsync(filmTestEntity);
            _mapperMock.Setup(x => x.Map<Review, ReviewEntity>(reviewTest))
                .Returns(reviewTestEntity);

            var review = await _service.PutReview(reviewId, reviewTest);

            review.ShouldBeFalse();
        }
        [Fact]
        public async Task PutReview_DifferentIdReview_ReturnsFalse()
        {
            var reviewId = new Random().Next();
            var filmId = new Random().Next();
            var reviewName = "Ok";
            var stars = 5;
            var filmTest = new Film { FilmId = filmId };
            var filmTestEntity = new FilmEntity { FilmId = filmId };
            var reviewTestEntity = new ReviewEntity
            {
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTestEntity,
                FilmId = filmId
            };
            var reviewTest = new Review
            {
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTest,
                FilmId = filmId
            };
            filmTestEntity.Reviews = new List<ReviewEntity>() { reviewTestEntity };

            _reviewRepoMock.Setup(x => x.UpdateReview(reviewTestEntity)).ReturnsAsync(reviewTestEntity);
            _filmRepoMock.Setup(x => x.UpdateFilm(filmTestEntity)).ReturnsAsync(filmTestEntity);
            _filmRepoMock.Setup(x => x.GetFilm(filmId)).ReturnsAsync(filmTestEntity);
            _mapperMock.Setup(x => x.Map<Review, ReviewEntity>(reviewTest))
                .Returns(reviewTestEntity);

            var review = await _service.PutReview(0, reviewTest);

            review.ShouldBeFalse();
        }
        [Fact]
        public async Task DeleteReview_ValidId_ReturnsTrue()
        {
            var reviewId = new Random().Next();
            var filmId = new Random().Next();
            var reviewName = "Ok";
            var stars = 5;
            var filmTest = new Film { FilmId = filmId };
            var filmTestEntity = new FilmEntity { FilmId = filmId };
            var reviewTestEntity = new ReviewEntity
            {
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTestEntity,
                FilmId = filmId
            };
            var reviewTest = new Review
            {
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTest,
                FilmId = filmId
            };
            filmTestEntity.Reviews = new List<ReviewEntity>() { reviewTestEntity };

            _reviewRepoMock.Setup(x => x.DeleteReview(reviewId)).ReturnsAsync(reviewTestEntity);
            _filmRepoMock.Setup(x => x.UpdateFilm(filmTestEntity)).ReturnsAsync(filmTestEntity);
            _filmRepoMock.Setup(x => x.GetFilm(filmId)).ReturnsAsync(filmTestEntity);
            _mapperMock.Setup(x => x.Map<Review, ReviewEntity>(reviewTest))
                .Returns(reviewTestEntity);

            var review = await _service.DeleteReview(reviewId);

            _reviewRepoMock.Verify(x => x.DeleteReview(reviewId));
            review.ShouldBeTrue();
        }
        [Fact]
        public async Task DeleteReview_InValidId_ReturnsFalse()
        {
            var reviewId = 0;
            var filmId = new Random().Next();
            var reviewName = "Ok";
            var stars = 5;
            var filmTest = new Film { FilmId = filmId };
            var filmTestEntity = new FilmEntity { FilmId = filmId };
            var reviewTestEntity = new ReviewEntity
            {
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTestEntity,
                FilmId = filmId
            };
            var reviewTest = new Review
            {
                ReviewId = reviewId,
                Title = reviewName,
                Stars = stars,
                Film = filmTest,
                FilmId = filmId
            };
            filmTestEntity.Reviews = new List<ReviewEntity>() { reviewTestEntity };

            _reviewRepoMock.Setup(x => x.DeleteReview(reviewId)).ReturnsAsync(reviewTestEntity);
            _filmRepoMock.Setup(x => x.UpdateFilm(filmTestEntity)).ReturnsAsync(filmTestEntity);
            _filmRepoMock.Setup(x => x.GetFilm(filmId)).ReturnsAsync(filmTestEntity);
            _mapperMock.Setup(x => x.Map<Review, ReviewEntity>(reviewTest))
                .Returns(reviewTestEntity);

            var review = await _service.DeleteReview(reviewId);

            _reviewRepoMock.Verify(x => x.DeleteReview(reviewId));
            review.ShouldBeFalse();
        }
    }
}
