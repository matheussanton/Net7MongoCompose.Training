using src.Models;
using src.Services;

namespace src
{
    public static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<UserDatabaseConfiguration>(
                configuration.GetSection(nameof(UserDatabaseConfiguration)));
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<UserService>();
        }
    }
}
