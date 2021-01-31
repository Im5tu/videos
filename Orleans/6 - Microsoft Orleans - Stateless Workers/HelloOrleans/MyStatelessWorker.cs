using Orleans;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloOrleans
{
    public interface IMyStatelessWorker : IGrainWithIntegerKey
    {
        Task<double> ComputeNextNumberAsync();
    }

    [StatelessWorker]
    public class MyStatelessWorker : Grain, IMyStatelessWorker
    {
        public async Task<double> ComputeNextNumberAsync()
        {
            var rnd = new Random();

            await Task.Delay(rnd.Next(1000, 5000));

            return rnd.NextDouble();
        }
    }
}
