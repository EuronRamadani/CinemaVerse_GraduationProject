using Microsoft.Extensions.DependencyInjection;
using Movies.Services.Services.Cinemas;
using Movies.Services.Services.Movies;
using Movies.Services.Services.Photos;

namespace Movies.API.Infrastructure.Extensions
{
    public static class ServicesExtension
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ICinemaService, CinemaService>();
            services.AddScoped<IPhotoService, PhotoService>();
        }
    }
}
