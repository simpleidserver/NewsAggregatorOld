namespace NewsAggregator.Microsoft.ML.CorrelatedTopicModel
{
    public class Options
    {
        public Options()
        {
            NbTopics = 10;
        }

        /// <summary>
        /// Number of topics.
        /// </summary>
        public int NbTopics { get; set; }
        /// <summary>
        /// Topic-word density factory 'β'.
        /// Controls the distribution of words per topic.
        /// </summary>
        public int TopicWordDensity { get; set; }
        /// <summary>
        /// Document-topic density factory : 'α'.
        /// Controls the number of topics expected in the document.
        /// </summary>
        public int DocumentTopicDensityFactory { get; set; }
    }
}
