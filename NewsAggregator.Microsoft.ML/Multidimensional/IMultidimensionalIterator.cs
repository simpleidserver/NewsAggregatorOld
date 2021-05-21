using MathNet.Numerics.LinearAlgebra;

namespace NewsAggregator.Microsoft.ML.Multidimensional
{
    public interface IMultidimensionalIterator
    {
        void Set(Vector<double> gradient, double stepSize, double tol);
    }
}
