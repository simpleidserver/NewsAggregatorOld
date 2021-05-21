using MathNet.Numerics.LinearAlgebra;

namespace NewsAggregator.Microsoft.ML.Multidimensional
{
    public class IterationResult
    {
        public double F { get; set; }
        public IterationResultStates State { get; set; }
    }
}
