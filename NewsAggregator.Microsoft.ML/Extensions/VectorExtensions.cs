using MathNet.Numerics.LinearAlgebra;
using System;
using System.Diagnostics;
using System.Linq;

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

        public static Vector<double> Invert(this Vector<System.Numerics.Complex> vector)
        {
            var result = Vector<double>.Build.Dense(vector.Count());
            int index = 0;
            for(int i = vector.Count() - 1; i >= 0; i--)
            {
                result[index] = vector[i].Real;
                index++;
            }

            return result;
        }

        public static void Display<T>(this Vector<T> vector) where T : struct, IEquatable<T>, IFormattable
        {
            foreach(var record in vector)
            {
                Debug.Write($"{record} ");
            }

            Debug.WriteLine(string.Empty);
        }

        public static double Multiply(this Vector<double> x, Vector<double> y)
        {
            double result = 0.0;
            for (int i = 0; i < x.Count(); i++)
            {
                result += x[i] * y[i];
            }

            return result;
        }

        public static Vector<double> Sum(this Vector<double> x, Vector<double> y)
        {
            var result = Vector<double>.Build.Dense(x.Count(), 0);
            for(int i = 0; i < x.Count(); i++)
            {
                result[i] = x[i] + y[i];
            }

            return result;
        }

        /// <summary>
        /// Compute y = x*alpha + y
        /// </summary>
        /// <param name="firstVector"></param>
        /// <param name="alpha"></param>
        /// <param name="secondVector"></param>
        /// <returns></returns>
        public static void ComputeDaxpy(this Vector<double> y, double alpha, Vector<double> x)
        {
            int i = 0;
            foreach(var xVal in x)
            {
                y[i] = xVal * alpha + y[i];
                i++;
            }
        }

        /// <summary>
        /// Compute y = alpha*A*x + beta*y.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="alpha"></param>
        /// <param name="a"></param>
        /// <param name="x"></param>
        /// <param name="beta"></param>
        public static void ComputeDsymv(this Vector<double> y, double alpha, Matrix<double> a, Vector<double> x, double beta)
        {
            var result = a.Multiply(alpha).Multiply(x).Sum(y.Multiply(beta));
            for(int i = 0; i < result.Count(); i++)
            {
                y[i] = result[i];
            }
        }
    }
}
