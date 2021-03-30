namespace NewsAggregator.Common.Exceptions
{
    public class NewsAggregatorResourceNotFoundException : NewsAggregatorException
    {
        public NewsAggregatorResourceNotFoundException(string message) : base(message)
        {
        }
    }
}
