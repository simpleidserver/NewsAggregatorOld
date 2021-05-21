using System;

namespace NewsAggregator.Math.Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            // DirichletDistributionSample.Execute();
            CorrelatedTopicModelSample.Execute();
            Console.WriteLine("Press Enter to quit the application...");
            Console.ReadLine();
        }
    }
}
