using FS.DAL.DataContext;
using FS.DAL.Entities;
using FS.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using FS.DAL.Constants;

namespace FS.DAL.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private DatabaseContext _context;

        public FilmRepository(DatabaseContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<FilmEntity> AddFilm(FilmEntity newFilm)
        {
            await _context.Films.AddAsync(newFilm);
            await _context.SaveChangesAsync();
            return newFilm;
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
            _context.Entry(film).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return film;
        }
    }
}
