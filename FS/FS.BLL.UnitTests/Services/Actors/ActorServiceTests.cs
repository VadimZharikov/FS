using AutoMapper;
using FS.BLL.Entities;
using FS.BLL.Services;
using FS.DAL.Entities;
using FS.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace FS.BLL.UnitTests.Services.Actors
{
    public class ActorServiceTests
    {
        private readonly Mock<IActorRepository> _repoMock = new Mock<IActorRepository>();
        private readonly Mock<ILogger<ActorService>> _loggerMock = new Mock<ILogger<ActorService>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly ActorService _service;

        public ActorServiceTests()
        {
            _service = new ActorService(_repoMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetActor_ValidId_ReturnsActorById()
        {
            var actorId = new Random().Next();
            var actorName = "Ok";
            var actEntity = new ActorEntity { ActorId = actorId, ActorName = actorName };
            var act = new Actor { ActorId = actorId, ActorName = actorName };

            _repoMock.Setup(x => x.GetActor(actorId)).ReturnsAsync(actEntity);
            _mapperMock.Setup(x => x.Map<ActorEntity, Actor>(actEntity))
                .Returns(act);

            var actor = await _service.GetActor(actorId);

            _repoMock.Verify(x => x.GetActor(actorId));
            actorId.ShouldBe(actor.ActorId);
            actorName.ShouldBe(actor.ActorName);
        }
        [Fact]
        public async Task GetActors_ReturnsActorsList()
        {
            _repoMock.Setup(x => x.GetActors()).ReturnsAsync(It.IsAny<List<ActorEntity>>());
            _mapperMock.Setup(x => x.Map<List<ActorEntity>, List<Actor>>(It.IsAny<List<ActorEntity>>()))
                .Returns(new List<Actor>());

            var actors = await _service.GetActors();

            _repoMock.Verify(x => x.GetActors());
            actors.ShouldBeOfType<List<Actor>>();
        }
        [Fact]
        public async Task AddActor_ValidActor_ReturnsTrue()
        {
            var actorId = new Random().Next();
            var actorName = "Ok";
            var actEntity = new ActorEntity { ActorId = actorId, ActorName = actorName };
            var act = new Actor { ActorId = actorId, ActorName = actorName };
            _repoMock.Setup(x => x.AddActor(actEntity)).ReturnsAsync(actEntity);
            _mapperMock.Setup(x => x.Map<Actor, ActorEntity>(act))
                .Returns(actEntity);

            var actor = await _service.AddActor(act);

            _repoMock.Verify(x => x.AddActor(actEntity));
            actor.ShouldBeTrue();
        }
        [Fact]
        public async Task AddActor_InValidActor_ReturnsFalse()
        {
            var actorId = 0;
            var actorName = "Ok";
            var actEntity = new ActorEntity { ActorId = actorId, ActorName = actorName };
            var act = new Actor { ActorId = actorId, ActorName = actorName };
            _repoMock.Setup(x => x.AddActor(actEntity)).ReturnsAsync(actEntity);
            _mapperMock.Setup(x => x.Map<Actor, ActorEntity>(act))
                .Returns(actEntity);

            var actor = await _service.AddActor(act);

            _repoMock.Verify(x => x.AddActor(actEntity));
            actor.ShouldBeFalse();
        }
        [Fact]
        public async Task PutActor_ValidActor_ReturnsTrue()
        {
            var actorId = new Random().Next();
            var actorName = "Ok";
            var actEntity = new ActorEntity { ActorId = actorId, ActorName = actorName };
            var act = new Actor { ActorId = actorId, ActorName = actorName };
            _repoMock.Setup(x => x.UpdateActor(actEntity)).ReturnsAsync(actEntity);
            _mapperMock.Setup(x => x.Map<Actor, ActorEntity>(act))
                .Returns(actEntity);

            var actor = await _service.PutActor(actorId, act);

            _repoMock.Verify(x => x.UpdateActor(actEntity));
            actor.ShouldBeTrue();
        }
        [Fact]
        public async Task PutActor_InValidActor_ReturnsFalse()
        {
            var actorId = 0;
            var actorName = "Ok";
            var actEntity = new ActorEntity { ActorId = actorId, ActorName = actorName };
            var act = new Actor { ActorId = actorId, ActorName = actorName };
            _repoMock.Setup(x => x.UpdateActor(actEntity)).ReturnsAsync(actEntity);
            _mapperMock.Setup(x => x.Map<Actor, ActorEntity>(act))
                .Returns(actEntity);

            var actor = await _service.PutActor(actorId, act);

            actor.ShouldBeFalse();
        }
        [Fact]
        public async Task PutActor_DifferentIdActor_ReturnsFalse()
        {
            var actorId = 1;
            var actorName = "Ok";
            var actEntity = new ActorEntity { ActorId = actorId, ActorName = actorName };
            var act = new Actor { ActorId = actorId + 1, ActorName = actorName };
            _repoMock.Setup(x => x.UpdateActor(actEntity)).ReturnsAsync(actEntity);
            _mapperMock.Setup(x => x.Map<Actor, ActorEntity>(act))
                .Returns(actEntity);

            var actor = await _service.PutActor(actorId, act);

            actor.ShouldBeFalse();
        }
        [Fact]
        public async Task DeleteActor_ValidActor_ReturnsTrue()
        {
            var actorId = new Random().Next();
            var actorName = "Ok";
            var actEntity = new ActorEntity { ActorId = actorId, ActorName = actorName };
            _repoMock.Setup(x => x.DeleteActor(actorId)).ReturnsAsync(actEntity);

            var actor = await _service.DeleteActor(actorId);

            _repoMock.Verify(x => x.DeleteActor(actorId));
            actor.ShouldBeTrue();
        }
        [Fact]
        public async Task DeleteActor_InValidActor_ReturnsFalse()
        {
            var actorId = 0;
            var actorName = "Ok";
            var actEntity = new ActorEntity { ActorId = actorId, ActorName = actorName };
            _repoMock.Setup(x => x.DeleteActor(actorId)).ReturnsAsync(actEntity);

            var actor = await _service.DeleteActor(actorId);

            _repoMock.Verify(x => x.DeleteActor(actorId));
            actor.ShouldBeFalse();
        }
    }
}
