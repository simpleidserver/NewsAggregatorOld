namespace NewsAggregator.Microsoft.ML.CorrelatedTopicModel
{
    public class CorrelatedTopicModelParameters
    {
        public CorrelatedTopicModelParameters()
        {
            EmMaxIter = 1000;
            VarMaxIter = 500;
            CgMaxIter = 500;
            // EmConvergence = 1e-3;
            EmConvergence = 0.056;
            VarConvergence = 1e-5;
            CgConvergence = 1e-5;
            CovEstimate = 1;
        }

        public double EmMaxIter { get; set; }
        public double VarMaxIter { get; set; }
        public double CgMaxIter { get; set; }
        public double EmConvergence { get; set; }
        public double VarConvergence { get; set; }
        public double CgConvergence { get; set; }
        public double CovEstimate { get; set; }
    }
}
