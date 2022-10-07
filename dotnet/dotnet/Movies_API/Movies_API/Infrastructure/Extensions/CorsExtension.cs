using Microsoft.Extensions.DependencyInjection;

namespace Movies.API.Infrastructure.Extensions
{
    public static class CorsExtension
    {
        public static void AddStorageCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
    }
}