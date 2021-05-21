using MathNet.Numerics.LinearAlgebra;

namespace NewsAggregator.Microsoft.ML.Multidimensional
{
    public class MultidimensionalIteratorState
    {
        public MultidimensionalIteratorState(int n)
        {
            X1 = Vector<double>.Build.Dense(n);
            DX1 = Vector<double>.Build.Dense(n);
            X2 = Vector<double>.Build.Dense(n);
            P = Vector<double>.Build.Dense(n);
            G0 = Vector<double>.Build.Dense(n);
        }

        public int Iter { get; set; }
        public double Step { get; set; }
        public double MaxStep { get; set; }
        public double Tol { get; set; }
        public Vector<double> X1 { get; set; }
        public Vector<double> DX1 { get; set; }
        public Vector<double> X2 { get; set; }
        public double PNorm { get; set; }
        public Vector<double> P { get; set; }
        public double G0Norm { get; set; }
        public Vector<double> G0 { get; set; }
    }
}
