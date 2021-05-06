using Microsoft.ML.Trainers;

namespace Microsoft.ML
{
    public static class HierarchicalClusteringExtensions
    {
        public static HierarchicalClusteringTrainer HierarchicalClustering(this ClusteringCatalog.ClusteringTrainers catalog, HierarchicalClustering.Options options)
        {
            return new HierarchicalClusteringTrainer(options);
        }
    }
}
