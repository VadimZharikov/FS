namespace FS.BLL.Entities
{
    public class Film
    {
        #nullable disable
        public int FilmId { get; set; }
        public string Title { get; set; }
        public float Stars { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
