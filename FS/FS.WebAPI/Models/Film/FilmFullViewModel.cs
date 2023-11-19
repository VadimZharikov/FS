using FS.WebAPI.Models.Actor;
using FS.WebAPI.Models.Review;

namespace FS.WebAPI.Models.Film
{
    public class FilmFullViewModel
    {
        #nullable disable
        public int FilmId { get; set; }
        public string Title { get; set; }
        public float Stars { get; private set; }
        public List<ActorViewModel> Actors { get; set; }
        public List<ReviewFullViewModel> Reviews { get; set; }
    }
}
