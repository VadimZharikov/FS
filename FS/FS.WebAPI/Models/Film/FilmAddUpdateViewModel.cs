using FS.WebAPI.Models.Actor;

namespace FS.WebAPI.Models.Film
{
    public class FilmAddUpdateViewModel
    {
        #nullable disable
        public int FilmId { get; set; }
        public string Title { get; set; }
        public float Stars { get; private set; }
        public List<ActorViewModel> Actors { get; set; }
    }
}
