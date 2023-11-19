using FS.BLL.Entities;

namespace FS.BLL.Interfaces
{
    public interface IFilmService
    {
        public Task<bool> AddFilm(Film film);
        public Task<List<Film>> GetFilms();
        public Task<Film> GetFilm(int id);
        public Task<bool> PutFilm(int id, Film film);
        public Task<bool> DeleteFilm(int id);
    }
}
