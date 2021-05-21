using Microsoft.ML;
using Microsoft.ML.Transforms.Text;

namespace NewsAggregator.Microsoft.ML.CorrelatedTopicModel
{
    public class CorrelatedTopicModelEstimator : IEstimator<CorrelatedTopicModelEstimatorTransformer>
    {
        private readonly string _outputColumnName;
        private readonly string _inputColumnName;
        private readonly int _nbTopics;

        public CorrelatedTopicModelEstimator(string outputColumnName, string inputColumnName, int nbTopics)
        {
            _outputColumnName = outputColumnName;
            _inputColumnName = inputColumnName;
            _nbTopics = nbTopics;
        }

        public CorrelatedTopicModelEstimatorTransformer Fit(IDataView input)
        {
            var result = new CorrelatedTopicModelEstimatorTransformer(_outputColumnName, _inputColumnName, _nbTopics);
            result.Fit(input);
            return result;
        }

        public SchemaShape GetOutputSchema(SchemaShape inputSchema)
        {
            return inputSchema;
        }
    }
}
