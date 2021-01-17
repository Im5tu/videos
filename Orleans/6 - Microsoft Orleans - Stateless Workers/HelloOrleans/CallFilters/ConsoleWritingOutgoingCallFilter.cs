using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloOrleans.CallFilters
{
    public class ConsoleWritingOutgoingCallFilter : IOutgoingGrainCallFilter
    {
        public async Task Invoke(IOutgoingGrainCallContext context)
        {
            Console.WriteLine("Before Method Invoke, outgoing call filter");
            await context.Invoke();
            Console.WriteLine("After Method Invoke, outgoing call filter");
        }
    }
}
