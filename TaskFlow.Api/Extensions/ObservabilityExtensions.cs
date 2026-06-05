using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


namespace TaskFlow.Api.Extensions
{
    public static class ObservabilityExtensions
    {
        public static IServiceCollection AddObservability(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            Console.WriteLine("-$#$$#$#$#------OpenTelemetry Registered-------------$#$$#$#$#------");
            const string serviceName = "TaskFlow.Api";

            services.AddOpenTelemetry()
                .ConfigureResource(resource =>
                {
                    resource.AddService(serviceName);
                })
                .WithTracing(tracing =>
                {
                    tracing
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddSqlClientInstrumentation(options =>
                        {
                            options.RecordException = true;
                        })
                        .AddConsoleExporter()
                        .AddOtlpExporter(options =>
                        {
                            options.Endpoint = new Uri("http://localhost:4318");
                            options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                        });
                })
                .WithMetrics(metrics =>
                {
                    metrics
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddRuntimeInstrumentation()
                        .AddOtlpExporter(options =>
                        {
                            options.Endpoint = new Uri("http://localhost:4318");
                            options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                        });
                });

            return services;
        }
    }
}
