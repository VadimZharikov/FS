using FS.DAL.DataContext;
using FS.DAL.Entities;
using FS.DAL.Repositories;

namespace FS.DAL.UnitTests.IntegrationTests.Repositories.Review
{
    public class ReviewRepositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private ReviewRepository? _repository;

        public ReviewRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "FSDatabase" + DateTime.Now.ToFileTimeUtc())
                .Options;
        }

        [Fact]
        public async Task GetReview_ValidId_ReturnsReviewEntity()
        {
            var model = new ReviewEntity
            {
                Title = "asd",
                Description = "Description",
                Stars = 5,
                FilmId = 1,
                Film = new() { Title = "Title"}
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);
            var entity = await context.Reviews.AddAsync(model);
            await context.SaveChangesAsync();

            var review = await _repository.GetReview(entity.Entity.ReviewId);

            review.ShouldNotBeNull();
            review.ShouldBeEquivalentTo(entity.Entity);
        }

        [Fact]
        public async Task ReviewExists_ValidId_ReturnsTrue()
        {
            var model = new ReviewEntity
            {
                Title = "asd"
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);
            var entity = await context.Reviews.AddAsync(model);
            await context.SaveChangesAsync();

            var review = await _repository.ReviewExists(entity.Entity.ReviewId);

            review.ShouldBeTrue();
        }

        [Fact]
        public async Task GetReviews_ValidModels_ReturnsReviewEntityList()
        {
            await using DatabaseContext context = new(_options);
            _repository = new(context);
            context.Reviews.Add(new ReviewEntity
            {
                Title = "asd"
            });

            context.Reviews.Add(new ReviewEntity
            {
                Title = "dsa"
            });

            await context.SaveChangesAsync();

            var review = await _repository.GetReviews();

            review.ShouldNotBeNull();
        }

        [Fact]
        public async Task AddReview_ValidModel_ReturnsReviewEntity()
        {
            var model = new ReviewEntity()
            {
                Title = "asd"
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);
            var review = await _repository.AddReview(model);

            review.ShouldNotBeNull();
        }

        [Fact]
        public async Task UpdateReview_ValidModel_ReturnsReviewEntity()
        {
            var addedModel = new ReviewEntity
            {
                Title = "asd"
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);
            var entity = await context.Reviews.AddAsync(addedModel);
            await context.SaveChangesAsync();

            var review = await _repository.UpdateReview(entity.Entity);

            review.ShouldNotBeNull();
            review.ShouldBeEquivalentTo(entity.Entity);
        }

        [Fact]
        public async Task DeleteReview_ValidId_NotThrow()
        {
            await using DatabaseContext context = new(_options);
            _repository = new(context);

            context.Reviews.Add(new ReviewEntity
            {
                Title = "asd"
            });
            await context.SaveChangesAsync();

            await _repository.DeleteReview(1).ShouldNotThrowAsync();
        }

        [Fact]
        public async Task GetReview_InvalidId_ThrowsKeyNotFoundException()
        {
            var model = new ReviewEntity
            {
                Title = "asd"
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);

            context.Reviews.Add(model);
            await context.SaveChangesAsync();

            await _repository.GetReview(-8).ShouldThrowAsync(typeof(KeyNotFoundException));
        }

        [Fact]
        public async Task ReviewExists_InvalidId_ReturnsFalse()
        {
            var model = new ReviewEntity
            {
                Title = "asd"
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);
            var entity = await context.Reviews.AddAsync(model);
            await context.SaveChangesAsync();

            var review = await _repository.ReviewExists(entity.Entity.ReviewId - 1);

            review.ShouldBeFalse();
        }

        [Fact]
        public async Task GetReviews_NoModels_ReturnsEmptyReviewEntityList()
        {
            await using DatabaseContext context = new(_options);
            _repository = new(context);

            await context.Database.EnsureDeletedAsync();
            var review = await _repository.GetReviews();

            review.ShouldBeEmpty();
        }

        [Fact]
        public async Task AddReview_InvalidModel_ThrowArgumentNullException()
        {
            await using DatabaseContext context = new(_options);
            _repository = new(context);

            await _repository.AddReview(null!).ShouldThrowAsync(typeof(ArgumentNullException));
        }

        [Fact]
        public async Task UpdateReview_InvalidModel_ThrowDbUpdateConcurrencyException()
        {
            var expectedModel = new ReviewEntity
            {
                Title = "Name"
            };

            var addedModel = new ReviewEntity();

            await using DatabaseContext context = new(_options);
            _repository = new(context);

            await context.Reviews.AddAsync(addedModel);
            await context.SaveChangesAsync();

            await _repository.UpdateReview(expectedModel).ShouldThrowAsync(typeof(DbUpdateConcurrencyException));
        }

        [Fact]
        public async Task DeleteReview_InvalidId_ThrowKeyNotFoundException()
        {
            await using DatabaseContext context = new(_options);
            _repository = new(context);
            await _repository.DeleteReview(-123).ShouldThrowAsync(typeof(KeyNotFoundException));
        }
    }
}
