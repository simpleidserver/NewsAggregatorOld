using MathNet.Numerics.LinearAlgebra;
using System.Linq;

namespace NewsAggregator.Microsoft.ML.CorrelatedTopicModel
{
    public class CorpusDocument
    {
        public CorpusDocument(uint[] words)
        {
            Init(words);
        }

        public Vector<double> WordsCount { get; set; }
        public Vector<double> Words { get; set; }
        public int NbTerms { get; set; }
        public int Total { get; set; }

        public int GetWord(int ind)
        {
            return (int)Words[ind] - 1;
        }

        public int GetCount(int ind)
        {
            return (int)WordsCount[ind];
        }

        private void Init(uint[] words)
        {
            var groupedWords = words.GroupBy(w => w);
            NbTerms = groupedWords.Count();
            WordsCount = Vector<double>.Build.Dense(NbTerms);
            Words = Vector<double>.Build.Dense(NbTerms);
            int n = 0;
            foreach(var grouped in groupedWords)
            {
                var count = grouped.Count();
                Words[n] = grouped.Key;
                WordsCount[n] = count;
                Total += count;
                n++;
            }
        }
    }
}