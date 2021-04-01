using Microsoft.ML.Data;
using System;

namespace NewsAggregator.ML.Models
{
    public class SessionData
    {
        [LoadColumn(0)]
        public int Id { get; set; }
        [LoadColumn(1)]
        public string UserId { get; set; }
        [LoadColumn(2)]
        public int InteractionType { get; set; }
        [LoadColumn(3)]
        public DateTime ActionDateTime { get; set; }
        [LoadColumn(4)]
        public string ArticleId { get; set; }
        [LoadColumn(5)]
        public string ArticleLanguage { get; set; }
    }
}
