using System.Collections.Generic;
using System.Reflection;

namespace NewsAggregator.ML
{
    public class NewsAggregatorMLOptions
    {
        public NewsAggregatorMLOptions()
        {
            WordEmbeddingOptions = new[]
            {
                new WordEmbeddingOption("en", "https://dl.fbaipublicfiles.com/fasttext/vectors-english/wiki-news-300d-1M.vec.zip")
            };
            LDANbTopics = 75;
            BlockThreadMS = 1 * 1000;
            DeadLetterTimeMS = 5 * 1000;
            NbSessionsToProcess = 2;
            NbArticlesToProcess = 5;
            NbRetry = 5;
            ApplicationAssembly = Assembly.GetCallingAssembly();
            ConnectionString = "Data Source=DESKTOP-T4INEAM\\SQLEXPRESS;Initial Catalog=NewsAggregator;Integrated Security=True";
        }

        public IEnumerable<WordEmbeddingOption> WordEmbeddingOptions { get; set; }
        public int LDANbTopics { get; set; }
        public int BlockThreadMS { get; set; }
        public int DeadLetterTimeMS { get; set; }
        public int NbSessionsToProcess { get; set; }
        public int NbArticlesToProcess { get; set; }
        public int NbRetry { get; set; }
        public Assembly ApplicationAssembly { get; set; }
        public string ConnectionString { get; set; }
    }
}
