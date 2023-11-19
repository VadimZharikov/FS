using AutoMapper;
using FS.BLL.Entities;
using FS.DAL.Entities;

namespace FS.BLL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FilmEntity, Film>();
            CreateMap<Film, FilmEntity>();
            CreateMap<ReviewEntity, Review>();
            CreateMap<Review, ReviewEntity>();
            CreateMap<ActorEntity, Actor>();
            CreateMap<Actor, ActorEntity>();
        }
    }
}
