using MathNet.Numerics.LinearAlgebra;
using Microsoft.ML.Data;
using NewsAggregator.Microsoft.ML.CorrelatedTopicModel;
using NewsAggregator.Microsoft.ML.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ML.Transforms.Text
{
    public class CorrelatedTopicModelEstimatorTransformer : ITransformer
    {
        private readonly string _outputColumnName;
        private readonly string _inputColumnName;
        private readonly int _nbTopics = 3;
        private readonly int _envMaxIter = 1000;
        private readonly double _emConvergence = 1e-3;
        private readonly int _varMaxIter = 500;
        private readonly double _varConvergence = 1e-5;

        public CorrelatedTopicModelEstimatorTransformer(string outputColumnName, string inputColumnName)
        {
            _outputColumnName = outputColumnName;
            _inputColumnName = inputColumnName;
        }

        public bool IsRowToRowMapper => false;

        public void Fit(IDataView input)
        {
            double convergence = 1, avgNiter = 0, lhood = 0, convergedPct = 0;
            bool resetVar = true;
            var corpus = ExtractCorpus(input);
            var llnaModel = LLNAModel.Create(_nbTopics, corpus);
            llnaModel.LogBeta.Display();
            var sufficientStatistic = new LLNASufficientStatistics(llnaModel);
            var corpusLambda = Matrix<double>.Build.Dense(corpus.NbDocuments, llnaModel.K, 0);
            var corpusNu = Matrix<double>.Build.Dense(corpus.NbDocuments, llnaModel.K, 0);
            var corpusPhiSum = Matrix<double>.Build.Dense(corpus.NbDocuments, llnaModel.K, 0);
            int iteration = 0;
            // Expectation - maximization algorithm (EM) algorithm.
            do
            {
                Expectation(corpus, llnaModel, sufficientStatistic, avgNiter, lhood, corpusLambda, corpusNu, corpusPhiSum, resetVar, convergedPct);
                iteration++;
            }
            while ((iteration < _envMaxIter) && ((convergence > _emConvergence) || (convergence < 0)));
        }

        public DataViewSchema GetOutputSchema(DataViewSchema inputSchema)
        {
            return null;
        }

        public IRowToRowMapper GetRowToRowMapper(DataViewSchema inputSchema)
        {
            return null;
        }

        public void Save(ModelSaveContext ctx)
        {

        }

        public IDataView Transform(IDataView input)
        {
            return null;
        }

        private void Expectation(
            Corpus corpus, 
            LLNAModel llnaModel, 
            LLNASufficientStatistics llnaSufficientStatistics, 
            double avgNiter, 
            double lhood,
            Matrix<double> corpusLambda,
            Matrix<double> corpusNu,
            Matrix<double> corpusPhiSum,
            bool resetVar,
            double convergedPct)
        {
            var phiSum = new double[llnaModel.K];
            Vector<double> lambda, nu;
            int total = 0;
            for(int i = 0; i < corpus.NbDocuments; i++)
            {
                var doc = corpus.GetDocument(i);
                var varInference = new VariationalInferenceParameter(doc.NbTerms, llnaModel.K);
                if(resetVar)
                {
                    varInference.Init(llnaModel);
                }
                else
                {
                    varInference.Lambda = corpusLambda.Row(i);
                    varInference.Nu = corpusNu.Row(i);
                    varInference.OptimizeZeta(llnaModel);
                    varInference.OptimizePhi(llnaModel, doc);
                }

                var lHood = Inference(varInference, doc, llnaModel);
                // Continue the implementation : https://github.com/blei-lab/ctm-c/blob/dfa139c3dac5d10059429f33faf90401a04125ea/estimate.c
                // expectation
            }
        }

        private double Inference(
            VariationalInferenceParameter varInference,
            CorpusDocument doc,
            LLNAModel model)
        {
            double oldLHood = 0, convergence = 0;
            varInference.UpdateLikelihoodBound(doc, model);
            do
            {
                varInference.IncrementIter();
                varInference.OptimizeZeta(model);
            }
            while ((convergence > _varConvergence) && (_varMaxIter < 0 || varInference.NIter < _varMaxIter));
            return varInference.LHood;
        }

        private Corpus ExtractCorpus(IDataView input)
        {
            var result = new Corpus();
            var inputColumn = input.Schema[_inputColumnName];
            var keyValues = new VBuffer<ReadOnlyMemory<char>>();
            inputColumn.Annotations.GetValue("KeyValues", ref keyValues);
            result.Vocabulary = keyValues.GetValues().ToArray();
            VBuffer<uint> tokensVector = default;
            using (var cursor = input.GetRowCursor(new[] { inputColumn }))
            {
                while (cursor.MoveNext())
                {
                    var cursorVectorsGetter = cursor.GetGetter<VBuffer<uint>>(inputColumn);
                    cursorVectorsGetter(ref tokensVector);
                    var words = tokensVector.GetValues().ToArray();
                    result.AddDocument(words);
                }
            }

            return result;
        }

        #region LDA algorithm

        private void LDAFit(IDataView input)
        {
            // https://www.depends-on-the-definition.com/lda-from-scratch/
            var rnd = new Random();
            var inputColumn = input.Schema[_inputColumnName];
            var keyValues = new VBuffer<ReadOnlyMemory<char>>();
            inputColumn.Annotations.GetValue("KeyValues", ref keyValues);

            var alpha = (double)1 / (double)_nbTopics;
            var beta = (double)1 / (double)_nbTopics;
            var vocSize = keyValues.Length;
            var nbDocuments = input.GetRowCount().Value;
            var docTopic = InitMatrix((int)nbDocuments, _nbTopics);
            var topicVoc = InitMatrix(_nbTopics, vocSize);
            var nbWords = new double[nbDocuments];
            var nbTopics = new double[_nbTopics];
            var wordTopics = new List<double[]>();

            VBuffer<uint> tokensVector = default;
            // Initialize
            using (var cursor = input.GetRowCursor(new[] { inputColumn }))
            {
                int d = 0;
                while (cursor.MoveNext())
                {
                    var cursorVectorsGetter = cursor.GetGetter<VBuffer<uint>>(inputColumn);
                    cursorVectorsGetter(ref tokensVector);
                    var words = tokensVector.GetValues();
                    var record = new double[tokensVector.Length];
                    for (int n = 0; n < tokensVector.Length; n++)
                    {
                        var w = words[n];
                        var z = rnd.Next(0, _nbTopics);
                        record[n] = z;
                        docTopic[d, z] = docTopic[d, z] + 1;
                        topicVoc[z, w] = topicVoc[z, w] + 1;
                        nbTopics[z] = nbTopics[z] + 1;
                        nbWords[d] = nbWords[d] + 1;
                    }

                    wordTopics.Add(record);
                    d++;
                }
            }

            // Learning
            using (var cursor = input.GetRowCursor(new[] { inputColumn }))
            {
                int d = 0;
                while (cursor.MoveNext())
                {
                    var cursorVectorsGetter = cursor.GetGetter<VBuffer<uint>>(inputColumn);
                    cursorVectorsGetter(ref tokensVector);
                    var words = tokensVector.GetValues();
                    for (int n = 0; n < tokensVector.Length; n++)
                    {
                        var w = words[n];
                        var z = (int)wordTopics.ElementAt(d)[n];
                        docTopic[d, z] = docTopic[d, z] - 1;
                        topicVoc[z, w] = topicVoc[z, w] - 1;
                        nbTopics[z] = nbTopics[z] - 1;
                        var probabilities = new Dictionary<int, double>();
                        for (int k = 0; k < _nbTopics; k++)
                        {
                            // Probability : P(t|d).
                            var pTopicDocument = (docTopic[d, k] + alpha) / (nbWords[d] - 1 + _nbTopics * alpha);
                            // Probability : P(w|t).
                            var pWordTopic = (topicVoc[k, (int)w] + beta) / (nbTopics.Sum() + vocSize * beta);
                            // Probability : P(w|t,d).
                            var pWordTopicDocument = pTopicDocument * pWordTopic;
                            probabilities.Add(k, pWordTopicDocument);
                        }

                        var kvp = probabilities.First(p => probabilities.Max(pr => pr.Value) == p.Key);
                        var newTopic = kvp.Key;
                        wordTopics.ElementAt(d)[n] = newTopic;
                        docTopic[d, newTopic] = docTopic[d, newTopic] + 1;
                        topicVoc[newTopic, w] = topicVoc[newTopic, w] - 1;
                        nbTopics[newTopic] = nbTopics[newTopic] - 1;
                    }

                    d++;
                }
            }
        }

        private static double[,] InitMatrix(int nbRows, int nbColumns)
        {
            var result = new double[nbRows, nbColumns];
            for(int i = 0; i < nbRows; i++)
            {
                for(int j = 0; j < nbColumns; j++)
                {
                    result[i, j] = 0;
                }
            }

            return result;
        }

        #endregion
    }
}
