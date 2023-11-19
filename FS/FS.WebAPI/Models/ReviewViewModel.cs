namespace FS.WebAPI.Models
{
    public class ReviewViewModel
    {
        #nullable disable
        public int ReviewId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public int FilmId { get; set; }
    }
}
