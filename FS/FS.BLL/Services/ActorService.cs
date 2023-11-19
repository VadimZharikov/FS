using AutoMapper;
using FS.BLL.Entities;
using FS.BLL.Interfaces;
using FS.DAL.Entities;
using FS.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace FS.BLL.Services
{
    public class ActorService : IActorService
    {
        private IActorRepository actorRepo;
        private IMapper _mapper;
        private ILogger<ActorService> _logger;

        public ActorService(IActorRepository _actor, IMapper mapper, ILogger<ActorService> logger)
        {
            this.actorRepo = _actor;
            this._mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddActor(Actor actor)
        {
            var result = await this.actorRepo.AddActor(_mapper.Map<Actor, ActorEntity>(actor));
            if (result.ActorId > 0)
            {
                _logger.LogInformation($"Actor {result.ActorId} - {result.ActorName} was added");
                return true;
            }
            return false;
        }

        public async Task<List<Actor>> GetActors()
        {
            List<Actor> actors = _mapper.Map<List<ActorEntity>, List<Actor>>(await actorRepo.GetActors());
            return actors;
        }

        public async Task<Actor> GetActor(int id)
        {
            Actor newActor = _mapper.Map<ActorEntity, Actor>(await actorRepo.GetActor(id));
            return newActor;
        }

        public async Task<bool> PutActor(int id, Actor actor)
        {
            if (id != actor.ActorId)
            {
                return false;
            }

            var result = await this.actorRepo.UpdateActor(_mapper.Map<Actor, ActorEntity>(actor));
            if (result.ActorId > 0)
            {
                _logger.LogInformation($"Actor {result.ActorId} - {result.ActorName} was updated");
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteActor(int id)
        {
            var result = await actorRepo.DeleteActor(id);
            if (result.ActorId > 0)
            {
                _logger.LogInformation($"Actor {result.ActorId} - {result.ActorName} was deleted");
                return true;
            }
            return false;
        }
    }
}
