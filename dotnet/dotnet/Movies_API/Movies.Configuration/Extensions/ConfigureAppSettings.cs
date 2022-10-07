using Movies.Core.Configuration;
using Movies.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Movies.Configuration.Extensions
{
    public static class ConfigureAppSettings
    {
        public static void RegisterAppSettings(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AppSettings>(config);
            Singleton<AppSettings>.Instance = services.GetAppSettings();

            services.Configure<ConnectionStrings>(config.GetSection("ConnectionStrings"));
        }

        public static AppSettings GetAppSettings(this IServiceCollection services)
        {
            return services.BuildServiceProvider().GetRequiredService<IOptions<AppSettings>>().Value;
        }
    }
}
