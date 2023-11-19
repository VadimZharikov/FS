using AutoMapper;
using FS.BLL.Entities;
using FS.WebAPI.Models;

namespace FS.WebAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Film, FilmViewModel>();
            CreateMap<FilmViewModel, Film>();
            CreateMap<Actor, ActorViewModel>();
            CreateMap<ActorViewModel, Actor>();
            CreateMap<Review, ReviewViewModel>();
            CreateMap<ReviewViewModel, Review>();
        }
    }
}
