using MathNet.Numerics.LinearAlgebra;
using NewsAggregator.Microsoft.ML.Extensions;

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

        public void Update(VariationalInferenceParameter varInference, CorpusDocument document)
        {
            for(int i = 0; i < CovarianceSufficientStatistic.RowCount; i++)
            {
                MuSufficientStatistic[i] = MuSufficientStatistic[i] + varInference.Lambda[i];
                for(int j = 0; j < CovarianceSufficientStatistic.ColumnCount; j++)
                {
                    double tmp = varInference.Lambda[i] * varInference.Lambda[j];
                    if (i == j)
                    {
                        CovarianceSufficientStatistic[i, j] = CovarianceSufficientStatistic[i, j] + varInference.Nu[i] + tmp;
                    }
                    else
                    {
                        CovarianceSufficientStatistic[i, j] = CovarianceSufficientStatistic[i, j] + tmp;
                    }
                }
            }

            for(int i = 0; i < document.NbTerms; i++)
            {
                for(int j = 0; j < BetaSufficientStatistic.RowCount; j++)
                {
                    var word = document.GetWord(i);
                    var count = document.GetCount(i);
                    BetaSufficientStatistic[j, word] = BetaSufficientStatistic[j, word] + count * varInference.Phi[i, j];
                }
            }

            NData++;
        }

        public void Reset()
        {
            BetaSufficientStatistic.SetValue(0);
            CovarianceSufficientStatistic.SetValue(0);
            for(int i = 0; i < MuSufficientStatistic.Length; i++)
            {
                MuSufficientStatistic[i] = 0;
            }
        }
    }
}
