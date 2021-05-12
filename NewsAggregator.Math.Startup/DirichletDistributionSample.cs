using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsAggregator.Math.Startup
{
    public static class DirichletDistributionSample
    {
        /// <summary>
        /// Number of documents.
        /// </summary>
        private const int N = 1000;
        /// <summary>
        /// Number of topics.
        /// </summary>
        private const int K = 5;
        private const double MIN_PROBABILITY = 0.3;

        public static void Execute()
        {
            Console.WriteLine($"Number of documents : {N}");
            Console.WriteLine($"Number of topics : {K}");
            double probability = (double)1 / (double)K;
            var alpha = Enumerable.Repeat(probability, K).ToArray();
            var dirichlet = new Dirichlet(alpha);
            var lstTopicProbabilities = new List<double[]>();
            for(int i = 0; i < N; i++)
            {
                lstTopicProbabilities.Add(dirichlet.Sample());
            }

            int nbDocumentsWithoutTopic = 0;
            var nbTopics = Enumerable.Repeat(0, K).ToArray();
            foreach (var topicProbabilities in lstTopicProbabilities)
            {
                var filtered = topicProbabilities.Where(_ => _ > MIN_PROBABILITY);
                if(!filtered.Any())
                {
                    nbDocumentsWithoutTopic++;
                    continue;
                }

                foreach(var topicProb in filtered)
                {
                    var index = topicProbabilities.ToList().IndexOf(topicProb);
                    nbTopics[index] = nbTopics[index] + 1;
                }
            }

            Console.WriteLine($"Number of documents without topic : {nbDocumentsWithoutTopic}");
            for(int i = 0; i < K; i++)
            {
                Console.WriteLine($"Number of Topic{i} : {nbTopics[i]}");
            }
        }
    }
}
