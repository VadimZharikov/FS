﻿using AutoMapper;
using FS.BLL.Entities;
using FS.BLL.Interfaces;
using FS.BLL.Utilities;
using FS.DAL.Entities;
using FS.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace FS.BLL.Services
{
    public class FilmService : IFilmService
    {
        private IFilmRepository _filmRepo;
        private IMapper _mapper;
        private ILogger<FilmService> _logger;

        public FilmService(IFilmRepository film, IMapper mapper, ILogger<FilmService> logger)
        {
            this._filmRepo = film;
            this._mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddFilm(Film film)
        {
            var result = await this._filmRepo.AddFilm(_mapper.Map<Film, FilmEntity>(film));
            if (result.FilmId > 0)
            {
                _logger.LogInformation($"Film {result.FilmId} - {result.Title} was added");
                return true;
            }
            return false;
        }

        public async Task<List<Film>> GetFilms()
        {
            List<Film> films = _mapper.Map<List<FilmEntity>, List<Film>>(await _filmRepo.GetFilms());
            return films;
        }

        public async Task<Film> GetFilm(int id)
        {
            Film newFilm = _mapper.Map<FilmEntity, Film>(await _filmRepo.GetFilm(id));
            return newFilm;
        }

        public async Task<bool> PutFilm(int id, Film film)
        {
            if (id != film.FilmId)
            {
                return false;
            }
            var result = await this._filmRepo.UpdateFilm(_mapper.Map<Film, FilmEntity>(film));
            if (result.FilmId > 0)
            {
                if (!await _filmRepo.TryUpdateFilmStars(result.FilmId))
                {
                    _logger.LogError($"Couldn't update Film star rating \"{result.Title}\"");
                }
                _logger.LogInformation($"Film {result.FilmId} - {result.Title} was updated");
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteFilm(int id)
        {
            var result = await _filmRepo.DeleteFilm(id);
            if (result.FilmId > 0)
            {
                _logger.LogInformation($"Film {result.FilmId} - {result.Title} was deleted");
                return true;
            }
            return false;
        }
    }
}
