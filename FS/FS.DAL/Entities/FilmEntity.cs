using System.ComponentModel.DataAnnotations;

namespace FS.DAL.Entities
{
    #nullable disable
    public class FilmEntity
    {
        [Key]
        public int FilmId { get; set; }
        public string Title { get; set; }
        public float Stars { get; set; }
        public virtual List<ActorEntity> Actors { get; set; }
        public virtual List<ReviewEntity> Reviews { get; set; }
    }
}
