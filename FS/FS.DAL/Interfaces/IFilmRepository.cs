using FS.DAL.Entities;

namespace FS.DAL.Interfaces
{
    public interface IFilmRepository
    {
        Task<FilmEntity> AddFilm(FilmEntity film);
        Task<List<FilmEntity>> GetFilms();
        Task<FilmEntity> GetFilm(int id);
        Task<FilmEntity> UpdateFilm(FilmEntity film);
        Task<FilmEntity> DeleteFilm(int id);
        Task<bool> FilmExists(int id);
    }
}
