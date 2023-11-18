using FS.DAL.Entities;

namespace FS.DAL.Interfaces
{
    public interface IActorRepository
    {
        Task<ActorEntity> AddActor(ActorEntity actor);
        Task<List<ActorEntity>> GetActors();
        Task<ActorEntity> GetActor(int id);
        Task<ActorEntity> UpdateActor(ActorEntity actor);
        Task<ActorEntity> DeleteActor(int id);
        Task<bool> ActorExists(int id);
    }
}
