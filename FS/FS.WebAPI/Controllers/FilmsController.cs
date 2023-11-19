using AutoMapper;
using FS.BLL.Entities;
using FS.BLL.Interfaces;
using FS.WebAPI.Models.Film;
using Microsoft.AspNetCore.Mvc;

namespace FS.WebAPI.Controllers
{
    [Route("api/Films")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private IFilmService filmService;
        private IMapper _mapper;
        public FilmsController(IFilmService filmService, IMapper mapper)
        {
            this.filmService = filmService;
            _mapper = mapper;
        }

        // GET: api/<FilmsController>/5
        [HttpGet("{id}")]
        public async Task<FilmFullViewModel> GetFilm(int id)
        {
            var film = _mapper.Map<Film, FilmFullViewModel>(await filmService.GetFilm(id));
            return film;
        }

        // GET api/<FilmsController>
        [HttpGet]
        public async Task<List<FilmViewModel>> GetFilms()
        {
            var films = await filmService.GetFilms();
            return _mapper.Map<List<Film>, List<FilmViewModel>>(films);
        }

        // POST api/<FilmsController>
        [HttpPost]
        public async Task<bool> Post(FilmViewModel film)
        {
            bool result = await filmService.AddFilm(_mapper.Map<FilmViewModel, Film>(film));
            return result;
        }

        // PUT api/<FilmsController>/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, FilmViewModel film)
        {
            bool result = await filmService.PutFilm(id, _mapper.Map<FilmViewModel, Film>(film));
            return result;
        }

        // DELETE api/<FilmsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            bool result = await filmService.DeleteFilm(id);
            return result;
        }
    }
}
