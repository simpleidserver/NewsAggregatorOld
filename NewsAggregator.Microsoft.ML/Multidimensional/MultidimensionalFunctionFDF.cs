using MathNet.Numerics.LinearAlgebra;
using System;

namespace NewsAggregator.Microsoft.ML.Multidimensional
{
    public class MultidimensionalFunctionFDF
    {
        public Func<Vector<double>, double> F { get; set; }
        /// <summary>
        /// Compute gradient.
        /// </summary>
        public Func<Vector<double>, Vector<double>> DF { get; set; }
        public Action<Vector<double>, double, Vector<double>> FDF { get; set; }
        public int N { get; set; }
    }
}
