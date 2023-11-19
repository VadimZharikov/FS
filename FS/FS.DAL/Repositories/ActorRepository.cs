using FS.DAL.Constants;
using FS.DAL.DataContext;
using FS.DAL.Entities;
using FS.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FS.DAL.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private DatabaseContext _context;

        public ActorRepository(DatabaseContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<ActorEntity> AddActor(ActorEntity newActor)
        {
            await _context.Actors.AddAsync(newActor);
            await _context.SaveChangesAsync();
            return newActor;
        }

        public async Task<ActorEntity> GetActor(int id)
        {
            var actor = await _context.Actors
            .Include(x => x.Films)
            .FirstAsync(x => x.ActorId == id);
            if (actor != null)
            {
                return actor;
            }
            throw new KeyNotFoundException(ExceptionConstants.ActorByIdNotFoundMsg);
        }

        public async Task<List<ActorEntity>> GetActors()
        {
            return await _context.Actors.ToListAsync();
        }

        public async Task<ActorEntity> DeleteActor(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor != null)
            {
                _context.Actors.Remove(actor);
                await _context.SaveChangesAsync();
                return actor;
            }
            throw new KeyNotFoundException(ExceptionConstants.ActorByIdNotFoundMsg);
        }

        public async Task<bool> ActorExists(int id)
        {
            return await _context.Actors.AnyAsync(e => e.ActorId == id);
        }

        public async Task<ActorEntity> UpdateActor(ActorEntity actor)
        {
            _context.Entry(actor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return actor;
        }
    }
}
