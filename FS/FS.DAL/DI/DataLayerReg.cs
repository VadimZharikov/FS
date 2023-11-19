using FS.DAL.DataContext;
using FS.DAL.Interfaces;
using FS.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FS.DAL.DI
{
    public static class DataLayerReg
    {
        public static void AddDataRepository(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(op => op.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IFilmRepository, FilmRepository>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
        }
    }
}
