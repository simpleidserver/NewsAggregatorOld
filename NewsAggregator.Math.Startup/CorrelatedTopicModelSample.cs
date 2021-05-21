using Microsoft.ML;
using Microsoft.ML.Transforms.Text;
using NewsAggregator.Microsoft.ML.CorrelatedTopicModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NewsAggregator.Math.Startup
{
    public static class CorrelatedTopicModelSample
    {
        public static void Execute()
        {
            var mlContext = new MLContext();
            var trainingData = mlContext.Data.LoadFromEnumerable(ReadArticles());
            var textPipeline = mlContext.Transforms.Text
                .NormalizeText("NormalizedText", "Text")
                 .Append(mlContext.Transforms.Text.TokenizeIntoWords("Tokens", "NormalizedText")) // Extract tokens.
                 // .Append(mlContext.Transforms.Text.RemoveDefaultStopWords("Tokens")) // Remove stop words.
                 .Append(mlContext.Transforms.Conversion.MapValueToKey("Tokens")
                 .Append(mlContext.Transforms.Text.ColleratedTopicModel("TransformedTxt", "Tokens", 10)));
            var fitResult = textPipeline.Fit(trainingData);
            var estimator = (CorrelatedTopicModelEstimatorTransformer)fitResult.LastTransformer.Last();
            DisplayModel(estimator.Model);
        }

        private static void DisplayModel(LLNAModel model)
        {
            Console.WriteLine("Covariance between topics");
            for(int i = 0; i < model.Covariance.RowCount; i++)
            {
                for (int j = 0; j < model.Covariance.ColumnCount; j++)
                {
                    Console.Write($"{model.Covariance[i, j]} ");
                }

                Console.WriteLine();
            }
        }

        private class Article
        {
            public string Text { get; set; }
        }

        private static IEnumerable<Article> ReadArticles()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "ap.txt");
            return File.ReadAllLines(path).Select(l => new Article
            {
                Text = l
            });
        }
    }
}
