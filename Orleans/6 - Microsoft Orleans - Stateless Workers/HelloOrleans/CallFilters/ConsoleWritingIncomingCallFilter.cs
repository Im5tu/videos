using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloOrleans.CallFilters
{
    public class ConsoleWritingIncomingCallFilter : IIncomingGrainCallFilter
    {
        public async Task Invoke(IIncomingGrainCallContext context)
        {
            Console.WriteLine("Before Method Invoke, incoming call filter");
            await context.Invoke();
            Console.WriteLine("After Method Invoke, incoming call filter");
        }
    }
}
