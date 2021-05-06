namespace Microsoft.ML.Trainers
{
    public class HierarchicalModelParameters
    {
        public HierarchicalModelParameters(RootCluster cluster)
        {
            Cluster = cluster;
        }

        public RootCluster Cluster { get; private set; }
    }
}
