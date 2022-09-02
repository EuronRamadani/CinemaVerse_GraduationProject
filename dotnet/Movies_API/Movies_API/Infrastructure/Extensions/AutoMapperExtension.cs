using Microsoft.Extensions.DependencyInjection;
using Movies.Api.Infrastructure.AutoMapper;

namespace Movies.Api.Infrastructure.Extensions
{
    public static class AutoMapperExtension
    {
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApiMapperConfiguration));
        }
    }
}
