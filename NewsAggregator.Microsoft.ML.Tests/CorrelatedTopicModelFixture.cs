using Microsoft.ML;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NewsAggregator.Microsoft.ML.Tests
{
    public class CorrelatedTopicModelFixture
    {
        [Fact]
        public void When_Compute_CorrelatedTopicModels()
        {
            var mlContext = new MLContext();
            var trainingData = mlContext.Data.LoadFromEnumerable(BuildArticles());
            var textPipeline = mlContext.Transforms.Text
                .NormalizeText("NormalizedText", "Text")
                 .Append(mlContext.Transforms.Text.TokenizeIntoWords("Tokens", "NormalizedText")) // Extract tokens.
                 // .Append(mlContext.Transforms.Text.RemoveDefaultStopWords("Tokens")) // Remove stop words.
                 .Append(mlContext.Transforms.Conversion.MapValueToKey("Tokens")
                 .Append(mlContext.Transforms.Text.ColleratedTopicModel("TransformedTxt", "Tokens", 10)));  // Convert keyword to integer (vocubulary)
            var textTransformer = textPipeline.Fit(trainingData);
            var tt = textTransformer.Transform(trainingData);
            /*
            var tokensColumn = tt.Schema["Tokens"];
            using (var cursor = tt.GetRowCursor(new[] { tokensColumn }))
            {
                while(cursor.MoveNext())
                {
                    VBuffer<uint> cursorVectors = default;
                    var cursorVectorsGetter = cursor.GetGetter<VBuffer<uint>>(tokensColumn);
                    cursorVectorsGetter(ref cursorVectors);
                    string sss = "";
                }
            }
            */
            // .Append(mlContext.Transforms.Text.ProduceNgrams("Tokens")) // Produces a vector of counts of n-gram
            // .Append(mlContext.Transforms.Text.LatentDirichletAllocation("Features", "Tokens", numberOfTopics: _options.LDANbTopics));
            // https://journals.sagepub.com/doi/10.1177/0020294019865750
            // https://www.mygreatlearning.com/blog/understanding-latent-dirichlet-allocation/
            // https://github.com/juanmangh/CNN-text-classification-with-Word2Vec/blob/master/CNN-text-classification-model-using-word2vec_Presentation.pdf
        }

        private class Article
        {
            public string Text { get; set; }
        }

        private static List<Article> BuildArticles()
        {
            return new List<Article>
            {
                new Article { Text = "eat turkey on turkey day holiday" },
                new Article { Text = "i like to eat cake on holiday" },
                new Article { Text = "turkey trot race on thanksgiving holiday" }
            };
        }
    }
}
