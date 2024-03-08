using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Web.API.Filters;

namespace Web.API.Configurations
{
    public static class ControllersConfiguration
    {
        public static IServiceCollection AddAndConfigureControllers(this IServiceCollection services, string serviceName)
        {
            services
                .AddControllers(options
                    => options.Filters.Add(typeof(ApiGlobalExceptionFilter))
                );
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });
            services.AddDocumentation(serviceName);

            services.AddControllers()
           .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
           });
            return services;
        }

        private static IServiceCollection AddDocumentation(this IServiceCollection services, string serviceName)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = string.Format("{0}", serviceName), Version = "v1" });
            });
            return services;
        }

        public static WebApplication UseDocumentation(this WebApplication app, string serviceName)
        {
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			return app;
        }
    }
}
