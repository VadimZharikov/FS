namespace FS.BLL.Entities
{
    public class Review
    {
        #nullable disable
        public int ReviewId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
    }
}
