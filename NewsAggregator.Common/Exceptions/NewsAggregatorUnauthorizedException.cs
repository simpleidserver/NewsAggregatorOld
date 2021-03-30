namespace NewsAggregator.Common.Exceptions
{
    public class NewsAggregatorUnauthorizedException : NewsAggregatorException
    {
        public NewsAggregatorUnauthorizedException(string message) : base(message)
        {
        }
    }
}
