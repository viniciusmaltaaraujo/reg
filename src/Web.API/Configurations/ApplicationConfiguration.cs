using Application.Facade;
using Application.Facade.Health;
using Application.Facade.Sample;
using Domain.Sample.Repository;
using Domain.Sample.Service;
using Domain.SeedWork;
using Infra.Repository.EF;
using Infra.Repository.Interfaces;
using Infra.Repository.Repositories;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Web.API.Configurations
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, string serviceName)
        {
            services.AddRepositories();
            services.AddDomainEvents();
            services.AddObervability(serviceName);
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IHealthRepository, HealthRepository>();
            services.AddTransient<IExampleRepository, ExampleRepository>();
            services.AddTransient<ExampleFacade, ExampleFacade>();
            services.AddTransient<HealthFacade, HealthFacade>();
            services.AddTransient<ExampleDomainService, ExampleDomainService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }


        private static IServiceCollection AddDomainEvents(this IServiceCollection services)
        {
            services.AddTransient<IDomainEventPublisher, DomainEventPublisher>();
            return services;
        }

        private static IServiceCollection AddObervability(this IServiceCollection services, string serviceName)
		{
			var jeagerUrl = Environment.GetEnvironmentVariable("JAEGER_URL")!;

			services.Configure<AspNetCoreTraceInstrumentationOptions>(options =>
			{
				// Filter out instrumentation of the Prometheus scraping endpoint.
				options.Filter = ctx => !(ctx.Request.Path == "/metrics" || ctx.Request.Path == "/health" || ctx.Request.Path == "/health/ready");
			});
			services.AddOpenTelemetry()
					.ConfigureResource(b =>
					{
						b.AddService(serviceName);
					})
					.WithTracing(b => b
						.AddAspNetCoreInstrumentation()
						.AddHttpClientInstrumentation()
						.AddOtlpExporter(opts => { opts.Endpoint = new Uri(jeagerUrl); })
						.AddConsoleExporter())
					.WithMetrics(b => b
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddRuntimeInstrumentation()
                        .AddProcessInstrumentation()
                        .AddPrometheusExporter()
                        .AddMeter("Microsoft.AspNetCore.Hosting")
                        .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                        .AddConsoleExporter());
            return services;
        }

    }
}
