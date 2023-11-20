using FS.DAL.DataContext;
using FS.DAL.Entities;
using FS.DAL.Repositories;

namespace FS.DAL.UnitTests.IntegrationTests.Repositories.Actor
{
    public class ActorRepositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private ActorRepository _repository;

        public ActorRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "FSDatabase" + DateTime.Now.ToFileTimeUtc())
                .Options;
        }

        [Fact]
        public async Task GetActor_ValidId_ReturnsActorEntity()
        {
            var model = new ActorEntity
            {
                ActorName = "asd",
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);
            var entity = await context.Actors.AddAsync(model);
            await context.SaveChangesAsync();

            var actor = await _repository.GetActor(entity.Entity.ActorId);

            actor.ShouldNotBeNull();
            actor.ShouldBeEquivalentTo(entity.Entity);
        }

        [Fact]
        public async Task ActorExists_ValidId_ReturnsTrue()
        {
            var model = new ActorEntity
            {
                ActorName = "asd"                
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);
            var entity = await context.Actors.AddAsync(model);
            await context.SaveChangesAsync();

            var actor = await _repository.ActorExists(entity.Entity.ActorId);

            actor.ShouldBeTrue();
        }

        [Fact]
        public async Task GetActors_ValidModels_ReturnsActorEntityList()
        {
            await using DatabaseContext context = new(_options);
            _repository = new(context);
            context.Actors.Add(new ActorEntity
            {
                ActorName = "asd"
            });

            context.Actors.Add(new ActorEntity
            {
                ActorName = "dsa"
            });

            await context.SaveChangesAsync();

            var actor = await _repository.GetActors();

            actor.ShouldNotBeNull();
        }

        [Fact]
        public async Task AddActor_ValidModel_ReturnsActorEntity()
        {
            var model = new ActorEntity()
            {
                ActorName = "asd"
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);
            var actor = await _repository.AddActor(model);

            actor.ShouldNotBeNull();
        }

        [Fact]
        public async Task UpdateActor_ValidModel_ReturnsActorEntity()
        {
            var addedModel = new ActorEntity
            {
                ActorName = "asd"
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);
            var entity = await context.Actors.AddAsync(addedModel);
            await context.SaveChangesAsync();

            var actor = await _repository.UpdateActor(entity.Entity);

            actor.ShouldNotBeNull();
            actor.ShouldBeEquivalentTo(entity.Entity);
        }

        [Fact]
        public async Task DeleteActor_ValidId_NotThrow()
        {
            await using DatabaseContext context = new(_options);
            _repository = new(context);

            context.Actors.Add(new ActorEntity
            {
                ActorName = "asd"
            });
            await context.SaveChangesAsync();

            await _repository.DeleteActor(1).ShouldNotThrowAsync();
        }

        [Fact]
        public async Task GetActor_InvalidId_ThrowsKeyNotFoundException()
        {
            var model = new ActorEntity
            {
                ActorName = "asd"
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);

            context.Actors.Add(model);
            await context.SaveChangesAsync();

            await _repository.GetActor(-8).ShouldThrowAsync(typeof(KeyNotFoundException));
        }

        [Fact]
        public async Task ActorExists_InvalidId_ReturnsFalse()
        {
            var model = new ActorEntity
            {
                ActorName = "asd"
            };

            await using DatabaseContext context = new(_options);
            _repository = new(context);
            var entity = await context.Actors.AddAsync(model);
            await context.SaveChangesAsync();

            var actor = await _repository.ActorExists(entity.Entity.ActorId-1);

            actor.ShouldBeFalse();
        }

        [Fact]
        public async Task GetActors_NoModels_ReturnsEmptyActorEntityList()
        {
            await using DatabaseContext context = new(_options);
            _repository = new(context);

            await context.Database.EnsureDeletedAsync();
            var actor = await _repository.GetActors();

            actor.ShouldBeEmpty();
        }

        [Fact]
        public async Task AddActor_InvalidModel_ThrowArgumentNullException()
        {
            await using DatabaseContext context = new(_options);
            _repository = new(context);

            await _repository.AddActor(null).ShouldThrowAsync(typeof(ArgumentNullException));
        }

        [Fact]
        public async Task UpdateActor_InvalidModel_ThrowDbUpdateConcurrencyException()
        {
            var expectedModel = new ActorEntity
            {
                ActorName = "Name"
            };

            var addedModel = new ActorEntity();

            await using DatabaseContext context = new(_options);
            _repository = new(context);

            await context.Actors.AddAsync(addedModel);
            await context.SaveChangesAsync();

            await _repository.UpdateActor(expectedModel).ShouldThrowAsync(typeof(DbUpdateConcurrencyException));
        }

        [Fact]
        public async Task DeleteActor_InvalidId_ThrowKeyNotFoundException()
        {
            await using DatabaseContext context = new(_options);
            _repository = new(context);
            await _repository.DeleteActor(-123).ShouldThrowAsync(typeof(KeyNotFoundException));
        }
    }
}
