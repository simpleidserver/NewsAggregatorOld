using MathNet.Numerics.LinearAlgebra;

namespace NewsAggregator.Microsoft.ML.CorrelatedTopicModel
{
    public class LLNASufficientStatistics
    {
        public LLNASufficientStatistics(LLNAModel model)
        {
            MuSufficientStatistic = new double[model.K - 1];
            CovarianceSufficientStatistic = Matrix<double>.Build.Dense(model.K - 1, model.K - 1, 0);
            BetaSufficientStatistic = Matrix<double>.Build.Dense(model.K, model.LogBeta.ColumnCount, 0);
            NData = 0;
        }

        public Matrix<double> CovarianceSufficientStatistic { get; set; }
        public double[] MuSufficientStatistic { get; set; }
        public Matrix<double> BetaSufficientStatistic { get; set; }
        public double NData { get; set; }
    }
}
