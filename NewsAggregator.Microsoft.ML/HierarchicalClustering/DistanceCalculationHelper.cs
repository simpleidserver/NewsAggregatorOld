using Microsoft.ML.HierarchicalClustering;
using System;
using System.Linq;

namespace Microsoft.ML.Trainers
{
    public static class DistanceCalculationHelper
    {
        public static double Calculate(DistanceMeasurementTypes distanceMeasurementType, double[] vector1, double[] vector2)
        {
            switch(distanceMeasurementType)
            {
                case DistanceMeasurementTypes.Canberra:
                    return CalculateCanberraDistance(vector1, vector2);
                case DistanceMeasurementTypes.Euclidian:
                    return CalculateEucledianDistance(vector1, vector2);
                case DistanceMeasurementTypes.Manhattan:
                    return CalculateManhattanDistance(vector1, vector2);
                case DistanceMeasurementTypes.Maximum:
                    return CalculateMaxDistance(vector1, vector2);
            }

            return default(double);
        }

        public static double CalculateEucledianDistance(double[] vector1, double[] vector2)
        {
            double result = 0;
            for (int i = 0; i < vector1.Count(); i++)
            {
                result += Math.Pow(vector1[i] - vector2[i], 2);
            }

            return Math.Sqrt(result);
        }

        public static double CalculateManhattanDistance(double[] vector1, double[] vector2)
        {
            double result = 0;
            for (int i = 0; i < vector1.Count(); i++)
            {
                result += Math.Abs(vector1[i] - vector2[i]);
            }

            return result;
        }

        public static double CalculateMaxDistance(double[] vector1, double[] vector2)
        {
            double result = 0;
            for (int i = 0; i < vector1.Count(); i++)
            {
                var record = Math.Abs(vector1[i] - vector2[i]);
                if (record > result)
                {
                    result = record;
                }
            }

            return result;
        }

        public static double CalculateCanberraDistance(double[] vector1, double[] vector2)
        {
            double result = 0;
            for (int i = 0; i < vector1.Count(); i++)
            {
                var a1 = Math.Abs(vector1[i] - vector2[i]);
                var a2 = Math.Abs(vector1[i]) + Math.Abs(vector2[i]);
                var a3 = a1 / a2;
                result += a3;
            }

            return result;
        }
    }
}
