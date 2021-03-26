using Microsoft.Extensions.DependencyInjection;
using System;

namespace NewsAggregator.ML.Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddNewsAggregatorML();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            Console.WriteLine("Hello World!");
        }
    }
}
