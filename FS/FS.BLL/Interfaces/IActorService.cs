using FS.BLL.Entities;

namespace FS.BLL.Interfaces
{
    public interface IActorService
    {
        public Task<bool> AddActor(Actor actor);
        public Task<List<Actor>> GetActors();
        public Task<Actor> GetActor(int id);
        public Task<bool> PutActor(int id, Actor actor);
        public Task<bool> DeleteActor(int id);
    }
}
