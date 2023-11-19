using FS.WebAPI.Models.Film;

namespace FS.WebAPI.Models.Actor
{
    public class ActorFullViewModel
    {
        #nullable disable
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public List<FilmViewModel> Films { get; set; }
    }
}
