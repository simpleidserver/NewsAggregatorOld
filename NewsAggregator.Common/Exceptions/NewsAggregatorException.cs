using System;

namespace NewsAggregator.Common.Exceptions
{
    public class NewsAggregatorException : Exception
    {
        public NewsAggregatorException(string message) : base(message) { }
    }
}
