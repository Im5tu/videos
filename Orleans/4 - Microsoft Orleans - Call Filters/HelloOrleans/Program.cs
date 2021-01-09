using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Orleans.Configuration;
using Orleans.Hosting;
using System.Net;

namespace HelloOrleans
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseOrleans(builder =>
                {
                    builder.UseDynamoDBClustering(options =>
                    {
                        options.Service = "http://localhost:4566";
                    });
                    builder.AddDynamoDBGrainStorageAsDefault(options =>
                    {
                        options.Service = "http://localhost:4566";
                        options.UseJson = true;
                    });

                    builder.Configure<EndpointOptions>(options =>
                    {
                        options.AdvertisedIPAddress = IPAddress.Loopback;
                        options.GatewayListeningEndpoint = new IPEndPoint(IPAddress.Any, EndpointOptions.DEFAULT_GATEWAY_PORT);
                        options.SiloListeningEndpoint = new IPEndPoint(IPAddress.Any, EndpointOptions.DEFAULT_SILO_PORT);
                    });

                    builder.Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "my-first-cluster";
                        options.ServiceId = "my-first-service";
                    });
                });
    }
}
