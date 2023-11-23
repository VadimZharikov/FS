using FS.BLL.Entities;
using FS.BLL.Interfaces;
using FS.WebAPI.Controllers;
using FS.WebAPI.Models.Actor;
using Moq;

namespace FS.WebAPI.UnitTests.Controllers.Actors
{
    public class ActorsControllerTests
    {
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IActorService> _serviceMock = new();

        [Fact]
        public async Task Post_HasData_ReturnsTrue()
        {
            var ActorId = new Random().Next();
            var ActorViewModel = new ActorFullViewModel()
            {
                ActorId = ActorId,
                ActorName = "Boba"
            };
            var inputActorModel = new Actor
            {
                ActorId = ActorId,
                ActorName = ActorViewModel.ActorName
            };

            _mapperMock.Setup(map => map.Map<ActorFullViewModel, Actor>(ActorViewModel)).Returns(inputActorModel);
            _mapperMock.Setup(map => map.Map<Actor, ActorFullViewModel>(inputActorModel)).Returns(ActorViewModel);
            _serviceMock.Setup(serv => serv.AddActor(_mapperMock.Object.Map<ActorFullViewModel, Actor>(ActorViewModel)))
                .ReturnsAsync(true);

            var controller = new ActorsController(_serviceMock.Object, _mapperMock.Object);

            var result = await controller.Post(ActorViewModel);

            result.ShouldBeTrue();
        }

        [Fact]
        public async Task GetActor_ValidId_ReturnsValidModel()
        {
            var ActorId = new Random().Next();
            var ActorViewModel = new ActorFullViewModel()
            {
                ActorId = ActorId,
                ActorName = "Boba"
            };
            var ActorModel = new Actor
            {
                ActorId = ActorId,
                ActorName = ActorViewModel.ActorName
            };

            _mapperMock.Setup(map => map.Map<Actor, ActorFullViewModel>(ActorModel)).Returns(ActorViewModel);
            _serviceMock.Setup(serv => serv.GetActor(ActorId))
                .ReturnsAsync(ActorModel);

            var controller = new ActorsController(_serviceMock.Object, _mapperMock.Object);

            var result = await controller.GetActor(ActorId);

            result.ShouldBeEquivalentTo(ActorViewModel);
        }
        [Fact]
        public async Task GetActors_HasData_ReturnsValidList()
        {
            var ActorId = new Random().Next();
            var ActorViewModel = new ActorViewModel()
            {
                ActorId = ActorId,
                ActorName = "Boba"
            };
            var ActorModel = new Actor()
            {
                ActorId = ActorId,
                ActorName = "Boba"
            };
            var VMList = new List<ActorViewModel>() { ActorViewModel };
            var objList = new List<Actor>() { ActorModel };

            _mapperMock.Setup(map => map.Map<List<Actor>, List<ActorViewModel>>(objList)).Returns(VMList);
            _serviceMock.Setup(serv => serv.GetActors())
                .ReturnsAsync(objList);

            var controller = new ActorsController(_serviceMock.Object, _mapperMock.Object);

            var result = await controller.GetActors();

            result.ShouldBeEquivalentTo(VMList);
        }
        [Fact]
        public async Task Put_ValidId_ReturnsTrue()
        {
            var ActorId = new Random().Next();
            var ActorViewModel = new ActorFullViewModel()
            {
                ActorId = ActorId,
                ActorName = "Boba"
            };
            var ActorModel = new Actor
            {
                ActorId = ActorId,
                ActorName = ActorViewModel.ActorName
            };

            _mapperMock.Setup(map => map.Map<Actor, ActorFullViewModel>(ActorModel)).Returns(ActorViewModel);
            _mapperMock.Setup(map => map.Map<ActorFullViewModel, Actor>(ActorViewModel)).Returns(ActorModel);
            _serviceMock.Setup(serv => serv.PutActor(ActorId, ActorModel))
                .ReturnsAsync(true);

            var controller = new ActorsController(_serviceMock.Object, _mapperMock.Object);

            var result = await controller.Put(ActorId, ActorViewModel);

            result.ShouldBeTrue();
        }
        [Fact]
        public async Task Delete_ValidId_ReturnsTrue()
        {
            var ActorId = new Random().Next();
            var ActorViewModel = new ActorViewModel()
            {
                ActorId = ActorId,
                ActorName = "Boba"
            };
            var ActorModel = new Actor
            {
                ActorId = ActorId,
                ActorName = ActorViewModel.ActorName
            };

            _serviceMock.Setup(serv => serv.DeleteActor(ActorId))
                .ReturnsAsync(true);

            var controller = new ActorsController(_serviceMock.Object, _mapperMock.Object);

            var result = await controller.Delete(ActorId);

            result.ShouldBeTrue();
        }
    }
}
