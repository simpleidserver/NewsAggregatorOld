namespace Microsoft.ML.HierarchicalClustering
{
    public class Options
    {
        public Options()
        {
            VectorColumnName = "Vector";
            DistanceMeasurement = DistanceMeasurementTypes.Euclidian;
            LinkageFunction = LinkageFunctionTypes.Single;
            NbClusters = 3;
        }

        public string VectorColumnName { get; set; }
        public DistanceMeasurementTypes DistanceMeasurement { get; set; }
        public LinkageFunctionTypes LinkageFunction { get; set; }
        public int NbClusters { get; set; }
    }
}
