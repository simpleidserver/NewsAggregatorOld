namespace NewsAggregator.Core.Exceptions
{
    public class DomainException : NewsAggregatorException
    {
        public DomainException(string message) : base(message) { }
    }
}