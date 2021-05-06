using Microsoft.ML.HierarchicalClustering;

namespace Microsoft.ML.Trainers
{
    public class RootCluster
    {
        public RootCluster(Cluster firstCluster, Cluster secondCluster, LinkageFunctionTypes linkageFunction, DistanceMeasurementTypes distanceMeasurement)
        {
            FirstCluster = firstCluster;
            SecondCluster = secondCluster;
            Distance = LinkageFunctionHelper.Calculate(linkageFunction, distanceMeasurement, FirstCluster, SecondCluster);
            FirstCluster.ComputeDistance(linkageFunction, distanceMeasurement);
            SecondCluster.ComputeDistance(linkageFunction, distanceMeasurement);
        }

        public Cluster FirstCluster { get; private set; }
        public Cluster SecondCluster { get; private set; }
        public double Distance { get; set; }
    }
}
