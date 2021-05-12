using MathNet.Numerics.LinearAlgebra;
using NewsAggregator.Microsoft.ML.Extensions;
using System;

namespace NewsAggregator.Microsoft.ML.CorrelatedTopicModel
{
    public class VariationalInferenceParameter
    {
        public VariationalInferenceParameter(int nbTerms, int k)
        {
            Lambda = Vector<double>.Build.Dense(k, 0);
            Nu = Vector<double>.Build.Dense(k, 0);
            Phi = Matrix<double>.Build.Dense(nbTerms, k);
            LogPhi = Matrix<double>.Build.Dense(nbTerms, k);
            Zeta = 0;
            TopicScores = Vector<double>.Build.Dense(k, 0);
        }

        public Vector<double> Lambda { get; set; }
        public Vector<double> Nu { get; set; }
        public Matrix<double> Phi { get; set; }
        public Matrix<double> LogPhi { get; set; }
        public double Zeta { get; set; }
        public int NIter { get; set; }
        public bool Converged { get; set; }
        public double LHood { get; set; }
        public Vector<double> TopicScores { get; set; }

        public void IncrementIter()
        {
            NIter++;
        }

        public void OptimizeZeta(LLNAModel model)
        {
            Zeta = 1;
            for (int i = 0; i < model.K - 1; i++)
            {
                Zeta += Math.Exp(Lambda[i] + 0.5 * (Nu[i]));
            }
        }

        public void OptimizePhi(LLNAModel model, CorpusDocument doc)
        {
            // Compute phi proportion in log space.
            for (int n = 0; n < doc.NbTerms; n++)
            {
                double logSumN = 0;
                for (int i = 0; i < model.K; i++)
                {
                    var word = doc.GetWord(n);
                    LogPhi[n, i] = Lambda[i] + model.LogBeta[i, word];
                    if (i == 0)
                    {
                        logSumN = LogPhi[n, i];
                    }
                    else
                    {
                        logSumN = MathHelper.LogSum(logSumN, LogPhi[n, i]);
                    }
                }

                for(int i = 0; i < model.K; i++)
                {
                    LogPhi[n, i] = LogPhi[n, i] - logSumN;
                    Phi[n, i] = Math.Exp(LogPhi[n, i]);
                }
            }
        }

        public void OptimizeLambda(LLNAModel model, CorpusDocument doc)
        {
            var bundle = new Bundle(model.K)
            {
                VarInference = this,
                Document = doc,
                Model = model,
            };
        }

        public void UpdateLikelihoodBound(CorpusDocument doc, LLNAModel model)
        {
            TopicScores.SetValue(0);
            // p(η | μ, Σ) (distribution of topic proportions) && q(η | λ, ν)
            // E[log p(\eta | \mu, \Sigma)] + H(q(\eta | \lambda, \nu)
            double lHood = 0.5 * model.LogDeterminantInvCovariance + 0.5 * (model.K - 1);
            for (int i = 0; i < (model.K - 1); i++)
            {
                double v = -0.5 * Nu[i] * model.InvCovariance[i, i];
                for (int j = 0; j < model.K - 1; j++)
                {
                    v -= 0.5 *
                        (Lambda[i] - model.Mu[i]) *
                        model.InvCovariance[i, j] *
                        (Lambda[j] - model.Mu[j]);
                }

                v += 0.5 * Math.Log(Nu[i]);
                lHood += v;
            }

            // E[log p(z_n | \eta)] + E[log p(w_n | \beta)] + H(q(z_n | \phi_n))
            lHood -= CalculateLikelihoodBound() * doc.Total;
            for (int i = 0; i < doc.NbTerms; i++)
            {
                for (int j = 0; j < model.K; j++)
                {
                    var phi = Phi[i, j];
                    var logPhi = LogPhi[i, j];
                    if (phi > 0)
                    {
                        TopicScores[j] += phi * doc.GetCount(i);
                        double a1 = Lambda[j];
                        int ww = doc.GetWord(i);
                        double a2 = model.LogBeta[j, doc.GetWord(i)];
                        var ss = doc.GetCount(i) * phi +
                            ((Lambda[j] + model.LogBeta[j, doc.GetWord(i)]) - logPhi);
                        lHood += ss;
                    }
                }
            }

            LHood = lHood;
        }

        public double CalculateLikelihoodBound()
        {
            double sumExp = 0;
            var nIter = Lambda.Count;
            for(int i = 0; i < nIter; i++)
            {
                sumExp += Math.Exp(Lambda[i] + 0.5 * Nu[i]);
            }

            return ((1 / Zeta) * sumExp - 1 + Math.Log(Zeta));
        }

        public void Init(LLNAModel model)
        {
            var phiVal = (double)1 / (double)model.K;
            var logPhiVal = -Math.Log(model.K);
            Phi.SetValue(phiVal);
            LogPhi.SetValue(logPhiVal);
            Zeta = 10;
            int i = 0;
            for(i = 0; i < (model.K - 1); i++)
            {
                Nu[i] = 10;
                Lambda[i] = 0;
            }

            Nu[i] = 0;
            Lambda[i] = 0;
            NIter = 0;
            LHood = 0;
        }
    }
}
