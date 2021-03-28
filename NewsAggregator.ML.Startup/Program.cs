using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace NewsAggregator.ML.Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            // Extract news from BBC & learn from articles : OK
            // Build several sessions
            // Manually decude the next articles...
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddNewsAggregatorML();
            serviceCollection.AddLogging(logging =>
            {
                logging.AddConsole();
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var jobServer = serviceProvider.GetRequiredService<IMLJobServer>();
            jobServer.Start();
            Console.WriteLine("Press Enter to quit the application...");
            Console.ReadLine();
        }
    }
}
