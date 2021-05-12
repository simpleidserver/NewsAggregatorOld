using System;

namespace NewsAggregator.Microsoft.ML
{
    public static class MathHelper
    {
        public static double SafeLog(double num)
        {
            if (num == 0)
            {
                return -1000;
            }

            return Math.Log(num);
        }

        public static double LogSum(double logA, double logB)
        {
            if (logA == -1)
            {
                return logB;
            }

            if (logA < logB)
            {
                return logB + Math.Log(1 + Math.Exp(logA - logB));
            }

            return logA + Math.Log(1 + Math.Exp(logB - logA));
        }
    }
}
