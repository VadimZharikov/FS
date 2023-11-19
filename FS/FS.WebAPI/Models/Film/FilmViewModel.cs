namespace FS.WebAPI.Models.Film
{
    public class FilmViewModel
    {
        #nullable disable
        public int FilmId { get; set; }
        public string Title { get; set; }
        public float Stars { get; private set; }
    }
}
