using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using Web.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});

string serviceName = builder.Configuration["SERVICE_NAME"] ?? "Sky.Toolkit.Sample";
var otelUrl = builder.Configuration["OTEL_URL"];

builder.Logging
	.AddOpenTelemetry((Action<OpenTelemetryLoggerOptions>?)(options =>
	{
		options.IncludeFormattedMessage = true;
		options.IncludeScopes = true;
		var resBuilder = ResourceBuilder.CreateDefault();
		resBuilder.AddService((string)serviceName);
		options.SetResourceBuilder(resBuilder);
		options.AddOtlpExporter(opts => { opts.Endpoint = new Uri(otelUrl!); });

		options.AddOtlpExporter();
	}));


builder.Services
	.AddAppConections(builder.Configuration)
	.AddApplicationConfiguration(serviceName)
	.AddAndConfigureControllers(serviceName)
	.AddCors(p => p.AddPolicy("CORS", builder =>
	{
		builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
	}));

var app = builder.Build();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.UseHttpLogging();
app.UseDeveloperExceptionPage();
//app.MigrateDatabase();
app.UseDocumentation(serviceName);
app.UseCors("CORS");

app.MapControllers();

app.Run();
public partial class Program { }