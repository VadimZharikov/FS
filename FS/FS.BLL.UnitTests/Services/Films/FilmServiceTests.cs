using AutoMapper;
using FS.BLL.Entities;
using FS.BLL.Services;
using FS.BLL.Utilities;
using FS.DAL.Entities;
using FS.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace FS.BLL.UnitTests.Services.Films
{
    public class FilmServiceTests
    {
        private readonly Mock<IFilmRepository> _repoMock = new Mock<IFilmRepository>();
        private readonly Mock<ILogger<FilmService>> _loggerMock = new Mock<ILogger<FilmService>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly FilmService _service;

        public FilmServiceTests()
        {
            _service = new FilmService(_repoMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetFilm_ValidId_ReturnsFilmById()
        {
            var filmId = new Random().Next();
            var filmName = "Ok";
            var stars = 5.0F;
            var filmTestEntity = new FilmEntity { FilmId = filmId, Title = filmName, Stars = stars };
            var filmTest = new Film { FilmId = filmId, Title = filmName, Stars = 5.0F };

            _repoMock.Setup(x => x.GetFilm(filmId)).ReturnsAsync(filmTestEntity);
            _mapperMock.Setup(x => x.Map<FilmEntity, Film>(filmTestEntity))
                .Returns(filmTest);

            var film = await _service.GetFilm(filmId);

            _repoMock.Verify(x => x.GetFilm(filmId));
            filmId.ShouldBe(film.FilmId);
            filmName.ShouldBe(film.Title);
        }
        [Fact]
        public async Task GetFilms_ReturnsFilmsList()
        {
            _repoMock.Setup(x => x.GetFilms()).ReturnsAsync(It.IsAny<List<FilmEntity>>());
            _mapperMock.Setup(x => x.Map<List<FilmEntity>, List<Film>>(It.IsAny<List<FilmEntity>>()))
                .Returns(new List<Film>());

            var films = await _service.GetFilms();

            _repoMock.Verify(x => x.GetFilms());
            films.ShouldBeOfType<List<Film>>();
        }
        [Fact]
        public async Task AddFilm_ValidFilm_ReturnsTrue()
        {
            var filmId = new Random().Next();
            var filmName = "Ok";
            var stars = 5.0F;
            var filmTestEntity = new FilmEntity { FilmId = filmId, Title = filmName, Stars = stars };
            var filmTest = new Film { FilmId = filmId, Title = filmName, Stars = stars };
            _repoMock.Setup(x => x.AddFilm(filmTestEntity)).ReturnsAsync(filmTestEntity);
            _mapperMock.Setup(x => x.Map<Film, FilmEntity>(filmTest))
                .Returns(filmTestEntity);

            var film = await _service.AddFilm(filmTest);

            _repoMock.Verify(x => x.AddFilm(filmTestEntity));
            film.ShouldBeTrue();
        }
        [Fact]
        public async Task AddFilm_InValidFilm_ReturnsFalse()
        {
            var filmId = 0;
            var filmName = "Ok";
            var stars = 5.0F;
            var filmTestEntity = new FilmEntity { FilmId = filmId, Title = filmName, Stars = stars };
            var filmTest = new Film { FilmId = filmId, Title = filmName, Stars = stars };
            _repoMock.Setup(x => x.AddFilm(filmTestEntity)).ReturnsAsync(filmTestEntity);
            _mapperMock.Setup(x => x.Map<Film, FilmEntity>(filmTest))
                .Returns(filmTestEntity);

            var film = await _service.AddFilm(filmTest);

            _repoMock.Verify(x => x.AddFilm(filmTestEntity));
            film.ShouldBeFalse();
        }
        [Fact]
        public async Task PutFilm_ValidFilm_ReturnsTrue()
        {
            var filmId = new Random().Next();
            var filmName = "Ok";
            var stars = 5.0F;
            var reviews_1 = new List<ReviewEntity>() { new() { FilmId = filmId } };
            var reviews_2 = new List<Review>() { new() { FilmId = filmId } };
            var filmTestEntity = new FilmEntity { FilmId = filmId, Title = filmName, Stars = stars, Reviews = reviews_1 };
            var filmTest = new Film { FilmId = filmId, Title = filmName, Stars = stars, Reviews = reviews_2 };
            _repoMock.Setup(x => x.UpdateFilm(filmTestEntity)).ReturnsAsync(filmTestEntity);
            _repoMock.Setup(x => x.GetFilm(filmId)).ReturnsAsync(filmTestEntity);
            _mapperMock.Setup(x => x.Map<Film, FilmEntity>(filmTest))
                .Returns(filmTestEntity);

            var film = await _service.PutFilm(filmId, filmTest);

            _repoMock.Verify(x => x.UpdateFilm(filmTestEntity));
            film.ShouldBeTrue();
        }
        [Fact]
        public async Task PutFilm_InValidFilm_ReturnsFalse()
        {
            var filmId = 0;
            var filmName = "Ok";
            var stars = 5.0F;
            var filmTestEntity = new FilmEntity { FilmId = filmId, Title = filmName, Stars = stars };
            var filmTest = new Film { FilmId = filmId, Title = filmName, Stars = stars };
            _repoMock.Setup(x => x.UpdateFilm(filmTestEntity)).ReturnsAsync(filmTestEntity);
            _mapperMock.Setup(x => x.Map<Film, FilmEntity>(filmTest))
                .Returns(filmTestEntity);

            var film = await _service.PutFilm(filmId, filmTest);

            film.ShouldBeFalse();
        }
        [Fact]
        public async Task PutFilm_DifferentIdFilm_ReturnsFalse()
        {
            var filmId = 1;
            var filmName = "Ok";
            var stars = 5.0F;
            var filmTestEntity = new FilmEntity { FilmId = filmId, Title = filmName, Stars = stars };
            var filmTest = new Film { FilmId = filmId + 1, Title = filmName, Stars = stars };
            _repoMock.Setup(x => x.UpdateFilm(filmTestEntity)).ReturnsAsync(filmTestEntity);
            _mapperMock.Setup(x => x.Map<Film, FilmEntity>(filmTest))
                .Returns(filmTestEntity);

            var film = await _service.PutFilm(filmId, filmTest);

            film.ShouldBeFalse();
        }
        [Fact]
        public async Task DeleteFilm_ValidFilm_ReturnsTrue()
        {
            var filmId = new Random().Next();
            var filmName = "Ok";
            var stars = 5.0F;
            var filmTestEntity = new FilmEntity { FilmId = filmId, Title = filmName, Stars = stars };
            _repoMock.Setup(x => x.DeleteFilm(filmId)).ReturnsAsync(filmTestEntity);

            var film = await _service.DeleteFilm(filmId);

            _repoMock.Verify(x => x.DeleteFilm(filmId));
            film.ShouldBeTrue();
        }
        [Fact]
        public async Task DeleteFilm_InValidFilm_ReturnsFalse()
        {
            var filmId = 0;
            var filmName = "Ok";
            var stars = 5.0F;
            var filmTestEntity = new FilmEntity { FilmId = filmId, Title = filmName, Stars = stars };
            _repoMock.Setup(x => x.DeleteFilm(filmId)).ReturnsAsync(filmTestEntity);

            var film = await _service.DeleteFilm(filmId);

            _repoMock.Verify(x => x.DeleteFilm(filmId));
            film.ShouldBeFalse();
        }
    }
}
