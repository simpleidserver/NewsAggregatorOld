using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Optimization;
using NewsAggregator.Microsoft.ML.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsAggregator.Microsoft.ML.CorrelatedTopicModel
{
    public class VariationalInferenceParameter
    {
        private const double NEWTON_THRESH = 1e-10;
        private List<Vector<double>> _temp;

        public VariationalInferenceParameter(int nbTerms, int k)
        {
            Lambda = Vector<double>.Build.Dense(k, 0);
            Nu = Vector<double>.Build.Dense(k, 0);
            Phi = Matrix<double>.Build.Dense(nbTerms, k);
            LogPhi = Matrix<double>.Build.Dense(nbTerms, k);
            Zeta = 0;
            TopicScores = Vector<double>.Build.Dense(k, 0);
            _temp = new List<Vector<double>>();
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
            int i = 0, n = model.K - 1, iter = 0;
            double fOld = 0;
            var bundle = new Bundle(model.K, doc, model, this);
            var x = Vector<double>.Build.Dense(model.K - 1);
            for(i = 0; i < model.K - 1; i++)
            {
                x[i] = Lambda[i];
            }

            /*
            var iterator = new MultidimensionalIterator(n);
            var fdf = new MultidimensionalFunctionFDF
            {
                F = (vector) =>
                {
                    return ComputeFunction(vector, bundle);
                },
                DF = (vector) =>
                {
                    return ComputeGradient(vector, bundle);
                },
                N = n
            };
            var multidimensionalMinimizer = new MultidimensionalMinimizer(n, iterator, fdf);
            multidimensionalMinimizer.Set(x, 0.01, 1e-3);
            do
            {
                iter++;
                fOld = multidimensionalMinimizer.F;
                multidimensionalMinimizer.Iterate();
            }
            while (true);
            */

            var obj = ObjectiveFunction.Gradient((vector) => {
                return ComputeFunction(vector, bundle);
            }, (vector) =>
            {
                return ComputeGradient(vector, bundle);
            });
            var solver = new ConjugateGradientMinimizer(1e-8, 5000);
            var result = solver.FindMinimum(obj, x);
            for(i = 0; i < model.K - 1; i++)
            {
                Lambda[i] = result.MinimizingPoint[i];
            }

            Lambda[i] = 0;
        }

        public void OptimizeNu(LLNAModel model, CorpusDocument doc)
        {
            for(int i = 0; i < model.K - 1; i++)
            {
                double initNu = 10;
                double nuI = 0, logNuI = 0, df = 0, d2f = 0;
                int iter = 0;
                logNuI = Math.Log(initNu);
                do
                {
                    iter++;
                    nuI = Math.Exp(logNuI);
                    if (double.IsNaN(nuI))
                    {
                        initNu = initNu * 2;
                        logNuI = Math.Log(initNu);
                        nuI = initNu;
                    }

                    df = -(model.InvCovariance[i, i] * 0.5)
                        - (0.5 * ((doc.Total / Zeta) * Math.Exp(Lambda[i] + nuI / 2)))
                        + (0.5 * (1 / nuI));
                    d2f = -(0.25 * (doc.Total / Zeta) * Math.Exp(Lambda[i] + nuI / 2))
                        - (0.5 * (1 / (nuI * nuI)));
                    logNuI = logNuI - (df * nuI) / (d2f * nuI * nuI + df * nuI);
                }
                while (Math.Abs(df) > NEWTON_THRESH);

                Nu[i] = Math.Exp(logNuI);
            }
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
                        var ss = doc.GetCount(i) * phi *
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
            for(i = 0; i < 4; i++)
            {
                _temp.Add(Vector<double>.Build.Dense(model.K - 1));
            }
        }

        private double ComputeFunction(Vector<double> vector, Bundle bundle)
        {
            var term1 = vector.Multiply(bundle.SumPhi);
            vector.CopyTo(_temp[1]);
            _temp[1].ComputeDaxpy(-1.0, bundle.Model.Mu);
            _temp[2].ComputeDsymv(1, bundle.Model.InvCovariance, _temp[1], -1);
            var term2 = _temp[2].Multiply(_temp[1]);
            term2 = -0.5 * term2;
            double term3 = 0;
            for(int i = 0; i < bundle.Model.K - 1; i++)
            {
                term3 += Math.Exp(vector[i] + 0.5 * bundle.VarInference.Nu[i]);
            }

            term3 = -((1 / bundle.VarInference.Zeta) * term3 - 1.0 + Math.Log(bundle.VarInference.Zeta)) * bundle.Document.Total;
            var result = -(term1 + term2 + term3);
            return result;
        }

        private Vector<double> ComputeGradient(Vector<double> vector, Bundle bundle)
        {
            _temp[0].SetValue(0);
            bundle.Model.Mu.CopyTo(_temp[1]);
            _temp[1] = _temp[1].Subtract(vector);
            _temp[0].ComputeDsymv(1, bundle.Model.InvCovariance, _temp[1], 0);
            for(int i = 0; i < _temp[3].Count(); i++)
            {
                _temp[3][i] = -(
                    ((bundle.Document.Total) / bundle.VarInference.Zeta) * 
                    Math.Exp(vector[i] + 0.5 * bundle.VarInference.Nu[i])
                );
            }

            var result = Vector<double>.Build.Dense(_temp[0].Count());
            result = result.Subtract(_temp[0]);
            result = result.Subtract(bundle.SumPhi);
            result = result.Subtract(_temp[3]);
            CleanInfiniteValues(result);
            return result;
        }

        private static void CleanInfiniteValues(Vector<double> gradient)
        {
            for(int i = 0; i < gradient.Count; i++)
            {
                if (double.IsInfinity(gradient[i]))
                {
                    gradient[i] = 0;
                }
            }
        }
    }
}
