using Library;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;

namespace App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder()
                    .ConfigureServices(services =>
                    {
                        services.AddHostedService<App>();
                        services.AddSingleton<ServiceA>();

                        services.AddOpenTelemetryTracing(builder =>
                        {
                            builder.AddConsoleExporter()
                                .SetSampler(new AlwaysOnSampler())
                                .AddSource(MyActivitySource.Name);
                        });
                    })
                    .Build()
                    .RunAsync();
        }
    }
}
