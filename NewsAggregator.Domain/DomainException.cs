using NewsAggregator.Common.Exceptions;

namespace NewsAggregator.Domain
{
    public class DomainException : NewsAggregatorException
    {
        public DomainException(string message) : base(message) { }
    }
}