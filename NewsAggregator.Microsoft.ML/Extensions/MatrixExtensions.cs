using MathNet.Numerics.LinearAlgebra;
using System;
using System.Diagnostics;

namespace NewsAggregator.Microsoft.ML.Extensions
{
    public static class MatrixExtensions
    {
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
    }
}
