using FS.DAL.Entities;
using FS.DAL.Interfaces;

namespace FS.BLL.Utilities
{
    public static class Helper
    {
        /// <summary>
        /// Updates film rating based on film reviews when reviews table is updated.
        /// Call after review table update.
        /// </summary>
        /// <param name="filmRepo"></param>
        /// <param name="filmId">Current film id</param>
        /// <returns>1 if success, 0 if failed</returns>
        public async static Task<bool> TryUpdateFilmStars(this IFilmRepository filmRepo, int filmId)
        {
            if (filmId > 0)
            {
                var film = await filmRepo.GetFilm(filmId);
                var reviewCount = film.Reviews.Count;

                if (reviewCount <= 0) return false;

                var reviewsStars = film.Reviews.Select(x => x.Stars);
                float stars = (float)reviewsStars.Sum() / reviewCount;
                film.Stars = (float)Math.Round(stars,2);
                var result = await filmRepo.UpdateFilm(film);

                if (result.FilmId > 0) return true;
            }
            return false;
        }
    }
}
