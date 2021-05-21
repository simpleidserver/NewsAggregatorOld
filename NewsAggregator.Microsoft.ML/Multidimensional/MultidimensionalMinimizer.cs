
using MathNet.Numerics.LinearAlgebra;
using NewsAggregator.Microsoft.ML.Extensions;

namespace NewsAggregator.Microsoft.ML.Multidimensional
{
    public class MultidimensionalMinimizer
    {
        public MultidimensionalMinimizer(int n, IMultidimensionalIterator iterator, MultidimensionalFunctionFDF fdf)
        {
            Init(n);
            Iterator = iterator;
            FDF = fdf;
        }

        public IMultidimensionalIterator Iterator { get; private set; }
        public MultidimensionalFunctionFDF FDF { get; private set; }
        public Vector<double> X { get; private set; }
        public Vector<double> Gradient { get; private set; }
        public Vector<double> DX { get; private set; }
        public double F { get; set; }

        public void Set(Vector<double> x, double stepSize, double tol)
        {
            F = FDF.F(x);
            Gradient = FDF.DF(x);
            x.CopyTo(X);
            DX.SetValue(0);
            Iterator.Set(Gradient, stepSize, tol);
        }

        public void Iterate()
        {

        }

        private void Init(int n)
        {
            X = Vector<double>.Build.Dense(n);
            Gradient = Vector<double>.Build.Dense(n);
            DX = Vector<double>.Build.Dense(n);
        }
    }
}
