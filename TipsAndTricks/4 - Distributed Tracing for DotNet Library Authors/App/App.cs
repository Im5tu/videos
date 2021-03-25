using System.Threading;
using System.Threading.Tasks;
using Library;
using Microsoft.Extensions.Hosting;

namespace App
{
    class App : BackgroundService
    {
        private readonly ServiceA _service;

        public App(ServiceA serviceA)
        {
            _service = serviceA;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            for (var i = 0; i < 5; i++)
                await _service.DoSomethingAsync();
        }
    }
}