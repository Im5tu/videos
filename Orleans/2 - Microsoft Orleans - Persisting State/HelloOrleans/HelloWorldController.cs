using Microsoft.AspNetCore.Mvc;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloOrleans
{
    public class HelloWorldController : Controller
    {
        private readonly IClusterClient clusterClient;

        public HelloWorldController(IClusterClient clusterClient)
        {
            this.clusterClient = clusterClient;
        }

        [HttpGet("/hello/{name}")]
        public async Task<IActionResult> Hello(string name)
        {
            var result = await clusterClient.GetGrain<IHelloWorldGrain>("Stu").SayHelloToAsync(name);

            return Ok(result);
        }
    }
}
