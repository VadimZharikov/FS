using FS.DAL.Constants;
using FS.DAL.DataContext;
using FS.DAL.Entities;
using FS.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FS.DAL.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private DatabaseContext _context;

        public ActorRepository(DatabaseContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<ActorEntity> AddActor(ActorEntity actor)
        {
            if (actor != null)
            {
                if (!actor.Films.IsNullOrEmpty())
                {
                    _context.Films.AttachRange(actor.Films);
                }
                await _context.Actors.AddAsync(actor);
                await _context.SaveChangesAsync();
                return actor;
            }
            throw new ArgumentNullException();
        }

        public async Task<ActorEntity> GetActor(int id)
        {
            var actor = await _context.Actors
            .Include(x => x.Films)
            .FirstOrDefaultAsync(x => x.ActorId == id);
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
            if (await ActorExists(actor.ActorId))
            {
                var dbActor = _context.Actors
                .Include(f => f.Films)
                .First(f => f.ActorId == actor.ActorId);
                //remove unused films
                dbActor.Films
                    .RemoveAll(a => !actor.Films
                        .Exists(x => x.FilmId == a.FilmId));
                //store new films
                actor.Films.RemoveAll(a => dbActor.Films
                                .Exists(x => x.FilmId == a.FilmId));

                dbActor.Films.AddRange(actor.Films);
                await _context.SaveChangesAsync();
                return actor;
            }
            throw new DbUpdateConcurrencyException();
        }
    }
}
