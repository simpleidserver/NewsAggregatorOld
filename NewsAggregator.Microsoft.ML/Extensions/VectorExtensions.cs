using MathNet.Numerics.LinearAlgebra;
using System;

namespace NewsAggregator.Microsoft.ML.Extensions
{
    public static class VectorExtensions
    {
        public static void SetValue<T>(this Vector<T> vector, T value) where T : struct, IEquatable<T>, IFormattable
        {
            for(int i = 0; i < vector.Count; i++)
            {
                vector[i] = value;
            }
        }
    }
}
