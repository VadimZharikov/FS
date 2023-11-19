using System.ComponentModel.DataAnnotations;

namespace FS.DAL.Entities
{
    #nullable disable
    public class ActorEntity
    {
        [Key]
        public int ActorId {  get; set; }
        public string ActorName { get; set; }
        public List<FilmEntity> Films { get; set; }
    }
}
