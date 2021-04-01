
namespace NewsAggregator.Core.Exceptions
{
    public class NewsAggregatorUnauthorizedException : NewsAggregatorException
    {
        public NewsAggregatorUnauthorizedException(string message) : base(message)
        {
        }
    }
}
