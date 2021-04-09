namespace NewsAggregator.Core.Domains.DataSources
{
    public class DataSourceArticle
    {
        private DataSourceArticle() { }

        public int NbArticles { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public void Increment()
        {
            NbArticles++;
        }

        public static DataSourceArticle Create(int month, int year)
        {
            return new DataSourceArticle { Month = month, Year = year };
        }
    }
}
