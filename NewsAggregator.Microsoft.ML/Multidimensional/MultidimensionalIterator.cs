using MathNet.Numerics.LinearAlgebra;
using NewsAggregator.Microsoft.ML.Extensions;
using System;

namespace NewsAggregator.Microsoft.ML.Multidimensional
{
    public class MultidimensionalIterator: IMultidimensionalIterator
    {
        public MultidimensionalIterator(int n)
        {
            State = new MultidimensionalIteratorState(n);
        }

        public MultidimensionalIteratorState State { get; private set; }

        public void Set(Vector<double> gradient, double stepSize, double tol)
        {
            State.Iter = 0;
            State.Step = stepSize;
            State.MaxStep = stepSize;
            State.Tol = tol;
            State.P.CopyTo(gradient);
            State.G0.CopyTo(gradient);
            double gnorm = gradient.L2Norm();
            State.PNorm = gnorm;
            State.G0Norm = gnorm;
        }

        public IterationResult Iterate(
            MultidimensionalFunctionFDF fdf,
            double fa,
            Vector<double> gradient,
            Vector<double> x,
            Vector<double> dx)
        {
            var p = State.P;
            if (State.PNorm == 0 || State.G0Norm == 0)
            {
                dx.SetValue(0);
                return new IterationResult
                {
                    State = IterationResultStates.ERROR
                };
            }

            double pg = p.Multiply(gradient);
            var dir = (pg > 0) ? 1 : -1;
            TakeStep(x, p, State.Step, dir / State.PNorm, State.X1, dx);
            double fc = fdf.F(State.X1);
            if (fc < fa)
            {
                State.Step = State.Step * 2;
                State.X1.CopyTo(x);
                return new IterationResult
                {
                    F = fc,
                    State = IterationResultStates.SUCCESS
                };
            }

            return null;
        }

        private void TakeStep(Vector<double> x, Vector<double> p, double step, double lambda, Vector<double> dx, Vector<double> x1)
        {
            dx.SetValue(0);
            dx.ComputeDaxpy(-step * lambda, p);
            x.CopyTo(x1);
            x1.ComputeDaxpy(1, dx);
        }

        private IntermediatePointResult IntermediatePoint(
            MultidimensionalFunctionFDF fdf,
            Vector<double> x,
            Vector<double> p,
            double lambda,
            double pg,
            double stepa,
            double stepc,
            double fa,
            double fc,
            Vector<double> x1,
            Vector<double> dx,
            double step,
            double f)
        {
            double stepb = 0, fb;
            Trial:
            {
                double u = Math.Abs(pg * lambda * stepc);
                stepb = 0.5 * stepc * u / ((fc - fa) + u);
            }

            TakeStep(x, p, stepb, lambda, x1, dx);
            if (x.Equals(x1))
            {
                return new IntermediatePointResult
                {
                    Step = 0,
                    F = fa,
                    Gradient = fdf.DF(x1)
                };
            }

            fb = fdf.F(x1);
            if (fb >= fa && stepb > 0)
            {
                fc = fb;
                stepc = stepb;
                goto Trial;
            }

            var gradient = fdf.DF(x1);
            return null;
        }

        private class IntermediatePointResult
        {
            public double Step { get; set; }
            public double F { get; set; }
            public Vector<double> Gradient { get; set; }
        }
    }
}
