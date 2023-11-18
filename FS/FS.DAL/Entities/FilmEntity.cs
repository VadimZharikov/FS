using System.ComponentModel.DataAnnotations;

namespace FS.DAL.Entities
{
    #nullable disable
    public class FilmEntity
    {
        [Key]
        public int FilmId { get; set; }
        public string Title { get; set; }
        public double Stars { get; set; } = 0.0;
        public List<ActorEntity> Actors { get; set; }
        public List<ReviewEntity> Reviews { get; set; }
    }
}
