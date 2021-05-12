using Microsoft.ML;
using Microsoft.ML.Transforms.Text;
using System;

namespace NewsAggregator.Microsoft.ML.CorrelatedTopicModel
{
    public class CorrelatedTopicModelEstimator : IEstimator<CorrelatedTopicModelEstimatorTransformer>
    {
        private readonly string _outputColumnName;
        private readonly string _inputColumnName;

        public CorrelatedTopicModelEstimator(string outputColumnName, string inputColumnName)
        {
            _outputColumnName = outputColumnName;
            _inputColumnName = inputColumnName;
        }

        public CorrelatedTopicModelEstimatorTransformer Fit(IDataView input)
        {
            var result = new CorrelatedTopicModelEstimatorTransformer(_outputColumnName, _inputColumnName);
            result.Fit(input);
            return result;
        }

        public SchemaShape GetOutputSchema(SchemaShape inputSchema)
        {
            return inputSchema;
        }
    }
}
