using FS.WebAPI.Models.Film;

namespace FS.WebAPI.Models.Review
{
    public class ReviewFullViewModel
    {
        #nullable disable
        public int ReviewId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public int FilmId { get; set; }
        public FilmViewModel Film { get; set; }
    }
}
