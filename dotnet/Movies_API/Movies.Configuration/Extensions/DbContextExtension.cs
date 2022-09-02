using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Movies.Data;

namespace Movies.Configuration.Extensions
{
    public static class DbContextExtension
    {
        public static void RegisterDbContext(this IServiceCollection services)
        {
            services.AddDbContextPool<MoviesDbContext>(options =>
                options.UseNpgsql(services.GetAppSettings().ConnectionStrings.MoviesDbContext));
        }
    }
}
