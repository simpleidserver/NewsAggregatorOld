using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Random;
using NewsAggregator.Microsoft.ML.Extensions;
using System;

namespace NewsAggregator.Microsoft.ML.CorrelatedTopicModel
{
    public class LLNAModel
    {
        private readonly CorrelatedTopicModelParameters _parameters;
        private const int SHRINK = 1;
        private const int NUM_INIT = 1;
        private const int SEED_INIT_SMOOTH = 1;

        private LLNAModel(CorrelatedTopicModelParameters parameters) 
        {
            _parameters = parameters;
        }

        public int K { get; set; }
        public Matrix<double> LogBeta { get; set; }
        public Vector<double> Mu { get; set; }
        public Matrix<double> InvCovariance { get; set; }
        public Matrix<double> Covariance { get; set; }
        public double LogDeterminantInvCovariance { get; set; }

        public static LLNAModel Create(int k, Corpus corpus, CorrelatedTopicModelParameters parameters)
        {
            var result = new LLNAModel(parameters)
            {
                K = k,
                Mu = Vector<double>.Build.Dense(k - 1),
                LogBeta = Matrix<double>.Build.Dense(k, corpus.VocabularySize, 0),
            };
            result.Covariance = Matrix<double>.Build.DenseIdentity(k - 1, k - 1);
            result.InvCovariance = result.Covariance.Inverse();
            result.InvCovariance.Display();
            result.LogDeterminantInvCovariance = result.InvCovariance.ComputeLNDeterminant();
            result.Init(corpus);
            return result;
        }

        public void Maximize(LLNASufficientStatistics sufficientStatistics)
        {
            for(int i = 0; i < K- 1; i++)
            {
                Mu[i] = sufficientStatistics.MuSufficientStatistic[i] / sufficientStatistics.NData;
            }

            for(int i = 0; i < K - 1; i++)
            {
                for(int j = 0; j < K - 1; j++)
                {
                    Covariance[i, j] =
                        (1 / sufficientStatistics.NData) *
                        (sufficientStatistics.CovarianceSufficientStatistic[i, j] +
                        sufficientStatistics.NData * Mu[i] * Mu[j] -
                        sufficientStatistics.MuSufficientStatistic[i] * Mu[j] -
                        sufficientStatistics.MuSufficientStatistic[j] * Mu[i]);
                }
            }

            if (_parameters.CovEstimate == SHRINK)
            {
                ShrinkCov(Covariance, sufficientStatistics.NData);
            }

            InvCovariance = Covariance.Inverse();
            LogDeterminantInvCovariance = InvCovariance.ComputeLNDeterminant();
            for (int i = 0; i < K; i++)
            {
                double sum = 0;
                for(int j = 0; j < LogBeta.ColumnCount; j++)
                {
                    sum += sufficientStatistics.BetaSufficientStatistic[i, j];
                }

                if (sum == 0)
                {
                    sum = MathHelper.SafeLog(sum) * LogBeta.ColumnCount;
                }
                else
                {
                    sum = MathHelper.SafeLog(sum);
                }

                for(int j = 0; j < LogBeta.ColumnCount; j++)
                {
                    LogBeta[i, j] = MathHelper.SafeLog(sufficientStatistics.BetaSufficientStatistic[i, j]) - sum;
                }
            }
        }

        private void Init(Corpus corpus)
        {
            var rnd = new MersenneTwister(-1115574245);
            for (int k = 0; k < K; k++)
            {
                double sum = 0;
                // Seed.
                for (int i = 0; i < NUM_INIT; i++)
                {
                    var doc = corpus.GetRandomDocument(rnd);
                    for (int n = 0; n < doc.NbTerms; n++)
                    {
                        var word = doc.GetWord(n);
                        LogBeta[k, word] = LogBeta[k, word] + doc.GetCount(n);
                    }
                }

                // Smooth.
                for (int n = 0; n < LogBeta.ColumnCount; n++)
                {
                    var randomNumber = rnd.NextDouble();
                    LogBeta[k, n] = LogBeta[k, n] + SEED_INIT_SMOOTH + randomNumber;
                    sum += LogBeta[k, n];
                }

                sum = MathHelper.SafeLog(sum);
                // Normalize
                for (int n = 0; n < LogBeta.ColumnCount; n++)
                {
                    LogBeta[k, n] = MathHelper.SafeLog(LogBeta[k, n]) - sum;
                }
            }
        }

        private Matrix<double> ShrinkCov(Matrix<double> mle, double n)
        {
            double logLambda = 0, tau = 0;
            var matrix = mle.Evd();
            var eigenVals = matrix.EigenValues;
            var eigenVects = matrix.EigenVectors;
            var d = Matrix<double>.Build.Dense(mle.RowCount, mle.RowCount);
            var firstResult = Matrix<double>.Build.Dense(mle.RowCount, mle.RowCount);
            var result = Matrix<double>.Build.Dense(mle.RowCount, mle.RowCount);
            var lambdaStar = Vector<double>.Build.Dense(mle.RowCount);
            for(int i = 0; i < mle.RowCount; i++)
            {
                double alpha = 1 / (n + mle.RowCount + 1 - 2 * i);
                lambdaStar[i] = n * alpha * eigenVals[i].Real;
            }

            d.SetDiagonal(mle.Diagonal());
            var secondEvd = d.Evd();
            var secondEigenVals = secondEvd.EigenValues;
            for(int i = 0; i < mle.RowCount; i++)
            {
                logLambda += Math.Log(secondEigenVals[i].Real);
            }

            logLambda = logLambda / mle.RowCount;
            for (int i = 0; i < mle.RowCount; i++)
            {
                tau += Math.Pow(Math.Log(lambdaStar[i])- logLambda, 2) / (mle.RowCount + 4) - 2 / n;
            }

            for(int i = 0; i < mle.RowCount; i++)
            {
                lambdaStar[i] = Math.Exp((2 / n) / ((2 / n) + tau) * logLambda + tau / ((2 / n) + tau) * Math.Log(lambdaStar[i]));
            }

            d.SetDiagonal(lambdaStar);
            firstResult.ComputeDGem(TransOp.CblasNoTrans, TransOp.CblasTrans, 1, d, eigenVects, 0);
            result.ComputeDGem(TransOp.CblasNoTrans, TransOp.CblasNoTrans, 1, eigenVects, firstResult, 0);
            return result;
        }
    }
}
