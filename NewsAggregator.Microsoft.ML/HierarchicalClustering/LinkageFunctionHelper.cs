using Microsoft.ML.HierarchicalClustering;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ML.Trainers
{
    public static class LinkageFunctionHelper
    {
        public static double Calculate(LinkageFunctionTypes linkageFunction, DistanceMeasurementTypes distanceMeasurement, Cluster firstCluster, Cluster secondCluster)
        {
            return Calculate(linkageFunction, distanceMeasurement, firstCluster.GetAllDatapoints(), secondCluster.GetAllDatapoints());
        }

        public static double Calculate(LinkageFunctionTypes linkageFunction, DistanceMeasurementTypes distanceMeasurement, List<double[]> firstClusterDatapoints, List<double[]> secondClusterDatapoints)
        {
            switch (linkageFunction)
            {
                case LinkageFunctionTypes.Single:
                    return CalculateSingle(distanceMeasurement, firstClusterDatapoints, secondClusterDatapoints);
                case LinkageFunctionTypes.Complete:
                    return CalculateComplete(distanceMeasurement, firstClusterDatapoints, secondClusterDatapoints);
            }

            return default(double);
        }

        public static double CalculateSingle(DistanceMeasurementTypes distanceMeasurement, List<double[]> firstClusterDatapoints, List<double[]> secondClusterDatapoints)
        {
            double min = double.MaxValue;
            for (int i = 0; i < firstClusterDatapoints.Count(); i++)
            {
                for (int j = 0; j < secondClusterDatapoints.Count(); j++)
                {
                    var firstDatapoint = firstClusterDatapoints[i];
                    var secondDatapoint = secondClusterDatapoints[j];
                    var distance = DistanceCalculationHelper.Calculate(distanceMeasurement, firstDatapoint, secondDatapoint);
                    if (distance < min)
                    {
                        min = distance;
                    }
                }
            }

            return min;
        }

        public static double CalculateComplete(DistanceMeasurementTypes distanceMeasurement, List<double[]> firstClusterDatapoints, List<double[]> secondClusterDatapoints)
        {
            double max = 0;
            for (int i = 0; i < firstClusterDatapoints.Count(); i++)
            {
                for (int j = 0; j < secondClusterDatapoints.Count(); j++)
                {
                    var firstDatapoint = firstClusterDatapoints[i];
                    var secondDatapoint = secondClusterDatapoints[j];
                    var distance = DistanceCalculationHelper.Calculate(distanceMeasurement, firstDatapoint, secondDatapoint);
                    if (distance > max)
                    {
                        max = distance;
                    }
                }
            }

            return max;
        }
    }
}
