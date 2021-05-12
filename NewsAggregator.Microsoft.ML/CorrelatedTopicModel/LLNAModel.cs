using MathNet.Numerics.LinearAlgebra;
using System;

namespace NewsAggregator.Microsoft.ML.CorrelatedTopicModel
{
    public class LLNAModel
    {
        private const int NUM_INIT = 1;
        private const int SEED_INIT_SMOOTH = 1;

        private LLNAModel() { }

        public int K { get; set; }
        public Matrix<double> LogBeta { get; set; }
        public double[] Mu { get; set; }
        public Matrix<double> InvCovariance { get; set; }
        public Matrix<double> Covariance { get; set; }
        public double LogDeterminantInvCovariance { get; set; }

        public static LLNAModel Create(int k, Corpus corpus)
        {
            var result = new LLNAModel
            {
                K = k,
                Mu = new double[k - 1],
                LogBeta = Matrix<double>.Build.Dense(k, corpus.VocabularySize, 0),
            };
            result.Covariance = Matrix<double>.Build.DenseIdentity(k - 1, k - 1);
            result.InvCovariance = result.Covariance.Inverse();
            result.LogDeterminantInvCovariance = Math.Log(result.InvCovariance.Determinant());
            result.Init(corpus);
            return result;
        }

        private void Init(Corpus corpus)
        {
            var rnd = new Random();
            for (int k = 0; k < K; k++)
            {
                double sum = 0;
                // Seed.
                for (int i = 0; i < NUM_INIT; i++)
                {
                    var doc = corpus.GetRandomDocument();
                    for (int n = 0; n < doc.NbTerms; n++)
                    {
                        var word = doc.GetWord(n);
                        LogBeta[k, word] = LogBeta[k, word] + 1;
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
    }
}
