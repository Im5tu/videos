using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloOrleans
{
    public interface IHelloWorldGrain : IGrainWithStringKey
    {
        Task<string> SayHelloToAsync(string name);
    }

    public class HelloWorldGrain : Grain, IHelloWorldGrain
    {
        private int _invocationCount = 0;

        public Task<string> SayHelloToAsync(string name)
        {
            return Task.FromResult($"Hello {name} from {this.GetPrimaryKeyString()} - I've said hello {_invocationCount++} times.");
        }
    }
}
