using System;

namespace NewsAggregator.Core.Exceptions
{
    public class NewsAggregatorException : Exception
    {
        public NewsAggregatorException(string message) : base(message) { }
    }
}
