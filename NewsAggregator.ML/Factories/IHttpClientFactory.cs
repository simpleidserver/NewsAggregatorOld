using System.Net.Http;

namespace NewsAggregator.ML.Factories
{
    public interface IHttpClientFactory
    {
        HttpClient BuildHttpClient();
    }
}
