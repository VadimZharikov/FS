using FS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FS.DAL.DataContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() 
        {
            Database.Migrate();
        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
        }

        #nullable disable
        public DbSet<FilmEntity> Films { get; set; }
        public DbSet<ActorEntity> Actors { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }


    }
}
