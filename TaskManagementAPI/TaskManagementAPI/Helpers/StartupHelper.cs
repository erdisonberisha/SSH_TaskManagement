using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;

namespace TaskManagementAPI.Helpers
{
    public static class StartupHelper
    {
        public static void AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskManagementDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"))
                       .UseSnakeCaseNamingConvention();
            });

        }
    }
}
