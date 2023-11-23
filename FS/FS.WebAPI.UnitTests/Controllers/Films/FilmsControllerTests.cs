using FS.BLL.Entities;
using FS.BLL.Interfaces;
using FS.WebAPI.Controllers;
using FS.WebAPI.Models.Film;

namespace FS.WebAPI.UnitTests.Controllers.Films
{
    public class FilmsControllerTests
    {
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IFilmService> _serviceMock = new();

        [Fact]
        public async Task Post_HasData_ReturnsTrue()
        {
            var filmId = new Random().Next();
            var filmViewModel = new FilmAddUpdateViewModel()
            {
                FilmId = filmId,
                Title = "Boba"
            };
            var inputFilmModel = new Film
            {
                FilmId = filmId,
                Title = filmViewModel.Title
            };

            _mapperMock.Setup(map => map.Map<FilmAddUpdateViewModel, Film>(filmViewModel)).Returns(inputFilmModel);
            _mapperMock.Setup(map => map.Map<Film, FilmAddUpdateViewModel>(inputFilmModel)).Returns(filmViewModel);
            _serviceMock.Setup(serv => serv.AddFilm(_mapperMock.Object.Map<FilmAddUpdateViewModel, Film>(filmViewModel)))
                .ReturnsAsync(true);

            var controller = new FilmsController(_serviceMock.Object, _mapperMock.Object);

            var result = await controller.Post(filmViewModel);

            result.ShouldBeTrue();
        }

        [Fact]
        public async Task GetFilm_ValidId_ReturnsValidModel()
        {
            var FilmId = new Random().Next();
            var FilmViewModel = new FilmFullViewModel()
            {
                FilmId = FilmId,
                Title = "Boba"
            };
            var FilmModel = new Film
            {
                FilmId = FilmId,
                Title = FilmViewModel.Title
            };

            _mapperMock.Setup(map => map.Map<Film, FilmFullViewModel>(FilmModel)).Returns(FilmViewModel);
            _serviceMock.Setup(serv => serv.GetFilm(FilmId))
                .ReturnsAsync(FilmModel);

            var controller = new FilmsController(_serviceMock.Object, _mapperMock.Object);

            var result = await controller.GetFilm(FilmId);

            result.ShouldBeEquivalentTo(FilmViewModel);
        }
        [Fact]
        public async Task GetFilms_HasData_ReturnsValidList()
        {
            var FilmId = new Random().Next();
            var FilmViewModel = new FilmViewModel()
            {
                FilmId = FilmId,
                Title = "Boba"
            };
            var FilmModel = new Film()
            {
                FilmId = FilmId,
                Title = "Boba"
            };
            var VMList = new List<FilmViewModel>() { FilmViewModel };
            var objList = new List<Film>() { FilmModel };

            _mapperMock.Setup(map => map.Map<List<Film>, List<FilmViewModel>>(objList)).Returns(VMList);
            _serviceMock.Setup(serv => serv.GetFilms())
                .ReturnsAsync(objList);

            var controller = new FilmsController(_serviceMock.Object, _mapperMock.Object);

            var result = await controller.GetFilms();

            result.ShouldBeEquivalentTo(VMList);
        }
        [Fact]
        public async Task Put_ValidId_ReturnsTrue()
        {
            var FilmId = new Random().Next();
            var FilmViewModel = new FilmAddUpdateViewModel()
            {
                FilmId = FilmId,
                Title = "Boba"
            };
            var FilmModel = new Film
            {
                FilmId = FilmId,
                Title = FilmViewModel.Title
            };

            _mapperMock.Setup(map => map.Map<Film, FilmAddUpdateViewModel>(FilmModel)).Returns(FilmViewModel);
            _mapperMock.Setup(map => map.Map<FilmAddUpdateViewModel, Film>(FilmViewModel)).Returns(FilmModel);
            _serviceMock.Setup(serv => serv.PutFilm(FilmId, FilmModel))
                .ReturnsAsync(true);

            var controller = new FilmsController(_serviceMock.Object, _mapperMock.Object);

            var result = await controller.Put(FilmId, FilmViewModel);

            result.ShouldBeTrue();
        }
        [Fact]
        public async Task Delete_ValidId_ReturnsTrue()
        {
            var FilmId = new Random().Next();
            var FilmViewModel = new FilmViewModel()
            {
                FilmId = FilmId,
                Title = "Boba"
            };
            var FilmModel = new Film
            {
                FilmId = FilmId,
                Title = FilmViewModel.Title
            };

            _serviceMock.Setup(serv => serv.DeleteFilm(FilmId))
                .ReturnsAsync(true);

            var controller = new FilmsController(_serviceMock.Object, _mapperMock.Object);

            var result = await controller.Delete(FilmId);

            result.ShouldBeTrue();
        }
    }
}
