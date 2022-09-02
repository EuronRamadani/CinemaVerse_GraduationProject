using Microsoft.Extensions.DependencyInjection;
using Movies.Data.Interfaces;
using Movies.Data.Repositories;

namespace Movies.Configuration.Extensions
{
    public static class RepositoryExtension
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));
        }
    }
}
