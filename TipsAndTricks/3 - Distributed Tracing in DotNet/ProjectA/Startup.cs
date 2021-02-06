using System;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ProjectA
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
            
            services.AddHttpClient("ProjectB")
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5002"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    context.Response.Headers.Add("Request-Id", Activity.Current?.TraceId.ToString() ?? string.Empty);

                    using var client = context.RequestServices.GetRequiredService<IHttpClientFactory>().CreateClient("ProjectB");
                    var content = client.GetStringAsync("/");

                    await context.Response.WriteAsync("Hello From Project A\n");
                    await context.Response.WriteAsync(await content);
                });
            });
        }
    }
}
