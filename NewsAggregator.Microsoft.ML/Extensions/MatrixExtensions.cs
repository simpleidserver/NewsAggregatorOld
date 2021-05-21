using MathNet.Numerics.LinearAlgebra;
using System;
using System.Diagnostics;

namespace NewsAggregator.Microsoft.ML.Extensions
{
    public static class MatrixExtensions
    {
        public static double ComputeLNDeterminant(this Matrix<double> matrix)
        {
            var luResult = matrix.LU();
            var lu = luResult.L.Multiply(luResult.U);
            double result = 0;
            for(int i = 0; i < lu.RowCount; i++)
            {
                result += Math.Log(Math.Abs(lu[i,i]));
            }

            return result;
        }

        public static void SetValue<T>(this Matrix<T> matrix, T value) where T : struct, IEquatable<T>, IFormattable
        {
            for(int row = 0; row < matrix.RowCount; row++)
            {
                for(int column = 0; column < matrix.ColumnCount; column++)
                {
                    matrix[row, column] = value;
                }
            }
        }

        public static Vector<double> ColumnSum(this Matrix<double> matrix)
        {
            var result = Vector<double>.Build.Dense(matrix.ColumnCount);
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for(int j = 0; j < matrix.ColumnCount; j++)
                {
                    result[j] = result[j] + matrix[i, j];
                }
            }

            return result;
        }


        public static void Display<T>(this Matrix<T> matrix) where T : struct, IEquatable<T>, IFormattable
        {
            for (int row = 0; row < matrix.RowCount; row++)
            {
                for(int column = 0; column < matrix.ColumnCount; column++)
                {
                    Debug.Write($"{matrix[row, column]} ");
                }

                Debug.WriteLine(string.Empty);
            }
        }

        /// <summary>
        /// Compute C = alpha * op(A) * op(B) + beta * C.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="trans1"></param>
        /// <param name="transB"></param>
        /// <param name="alpha"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="beta"></param>
        public static void ComputeDGem(this Matrix<double> c, TransOp transA, TransOp transB, double alpha, Matrix<double> a, Matrix<double> b, double beta)
        {
            a = Transform(a, transA);
            b = Transform(b, transB);
            var newResult = a.Multiply(alpha).Multiply(b).Add(c.Multiply(beta));
            newResult.CopyTo(c);
        }

        private static Matrix<double> Transform(Matrix<double> matrix, TransOp op)
        {
            switch(op)
            {
                case TransOp.CblasNoTrans:
                    return matrix;
                case TransOp.CblasTrans:
                    return matrix.Transpose();
                case TransOp.CblasConjTrans:
                    return matrix.ConjugateTranspose();
            }

            return null;
        }
    }
}
