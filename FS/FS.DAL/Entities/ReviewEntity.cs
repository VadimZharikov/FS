using System.ComponentModel.DataAnnotations;

namespace FS.DAL.Entities
{
    #nullable disable
    public class ReviewEntity
    {
        [Key]
        public int ReviewId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Stars {  get; set; }
        public FilmEntity Film { get; set; }
        public int FilmId {  get; set; }

    }
}