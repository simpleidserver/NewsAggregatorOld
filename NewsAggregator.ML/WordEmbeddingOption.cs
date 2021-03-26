namespace NewsAggregator.ML
{
    public class WordEmbeddingOption
    {
        public WordEmbeddingOption(string language, string downloadUrl)
        {
            Language = language;
            DownloadUrl = downloadUrl;
        }

        public string Language { get; private set; }
        public string DownloadUrl { get; private set; }
    }
}
