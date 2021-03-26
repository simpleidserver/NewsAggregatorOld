using System.Net.Http;

namespace NewsAggregator.ML.Factories
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClient BuildHttpClient()
        {
            return new HttpClient();
        }
    }
}