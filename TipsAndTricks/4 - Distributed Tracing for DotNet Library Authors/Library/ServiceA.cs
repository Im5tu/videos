using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Library
{
    public class ServiceA
    {
        public async Task DoSomethingAsync()
        {
            using var activity = MyActivitySource.Instance.StartActivity("MyApiCall");

            activity?.AddTag("success", "true");

            await Task.Delay(new Random().Next(500, 2000));

            activity?.Stop();

            Console.WriteLine("Hello");
        }
    }

    public class MyActivitySource
    {
        public static string Name = nameof(MyActivitySource);
        public static ActivitySource Instance = new ActivitySource(Name);
    }
}
