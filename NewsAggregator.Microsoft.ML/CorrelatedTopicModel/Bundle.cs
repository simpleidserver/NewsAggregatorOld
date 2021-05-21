using MathNet.Numerics.LinearAlgebra;

namespace NewsAggregator.Microsoft.ML.CorrelatedTopicModel
{
    public class Bundle
    {
        public Bundle(int k, CorpusDocument document, LLNAModel model, VariationalInferenceParameter inference)
        {
            SumPhi = Vector<double>.Build.Dense(k - 1, 0);
            Document = document;
            Model = model;
            VarInference = inference;
            Init(k);
        }

        public VariationalInferenceParameter VarInference { get; set; }
        public LLNAModel Model { get; set; }
        public CorpusDocument Document { get; set; }
        public Vector<double> SumPhi { get; set; }

        private void Init(int k)
        {
            for(int i = 0; i < Document.NbTerms; i++)
            {
                for(int j = 0; j < k - 1; j++)
                {
                    SumPhi[j] = SumPhi[j] + (Document.GetCount(i) * VarInference.Phi[i, j]);
                }
            }
        }
    }
}
