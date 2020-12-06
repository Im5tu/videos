using Orleans;
using System.Threading.Tasks;

namespace HelloOrleans
{
    public interface IHelloWorldGrain : IGrainWithStringKey
    {
        Task<string> SayHelloToAsync(string name);
    }

    public class HelloWorldGrain : Grain<HelloState>, IHelloWorldGrain
    {
        public async Task<string> SayHelloToAsync(string name)
        {
            var count = State.InvocationCount++;
            await WriteStateAsync();
            return $"Hello {name} from {this.GetPrimaryKeyString()} - I've said hello {count} times.";
        }
    }

    public class HelloState
    {
        public int InvocationCount { get; set; }
    }
}
