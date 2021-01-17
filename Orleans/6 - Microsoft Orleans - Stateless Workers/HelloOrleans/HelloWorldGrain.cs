using Orleans;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace HelloOrleans
{
    public interface IHelloWorldGrain : IGrainWithStringKey
    {
        Task<string> SayHelloToAsync(string name);
    }

    public class HelloWorldGrain : Grain<HelloState>, IHelloWorldGrain, IRemindable
    {
        // private IDisposable _timer;

        public async Task<string> SayHelloToAsync(string name)
        {
            var count = State.InvocationCount++;
            await WriteStateAsync();
            return $"Hello {name} from {this.GetPrimaryKeyString()} - I've said hello {count} times.";
        }

        public override async Task OnActivateAsync()
        {
            //_timer = RegisterTimer(state =>
            //{
            //    Console.WriteLine("Hello World");
            //    return Task.CompletedTask;
            //}, null, TimeSpan.FromSeconds(5), TimeSpan.FromMinutes(1));

            await RegisterOrUpdateReminder("GrainReminder", TimeSpan.FromSeconds(5), TimeSpan.FromMinutes(1));

            await base.OnActivateAsync();
        }

        public Task ReceiveReminder(string reminderName, TickStatus status)
        {
            if ("GrainReminder".Equals(reminderName))
                Console.WriteLine("Hello Reminder");

            return Task.CompletedTask;
        }
    }

    public class HelloState
    {
        public int InvocationCount { get; set; }
    }
}
