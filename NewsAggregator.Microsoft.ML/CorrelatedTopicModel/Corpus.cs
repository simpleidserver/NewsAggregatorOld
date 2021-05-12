using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsAggregator.Microsoft.ML.CorrelatedTopicModel
{
    public class Corpus
    {
        private readonly MersenneTwister _rnd;

        public Corpus()
        {
            _rnd = new MersenneTwister(-1188926917);
            Documents = new List<CorpusDocument>();
        }

        public List<CorpusDocument> Documents { get; set; }
        public ReadOnlyMemory<char>[] Vocabulary { get; set; }
        public int VocabularySize => Vocabulary.Length;
        public int NbDocuments => Documents.Count();

        public void AddDocument(uint[] words)
        {
            Documents.Add(new CorpusDocument(words));
        }

        public CorpusDocument GetDocument(int documentId)
        {
            return Documents.ElementAt(documentId);
        }

        public CorpusDocument GetRandomDocument()
        {
            var n = _rnd.NextDouble();
            var documentId = (int)Math.Floor(n * (double)NbDocuments);
            return GetDocument(documentId);
        }

        public int Encode(int seed)
        {
            seed ^= seed >> 13;
            seed ^= seed << 18;
            seed &= 0x7FFFFFFF;
            return seed;
        }
    }
}
