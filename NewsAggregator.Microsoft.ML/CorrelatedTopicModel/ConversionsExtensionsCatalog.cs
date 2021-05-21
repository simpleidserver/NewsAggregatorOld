
using NewsAggregator.Microsoft.ML.CorrelatedTopicModel;

namespace Microsoft.ML
{
    public static class ConversionsExtensionsCatalog
    {
        public static CorrelatedTopicModelEstimator ColleratedTopicModel(this TransformsCatalog.TextTransforms catalog, string outputColumnName, string inputColumnName, int nbTopics = 3)
        {
            return new CorrelatedTopicModelEstimator(outputColumnName, inputColumnName, nbTopics);
        }
    }
}
