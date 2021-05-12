
using NewsAggregator.Microsoft.ML.CorrelatedTopicModel;

namespace Microsoft.ML
{
    public static class ConversionsExtensionsCatalog
    {
        public static CorrelatedTopicModelEstimator ColleratedTopicModel(this TransformsCatalog.TextTransforms catalog, string outputColumnName, string inputColumnName)
        {
            return new CorrelatedTopicModelEstimator(outputColumnName, inputColumnName);
        }
    }
}
