using AutoMapper;
using FS.BLL.Entities;
using FS.WebAPI.Models.Actor;
using FS.WebAPI.Models.Film;
using FS.WebAPI.Models.Review;

namespace FS.WebAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Film, FilmViewModel>();
            CreateMap<FilmViewModel, Film>();
            CreateMap<FilmFullViewModel, Film>();
            CreateMap<Film, FilmFullViewModel>();
            CreateMap<Film, FilmAddUpdateViewModel>();
            CreateMap<FilmAddUpdateViewModel, Film>();
            
            CreateMap<Actor, ActorViewModel>();
            CreateMap<ActorViewModel, Actor>();
            CreateMap<ActorFullViewModel, Actor>();
            CreateMap<Actor, ActorFullViewModel>();

            CreateMap<Review, ReviewFullViewModel>();
            CreateMap<ReviewFullViewModel, Review>();
            CreateMap<ReviewViewModel, Review>();
            CreateMap<Review, ReviewViewModel>();
        }
    }
}
