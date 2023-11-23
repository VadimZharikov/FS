using FS.DAL.DataContext;
using FS.DAL.Entities;
using FS.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using FS.DAL.Constants;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace FS.DAL.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private DatabaseContext _context;

        public FilmRepository(DatabaseContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<FilmEntity> AddFilm(FilmEntity film)
        {
            _context.Actors.AttachRange(film.Actors);
            await _context.Films.AddAsync(film);
            await _context.SaveChangesAsync();
            return film;
        }

        public async Task<FilmEntity> GetFilm(int id)
        {
            var film = await _context.Films
                .Include(x => x.Reviews)
                .Include(x => x.Actors)
                .FirstOrDefaultAsync(x => x.FilmId == id);
            if (film != null)
            {
                return film;
            }
            throw new KeyNotFoundException(ExceptionConstants.FilmByIdNotFoundMsg);
        }

        public async Task<List<FilmEntity>> GetFilms()
        {
            return await _context.Films.ToListAsync();
        }

        public async Task<FilmEntity> DeleteFilm(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film != null)
            {
                _context.Films.Remove(film);
                await _context.SaveChangesAsync();
                return film;
            }
            throw new KeyNotFoundException(ExceptionConstants.FilmByIdNotFoundMsg);
        }

        public async Task<bool> FilmExists(int id)
        {
            return await _context.Films.AnyAsync(e => e.FilmId == id);
        }

        public async Task<FilmEntity> UpdateFilm(FilmEntity film)
        {
            var dbFilm = _context.Films
                .Include(f => f.Actors)
                .First(f => f.FilmId == film.FilmId);
            //remove unused actors
            dbFilm.Actors
                .RemoveAll(a => !film.Actors
                    .Exists(x => x.ActorId == a.ActorId));
            //store new actors
            film.Actors.RemoveAll(a => dbFilm.Actors
                            .Exists(x => x.ActorId == a.ActorId));

            dbFilm.Actors.AddRange(film.Actors);
            await _context.SaveChangesAsync();
            return film;
        }
    }
}
