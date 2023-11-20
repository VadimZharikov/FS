using FS.DAL.DataContext;
using FS.DAL.Entities;
using FS.DAL.Repositories;

namespace FS.DAL.UnitTests.IntegrationTests.Repositories.Film
{
    public class FilmRepositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private FilmRepository? _repository;

        public FilmRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "FSDatabase" + DateTime.Now.ToFileTimeUtc())
                .Options;
        }

        [Fact]
        public async Task GetFilm_ValidId_ReturnsFilmEntity()
        {
            var model = new FilmEntity
            {
                Title = "asd",
                Actors = new List<ActorEntity>
                {
                    new()
                    {
                        ActorName = "asd",
                    }
                },
                Reviews = new List<ReviewEntity>
                {
                    new()
                    {
                        Title = "asd"
                    }
                }
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);
            var entity = await context.Films.AddAsync(model);
            await context.SaveChangesAsync();

            var film = await _repository.GetFilm(entity.Entity.FilmId);

            film.ShouldNotBeNull();
            film.ShouldBeEquivalentTo(entity.Entity);
        }

        [Fact]
        public async Task FilmExists_ValidId_ReturnsTrue()
        {
            var model = new FilmEntity
            {
                Title = "asd",
                Actors = new List<ActorEntity>
                {
                    new()
                    {
                        ActorName = "asd",
                    }
                },
                Reviews = new List<ReviewEntity>
                {
                    new()
                    {
                        Title = "asd"
                    }
                }
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);
            var entity = await context.Films.AddAsync(model);
            await context.SaveChangesAsync();

            var film = await _repository.FilmExists(entity.Entity.FilmId);

            film.ShouldBeTrue();
        }
        [Fact]
        public async Task GetFilms_ValidModels_ReturnsFilmEntityList()
        {
            await using DatabaseContext context = new(_options);
            _repository = new(context);
            context.Films.Add(new FilmEntity
            {
                Title = "asd"
            });

            context.Films.Add(new FilmEntity
            {
                Title = "dsa"
            });

            await context.SaveChangesAsync();

            var film = await _repository.GetFilms();

            film.ShouldNotBeNull();
        }

        [Fact]
        public async Task AddFilm_ValidModel_ReturnsFilmEntity()
        {
            var model = new FilmEntity()
            {
                Title = "asd"
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);
            var film = await _repository.AddFilm(model);

            film.ShouldNotBeNull();
        }

        [Fact]
        public async Task UpdateFilm_ValidModel_ReturnsFilmEntity()
        {
            var addedModel = new FilmEntity
            {
                Title = "asd"
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);
            var entity = await context.Films.AddAsync(addedModel);
            await context.SaveChangesAsync();

            var film = await _repository.UpdateFilm(entity.Entity);

            film.ShouldNotBeNull();
            film.ShouldBeEquivalentTo(entity.Entity);
        }

        [Fact]
        public async Task DeleteFilm_ValidId_NotThrow()
        {
            await using DatabaseContext context = new(_options);
            _repository = new(context);

            context.Films.Add(new FilmEntity
            {
                Title = "asd"
            });
            await context.SaveChangesAsync();

            await _repository.DeleteFilm(1).ShouldNotThrowAsync();
        }

        [Fact]
        public async Task GetFilm_InvalidId_ThrowsKeyNotFoundException()
        {
            var model = new FilmEntity
            {
                Title = "asd"
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);

            context.Films.Add(model);
            await context.SaveChangesAsync();

            await _repository.GetFilm(-8).ShouldThrowAsync(typeof(KeyNotFoundException));
        }

        [Fact]
        public async Task FilmExists_InvalidId_ReturnsFalse()
        {
            var model = new FilmEntity
            {
                Title = "asd",
                Actors = new List<ActorEntity>
                {
                    new()
                    {
                        ActorName = "asd",
                    }
                },
                Reviews = new List<ReviewEntity>
                {
                    new()
                    {
                        Title = "asd"
                    }
                }
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);
            var entity = await context.Films.AddAsync(model);
            await context.SaveChangesAsync();

            var film = await _repository.FilmExists(entity.Entity.FilmId-1);

            film.ShouldBeFalse();
        }

        [Fact]
        public async Task GetFilms_NoModels_ReturnsEmptyFilmEntityList()
        {
            await using DatabaseContext context = new(_options);
            _repository = new(context);

            await context.Database.EnsureDeletedAsync();
            var film = await _repository.GetFilms();

            film.ShouldBeEmpty();
        }

        [Fact]
        public async Task AddFilm_InvalidModel_ThrowArgumentNullException()
        {
            await using DatabaseContext context = new(_options);
            _repository = new(context);
            await _repository.AddFilm(null!).ShouldThrowAsync(typeof(ArgumentNullException));
        }

        [Fact]
        public async Task UpdateFilm_InvalidModel_ThrowDbUpdateConcurrencyException()
        {
            var expectedModel = new FilmEntity
            {
                Title = "Name"
            };

            var addedModel = new FilmEntity();

            await using DatabaseContext context = new(_options);
            _repository = new(context);

            await context.Films.AddAsync(addedModel);
            await context.SaveChangesAsync();

            await _repository.UpdateFilm(expectedModel).ShouldThrowAsync(typeof(DbUpdateConcurrencyException));
        }

        [Fact]
        public async Task DeleteFilm_InvalidId_ThrowKeyNotFoundException()
        {
            await using DatabaseContext context = new(_options);
            _repository = new(context);
            await _repository.DeleteFilm(-123).ShouldThrowAsync(typeof(KeyNotFoundException));
        }
    }
}
