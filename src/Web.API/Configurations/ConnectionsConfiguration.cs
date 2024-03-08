using Infra.Repository.EF;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Configurations
{
    public static class ConnectionsConfiguration
    {

        public static IServiceCollection AddAppConections(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbConnection(configuration);
            return services;
        }

        private static IServiceCollection AddDbConnection(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var user = Environment.GetEnvironmentVariable("API_DB_USER");
            var password = Environment.GetEnvironmentVariable("API_DB_PASSWORD");
            var datasource = Environment.GetEnvironmentVariable("API_DB_DATASOURCE");
            var connectionString = $"User Id={user};Password={password};{datasource}";

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseOracle(connectionString, options => options
                        .UseOracleSQLCompatibility("11"));
            })
                ;
            return services;
        }

        public static WebApplication MigrateDatabase(
            this WebApplication app)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment == "EndToEndTest") return app;

            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
            return app;
        }
    }

}
